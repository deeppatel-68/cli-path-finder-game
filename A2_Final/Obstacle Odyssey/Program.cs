

using Obstacle_Odyssey.GameLogic;
using Obstacle_Odyssey.Obstacles;
using StealthQuest;
using System;
using System.Collections.Generic;

namespace Obstacle_Odyssey
{
    /// <summary>
    /// Represents the main entry point for the StealthQuest game.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to start the game.
        /// </summary>
        static void Main(string[] args)
        {
            var scenario = new GameScenario(); // Initialize a new game scenario
            RunGameLoop(scenario);             // Enter the game's main loop
        }

        /// <summary>
        /// The primary game loop, where the menu is repeatedly presented to the user.
        /// </summary>
        /// <param name="scenario">The game scenario to operate on.</param>
        static void RunGameLoop(GameScenario scenario)
        {
            while (true) // Infinite loop until user exits
            {
                string choice = DisplayMenu();   // Show menu and get user choice
                ProcessMenuChoice(scenario, choice); // Handle the chosen action
            }
        }

        /// <summary>
        /// Displays the main game menu and retrieves the user's selection.
        /// </summary>
        /// <returns>The user's menu choice as a string.</returns>
        static string DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("Select one of the following options:");
                string[] options =
                {
            "g) Add 'Guard'", "f) Add 'Fence'", "s) Add 'Sensor'",
            "c) Add 'Camera'", "l) Add 'Laser Beam'",
            "d) Show safe directions", "m) Display obstacle map",
            "p) Find safe path", "x) Exit"
        };

                foreach (var option in options)
                {
                    Console.WriteLine(option); // Display each menu option
                }

                Console.Write("Enter code: ");

                try
                {
                    string choice = Console.ReadLine()?.ToLower() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(choice))
                    {
                        Console.WriteLine("Please enter a valid option.");
                        continue;
                    }

                    if (!IsValidChoice(choice))
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                        continue;
                    }

                    return choice;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}. Please try again.");
                }
            }
        }

        /// <summary>
        /// Handles the user's selected menu choice.
        /// </summary>
        /// <param name="scenario">Current game scenario.</param>
        /// <param name="choice">User's menu choice.</param>
        static void ProcessMenuChoice(GameScenario scenario, string choice)
        {
            if (IsValidChoice(choice))
            {
                ExecuteChoice(scenario, choice); // Perform the action associated with the choice
            }
        }

        /// <summary>
        /// Validates the user's menu selection.
        /// </summary>
        /// <param name="choice">User's selected option.</param>
        /// <returns>True if choice is valid; otherwise, false.</returns>
        static bool IsValidChoice(string choice)
        {
            return !string.IsNullOrEmpty(choice) && "gfscldmptx".Contains(choice); // Validate against known options
        }

        /// <summary>
        /// Executes the specific action associated with the user's menu choice.
        /// </summary>
        /// <param name="scenario">Current game scenario.</param>
        /// <param name="choice">User's menu choice.</param>
        static void ExecuteChoice(GameScenario scenario, string choice)
        {
            try
            {
                switch (choice)
                {
                    case "g": GameUIHandler.AddGuard(scenario); break;
                    case "f": GameUIHandler.AddFence(scenario); break;
                    case "s": GameUIHandler.AddSensor(scenario); break;
                    case "c": GameUIHandler.AddCamera(scenario); break;
                    case "l": GameUIHandler.AddLaserBeam(scenario); break;
                    case "d": GameUIHandler.ShowSafeDirections(scenario); break;
                    case "m": GameUIHandler.DisplayMap(scenario); break;
                    case "p": GameUIHandler.FindSafePath(scenario); break;
                    case "x": Environment.Exit(0); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing your choice: {ex.Message}");
            }
        }


        /// <summary>
        /// Converts a list of path coordinates into a direction string representation.
        /// </summary>
        /// <param name="path">List of coordinates representing the path.</param>
        /// <returns>Directional representation of the path. Returns an empty string if there's an error in conversion.</returns>
        public static string PathToDirectionString(List<(int, int)> path)
        {
            try
            {
                // Use utility method for conversion
                return GameUtility.PathToDirectionString(path);
            }
            catch (Exception ex)
            {
                // Log the exception if you have a logging mechanism. For now, we'll just print to console.
                Console.WriteLine($"Error converting path to direction string: {ex.Message}");

                // Return an empty string or any other default value indicating a failure in conversion.
                return string.Empty;
            }
        }

    }
}
