from flask import Flask, request, jsonify
import pytesseract
from PIL import Image
import os

pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files\Tesseract-OCR\tesseract.exe'

app = Flask(__name__)

@app.route('/ocr', methods=['POST'])
def ocr():
    if 'image' not in request.files:
        return jsonify({"error": "No image provided"}), 400

    file = request.files['image']

    if file and file.filename.lower().endswith(('.png', '.jpg', '.jpeg', '.bmp', '.tiff')):
        try:
            image = Image.open(file)

            extracted_text = pytesseract.image_to_string(image)

            return jsonify({"extracted_text": extracted_text}), 200

        except Exception as e:
            return jsonify({"error": str(e)}), 500

    else:
        return jsonify({"error": "Unsupported file type"}), 400

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
