using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
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

namespace ChatApplication
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            InitializeRemoteServer();
        }

        private void InitializeRemoteServer()
        {
            RemotingConfiguration.RegisterWellKnownClientType(
            typeof(ChatServer), "http://localhost:12345/ChatServer");
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(txtUser.Text);
            this.Hide();
            if(mw.isOnline)
                mw.ShowDialog();
            this.Show();

        }
    }
}
