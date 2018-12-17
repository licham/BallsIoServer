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
            Task.Factory.StartNew(() => 
            {
                _mutex.WaitOne();
                _gameState.UpdateState();
                _broadcaster.SendMessage(_gameState.Players.ToString());
                _mutex.ReleaseMutex();
                Thread.Sleep(10);
            });
        }

        public void AddPlayer(Player player, ConnectedSocket playerSocket)
        {
            Task.Factory.StartNew(() =>
            {
                _mutex.WaitOne();
                var random = new System.Random(System.DateTime.Now.Millisecond);
                var x = random.Next((int)_gameState.FieldSize.X);
                var y = random.Next((int)_gameState.FieldSize.Y);
                player.Circles.Add(new Circle(new Point(x, y)));
                _gameState.Players.Add(player);
                _broadcaster.AddClient(playerSocket);
                _mutex.ReleaseMutex();
            });            
        }
    }
}
