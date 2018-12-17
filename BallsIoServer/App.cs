using System.Collections.Generic;
using System.Linq;

namespace BallsIoServer
{
    public class App
    {
        private readonly List<Session> _sessions = new List<Session>();
        private readonly Listener _listener = new Listener();

        public App()
        {
            _listener.ClientAdded += (socket, message) =>
                _sessions.First().AddPlayer(new Player(socket), socket);
        }
    }
}
