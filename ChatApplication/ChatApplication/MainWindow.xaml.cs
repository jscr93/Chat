using System.Windows;
using System.Collections.ObjectModel;
using System;

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
        ChatClient chat;

        public MainWindow(string name)
        {

            InitializeComponent();
            lstContacts = new ObservableCollection<string>();
            lstChat = new ObservableCollection<string>();
            isOnline = true;

            lstBxContacts.ItemsSource = lstContacts;
            lstBxChat.ItemsSource = lstChat;

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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(chat != null)
                chat.LogOut();
        }
    }


}
