using System.Collections.Generic;
using SocketLibrary;

namespace BallsIoServer
{
    public class Broadcaster
    {
        public List<ConnectedSocket> Clients { get; } = new List<ConnectedSocket>();

        public void SendMessage(string message) => Clients.ForEach(x => x.Send(message));
    }
}
