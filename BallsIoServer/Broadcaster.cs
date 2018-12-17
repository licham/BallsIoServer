using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace BallsIoServer
{
    public class Broadcaster
    {
        private readonly List<Socket> Clients = new List<Socket>();

        public void AddClient(Socket socket) =>
            Clients.Add(socket);

        public void SendMessage(string message) =>
            Clients.ForEach(x => x.Send(Encoding.ASCII.GetBytes(message)));
    }
}
