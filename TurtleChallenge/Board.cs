using System;
using System.Collections.Generic;
using static TurtleChallenge.Program;
using static TurtleChallenge.StructPoint;
using static TurtleChallenge.EnumState;
using static TurtleChallenge.EnumMoveResult;

namespace TurtleChallenge
{

    public class Board
    {
        public Point CurrentPosition { get; private set; }
        public int Height { get; }
        public int Width { get; }
        State[,] board;

        /// <summary>
        /// Include in the board the start and exit position
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="initialPosition"></param>
        /// <param name="exitPosition"></param>
        public Board(int width, int height, Point initialPosition, Point exitPosition)
        {
            Width = width;
            Height = height;

            board = new State[Height, Width];
            CurrentPosition = initialPosition;
            board[initialPosition.Y, initialPosition.X] = State.Start;
            board[exitPosition.Y, exitPosition.X] = State.Exit;
        }

        /// <summary>
        /// Include in the board the mines 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="initialPosition"></param>
        /// <param name="exitPosition"></param>
        /// <param name="minePositions"></param>
        public Board(int width, int height, Point initialPosition, Point exitPosition, IEnumerable<Point> minePositions)
            : this(width, height, initialPosition, exitPosition)
        {
            foreach (var pos in minePositions)
            {
                board[pos.Y, pos.X] = State.Mine;
            }
        }

        /// <summary>
        /// Make a move on the board and return a value indicating if the move was successful
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public MoveResult Move(Direction direction)
        {
            // Get the move from the dictionary
            Point newPosition = CurrentPosition + moves[direction];

            //Check if the Point is inside the board
            if (newPosition.X < 0 || newPosition.Y < 0 || newPosition.X >= Width || newPosition.Y >= Height)
            {
                return MoveResult.OutOfBounds;
            }

            //Check if the Point is a mine
            if (board[newPosition.Y, newPosition.X] == State.Mine)
            {
                return MoveResult.MineHit;
            }

            //Check if we reach the exit position
            if (board[newPosition.Y, newPosition.X] == State.Exit)
            {
                return MoveResult.Exit;
            }
            
            CurrentPosition = newPosition;

            //Normal movement(no out of bond, no mine and no exit)
            return MoveResult.Good;
        }

        /// <summary>
        /// Print the board in the screeen wih the values of empty, mine, start and exit.
        /// </summary>
        public void Print()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    switch (board[y, x])
                    {
                        case State.Empty:
                            Console.Write(0);
                            break;
                        case State.Mine:
                            Console.Write("M");
                            break;
                        case State.Exit:
                            Console.Write("E");
                            break;
                        case State.Start:
                            Console.Write("S");
                            break;

                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static readonly Dictionary<Direction, Point> moves = new Dictionary<Direction, Point>
        {
            { Direction.North, new Point(0, -1) },
            { Direction.East,  new Point(1,  0) },
            { Direction.South, new Point(0,  1) },
            { Direction.West,  new Point(-1, 0) },
        };
    }
}
