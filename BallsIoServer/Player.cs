using SocketLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BallsIoServer
{
    public class Player
    {
        private static int _id = 0;
        private readonly ConnectedSocket _socket;

        public int Id { get; }

        public List<Circle> Circles { get; } = new List<Circle>();

        public Point MoveDirection { get; set; }

        public Player(ConnectedSocket socket, Point moveDirection = null, List<Circle> circles = null)
        {
            Id = _id++;
            socket.Send("Id: " + Id);
            _socket = socket;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        if (!socket.AnythingToReceive)
                            continue;
                        string message = socket.Receive();
                        string[] newCoords = message.Split(' ');
                        MoveDirection = new Point(double.Parse(newCoords[0]), double.Parse(newCoords[1]));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
            });
            MoveDirection = moveDirection ?? new Point();
            if (circles != null)
                circles.ForEach(x => Circles.Add(x));
        }
    }
}
