using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Automation.Provider;
using System.Timers;
using SocketIOClient;
using System.Net.Sockets;
using System.Text.Json;
using Newtonsoft.Json;

namespace OmniApp
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : UserControl
    {
        private bool isOpen = false;
        public ObservableCollection<Message> Messages;
        private Chat chat;
        public ChatWindow(Chat chat)
        {
            this.chat = chat;
            InitializeComponent();
            IsHidden();
            this.Loaded += OnWindowLoaded;
        }
        public ChatWindow() 
        {
            InitializeComponent();
            IsHidden();
        }
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadMessages();
            SubscribeToNewMessages();
            ScrollToBottom();
        }
        private void SubscribeToNewMessages()
        {
            ServerConnection.Client.On("new_message", response =>
            {
                var newMessage = JsonConvert.DeserializeObject<List<Message>>(response.ToString())[0];
                //Console.WriteLine($"newMessage: {newMessage.Chat_id}, {newMessage.Chat_sourсe}, {newMessage.Text}");
                if (newMessage.Chat_id == chat.Chat_id && newMessage.Chat_sourсe == chat.Source)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Messages.Add(newMessage);
                        if (Messages.Count > 0)
                        {
                            ChatName.Text = Messages[0].Source;
                        }
                    });
                }
            });
            
        }
        private void LoadMessages()
        {
            ServerConnection.Client.EmitAsync("get_chat_messages", chat.Source, chat.Chat_id);
            ServerConnection.Client.On("get_chat_messages_response", response => 
            {
                var messages = JsonConvert.DeserializeObject<List<List<Message>>>(response.ToString())[0];
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages = new ObservableCollection<Message>(messages ?? new List<Message>());
                    MessagesList.ItemsSource = Messages;
                    if (Messages.Count > 0)
                    {
                        ChatName.Text = Messages[0].Source;
                    }
                });
            });
        }

        private void On_Closing(object sender, RoutedEventArgs e)
        {

        }

        public void IsHidden()
        {
            btn1.Visibility = Visibility.Hidden;
            btn2.Visibility = Visibility.Hidden;
            img1.Visibility = Visibility.Hidden;
            img2.Visibility = Visibility.Hidden;
        }

        public void IsVisible()
        {
            btn1.Visibility=Visibility.Visible;
            btn2.Visibility=Visibility.Visible;
            img1.Visibility=Visibility.Visible;
            img2.Visibility=Visibility.Visible;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox.Text = "";
        }
        private void ScrollToBottom()
        {
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
            }
        }

        private void UploadButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!isOpen) 
            {
                IsVisible();
            }
            else
            {
                IsHidden();
            }
            isOpen = !isOpen;
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textbox.Text))
            {
                Message message = new Message("user", chat.Source, chat.Chat_id, textbox.Text);
                Messages.Add(message);
                textbox.Clear();
                ScrollToBottom();
                ServerConnection.Client.EmitAsync("send_message", chat.Source, chat.Chat_id, message.Text);
            }
        }
        private void CloseStatusButton_Click(object sender, RoutedEventArgs e)
        {
            ServerConnection.Client.EmitAsync("close_chat", chat.Source, chat.Chat_id);
            LoadMessages();
        }
    }
}
