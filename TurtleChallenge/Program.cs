using System;
using System.IO;

namespace TurtleChallenge
{

    public partial class Program
    {
        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        public static void Main()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Files\\";

            if (File.Exists(path + "moves.xml") && File.Exists(path + "game-settings.xml"))
            {
                var mines = ReadFile.GetMines(path + "game-settings.xml");

                var configuration = ReadFile.GetSettings(path + "game-settings.xml");

                var movements = ReadFile.GetSettings(path + "moves.xml")["sequence"].ToString().Split(',');

                var movements1 = ReadFile.GetSettings(path + "moves.xml")["sequence1"].ToString().Split(',');

                TurtleMoving turtle = new TurtleMoving();

                Console.WriteLine("Sequence 1");
                turtle.TurtleMove(configuration, movements, mines);
                Console.WriteLine("");
                Console.WriteLine("");

                Console.WriteLine("Sequence 2");
                turtle.TurtleMove(configuration, movements1, mines);

                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Please, put the files in the execution folder. Thank you.");
                Console.ReadKey();
            }
        }
    }

}
