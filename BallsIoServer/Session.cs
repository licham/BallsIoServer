using System.Threading;
using System.Threading.Tasks;
using SocketLibrary;

namespace BallsIoServer
{
    public class Session
    {
        private readonly GameState _gameState;
        private readonly Broadcaster _broadcaster = new Broadcaster();
        private readonly Mutex _mutex = new Mutex();

        public Session(Point fieldSize)
        {
            _gameState = new GameState(fieldSize);
        }

        public void UpdateSession()
        {
            _mutex.WaitOne();
            _gameState.UpdateState();
            _broadcaster.SendMessage(_gameState.ToString());
            _mutex.ReleaseMutex();
        }

        public void AddPlayer(Player player, ConnectedSocket playerSocket)
        {
            Task.Factory.StartNew(() =>
            {
                _mutex.WaitOne();
                var random = new System.Random(System.DateTime.Now.Millisecond);
                var x = random.Next((int)_gameState.FieldSize.X);
                var y = random.Next((int)_gameState.FieldSize.Y);
                player.Circles.Add(new Circle(new Point(x, y), 2));
                _gameState.Players.Add(player);
                _broadcaster.Clients.Add(playerSocket);
                _mutex.ReleaseMutex();
            });            
        }
    }
}
