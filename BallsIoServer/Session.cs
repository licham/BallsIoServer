using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

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

        public void AddPlayer(Player player, Socket playerSocket)
        {
            Task.Factory.StartNew(() =>
            {
                _mutex.WaitOne();
                _gameState.Players.Add(player);
                _broadcaster.AddClient(playerSocket);
                _mutex.ReleaseMutex();
            });            
        }
    }
}
