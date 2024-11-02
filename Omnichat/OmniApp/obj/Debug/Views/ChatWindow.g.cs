﻿#pragma checksum "..\..\..\Views\ChatWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3058D0C6FEE8A080ED6A836E99D5159DAB50D92F5EC4FDFF3C6567E378BF9DB0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using OmniApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace OmniApp {
    
    
    /// <summary>
    /// ChatWindow
    /// </summary>
    public partial class ChatWindow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ChatName;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollViewer;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox MessagesList;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UploadButton;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseStatusButton;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn2;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn1;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img1;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\Views\ChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OmniApp;component/views/chatwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ChatWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\Views\ChatWindow.xaml"
            ((OmniApp.ChatWindow)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.On_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ChatName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\Views\ChatWindow.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.scrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 5:
            this.MessagesList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 91 "..\..\..\Views\ChatWindow.xaml"
            this.textbox.GotFocus += new System.Windows.RoutedEventHandler(this.textbox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.UploadButton = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\..\Views\ChatWindow.xaml"
            this.UploadButton.Click += new System.Windows.RoutedEventHandler(this.UploadButton_Click_1);
            
            #line default
            #line hidden
            return;
            case 8:
            this.CloseStatusButton = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\Views\ChatWindow.xaml"
            this.CloseStatusButton.Click += new System.Windows.RoutedEventHandler(this.CloseStatusButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 109 "..\..\..\Views\ChatWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SendButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn2 = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.btn1 = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.img1 = ((System.Windows.Controls.Image)(target));
            return;
            case 13:
            this.img2 = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
