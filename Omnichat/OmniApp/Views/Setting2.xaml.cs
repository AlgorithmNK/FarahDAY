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
    public partial class Setting2 : UserControl
    {
        public Setting2()
        {
            InitializeComponent();
            TextBoxWhatsAppId.Text = Properties.Settings.Default.WhatsAppId;
            PasswordBoxWhatsAppToken.Password = Properties.Settings.Default.WhatsAppToken;
            CheckBoxGenerateAnswer.IsChecked = Properties.Settings.Default.GenerateAnswer;
            CheckBoxDetectTheme.IsChecked = Properties.Settings.Default.DetectTheme;

        }
        public event EventHandler CloseSettingRequested;
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WhatsAppId = TextBoxWhatsAppId.Text;
            Properties.Settings.Default.WhatsAppToken = PasswordBoxWhatsAppToken.Password;
            Properties.Settings.Default.GenerateAnswer = CheckBoxGenerateAnswer.IsChecked.Value;
            Properties.Settings.Default.DetectTheme = CheckBoxDetectTheme.IsChecked.Value;
            Properties.Settings.Default.Save();
            ServerConnection.RunBots();
            CloseSettingRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ToLeftSettingRequested;

        private void ToLeftButton_Click(object sender, RoutedEventArgs e)
        {
            ToLeftSettingRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
