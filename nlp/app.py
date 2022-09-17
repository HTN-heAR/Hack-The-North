"""
send: raw transcription

return: summary
return: translation
"""
from flask import Flask, request
from googletrans import Translator
import cohere

app = Flask(__name__)
translator = Translator()

api_key = ""
co = cohere.Client(api_key)


def get_translation(received_data):
    translated_text = translator.translate(received_data)
    return translated_text.text


def get_summary():
    prompt = '''"" In summary: ""'''
    #parameters to change outcome of summary
    co.generate(
    model='large', #model size 
    prompt=prompt, #the prompt
    temperature = 0.1, #number to determine likelyhood of random responses
    k = 500, #ensures top k of number of tokens to generate
    p = 1, #t ensures that only the most likely tokens, with total probability mass of p, are considered for generation at each step
    frequency_penalty= 0.5 #penalizes new tokens based on their existing frequency in the text so far
    presence_penalty= 0.5 #penalizes new tokens based on whether they appear in the text so far
    )
    return prompt


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