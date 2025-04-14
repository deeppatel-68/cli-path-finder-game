using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.Obstacles
{
    /// <summary>
    /// Represents a guard obstacle in the StealthQuest game. 
    /// A guard is a stationary obstacle that blocks the position it is placed at.
    /// </summary>
    public class Guard : Obstacle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Guard"/> class with specified coordinates.
        /// </summary>
        /// <param name="x">The X-coordinate of the guard's position.</param>
        /// <param name="y">The Y-coordinate of the guard's position.</param>
        public Guard(int x, int y) : base(x, y)
        {
            // Setting the symbol for the guard
            this.Symbol = 'g';
        }

        /// <summary>
        /// Determines whether the specified position is blocked by the guard.
        /// </summary>
        /// <param name="x">The X-coordinate of the position to check.</param>
        /// <param name="y">The Y-coordinate of the position to check.</param>
        /// <returns><c>true</c> if the position is blocked by the guard; otherwise, <c>false</c>.</returns>
        public override bool IsPositionBlocked(int x, int y)
        {
            // Guard blocks the exact position it is placed at.
            return X == x && Y == y;
        }
    }
}
