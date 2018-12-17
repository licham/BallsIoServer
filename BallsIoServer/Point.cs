namespace BallsIoServer
{
    public class Point
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Point(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }        

        public double Length() => System.Math.Sqrt(X * X + Y * Y);

        public Point Normalize() => new Point(X / Length(), Y / Length());

        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b) => new Point(b.X - a.X, b.Y - a.Y);

        public static Point operator *(Point point, double size) => new Point(point.X * size, point.Y * size);
    }
}
