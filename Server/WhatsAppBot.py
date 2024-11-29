from whatsapp_chatbot_python import GreenAPIBot, Notification
import requests
import threading

bot = GreenAPIBot("1103157136", "8c799acfe25e446090a237cfe6aa8ef4d07cb911307041e3ac")
flag = False 
bot_message = ""
@bot.router.message()
def message_handler(notification: Notification) -> None:
    # Выводим все атрибуты для диагностики
    #print(vars(notification))  # или dir(notification)
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
    with open('bot_message.txt', 'r') as f:
                bot_message = f.read()
                if bot_message != "":
                    notification.answer(bot_message)
    #print(chat_id, user_name, user_message)

def send_data(user_data):
    # Отправляем данные на сервер
    try:
        response = requests.post('http://127.0.0.1:5000/whatsapp', json=user_data)
        if response.status_code == 200:
            print("Данные успешно отправлены на сервер.")
        else:
            print("Ошибка при отправке данных на сервер:", response.status_code)
    except Exception as e:
        print("Не удалось отправить данные на сервер:", e)




bot.run_forever()