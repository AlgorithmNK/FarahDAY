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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OmniApp
{
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
            TextBoxUrl.Text = Properties.Settings.Default.ServerUrl;
            PasswordBoxTgToken.Password = Properties.Settings.Default.TgToken;
            PasswordBoxVkToken.Password = Properties.Settings.Default.VkToken;
            TextBoxMailAddress.Text = Properties.Settings.Default.MailAddress;
            PasswordBoxMailPassword.Password = Properties.Settings.Default.MailPassword;
        }
        public event EventHandler CloseSettingRequested;
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ServerUrl = TextBoxUrl.Text;
            Properties.Settings.Default.TgToken = PasswordBoxTgToken.Password;
            Properties.Settings.Default.VkToken = PasswordBoxVkToken.Password;
            Properties.Settings.Default.MailAddress = TextBoxMailAddress.Text;
            Properties.Settings.Default.MailPassword = PasswordBoxMailPassword.Password;
            Properties.Settings.Default.Save();
            ServerConnection.RunBots();
            CloseSettingRequested?.Invoke(this, EventArgs.Empty);

        }
        public event EventHandler ToRightSettingRequested;
        private void ToRightButton_Click(object sender, RoutedEventArgs e)
        {
            ToRightSettingRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
