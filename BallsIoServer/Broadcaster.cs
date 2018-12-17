using System.Collections.Generic;
using SocketLibrary;

namespace BallsIoServer
{
    public class Broadcaster
    {
        private readonly List<ConnectedSocket> Clients = new List<ConnectedSocket>();

        public void AddClient(ConnectedSocket socket) => Clients.Add(socket);

        public void SendMessage(string message) => Clients.ForEach(x => x.Send(message));
    }
}
