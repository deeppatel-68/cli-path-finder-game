
using Obstacle_Odyssey.GameLogic;
using Obstacle_Odyssey.Obstacles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StealthQuest
{
    /// <summary>
    /// Represents a game scenario with various obstacles and provides methods to interact with and navigate through the scenario.
    /// </summary>
    public class GameScenario
    {

        public interface IPathfindingService
        {
            List<(int, int)> FindSafePath(int startX, int startY, int endX, int endY, GameScenario scenario);
        }

        /// <summary>
        /// Gets or sets the list of obstacles in the scenario.
        /// </summary>
        public List<Obstacle> Obstacles { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScenario"/> class with an empty list of obstacles.
        /// </summary>
        public GameScenario()
        {
            Obstacles = new List<Obstacle>();
        }

        /// <summary>
        /// Adds an obstacle to the scenario.
        /// </summary>
        /// <param name="obstacle">The obstacle to add.</param>
        public void AddObstacle(Obstacle obstacle)
        {
            Obstacles.Add(obstacle); // Add the specified obstacle to the list
        }

        /// <summary>
        /// Checks if a given position on the game grid is blocked by any of the obstacles.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        /// <param name="obs">The obstacle that blocks the position, if any.</param>
        /// <returns>True if the position is blocked, otherwise false.</returns>.
        public bool IsPositionBlocked(int x, int y, out Obstacle? obs)
        {
            obs = null; // Initialize obs to null.


            // Loop through all obstacles to check if the position is blocked.
            foreach (var obstacle in Obstacles)
            {
                if (obstacle.IsPositionBlocked(x, y))
                {
                    obs = obstacle;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines the safe directions a player can move from a given position, avoiding obstacles.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        public void ShowSafeDirections(int x, int y)
        {
            List<string> safeDirections = new List<string>();

            // Check each direction(N, E, S, W) to see if it's safe
            if (!IsPositionBlocked(x, y - 1, out Obstacle? o))
                safeDirections.Add("N"); // 'N' for North or Up
            if (!IsPositionBlocked(x + 1, y, out o))
                safeDirections.Add("E"); // 'E' for East or Right
            if (!IsPositionBlocked(x, y + 1, out o))
                safeDirections.Add("S"); // 'S' for South or Down
            if (!IsPositionBlocked(x - 1, y, out o))
                safeDirections.Add("W"); // 'W' for West or Left

            if (safeDirections.Count == 0)
            {
                Console.WriteLine("You cannot safely move in any direction. Abort mission.");
            }
            else
            {
                Console.WriteLine($"You can safely take any of the following directions: {string.Join("", safeDirections)}");
            }
        }

        /// <summary>
        /// Places a segment of fence on the game grid, either vertically or horizontally.
        /// </summary>
        /// <param name="startX">The x-coordinate of the starting position.</param>
        /// <param name="startY">The y-coordinate of the starting position.</param>
        /// <param name="endX">The x-coordinate of the ending position.</param>
        /// <param name="endY">The y-coordinate of the ending position.</param>

        public void PlaceFenceSegment(int startX, int startY, int endX, int endY)
        {
            // Validate that the fence is either vertical or horizontal
            if (startX == endX || startY == endY)
            {
                // Create a single Fence object for the entire segment
                AddObstacle(new Fence(startX, startY, endX, endY));
            }
            else
            {
                Console.WriteLine("Invalid fence coordinates. Fences can only be vertical or horizontal.");
            }
        }



        /// <summary>
        /// Finds a safe path between two positions while avoiding obstacles, by reffering to the pathfinding algorithm. 
        /// </summary>
        /// <param name="startX">The x-coordinate of the starting position.</param>
        /// <param name="startY">The y-coordinate of the starting position.</param>
        /// <param name="endX">The x-coordinate of the ending position.</param>
        /// <param name="endY">The y-coordinate of the ending position.</param>
        /// <returns>A list of coordinates representing the safe path.</returns>
        public List<(int, int)> FindSafePath(int startX, int startY, int endX, int endY)
        {
            // Create a pathfinding instance and find the safe path
            PathfindingNode pathfinding = new PathfindingNode(this);
            return pathfinding.FindSafePath(startX, startY, endX, endY); ;
        }


    }
}
