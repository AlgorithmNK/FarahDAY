from cx_Freeze import setup, Executable

build_options = {
    'packages': ['telebot', 'vk_api', 'whatsapp_chatbot_python', 'requests'],
    'includes': ['telebot', 'vk_api', 'whatsapp_chatbot_python', 'requests'],
    'include_files': []  # Add any external files if needed
}

setup(
    name="FarahDAY",
    version="1.0",
    options={'build_exe': build_options},
    executables=[Executable("Server.py")]
)