namespace BallsIoServer
{
    public class Circle
    {
        public Point Position { get; set; }

        public int Score { get; set; }

        public Circle(Point position = null, int score = 1)
        {
            Position = position ?? new Point();
            Score = score;
        }

        public bool ContainsOtherCircle(Circle other)
        {
            Point direction = (other.Position - Position).Normalize();
            Point firstPoint = Position + direction * Score;
            Point secondPoint = other.Position + direction * other.Score;
            Point thirdPoint = firstPoint - other.Position;
            return thirdPoint.Length() > secondPoint.Length();
        }
    }
}
