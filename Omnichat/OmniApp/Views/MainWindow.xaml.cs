using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;

namespace OmniApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Window_MouseLeftButton(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private bool flag = false;
        private double offset = 50;
        public ObservableCollection<Chat> Chats { get; set; }
        public MainWindow(string userName)
        {
            InitializeComponent();
            //CreateMenuElements();
            UserAccount user = new UserAccount(userName);
            user.UserName = userName;
            this.Loaded += OnWindowLoaded;
            ProfilPlace.Children.Add(user);
            HideElements();
            SettingWindow.ToRightSettingRequested += OnToRightSettingRequested;
            SettingWindow.CloseSettingRequested += OnCloseSettingRequested;
            SettingWindow2.ToLeftSettingRequested += OnToLeftSettingRequested;
            SettingWindow2.CloseSettingRequested += OnCloseSettingRequested;
        }
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            ServerConnection.RunBots();
            ChatUpdating();
        }
        private void ChatUpdating()
        {
            ServerConnection.Client.EmitAsync("get_chats");
            ServerConnection.Client.On("get_chats_response", response => 
            {
                Console.WriteLine(response.ToString());
                var chats = JsonConvert.DeserializeObject<List<List<Chat>>>(response.ToString())[0];
                Chats = new ObservableCollection<Chat>(chats ?? new List<Chat>());
                var AwaitingChats = Chats.Where(chat => chat.Status == "awaiting");
                var OpenChats = Chats.Where(chat => chat.Status == "open");
                var ClosedChats = Chats.Where(chat => chat.Status == "closed");
                var OfflineChats = Chats.Where(chat => chat.Status == "offline");
                Application.Current.Dispatcher.Invoke(() =>
                {
                    UpdateMenuElements(AwaitingChats, OpenChats, ClosedChats, OfflineChats);
                });

            });
        }

        private void UpdateMenuElements(IEnumerable<Chat> awaitingChats, IEnumerable<Chat> openChats, IEnumerable<Chat> closedChats, IEnumerable<Chat> offlineChats)
        {
            MenuPlace.Children.Clear();
            AddMenuElement("Ожидают ответа", awaitingChats);
            AddMenuElement("В диалоге", openChats);
            AddMenuElement("Закрытые диалоги", closedChats);
            AddMenuElement("Офлайн-обращения", offlineChats);
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Right;
            Button addButton = new Button
            {
                Content = "+",
                Width = 30,
                Height = 30,
                Margin = new Thickness(0, 0, 7, 0)
            };
            addButton.Click += AddButton_Click;
            grid.Children.Add(addButton);
            MenuPlace.Children.Add(grid);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        string fileContent = sr.ReadToEnd();
                        var newOfflineChat = new Chat("offline", openFileDialog.FileName, "offline");
                        var newOfflineMessage = new Message("offline", newOfflineChat.Chat_id, newOfflineChat.Source, fileContent);
                        ServerConnection.Client.EmitAsync("save_user_offline", newOfflineChat.Source, newOfflineChat.Chat_id, newOfflineMessage.Text);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                }
            }
        }

        private void AddMenuElement(string header, IEnumerable<Chat> chats)
        {
            ClientMenuElem menuElem = new ClientMenuElem
            {
                Text = header,
                listBox = { ItemsSource = chats }
            };
            menuElem.SelectionChangedEvent += OnChatSelected;
            Style listBoxStyle = new Style(typeof(ListBoxItem));
            listBoxStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new FontFamily("Georgia")));
            MenuPlace.Children.Add(menuElem);

        }


        private void OnChatSelected(object sender, Chat selectedChat)
        {
            Console.WriteLine($"Выбран чат с ID: {selectedChat.Chat_id} в MainWindow");
            WithoutChat.Visibility = Visibility.Collapsed;
            SettingWindow.Visibility = Visibility.Collapsed;
            MainGrid.Children.Remove(ChatWindow);
            ChatWindow = new ChatWindow(selectedChat);
            ChatWindow.Visibility = Visibility.Visible;
            Grid.SetRow(ChatWindow, 1);
            Grid.SetColumn(ChatWindow, 2);
            MainGrid.Children.Add(ChatWindow);
            if (sender is ClientMenuElem menuElem)
            {
                menuElem.listBox.SelectedItem = null;
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                ShowElements();
                ShiftElements(offset);
            }
            else
            {
                HideElements();
                ShiftElements(-offset);
            }
            flag = !flag;
        }

        private void ShowElements()
        {
            img.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            //label3.Visibility = Visibility.Visible;
            //label4.Visibility = Visibility.Visible;
        }

        private void HideElements()
        {
            img.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            //label3.Visibility = Visibility.Hidden;
            //label4.Visibility = Visibility.Hidden;
        }

        private void ShiftElements(double offset)
        {
            foreach (var child in MenuPlace.Children)
            {
                if (child is ClientMenuElem elem)
                {
                    var transform = elem.RenderTransform as TranslateTransform;

                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        elem.RenderTransform = transform;
                    }

                    // Сдвигаем элемент вправо
                    transform.X += offset;
                }
            }
        }
        private void Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            WithoutChat.Visibility = Visibility.Collapsed;
            ChatWindow.Visibility = Visibility.Collapsed;
            SettingWindow.Visibility = Visibility.Visible;
            SettingWindow2.Visibility = Visibility.Collapsed;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SetWindowSize()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            bor.Width = screenWidth;
            bor.Height = screenHeight;
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = 650;
            Width = 1200;
            bor.Width = Width;         
            bor.Height = Height;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                SetWindowSize();
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }
        private void OnToRightSettingRequested(object sender, EventArgs e)
        {
            WithoutChat.Visibility = Visibility.Collapsed;
            ChatWindow.Visibility = Visibility.Collapsed;
            SettingWindow.Visibility = Visibility.Collapsed;
            SettingWindow2.Visibility = Visibility.Visible;
        }
        private void OnToLeftSettingRequested(object sender, EventArgs e)
        {
            WithoutChat.Visibility = Visibility.Collapsed;
            ChatWindow.Visibility = Visibility.Collapsed;
            SettingWindow.Visibility = Visibility.Visible;
            SettingWindow2.Visibility = Visibility.Collapsed;
        }

        private void OnCloseSettingRequested(object sender, EventArgs e)
        {
            WithoutChat.Visibility = Visibility.Visible;
            ChatWindow.Visibility = Visibility.Collapsed;
            SettingWindow.Visibility = Visibility.Collapsed;
            SettingWindow2.Visibility = Visibility.Collapsed;
        }
    }
}
