using SocketLibrary;
using System;

namespace BallsIoServer
{
    public class Listener
    {
        private readonly SocketListener _socket;

        public Listener()
        {
            _socket = new SocketListener(1998);
            
            while (true)
            {
                ConnectedSocket player = _socket.Accept();
                string message = player.Receive();
                ClientAdded?.Invoke(player, message);
            }
        }

        public event Action<ConnectedSocket, string> ClientAdded;
    }
}
