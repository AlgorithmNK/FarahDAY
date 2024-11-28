import threading, requests, sys, smtplib, email, os, subprocess
from flask import Flask, request, jsonify, send_from_directory
from flask_socketio import SocketIO, emit
from engineio.async_drivers import gevent
from email.mime.text import MIMEText
from email.header import decode_header
import vk_api
from dbmanager import DBManager
sys.path.append('/task_classifier')
sys.path.append('/text_generation')
from task_classifier.task_classifier import TaskClassifier
from text_generation.text_generator import TextGenerator


HOST = '127.0.0.1'
PORT = '5000'
if (len(sys.argv) > 1):
    HOST, PORT = sys.argv[1].split(":")
URL = 'http://' + HOST + ':' + PORT
IMAGE_FOLDER = os.path.join(os.getcwd(), 'images')
app = Flask(__name__)
socketio = SocketIO(app, async_mode='gevent')
users_data = []
dbmanager = DBManager()
taskclassifier = TaskClassifier()
textgenerator = TextGenerator()
tg_bot_token = ''
tg_url = ''
vk_bot_token = ''
SMTP_SERVER = 'smtp.gmail.com'
SMTP_PORT = 587
EMAIL_ADDRESS = ''
APP_PASSWORD = ''


@app.route('/images/<filename>')
def get_image(filename):
    return send_from_directory(IMAGE_FOLDER, filename)

def save_user(chat_source, chat_id, name, message, photo='None'):
    bot_name = 'system'
    if (dbmanager.is_chat_exists(chat_source, chat_id)):
        dbmanager.add_message(name, chat_source, chat_id, message, photo)
        if chat_source == 'offline':
            dbmanager.change_status(chat_source, chat_id, 'offline')
        else:
            dbmanager.change_status(chat_source, chat_id, 'awaiting')
        socketio.emit(
            'new_message', 
            {'source': name, 'chat_source': chat_source, 'chat_id': chat_id, 'text': message, 'photo': photo}
        )
    else:
        if (len(message) > 0):
            message_type = taskclassifier.get_predict(message)
            bot_answer = textgenerator.get_response(message)
        if chat_source == 'tg':
            send_user_telegram(chat_id, bot_answer)
        elif chat_source == 'vk':
            send_user_vk(chat_id, bot_answer)
        elif chat_source == 'mail':
            send_user_gmail(chat_id, bot_answer)
        dbmanager.add_message(name, chat_source, chat_id, message, photo)
        dbmanager.add_chat_type(chat_source, chat_id, message_type)
        if (len(bot_answer) > 0):
            dbmanager.add_message(bot_name, chat_source, chat_id, bot_answer)
            if chat_source == 'offline':
                dbmanager.change_status(chat_source, chat_id, 'offline')
            else:
                dbmanager.change_status(chat_source, chat_id, 'open')
            socketio.emit(
                'new_message', 
                {'source': bot_name, 'chat_source': chat_source, 'chat_id': chat_id, 'text': bot_answer}
            )
        get_chats()

@app.route('/telegram', methods=['POST'])
def save_user_telegram():
    data = request.json
    chat_source = 'tg'
    chat_id = str(data['ID чата'])
    name = f'{str(data['Имя'])} {str(data['Фамилия'])}'
    message = str(data['Сообщение'])
    photo = str(data['Фото'])
    save_user(chat_source, chat_id, name, message, photo)
    return jsonify({'message': 'Данные успешно сохранены'})

def send_user_telegram(chat_id, message_bot):
    payload = {
        'chat_id': chat_id,
        'text': message_bot
    }
    response = requests.post(f'https://api.telegram.org/bot{tg_bot_token}/sendMessage', json=payload)

@app.route('/vk', methods=['POST'])
def save_user_vk():
    data = request.json
    chat_source = 'vk'
    chat_id = str(data['ID чата'])
    name = f'{str(data['Имя'])} {str(data['Фамилия'])}'
    message = str(data['Сообщение'])
    save_user(chat_source, chat_id, name, message)
    return jsonify({'message': 'Данные успешно сохранены'})

def send_user_vk(id, text):
    vk = vk_api.VkApi(token = vk_bot_token)
    vk.method('messages.send', {'user_id' : id, 'message' : text, 'random_id': 0})

@app.route('/gmail', methods=['POST'])
def save_user_gmail():
    data = request.json
    chat_source = 'mail'
    chat_id = str(data['email_address'])
    name = str(data['name'])
    message = f'{str(data['subject'])}\n{str(data['text'])}'
    save_user(chat_source, chat_id, name, message)
    return jsonify({'message': 'Данные успешно сохранены'})

def send_user_gmail(chat_id, text):
    msg = MIMEText(text)
    msg['Subject'] = ''
    msg['From'] = EMAIL_ADDRESS
    msg['To'] = chat_id
    with smtplib.SMTP(SMTP_SERVER, SMTP_PORT) as server:
        server.starttls()
        server.login(EMAIL_ADDRESS, APP_PASSWORD)
        server.sendmail(EMAIL_ADDRESS, chat_id, msg.as_string())

@socketio.on('save_user_offline')
def handle_ave_user_offline(chat_source, chat_id, message):
    name = ''
    save_user(chat_source, chat_id, name, message)

@socketio.on('get_chats')
def handle_get_chats():
    get_chats()

def get_chats():
    res_data = dbmanager.get_chats()
    emit('get_chats_response', res_data, namespace='/')

@socketio.on('get_chat_messages')
def handle_get_chat_messages(chat_source, chat_id):
    res_data = dbmanager.get_chat_messages(chat_source, chat_id)
    emit('get_chat_messages_response', res_data)

@socketio.on('send_message')
def handle_send_message(chat_source, chat_id, message):
    source = 'user'
    if (len(message) > 0):
        dbmanager.add_message(source, chat_source, chat_id, message)
        dbmanager.change_status(chat_source, chat_id, 'awaiting')
        if chat_source  == 'tg':
            send_user_telegram(chat_id, message)
        elif chat_source  == 'vk':
            send_user_vk(chat_id, message)
        elif chat_source  == 'mail':
            send_user_gmail(chat_id, message)      

@socketio.on('close_chat')
def handle_close_chat(chat_source, chat_id):
    dbmanager.change_status(chat_source, chat_id, 'closed')
    get_chats()

processes = []

@socketio.on('run_bots')
def handle_run_bots(tg_token, vk_token, email_address, email_password):
    global processes, tg_bot_token, vk_bot_token, EMAIL_ADDRESS, APP_PASSWORD
    tg_bot_token = tg_token
    vk_bot_token = vk_token
    EMAIL_ADDRESS = email_address
    APP_PASSWORD = email_password
    for process in processes:
        process.terminate()  
        process.wait()      
    if len(tg_bot_token) > 0:
        processes.append(subprocess.Popen(['python', 'BotTelegram.py', tg_bot_token, URL]))
    if len(vk_bot_token) > 0:
        processes.append(subprocess.Popen(['python', 'VkBot.py', vk_bot_token, URL]))
    if len(EMAIL_ADDRESS) > 0 and len(APP_PASSWORD) > 0:
        processes.append(subprocess.Popen(['python', 'GmailBot.py', EMAIL_ADDRESS, APP_PASSWORD, URL]))

if __name__ == '__main__':
    if not os.path.exists("images"):
        os.makedirs("images")
    dbmanager.create_db()
    socketio.run(app, host=HOST, port=int(PORT))
