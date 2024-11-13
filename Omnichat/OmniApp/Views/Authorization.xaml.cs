using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace OmniApp
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        MainWindow mainWindow;
        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextbox.Text) || string.IsNullOrWhiteSpace(PasswordTextbox.Password))
            {
                ErrorText.Visibility = Visibility.Visible;
                return;
            }
            CheckUser();
                
        }

       
        private void CheckUser()
        {
            //проверка наличия пользователя в системе. В данный момент переменные user и password используются для создания реакции приложения в случае неверности данных
            string user = "d"; 
            string password = "1";
            if (NameTextbox.Text == user && PasswordTextbox.Password == password) 
            {
                mainWindow = new MainWindow(NameTextbox.Text);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorText.Visibility = Visibility.Visible;
                return;
            }
            
        }

        private void Authorization_MouseLeftButton(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        private void Authorization_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = 650;
            Width = 1000;
            bor.Width = Width;
            bor.Height = Height;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }


    }
}
