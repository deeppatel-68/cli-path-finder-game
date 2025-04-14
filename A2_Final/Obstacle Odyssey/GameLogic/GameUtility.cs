

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.GameLogic
{
    /// <summary>
    /// A utility class providing core game-related functionalities for StealthQuest.
    /// </summary>
    public static class GameUtility
    {
        /// <summary>
        /// Validates whether the provided choice string is valid for game commands.
        /// </summary>
        /// <param name="choice">The input choice string to validate.</param>
        /// <returns><c>true</c> if the choice is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidChoice(string choice)
        {
            // Check if the choice is not empty and belongs to the set of valid game commands
            return !string.IsNullOrEmpty(choice) && "gfscldmpx".Contains(choice);
        }

        /// <summary>
        /// Requests position information from the user in the format "X,Y".
        /// </summary>
        /// <param name="x">Output parameter for the X-coordinate if valid, otherwise 0.</param>
        /// <param name="y">Output parameter for the Y-coordinate if valid, otherwise 0.</param>
        /// <returns><c>true</c> if a valid position input is provided; otherwise, <c>false</c>.</returns>
        public static bool GetPositionInput(out int x, out int y)
        {
            var positionInput = Console.ReadLine()?.Split(',');

            // Check if input has two parts and both parts can be parsed to integers
            if (positionInput?.Length == 2 &&
                int.TryParse(positionInput[0], out x) &&
                int.TryParse(positionInput[1], out y))
            {
                return true;
            }
            else
            {
                x = 0;
                y = 0;
                return false;
            }
        }

        /// <summary>
        /// Represents cardinal directions as angles in degrees.
        /// </summary>
        public enum Direction
        {
            E = 0, //East direction with an angle of 0 degrees.
            S = 90, //South direction with an angle of 90 degrees.
            W = 180, //West direction with an angle of 180 degrees.
            N = 270  //North direction with an angle of 270 degrees.
        }

        /// <summary>
        /// Converts a direction string to its corresponding angle in degrees.
        /// </summary>
        /// <param name="dir">The direction string to be converted (e.g., "n", "e", "s", "w").</param>
        /// <returns>
        /// The corresponding angle in degrees for valid directions, 
        /// or -1 for invalid or unrecognized directions.
        /// </returns>

        public static int DirToDeg(string dir)
        {
            // Try to parse the input string to a Direction enum
            if (Enum.TryParse(dir.ToUpper(), out Direction direction))
            {
                return (int)direction;
            }
            else
            {
                return -1; // Return -1 for invalid direction
            }
        }

        /// <summary>
        /// Converts a list of path coordinates into a direction string representation.
        /// </summary>
        /// <param name="path">A list of coordinates representing the path.</param>
        /// <returns>A string representation of the path where "E" denotes east, "W" denotes west, "N" denotes north, and "S" denotes south.</returns>
        public static string PathToDirectionString(List<(int, int)> path)
        {
            StringBuilder pathStr = new StringBuilder();

            // Iterate over the path and determine the direction for each step
            for (int i = 0; i < path.Count - 1; i++)
            {
                int dx = path[i + 1].Item1 - path[i].Item1;
                int dy = path[i + 1].Item2 - path[i].Item2;

                // Convert the change in coordinates to its corresponding direction string
                if (dx == 1) pathStr.Append("E");
                else if (dx == -1) pathStr.Append("W");
                else if (dy == -1) pathStr.Append("N");
                else if (dy == 1) pathStr.Append("S");
            }
            return pathStr.ToString();
        }
    }
}