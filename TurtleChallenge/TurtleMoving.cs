using System;
using System.Collections;
using System.Collections.Generic;
using static TurtleChallenge.Program;
using static TurtleChallenge.StructPoint;

namespace TurtleChallenge
{

    public class TurtleMoving
    {
        public void TurtleMove(Dictionary<string, string> configuration, string[] movements, List<Point> mines)
        {

            // using a Queue here as it fits better
            Queue<string> sequence = new Queue<string>(movements);

            var start = new Point(System.Windows.Vector.Parse(configuration["start"].ToString()));
            var exit = new Point(System.Windows.Vector.Parse(configuration["exit"].ToString()));


            var initialDirection = (Direction)Enum.Parse(typeof(Direction), configuration["initialDirection"].ToString());
            var currentDirection = initialDirection;

            int boardWidth = Convert.ToInt32(configuration["boardWidth"].ToString());
            int boardHeight = Convert.ToInt32(configuration["boardHeight"].ToString());

            var board = new Board(boardWidth, boardHeight, start, exit, mines);
            board.Print();

            while (sequence.Count > 0)
                {
                var move = sequence.Dequeue();
                if (move == "m")
                {
                    switch (board.Move(currentDirection))
                    {
                        case EnumMoveResult.MoveResult.Good:
                            // normal move - do nothing
                            Console.WriteLine("good");
                            break;
                        case EnumMoveResult.MoveResult.OutOfBounds:
                            // moved beyond the board - loss
                            Console.WriteLine("out of bounds");
                            return;
                        case EnumMoveResult.MoveResult.MineHit:
                            // moved on top of a mine - loss
                            Console.WriteLine("mine");
                            return;
                        case EnumMoveResult.MoveResult.Exit:
                            // if we require the moves to go exactly to the exit, not beyond,
                            // check whether the queue is empty here:
                            if (sequence.Count == 0)
                            {
                                Console.WriteLine("win");
                            }
                            return;
                    }
                }
                else if (move == "r")
                {
                    // Rotate the direction 90° clockwise by adding 1 modulo 4 as there are 4 directions.
                    // This requires the Direction enum to have directions in the right order.
                    // The rotate only change the direction of the movement, don't do a movement.
                    currentDirection = (Direction)((int)(currentDirection + 1) % 4);
                }
            }

        }

    }
}

