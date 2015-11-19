using System;
using System.Collections;

namespace ChatServerDLL
{
    public class ChatServer : MarshalByRefObject
    {
        private ArrayList clients = new ArrayList(); // Names of clients

        private String chatSession = ""; // Holds text for chat session

        // Commands
        public void AddClient(String name)
        {
            if (name != null)
            {
                lock (clients)
                {
                    clients.Add(name);
                }
            }
        }

        public void RemoveClient(String name)
        {
            lock (clients)
            {
                clients.Remove(name);
            }
        }

        public void AddText(String newText)
        {
            if (newText != null)
            {
                lock (chatSession)
                {
                    chatSession += newText;
                }
            }
        }

        // Queries
        public ArrayList Clients()
        {
            return clients;
        }

        public String ChatSession()
        {
            return chatSession;
        }
    }


}
