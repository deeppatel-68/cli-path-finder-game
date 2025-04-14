using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.GameLogic
{
    /// <summary>
    /// Provides static methods for handling user input and output operations.
    /// </summary>
    public static class UserIOHandler
    {

        /// <summary>
        /// Reads a position from the console in the format "X,Y" and parses it.
        /// </summary>
        /// <param name="x">The parsed X-coordinate of the position.</param>
        /// <param name="y">The parsed Y-coordinate of the position.</param>
        /// <returns><c>true</c> if the position was successfully parsed; otherwise, <c>false</c>.</returns>
        public static bool ReadPosition(out int x, out int y)
        {
            try
            {
                // Read input from the console and split it by the comma
                var positionInput = Console.ReadLine()?.Trim().Split(',');

                // Default values if parsing fails
                x = 0;
                y = 0;

                // Check if the input has exactly two parts
                if (positionInput?.Length != 2)
                {
                    return false;
                }

                // Check if both parts can be parsed as integers
                if (int.TryParse(positionInput[0], out x) && int.TryParse(positionInput[1], out y))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Handle general exceptions (this is broad, but ensures no unexpected crashes)
                UserIOHandler.DisplayMessage($"Error reading position: {ex.Message}");
                x = 0;
                y = 0;
                return false;
            }
        }

        /// <summary>
        /// Displays a message to the console.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Reads a line of input from the console.
        /// </summary>
        /// <returns>The input string if available; otherwise, <c>null</c>.</returns>
        public static string? ReadInput()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts the user for a double value and reads it from the console.
        /// </summary>
        /// <param name="promptMessage">The message displayed to prompt the user.</param>
        /// <returns>The parsed double value or -1 if the input is invalid.</returns>
        public static double ReadDouble(string promptMessage)
        {
            try
            {
                UserIOHandler.DisplayMessage(promptMessage);
                string? input = UserIOHandler.ReadInput();

                if (Double.TryParse(input, out double result))
                {
                    return result;
                }
                else
                {
                    // Display a warning to the user about invalid input
                    UserIOHandler.DisplayMessage("Invalid input. Please enter a valid number.");
                    return -1; // Return -1 as an indicator of invalid input
                }
            }
            catch (Exception ex)
            {
                // Handle general exceptions to ensure no unexpected crashes
                UserIOHandler.DisplayMessage($"Error reading input: {ex.Message}");
                return -1; // Return -1 as a default fallback
            }
        }

        /// <summary>
        /// Prompts the user for a direction and reads it from the console.
        /// </summary>
        /// <param name="promptMessage">The message displayed to prompt the user.</param>
        /// <returns>The input direction string or an empty string if the input is <c>null</c>.</returns>
        public static string? ReadDirection(string promptMessage)
        {
            UserIOHandler.DisplayMessage(promptMessage);
            // Returns an empty string if ReadInput is null
            return UserIOHandler.ReadInput() ?? string.Empty;
        }
    }

}


