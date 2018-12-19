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
            //Console.WriteLine(print);
            Clients.RemoveAll(client => !client.UnderlyingSocket.Connected);
            Clients.ForEach(client => {
                try
                {
                    if (client.UnderlyingSocket.Connected)
                        client.Send(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);                    
                }
            });
        }
    }
}
