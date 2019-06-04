using System;
using System.Windows;

namespace TurtleChallenge
{

public class StructPoint
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y) { X = x; Y = y; }
        public Point(Vector vector) { X = Convert.ToInt32(vector.X); Y = Convert.ToInt32(vector.Y); }

        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
    }
}

}
