using System.Collections.Generic;

namespace BallsIoServer
{
    public class GameState
    {
        public List<Player> Players { get; } = new List<Player>();

        public Point FieldSize { get; }

        public GameState(Point fieldSize, List<Player> players = null)
        {
            FieldSize = fieldSize;
            if (players != null)
            {
                players.ForEach(x => Players.Add(x));
            }
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

                        otherPlayer.Circles.ForEach(otherCircle =>
                        {
                            Point direction = (otherCircle.Position - circle.Position).Normalize();
                            Point firstPoint = circle.Position + direction * circle.Score;
                            Point secondPoint = otherCircle.Position + direction * otherCircle.Score;
                            Point thirdPoint = firstPoint - otherCircle.Position;
                            if (thirdPoint.Length() > secondPoint.Length())
                            {
                                circle.Score += otherCircle.Score;
                                otherCircle.Score = 0;
                            }
                        });
                        otherPlayer.Circles.RemoveAll(otherCircle => otherCircle.Score == 0);
                    });
                });
            });
            Players.RemoveAll(player => player.Circles.Count == 0);
        }
    }
}
