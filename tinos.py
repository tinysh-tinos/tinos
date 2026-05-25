import sys
import subprocess

def install_and_import(package):
    try:
        __import__(package)
    except ImportError:
        subprocess.check_call([sys.executable, '-m', 'pip', 'install', package])

install_and_import('pywebview')

from tkinter import *
from tkinter import ttk
import webview

root = Tk()
root.title("METANIT.COM")
root.attributes("-fullscreen", True)
root.config(cursor="X_cursor")
root["bg"] = "Cornsilk"
style = ttk.Style()
style.configure("TLabel", background="Cornsilk", foreground="Black", font=("Arial Black", 16))
style.configure("TButton", background="Silver", foreground="Black", font=("Arial", 12))

def dismiss(root):
    root.grab_release() 
    root.destroy()

def click():
    window = Toplevel()
    window.title("РБК(Российская библиотека кодов)")
    window.geometry("600x600")

    def click2():
        webview.create_window('HABR.com', 'https://habr.com/ru/companies/vdsina/articles/563432/') 
        webview.start() 
        
    def click3():
        webview.create_window('Sky.pro', 'https://sky.pro/wiki/python/python-kody-dlya-nachinayushih/') 
        webview.start() 

    open_button = ttk.Button(window, text="HABR.com", command=click2)
    open_button.place(x=20, y=20)
    open_button = ttk.Button(window, text="Sky.pro", command=click3)
    open_button.place(x=500, y=20)
    window.grab_set()    

def click7():

    html = """
            <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=], initial-scale=1.0">
    <link rel="stylesheet" href="style.css" class="rel">
    <title>яндекс браузер</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Nunito:ital,wght@0,500;1,500&display=swap');

        *{
            font-family: Nunito;
        }

        body{
            background-color: #fff;
            background: linear-gradient(to right, lightgray, white, lightgray);
        }
    </style>

</head>
<body>
    
    <div>
        <h1 style="background-color: lightgray;width:1240;height:80; font-family: 'Times New Roman', Times, serif;font-size: 2cm; text-align: center;"><time id="currentTime"></time></h1>

<script>
  var timeElement = document.getElementById('currentTime');
  setInterval(function () {
    var currentTime = new Date();
    timeElement.textContent = currentTime.toLocaleTimeString();
  }, 1000);
</script></div>
</div>

<div style="text-align: center;">
    <button style="color: aliceblue;text-align: center;font-family: Сentury gothic, Haettenschweiler, 'Arial Narrow Bold', sans-serif;font-size: larger;"><a href="https://ya.ru/">Яндекс</a></button>
    <button style="color: aliceblue;text-align: center;font-family: Сentury gothic, Haettenschweiler, 'Arial Narrow Bold', sans-serif;font-size: larger;"><a href="https://vk.com/?ysclid=m7o4527utv611868157">ВКонтакте</a></button>
    <button style="color: aliceblue;text-align: center;font-family: Сentury gothic, Haettenschweiler, 'Arial Narrow Bold', sans-serif;font-size: larger;"><a href="https://ya.ru/all">Сервисы Яндекса</a></button>
    <button style="color: aliceblue;text-align: center;font-family: Сentury gothic, Haettenschweiler, 'Arial Narrow Bold', sans-serif;font-size: larger;"><a href="https://vk.com/services">Сервисы ВКонтакте</a></button></p>
</div>
<div style="text-align: center;">
    <form action="https://ya.ru/search" method="get">
        <input type="text" name="text" placeholder="Поиск...">
        <button type="submit">Искать</button>
    </form>
</div>
</body>
</html>"""



    webview.create_window('ЯндексБраузер beta', 'https://ya.ru/', html=html, width=1440, height=900)   
    webview.start()


def pusk1():
    window = Toplevel()
    window.title("Меню пуск")
    window.attributes("-fullscreen", True)
    window["bg"] = "Cornsilk"
    style = ttk.Style()
    style.configure("TLabel", background="Cornsilk", foreground="Black", font=("Century gothic", 24))
    label345 = ttk.Label(window, text="Пуск")
    label345.place(x=50, y=20)
    open_button = ttk.Button(window, text="РБК(Российская библиотека кодов)", command=click)
    open_button.place(x=50, y=100, width=170, height=170)
    open_button1 = ttk.Button(window, text="DeepSeek", command=click1)
    open_button1.place(x=260, y=100, width=170, height=170)
    open_button1 = ttk.Button(window, text="Текстовый редактор", command=click6)
    open_button1.place(x=50, y=300, width=170, height=170)
    open_button1 = ttk.Button(window, text="ЯндексБраузер Beta", command=click7)
    open_button1.place(x=260, y=300, width=170, height=170)
    close_button = ttk.Button(window, text="Рабочий стол", command=lambda: dismiss(window))
    close_button.place(x=50, y=500, width=380, height=170)
    def dismiss(window):
        window.grab_release() 
        window.destroy()



def click1():
    webview.create_window('DeepSeek', 'https://chat.deepseek.com/') 
    webview.start() 

def click6():
    window = Tk()
    window.title("Текстовый редактор")
    window.geometry("1000x700")
  
    window.grid_rowconfigure(index=0, weight=1)
    window.grid_columnconfigure(index=0, weight=1)
    window.grid_columnconfigure(index=1, weight=1)
 
    text_editor = Text(window)
    text_editor.grid(column=0, columnspan=2, row=0)
 
    # открываем файл в текстовое поле
    def open_file():
        filepath = text_editor.askopenfilename()
        if filepath != "":
            with open(filepath, "r") as file:
                text =file.read()
                text_editor.delete("1.0", END)
                text_editor.insert("1.0", text)
     

    # сохраняем текст из текстового поля в файл
    def save_file():
        filepath = text_editor.asksaveasfilename()
        if filepath != "":
            text = text_editor.get("1.0", END)
            with open(filepath, "w") as file:
                file.write(text)
        
 
    open_button = ttk.Button(window, text="Открыть файл", command=open_file)
    open_button.grid(column=0, row=1, sticky=NSEW, padx=10)
 
    save_button = ttk.Button(window, text="Сохранить файл", command=save_file)
    save_button.grid(column=1, row=1, sticky=NSEW, padx=10)
 
open_button = ttk.Button(text="РБК(Российская библиотека кодов)", command=click)
open_button.place(x=20, y=20, width=250, height=35)
open_button1 = ttk.Button(text="DeepSeek", command=click1)
open_button1.place(x=20, y=100, width=100, height=35)
open_button1 = ttk.Button(text="Текстовый редактор", command=click6)
open_button1.place(x=20, y=180, width=165, height=35)
open_button1 = ttk.Button(text="ЯндексБраузер Beta", command=click7)
open_button1.place(x=20, y=260, width=160, height=35)

button_pusk = ttk.Button(text="Пуск", command=pusk1)
button_pusk.place(x=20, y=1010)

close_button = ttk.Button(text="Закрыть окно", command=lambda: dismiss(root))
close_button.place(x=1770, y=20)

text_tinos = ttk.Label(text="TiNOS 1.0", font=("segoe ui", 60, "bold"))
text_tinos.place(x=1500, y=950)
 
root.mainloop()
