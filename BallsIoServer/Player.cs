using System.Collections.Generic;

namespace BallsIoServer
{
    public class Player
    {
        public List<Circle> Circles { get; } = new List<Circle>();

        public int Id { get; }

        public Point MoveDirection { get; set; }

        public Player(int id, Point moveDirection = null, List<Circle> circles = null)
        {
            Id = id;
            MoveDirection = moveDirection ?? new Point();
            if (circles != null)
            {
                circles.ForEach(x => Circles.Add(x));
            }
        }
    }
}
