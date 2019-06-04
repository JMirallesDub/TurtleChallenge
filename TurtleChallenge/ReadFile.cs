using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static TurtleChallenge.StructPoint;

namespace TurtleChallenge
{
    /// <summary>
    /// Class for read the different files for configuration, movements and mines.
    /// </summary>
    public static class ReadFile
    {
        public static Dictionary<string, string> GetSettings(string path)
        {
            return getSettings(path);
        }

        public static List<Point> GetMines(string path)
        {
            return getMines(path);
        }

        /// <summary>
        /// Method for get a Dictionary for configuration and movements.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Dictionary<string, string> getSettings(string path)
        {
            Dictionary<string, string> _ret = new Dictionary<string, string>();
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
            XmlNode root = doc.SelectSingleNode("Settings");
            XmlNodeList nodelist = doc.GetElementsByTagName("add");

            foreach (XmlNode n in nodelist)
            {
                _ret.Add(
                    n.Attributes["key"].Value,
                    n.Attributes["value"].Value
                    );
            };

            return (_ret);
        }

        /// <summary>
        /// Modify the former method for return a list of Points
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<Point> getMines(string path)
        {
            List<Point> _ret = new List<Point>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode root = doc.SelectSingleNode("Settings");
            XmlNodeList nodelist = doc.GetElementsByTagName("Point");

            foreach (XmlNode n in nodelist)
            {
                Point point = new Point(
                Convert.ToInt32(n.Attributes["x"].Value),
                Convert.ToInt32(n.Attributes["y"].Value));

                _ret.Add(point);
            }
            return _ret;
        }

    }
}
