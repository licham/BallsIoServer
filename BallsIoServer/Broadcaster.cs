using System.Collections.Generic;
using SocketLibrary;
using System;

namespace BallsIoServer
{
    public class Broadcaster
    {
        public List<ConnectedSocket> Clients { get; } = new List<ConnectedSocket>();

        public void SendMessage(string message)
        {
            var print = "Message sent\nBody:\n" + message + "\n";
            Console.WriteLine(print);
            Clients.ForEach(x => x.Send(message));
        }
    }
}
