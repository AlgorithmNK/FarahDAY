﻿using System;
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
    /// <summary>
    /// Логика взаимодействия для ExitAccount.xaml
    /// </summary>
    public partial class ExitAccount : UserControl
    {
        public ExitAccount()
        {
            InitializeComponent();
        }

        Authorization authorization = new Authorization();
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                authorization = new Authorization();
                authorization.Show();
                mainWindow.Close();
            }

        }
    }
}
