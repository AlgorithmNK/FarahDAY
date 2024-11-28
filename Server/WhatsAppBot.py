from flask import Flask, request
import requests
import threading
import os
import sys

app = Flask(__name__)
url = os.getenv("SERVER_URL")  

def send_data(user_data):
    try:
        response = requests.post(url + '/whatsapp', json=user_data)
        if response.status_code == 200:
            print("Данные успешно отправлены на сервер.")
        else:
            print("Ошибка при отправке данных на сервер:", response.status_code)
    except Exception as e:
        print("Не удалось отправить данные на сервер:", e)

@app.route("/whatsapp", methods=["POST"])
def whatsapp_webhook():
    incoming_msg = request.values.get("Body", "").strip()
    from_number = request.values.get("From", "").replace("whatsapp:", "")
    media_url = request.values.get("MediaUrl0", None)

    user_data = {
        "Имя": request.values.get("ProfileName", "Неизвестно"),
        "Сообщение": incoming_msg,
        "Номер": from_number,
        "Фото": media_url,
    }

    print("Получено сообщение:", user_data)

    t = threading.Thread(target=send_data, args=(user_data,))
    t.start()

    return "OK", 200  