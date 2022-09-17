"""
send: raw transcription

return: summary
return: translation
"""
from flask import Flask, request
from googletrans import Translator

app = Flask(__name__)
translator = Translator()

def get_translation(received_data):
    translated_text = translator.translate(received_data)
    return translated_text.text


def get_summary():
    return "summarized paragraph"


@app.route("/")
def index():
    return "Hello World!"


# @app.route('/login', methods=['GET'])
@app.route('/summary')
def summary():
    res = request.args.get("q")
    return res

@app.route('/translate')
def translate():
    res = request.args.get("q")
    return get_translation(res)

if __name__ == "__main__":
    app.run()