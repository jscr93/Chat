using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ChatServerDLL
{
    public class ChatServer : MarshalByRefObject
    {
        private ArrayList clients = new ArrayList(); // Names of clients
        private List<Conversation> conversations = new List<Conversation>();

        private class Conversation
        {
            public string user1 { get; set; }
            public string user2 { get; set; }
            public string lastUser = null;
            public ArrayList chatSession = new ArrayList(); // Holds text for chat session
        }

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

        public void AddText(string newText, string name1, string name2)
        {
            if (newText != null)
            {
                Conversation usersConversation = conversations.Where(c => (c.user1 == name1 && c.user2 == name2) || (c.user1 == name2 && c.user2 == name1)).First();
                lock (usersConversation.chatSession)
                {
                    if(usersConversation.lastUser == name1)
                        usersConversation.chatSession.Add(newText);
                    else
                    {
                        usersConversation.lastUser = name1;
                        usersConversation.chatSession.Add("");
                        usersConversation.chatSession.Add(name1 + " dice:");
                        usersConversation.chatSession.Add(newText);
                    }
                }
            }
        }

        // Queries
        public ArrayList Clients()
        {
            return clients;
        }

        public ArrayList ChatSession(string name1, string name2)
        {
            List<Conversation> usersConversation = conversations.Where(c => (c.user1 == name1 && c.user2 == name2) || (c.user1 == name2 && c.user2 == name1)).ToList();
            if (usersConversation.Count > 0)
                return usersConversation[0].chatSession;
            else
            {
                Conversation newConversation = new Conversation { user1 = name1, user2 = name2 };
                lock (conversations)
                {
                    conversations.Add(newConversation);
                }
                return newConversation.chatSession;
            }
        }
    }


}
