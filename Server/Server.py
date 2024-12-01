import threading, requests, sys, smtplib, email, os, subprocess
from flask import Flask, request, jsonify, send_from_directory
from flask_socketio import SocketIO, emit
from engineio.async_drivers import gevent
from email.mime.text import MIMEText
from email.header import decode_header
import vk_api, telebot, whatsapp_chatbot_python
from dbmanager import DBManager
sys.path.append('/task_classifier')
sys.path.append('/text_generation')
from task_classifier.task_classifier import TaskClassifier
from text_generation.text_generator import TextGenerator
from whatsapp_chatbot_python import GreenAPIBot
import psutil

HOST = '127.0.0.1'
PORT = '5000'
if (len(sys.argv) > 2):
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
whatsapp_bot_id = ''
whatsapp_bot_token = ''
viber_bot_token = ''
GENERATE_ANSWER = False
DETECT_THEME = True
bot_message_file = 'bot_message.txt'

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
        message_type = ''
        bot_answer = ''
        print("Новое сообщение: " + message)
        print(GENERATE_ANSWER, type(GENERATE_ANSWER))
        if (len(message) > 0):
            if DETECT_THEME:
                message_type = taskclassifier.get_predict(message)
            if GENERATE_ANSWER:
                print("Генерация ответа...")
                bot_answer = textgenerator.get_response(message)
        if chat_source == 'tg':
            send_user_telegram(chat_id, bot_answer)
        elif chat_source == 'vk':
            send_user_vk(chat_id, bot_answer)
        elif chat_source == 'mail':
            send_user_gmail(chat_id, bot_answer)
        elif chat_source == 'whatsapp':
            send_user_whatsapp(chat_id, bot_answer)
        dbmanager.add_message(name, chat_source, chat_id, message, photo)
        if (len(message_type) > 0):
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

@app.route('/whatsapp', methods=['POST'])
def save_user_whatsapp():
    data = request.json
    chat_source = 'whatsapp'
    chat_id = data.get("ID чата")
    name = data.get('Имя и фамилия')
    message = data.get('Сообщение')
    save_user(chat_source, chat_id, name, message)
    return jsonify({'message': 'Данные успешно сохранены'})

def send_user_whatsapp(chat_id, text):
    url = f"https://1103.api.green-api.com/waInstance{whatsapp_bot_id}/sendMessage/{whatsapp_bot_token}"
    payload = {
        "chatId": chat_id,
        "message": text
    }
    headers = {
    'Content-Type': 'application/json'
    }
    requests.post(url, json=payload, headers=headers)

@socketio.on('save_user_offline')
def handle_ave_user_offline(chat_source, chat_id, message):
    name = ''
    save_user(chat_source, chat_id, name, message)

@socketio.on('get_chats')
def handle_get_chats():
    get_chats()

def get_chats():
    res_data = dbmanager.get_chats()
    socketio.emit('get_chats_response', res_data)

@socketio.on('get_chat_messages')
def handle_get_chat_messages(chat_source, chat_id):
    res_data = dbmanager.get_chat_messages(chat_source, chat_id)
    emit('get_chat_messages_response', res_data)

@socketio.on('send_message')
def handle_send_message(chat_source, chat_id, message):
    source = 'user'
    if (len(message) > 0):
        dbmanager.add_message(source, chat_source, chat_id, message)
        dbmanager.change_status(chat_source, chat_id, 'open')
        if chat_source  == 'tg':
            send_user_telegram(chat_id, message)
        elif chat_source  == 'vk':
            send_user_vk(chat_id, message)
        elif chat_source  == 'mail':
            send_user_gmail(chat_id, message)
        elif chat_source  == 'whatsapp':
            send_user_whatsapp(chat_id, message)

@socketio.on('close_chat')
def handle_close_chat(chat_source, chat_id):
    dbmanager.change_status(chat_source, chat_id, 'closed')
    get_chats()

def terminate_process_and_children(proc):
    try:
        parent = psutil.Process(proc.pid)
        for child in parent.children(recursive=True):  
            child.terminate()
        parent.terminate()
        gone, still_alive = psutil.wait_procs([parent] + parent.children(), timeout=5)
        for p in still_alive:
            p.kill()  
    except psutil.NoSuchProcess:
        pass  

processes = []

@socketio.on('run_bots')
def handle_run_bots(tg_token, vk_token, email_address, email_password, whatsapp_id, whatsapp_token, viber_token, generate_answer = False, detect_theme = True):
    global processes, tg_bot_token, vk_bot_token, EMAIL_ADDRESS, APP_PASSWORD, whatsapp_bot_id, whatsapp_bot_token, GENERATE_ANSWER, DETECT_THEME
    tg_bot_token = tg_token
    vk_bot_token = vk_token
    EMAIL_ADDRESS = email_address
    APP_PASSWORD = email_password
    whatsapp_bot_id = whatsapp_id
    whatsapp_bot_token = whatsapp_token
    GENERATE_ANSWER = bool(generate_answer)
    DETECT_THEME = bool(detect_theme)
    for process in processes:
        terminate_process_and_children(process)
    processes.clear()
    if len(tg_bot_token) > 0:
        processes.append(subprocess.Popen(['BotTelegram.exe', tg_bot_token, URL]))
    if len(vk_bot_token) > 0:
        processes.append(subprocess.Popen(['VkBot.exe', vk_bot_token, URL]))
    if len(EMAIL_ADDRESS) > 0 and len(APP_PASSWORD) > 0:
        processes.append(subprocess.Popen(['GmailBot.exe', EMAIL_ADDRESS, APP_PASSWORD, URL]))
    if len(whatsapp_bot_id) > 0 and len(whatsapp_bot_token) > 0:
        processes.append(subprocess.Popen(['WhatsAppBot.exe', whatsapp_bot_id, whatsapp_bot_token, URL]))

if __name__ == '__main__':
    if not os.path.exists("images"):
        os.makedirs("images")
    dbmanager.create_db()
    socketio.run(app, host=HOST, port=int(PORT))
