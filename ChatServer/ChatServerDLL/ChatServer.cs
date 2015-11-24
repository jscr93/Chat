using System;
using System.Linq;
using System.Collections;

namespace ChatServerDLL
{
    public class ChatServer : MarshalByRefObject
    {
        private ArrayList clients = new ArrayList(); // Names of clients

        private ArrayList chatSession = new ArrayList(); // Holds text for chat session

        private String lastUser = null;
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

        public void AddText(String newText, String name)
        {
            if (newText != null)
            {
                lock (chatSession)
                {
                    if(lastUser == name)
                        chatSession.Add(newText);
                    else
                    {
                        if(lastUser != null)
                            chatSession.Add("");
                        lastUser = name;
                        chatSession.Add(name + " dice:");
                        chatSession.Add(newText);
                    }
                }
            }
        }

        // Queries
        public ArrayList Clients()
        {
            return clients;
        }

        public ArrayList ChatSession()
        {
            return chatSession;
        }
    }


}
