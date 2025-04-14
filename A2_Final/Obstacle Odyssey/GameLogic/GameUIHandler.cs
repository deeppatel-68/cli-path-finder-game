
using Obstacle_Odyssey.Obstacles;
using StealthQuest;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Obstacle_Odyssey.GameLogic
{
    
    
    /// <summary>
    /// Provides static methods to handle user interface interactions for the game, such as taking user input and adding game elements.
    /// </summary>
    public class GameUIHandler
    {
        public class InvalidPositionException : Exception
        {
            public InvalidPositionException(string message) : base(message) { }
        }

        public class InvalidScenarioException : Exception
        {
            public InvalidScenarioException(string message) : base(message) { }
        }


        /// <summary>
        /// Asks for position information from the user and adds a guard to the specified game scenario at the provided position.
        /// </summary>
        /// <param name="scenario">The game scenario into which the guard is added.</param>
        public static void AddGuard(GameScenario scenario)
        {
            // Get guard's position
            UserIOHandler.DisplayMessage("Enter the guard's location (X,Y): ");

            if (UserIOHandler.ReadPosition(out int guardX, out int guardY))
            {
                try
                {
                    // Add the guard to the scenario
                    scenario.AddObstacle(new Guard(guardX, guardY));
                }
                catch (InvalidPositionException ex)
                {
                    // Display an error message if adding the guard fails due to invalid position
                    UserIOHandler.DisplayMessage($"Error adding guard: {ex.Message}");
                }
            }
            else
            {
                // Display an error message if the input is invalid
                UserIOHandler.DisplayMessage("Invalid input.");
            }
        }

        /// <summary>
        /// Asks for position information from the user for both the start and end of a fence and places it in the specified game scenario.
        /// </summary>
        /// <param name="scenario">The game scenario where the fence segment is placed.</param>
        public static void AddFence(GameScenario scenario)
        {
            // Get starting position of the fence
            UserIOHandler.DisplayMessage("Enter the location where the fence starts (X,Y):");

            if (UserIOHandler.ReadPosition(out int fenceStartX, out int fenceStartY))
            {
                // Get the end position of the fence
                UserIOHandler.DisplayMessage("Enter the location where the fence ends (X,Y):");

                if (UserIOHandler.ReadPosition(out int fenceEndX, out int fenceEndY))
                {
                    try
                    {
                        // Place the fence segment in the scenario
                        scenario.PlaceFenceSegment(fenceStartX, fenceStartY, fenceEndX, fenceEndY);
                    }
                    catch (InvalidPositionException ex)
                    {
                        // Handle any exceptions related to invalid fence positioning
                        UserIOHandler.DisplayMessage($"Error placing fence: {ex.Message}");
                    }
                }
                else
                {
                    // Display an error message if the ending position is invalid
                    UserIOHandler.DisplayMessage("Invalid fence end position.");
                }
            }
            else
            {
                // Display an error message if the starting position is invalid
                UserIOHandler.DisplayMessage("Invalid fence start position.");
            }
        }


        /// <summary>
        /// Asks for position (X and Y) and range (in klicks), and then places a sensor with provided inputs.
        /// </summary>
        /// <param name="scenario">The game scenario into which the sensor is introduced.</param>
        public static void AddSensor(GameScenario scenario)
        {
            // Get sensor position from the user
            UserIOHandler.DisplayMessage("Enter position for Sensor (x,y): ");

            if (UserIOHandler.ReadPosition(out int sensorX, out int sensorY))
            {
                // Get the sensor range from the user
                UserIOHandler.DisplayMessage("Please enter a valid range for the sensor in klicks: ");
                string? sensorRangeInput = UserIOHandler.ReadInput();

                if (sensorRangeInput == null || !Double.TryParse(sensorRangeInput, out double range) || range <= 0)
                {
                    UserIOHandler.DisplayMessage("Invalid Input");
                }
                else
                {
                    try
                    {
                        // Add sensor range to game scenario
                        scenario.AddObstacle(new Sensor(sensorX, sensorY, range));
                    }
                    catch (InvalidPositionException ex)
                    {
                        // Handle any exceptions related to sensor positioning
                        UserIOHandler.DisplayMessage($"Error adding sensor: {ex.Message}");
                    }
                }
            }
            else
            {
                UserIOHandler.DisplayMessage("Invalid Input");
            }
        }

        /// <summary>
        /// Asks the user for a position and a direction, then places a camera in the game scenario based on the given information.
        /// </summary>
        /// <param name="scenario">The game scenario where the camera is set up.</param>
        public static void AddCamera(GameScenario scenario)
        {
            // Get camera position from user
            UserIOHandler.DisplayMessage("Enter position for Camera (x,y): ");

            if (UserIOHandler.ReadPosition(out int cameraX, out int cameraY))
            {
                // Get camera direction
                UserIOHandler.DisplayMessage("Camera facing direction: (n, s, e, w): ");
                string? dir = UserIOHandler.ReadInput();

                if (dir == null)
                {
                    UserIOHandler.DisplayMessage("Direction is null. Cannot proceed.");
                    return;
                }

                // Convert the direction string to its corresponding degree
                int res = GameUtility.DirToDeg(dir);
                if (res == -1)
                {
                    UserIOHandler.DisplayMessage("Invalid Direction");
                }
                else
                {
                    try
                    {
                        // Add camera to the scenario with the specified position and direction
                        scenario.AddObstacle(new Camera(cameraX, cameraY, res));
                    }
                    catch (InvalidPositionException ex)
                    {
                        // Handle any exceptions related to camera positioning
                        UserIOHandler.DisplayMessage($"Error adding camera: {ex.Message}");
                    }
                }
            }
        }


        /// <summary>
        /// Interacts with the user to determine the starting position and direction of a laser beam, then sets it up in the game scenario.
        /// </summary>
        /// <param name="scenario">The game scenario where the laser beam is established.</param>
        public static void AddLaserBeam(GameScenario scenario)
        {
            // Ask user for starting position of the laser beam
            UserIOHandler.DisplayMessage("Enter the starting location of the laser beam (X,Y):");

            if (UserIOHandler.ReadPosition(out int startX, out int startY))
            {
                // Ask user for the direction of the laser beam
                UserIOHandler.DisplayMessage("Please enter direction for laser (H for Horizontal or V for Vertical):");

                var laserdirectioninput = UserIOHandler.ReadInput()?.ToUpper();

                if (laserdirectioninput != "H" && laserdirectioninput != "V")
                {
                    UserIOHandler.DisplayMessage("Invalid Laser Direction");
                    return;
                }

                char laser_direction = laserdirectioninput[0];

                try
                {
                    // Add the laser beam to the scenario with the specified position and direction
                    scenario.AddObstacle(new LaserBeam(startX, startY, laser_direction));
                }
                catch (InvalidPositionException ex)
                {
                    // Handle any exceptions related to laser beam positioning
                    UserIOHandler.DisplayMessage($"Error adding laser beam: {ex.Message}");
                }
            }
            else
            {
                UserIOHandler.DisplayMessage("Invalid laser beam start position.");
            }
        }



        /// <summary>
        /// Asks the user for their current location in the game scenario, then provides safe directions from that point.
        /// </summary>
        /// <param name="scenario">The game scenario used to calculate and display safe directions.</param>
        public static void ShowSafeDirections(GameScenario scenario)
        {

            // Ask user for their current position
            UserIOHandler.DisplayMessage("Enter your current location (X,Y):");
            // If successfully read the position
            // If successfully read the position
            if (UserIOHandler.ReadPosition(out int x, out int y))
            {
                try
                {
                    // Display safe directions based on user's current position
                    scenario.ShowSafeDirections(x, y);
                }
                catch (InvalidScenarioException ex)
                {
                    UserIOHandler.DisplayMessage($"Error showing safe directions: {ex.Message}");
                }
            }
            else
            {
                // Inform user if the input is invalid
                UserIOHandler.DisplayMessage("Invalid input.");
            }
        }

        /// <summary>
        /// Asks the user for the corners of a map section and then visually represents obstacles in that area of the game scenario.
        /// </summary>
        /// <param name="scenario">The game scenario from which the obstacle data is sourced.</param>
        public static void DisplayMap(GameScenario scenario)
        {
            // Ask user for the top-left corner of the map section to display
            UserIOHandler.DisplayMessage("Enter the location of the top-left cell of the map (X,Y): ");

            // If successfully read the top-left corner
            if (UserIOHandler.ReadPosition(out int startX, out int startY))
            {
                // Ask user for the bottom-right corner of the map section
                UserIOHandler.DisplayMessage("Enter the location of the bottom-right cell of the map (X,Y): ");
                // If successfully read the bottom-right corner
                if (UserIOHandler.ReadPosition(out int endX, out int endY))
                {
                    // Display the map section with obstacles
                    for (int y = startY; y <= endY; y++)
                    {
                        for (int x = startX; x <= endX; x++)
                        {
                            // Display obstacle symbol if position is blocked, otherwise show '.'
                            if (scenario.IsPositionBlocked(x, y, out Obstacle? obs))
                                Console.Write(obs?.Symbol);
                            else
                                Console.Write(".");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Asks the user to identify their current position and a target destination, subsequently determining and showcasing a safe route within the game scenario.
        /// </summary>
        /// <param name="scenario">The game scenario in which the safe path is calculated and presented.</param>
        public static void FindSafePath(GameScenario scenario)
        {
            // Prompt user for their current position
            UserIOHandler.DisplayMessage("Enter your current location (X,Y):");

            if (!UserIOHandler.ReadPosition(out int curX, out int curY))
            {
                UserIOHandler.DisplayMessage("Invalid input for current location.");
                return;
            }

            // Prompt user for their objective's position
            UserIOHandler.DisplayMessage("Enter the location of your objective (X,Y):");

            if (!UserIOHandler.ReadPosition(out int objX, out int objY))
            {
                UserIOHandler.DisplayMessage("Invalid input for objective location.");
                return;
            }
            // Calculate the safe path between user's position and the objective
            try
            {
                List<(int, int)> path = scenario.FindSafePath(curX, curY, objX, objY);

                // Display the safe path directions to the user
                UserIOHandler.DisplayMessage("The following path will take you to the objective:");
                UserIOHandler.DisplayMessage(GameUtility.PathToDirectionString(path));
            }
            catch (InvalidScenarioException ex)
            {
                UserIOHandler.DisplayMessage($"Error finding a safe path: {ex.Message}");
            }
        }


    }
}

