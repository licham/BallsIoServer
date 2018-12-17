using SocketLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BallsIoServer
{
    public class Player
    {
        private readonly ConnectedSocket _socket;

        public List<Circle> Circles { get; } = new List<Circle>();

        public Point MoveDirection { get; set; }

        public Player(ConnectedSocket socket, Point moveDirection = null, List<Circle> circles = null)
        {
            _socket = socket;
            Task.Factory.StartNew(() =>
            {
                string message = socket.Receive();
                string[] newCoords = message.Split(' ');
                MoveDirection = new Point(double.Parse(newCoords[0]), double.Parse(newCoords[1]));
            });
            MoveDirection = moveDirection ?? new Point();
            if (circles != null)
                circles.ForEach(x => Circles.Add(x));
        }
    }
}
