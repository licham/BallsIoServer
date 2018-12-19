using System.Collections.Generic;

namespace BallsIoServer
{
    public class GameState
    {
        private int _feedsCount => (int)FieldSize.Length();

        private readonly List<Circle> _feeds = new List<Circle>();

        public List<Player> Players { get; } = new List<Player>();

        public Point FieldSize { get; }

        public GameState(Point fieldSize, List<Player> players = null)
        {
            FieldSize = fieldSize;          
            if (players != null)
                players.ForEach(x => Players.Add(x));
            GenerateFeeds();
        }

        public void UpdateState()
        {
            Players.ForEach(player =>
            {
                player.Circles.ForEach(circle =>
                {
                    circle.Position += player.MoveDirection;
                    if (circle.Position.X < 0)
                        circle.Position.X = 0;
                    if (circle.Position.X > FieldSize.X)
                        circle.Position.Y = FieldSize.Y;
                    if (circle.Position.Y < 0)
                        circle.Position.Y = 0;
                    if (circle.Position.Y > FieldSize.Y)
                        circle.Position.Y = FieldSize.Y;
                    Players.ForEach(otherPlayer =>
                    {
                        if (player == otherPlayer)
                            return;

                        otherPlayer.Circles.ForEach(otherCircle => ProcessConcatenation(circle, otherCircle));
                        otherPlayer.Circles.RemoveAll(otherCircle => otherCircle.Score == 0);
                    });
                    _feeds.ForEach(feed => ProcessConcatenation(circle, feed));
                    _feeds.RemoveAll(feed => feed.Score == 0);
                });
            });
            Players.RemoveAll(player => player.Circles.Count == 0);
            GenerateFeeds();
        }

        private void ProcessConcatenation(Circle one, Circle other)
        {
            if (one.ContainsOtherCircle(other))
            {
                one.Score += other.Score;
                other.Score = 0;
            }
        }

        private void GenerateFeeds()
        {
            for (int i = 0; i < _feedsCount - _feeds.Count; i++)
            {
                var random = new System.Random(System.DateTime.Now.Millisecond);
                var x = random.Next((int)FieldSize.X);
                var y = random.Next((int)FieldSize.Y);
                _feeds.Add(new Circle(new Point(x, y)));
            }
        }
    }
}
