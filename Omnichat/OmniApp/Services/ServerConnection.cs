﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmniApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OmniApp
{
    public static class ServerConnection
    {
        public static SocketIOClient.SocketIO Client;
        static ServerConnection()
        {
            Client = new SocketIOClient.SocketIO(Properties.Settings.Default.ServerUrl);
            Client.Options.AutoUpgrade = false;
        }
        public static void RunBots()
        {
            var tg_token = Properties.Settings.Default.TgToken;
            var vk_token = Properties.Settings.Default.VkToken;
            var email_address = Properties.Settings.Default.MailAddress;
            var email_password = Properties.Settings.Default.MailPassword;
            var whatsapp_id = Properties.Settings.Default.WhatsAppId;
            var whatsapp_token = Properties.Settings.Default.WhatsAppToken;
            var viber_token = Properties.Settings.Default.ViberToken;
            var generate_answer = Properties.Settings.Default.GenerateAnswer;
            var detect_theme = Properties.Settings.Default.DetectTheme;
            Client.EmitAsync("run_bots", tg_token, vk_token, email_address, email_password, whatsapp_id, whatsapp_token, viber_token, generate_answer, detect_theme);
        }


    }
}
