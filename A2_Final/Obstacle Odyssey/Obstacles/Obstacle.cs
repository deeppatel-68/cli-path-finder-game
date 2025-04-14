using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.Obstacles
{
    /// <summary>
    /// Represents an abstract obstacle in the StealthQuest game.
    /// This is a base class for all game obstacles, providing a common interface and properties.
    /// </summary>
    public abstract class Obstacle
    {
        /// <summary>
        /// Gets or sets the X-coordinate of the obstacle's position.
        /// </summary>
        public int X { get; protected set; }

        /// <summary>
        /// Gets or sets the Y-coordinate of the obstacle's position.
        /// </summary>
        public int Y { get; protected set; }

        /// <summary>
        /// Gets or sets the symbol representing the obstacle on the game map.
        /// </summary>
        public char Symbol { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/> class with specified coordinates.
        /// </summary>
        /// <param name="x">The X-coordinate of the obstacle.</param>
        /// <param name="y">The Y-coordinate of the obstacle.</param>
        public Obstacle(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Determines whether the specified position is blocked by the obstacle.
        /// </summary>
        /// <param name="x">The X-coordinate of the position to check.</param>
        /// <param name="y">The Y-coordinate of the position to check.</param>
        /// <returns><c>true</c> if the position is blocked by the obstacle; otherwise, <c>false</c>.</returns>
        public abstract bool IsPositionBlocked(int x, int y);
    }
}
