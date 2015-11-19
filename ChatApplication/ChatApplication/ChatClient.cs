using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Threading;
using System.Collections;
using ChatServerDLL;

namespace ChatApplication
{
    public class ChatClient
    {
        // Fields
        private ChatServer server;
        private String name;
        private Thread thrd; // Used to poll the server for client names

        // Constructor
        public ChatClient(String name)
        {
            this.name = name;
            try
            {
                login();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void LogOut()
        {
            server.RemoveClient(this.name);
            thrd.Abort();
        }

        private void login()
        {

            server = new ChatServer();
            server.AddClient(this.name);

            thrd = new Thread(new ThreadStart(PollServer));
            thrd.Start();
        }


        private void PollServer()
        {
            for (;;)
            {
                Thread.Sleep(1000);
                ArrayList clients = server.Clients();

                MainWindow.lstContacts.ClearOnUI();

                foreach (String clientName in clients)
                {
                    MainWindow.lstContacts.AddOnUI(clientName);
                }

                String sessionText = server.ChatSession();
                MainWindow.lstChat.ClearOnUI();
                MainWindow.lstChat.AddOnUI(sessionText);
            }
        }

        public void sendText(string text)
        {
            String toSend = name + ": ";
            server.AddText(name + ":\n" + text + "\n\n");
        }
    }


}