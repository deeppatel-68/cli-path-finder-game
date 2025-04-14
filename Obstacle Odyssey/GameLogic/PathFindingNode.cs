using Obstacle_Odyssey.Obstacles;
using StealthQuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

namespace Obstacle_Odyssey.GameLogic
{

    /// <summary>
    /// Represents a node used in the A* pathfinding algorithm, which facilitates finding safe paths within the game scenario.
    /// </summary>
    public class PathfindingNode
    {

        /// <summary>
        /// Gets or sets the X-coordinate of the node.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y-coordinate of the node.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the game scenario associated with the node.
        /// </summary>
        public GameScenario? Scenario { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathfindingNode"/> class and associates it with a game scenario.
        /// </summary>
        /// <param name="scenario">The game scenario in which pathfinding will be performed.</param>
        public PathfindingNode(GameScenario scenario)
        {
            this.Scenario = scenario;
        }

        public int x, y, f, g, h; // Node properties for A* pathfinding
        private PathfindingNode? parent;   // Reference to the previous node in the path


        /// <summary>
        /// Initializes a new instance of the <see cref="PathfindingNode"/> class with X and Y coordinates.
        /// </summary>
        /// <param name="x">The X-coordinate.</param>
        /// <param name="y">The Y-coordinate.</param>
        public PathfindingNode(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.f = 0; // Total cost of node (g + h)
            this.g = 0; // Total cost of node (g + h)
            this.h = 0; // Heuristic: Estimated distance from this node to end node
        }

        /// <summary>
        /// Converts the node to a string representation.
        /// </summary>
        /// <returns>The string representation of the node.</returns>
        public override string ToString()
        {
            return $"x: {x}, y: {y}";
        }



        /// <summary>
        /// Gets the immediate neighboring nodes (North, South, East, and West) for the specified position.
        /// </summary>
        /// <param name="x">The X-coordinate of the current position.</param>
        /// <param name="y">The Y-coordinate of the current position.</param>
        /// <returns>A list of <see cref="PathfindingNode"/> representing the neighboring nodes.</returns>
        private List<PathfindingNode> GetNeighbors(int x, int y)
        {
            // Return nodes representing the four cardinal directions
            var neighbors = new List<PathfindingNode>
            {
                 new PathfindingNode(x + 1, y),  // East
                 new PathfindingNode(x - 1, y),  // West
                 new PathfindingNode(x, y + 1),  // South
                 new PathfindingNode(x, y - 1)   // North
            };

            return neighbors;
        }


        /// <summary>
        /// Calculates the Manhattan heuristic between two points.
        /// </summary>
        /// <param name="x1">The X-coordinate of the first point.</param>
        /// <param name="x2">The X-coordinate of the second point.</param>
        /// <param name="y1">The Y-coordinate of the first point.</param>
        /// <param name="y2">The Y-coordinate of the second point.</param>
        /// <returns>The Manhattan heuristic.</returns>
        public int ManhattanHeuristic(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        /// <summary>
        /// Finds a safe path from a starting position to an ending position within the game scenario using the A* pathfinding algorithm. 
        /// This method avoids paths that are blocked by obstacles. If a path is found, it returns the sequence of positions to reach the destination. 
        /// If no path is found, it returns an empty list.
        /// </summary>
        /// <param name="startX">The X-coordinate of the starting position.</param>
        /// <param name="startY">The Y-coordinate of the starting position.</param>
        /// <param name="endX">The X-coordinate of the ending position.</param>
        /// <param name="endY">The Y-coordinate of the ending position.</param>
        /// <returns>
        /// A list of position tuples representing the sequence of steps in the safe path. 
        /// The first item in the list is the starting position, and the last item is the ending position.
        /// If no path can be found, the list is empty.
        /// </returns>
        /// <remarks>
        /// This method utilizes the A* pathfinding algorithm, The pathfinding takes into account obstacles in the game scenario and avoids them.
        /// </remarks>
        public List<(int, int)> FindSafePath(int startX, int startY, int endX, int endY)
        {
            List<(int, int)> path = new List<(int, int)>();
            List<PathfindingNode> openList = new List<PathfindingNode>();
            List<PathfindingNode> closedList = new List<PathfindingNode>();
            PathfindingNode currentNode = new PathfindingNode(startX, startY);
            currentNode.parent = null;
            openList.Add(currentNode);


            // Ensure the scenario is not null
            if (Scenario == null)
            {
                Console.WriteLine("Scenario is null. Cannot proceed.");
                return new List<(int, int)>();
            }

            // Check if the end position is blocked by any obstacle
            if (Scenario.IsPositionBlocked(endX, endY, out Obstacle? obs))
            {
                Console.WriteLine($"The end position ({endX}, {endY}) is blocked by an obstacle: {obs?.GetType().Name}. Cannot proceed.");
                return new List<(int, int)>();
            }


            try
            {
                // Begin A* pathfinding
                while (openList.Count > 0)
                {
                    // Choose the node with the lowest 'f' value
                    currentNode = openList.Aggregate((min, current) => current.f < min.f ? current : min);
                    openList.Remove(currentNode);
                    closedList.Add(currentNode);

                    // If the current node is the destination, backtrack to get the full path
                    if (currentNode.x == endX && currentNode.y == endY)
                    {
                        while (currentNode.parent != null)
                        {
                            path.Add((currentNode.x, currentNode.y));
                            currentNode = currentNode.parent;
                        }
                        path.Add((currentNode.x, currentNode.y));
                        path.Reverse();
                        return path;
                    }

                    // Iterate over the neighboring nodes
                    List<PathfindingNode> neighbors = GetNeighbors(currentNode.x, currentNode.y);
                    foreach (PathfindingNode neighbor in neighbors)
                    {
                        // Skip nodes that are blocked or have already been evaluated
                        if (
                            Scenario.IsPositionBlocked(neighbor.x, neighbor.y, out _)
                            || closedList.Any(item => item.x == neighbor.x && item.y == neighbor.y))
                        {
                            continue;
                        }

                        // Update scores for A* algorithm
                        neighbor.parent = currentNode;
                        neighbor.g = currentNode.g + 1;
                        neighbor.h = ManhattanHeuristic(neighbor.x, endX, neighbor.y, endY);
                        neighbor.f = neighbor.g + neighbor.h;

                        // If this neighbor node is a better path (lower 'g' score), update the node to have the current node as its parent
                        PathfindingNode found = openList.Find(item => item.x == neighbor.x && item.y == neighbor.y)!;
                        if (found != null)
                        {
                            if (neighbor.g < found.g)
                            {
                                found.parent = currentNode;
                                found.g = neighbor.g;
                                found.f = found.g + found.h;
                            }
                        }

                        else
                        {
                            // If the neighbor node isn't in the open list, add it for evaluation
                            openList.Add(neighbor);
                        }
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine($"Memory limit exceeded during pathfinding: {ex.Message}");
                return new List<(int, int)>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during pathfinding: {ex.Message}");
                return new List<(int, int)>();
            }

            // If we've exhausted all possibilities and haven't returned a path, then no path exists
            return path;  // Return an empty list if no path is found.
        }


    }
}
