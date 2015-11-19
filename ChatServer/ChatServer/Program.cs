using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using ChatServerDLL;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and register the channel
            HttpChannel channel = new HttpChannel(12345);
            ChannelServices.RegisterChannel(channel);
            Console.WriteLine("Starting ChatServer");

            // Register the ChatServer for remoting
            RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(ChatServer),
                    "ChatServer",
                    WellKnownObjectMode.Singleton);

            Console.WriteLine("Press return to exit ChatServer.");
            Console.ReadLine();
        }
    }
}