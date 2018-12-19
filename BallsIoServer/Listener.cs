using SocketLibrary;
using System.Net;
using System;

namespace BallsIoServer
{
    public class Listener
    {
        private readonly SocketListener _socket;

        public Listener()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var address in ipHostInfo.AddressList)
                Console.WriteLine(address);
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            _socket = new SocketListener(10000, ipAddress.ToString());
            Console.WriteLine(_socket.UnderlyingSocket.LocalEndPoint);
            
            while (true)
            {
                ConnectedSocket player = _socket.Accept();
                Console.WriteLine("Client added");
                string message = player.Receive();
                ClientAdded?.Invoke(player, message);
            }
        }

        public event Action<ConnectedSocket, string> ClientAdded;
    }
}
