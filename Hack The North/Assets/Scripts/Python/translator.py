# pip install googletrans==3.1.0a0

from googletrans import Translator

import socket
import time

host, port = "127.0.0.1", 25001
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host, port))

translator = Translator()

while True:
    print("Waiting for data...")
    time.sleep(0.1)  # sleep 0.5 sec

    receivedData = sock.recv(1024).decode("UTF-8")

    translated_text = translator.translate(receivedData)
    print(translated_text.text)

    # Converting string to Byte, and sending it to C#
    sock.sendall(translated_text.text.encode("UTF-8"))
