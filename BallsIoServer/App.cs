using SocketLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BallsIoServer
{
    public class App
    {
        private readonly List<Session> _sessions = new List<Session>();
        private readonly Listener _listener = new Listener();

        public App()
        {
            _sessions.Add(new Session(new Point(10000, 10000)));
            _listener.ClientAdded += ProcessNewClient;
            while (true)
            {
                _sessions.ForEach(session => {
                    session.UpdateSession();
                    Thread.Sleep(100);
                });
            }
        }

        public void ProcessNewClient(ConnectedSocket socket, string message) =>
            _sessions.First().AddPlayer(new Player(socket), socket);
    }
}
