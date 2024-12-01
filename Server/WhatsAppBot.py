from whatsapp_chatbot_python import GreenAPIBot, Notification
import requests
import threading
import sys

bot = GreenAPIBot(sys.argv[1], sys.argv[2])
url = sys.argv[3]
flag = False 
bot_message = ""
@bot.router.message()
def message_handler(notification: Notification) -> None:
    chat_id = notification.sender
    sender_data = notification.event["senderData"]
    user_name = sender_data["senderName"]
    user_message = notification.message_text
    user_data = {
        'Имя и фамилия': user_name,
        'Сообщение': user_message,
        'ID чата': chat_id
    } 
    t = threading.Thread(target=send_data, args=(user_data,))
    t.start()
    """with open('bot_message.txt', 'r') as f:
                bot_message = f.read()
                if bot_message != "":
                    notification.answer(bot_message)"""

def send_data(user_data):
    try:
        response = requests.post(url + '/whatsapp', json=user_data)
        if response.status_code == 200:
            print("Данные успешно отправлены на сервер.")
        else:
            print("Ошибка при отправке данных на сервер:", response.status_code)
    except Exception as e:
        print("Не удалось отправить данные на сервер:", e)




bot.run_forever()