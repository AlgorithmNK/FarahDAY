import sys
import telebot
import requests
import threading

bot = telebot.TeleBot(sys.argv[1])
url = sys.argv[2]

@bot.message_handler(commands=['start', 'help'])
def main(message):
    bot.send_message(message.chat.id, text='Здравствуйте, чем могу помочь?')

@bot.message_handler(content_types=['text', 'photo'])
def answer(message):
    user_data = {
        'Имя': message.from_user.first_name,
        'Фамилия': message.from_user.last_name,
        'Сообщение': message.text,
        'ID чата': message.chat.id,
        'Фото': None
    } 
    if message.content_type == 'photo':
        photo = message.photo[-1]
        file_info = bot.get_file(photo.file_id)
        downloaded_file = bot.download_file(file_info.file_path)
        photo_path = f"{url}/images/{photo.file_id}.jpg"
        with open(f'images/{photo.file_id}.jpg', "wb") as file:
            file.write(downloaded_file)
        user_data['Фото'] = photo_path
    print(user_data)
    t = threading.Thread(target=send_data, args=(user_data,))
    t.start()

def send_data(user_data):
    try:
        response = requests.post(url + '/telegram', json=user_data)
        if response.status_code == 200:
            print("Данные успешно отправлены на сервер.")
        else:
            print("Ошибка при отправке данных на сервер:", response.status_code)
    except Exception as e:
        print("Не удалось отправить данные на сервер:", e)

bot.polling(none_stop=True)
