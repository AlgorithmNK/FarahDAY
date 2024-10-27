﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniApp
{
    public class Chat
    {
        public string Source { get; set; }
        public string Chat_id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public Chat(string source, string chat_id, string status = null, string type = null, string priority = null)
        {
            Source = source;
            Chat_id = chat_id;
            Status = status;
            Type = type;
            Priority = priority;
        }
    }

    public class Message
    {
        public string Source { get; set; }
        public string Chat_sourсe { get; set; }
        public string Chat_id { get; set; }
        public string Text { get; set; }

        public Message(string source, string chat_source, string chat_id, string text)
        {
            Source = source;
            Chat_sourсe = chat_source;
            Chat_id = chat_id;
            Text = text;
        }
    }
}
