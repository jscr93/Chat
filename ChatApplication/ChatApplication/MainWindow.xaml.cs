using System.Windows;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace ChatApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<string> lstContacts;
        public static ObservableCollection<string> lstChat;
        public bool isOnline;
        private ChatClient chat;
        public static ListBox lstBxChatCode;

        public MainWindow(string name)
        {

            InitializeComponent();

            lstContacts = new ObservableCollection<string>();
            lstChat = new ObservableCollection<string>();
            isOnline = true;

            lstBxContacts.ItemsSource = lstContacts;
            lstBxChat.ItemsSource = lstChat;

            lstBxChatCode = lstBxChat;

            try
            {
                chat = new ChatClient(name);
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo conectar al servidor","Ha ocurrido un problema",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                isOnline = false;
                this.Close();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtMessage.Clear();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            chat.sendText(txtMessage.Text);
            txtMessage.Clear();

            //if (lstBxChat != null)
            //{
            //    var border = (Border)VisualTreeHelper.GetChild(lstBxChat, 0);
            //    var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
            //    scrollViewer.ScrollToBottom();
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(chat != null)
                chat.LogOut();
        }

        private void txtMessage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                btnSend.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }


}
