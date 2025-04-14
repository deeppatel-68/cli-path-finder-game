using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.Obstacles
{
    /// <summary>
    /// Represents a fence obstacle in the StealthQuest game. 
    /// Fences are linear obstacles that can be positioned either vertically or horizontally.
    /// </summary>
    public class Fence : Obstacle
    {
        /// <summary>
        /// Gets the X-coordinate of the fence's end point.
        /// </summary>
        public int EndX { get; private set; }
        /// <summary>
        /// Gets the Y-coordinate of the fence's end point.
        /// </summary>
        public int EndY { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fence"/> class with specified start and end coordinates.
        /// </summary>
        /// <param name="startX">The X-coordinate of the fence's starting point.</param>
        /// <param name="startY">The Y-coordinate of the fence's starting point.</param>
        /// <param name="endX">The X-coordinate of the fence's end point.</param>
        /// <param name="endY">The Y-coordinate of the fence's end point.</param>
        public Fence(int startX, int startY, int endX, int endY) : base(startX, startY)
        {
            this.EndX = endX;
            this.EndY = endY;
            this.Symbol = 'f'; // Setting the symbol for the fence
        }

        /// <summary>
        /// Determines whether the specified position is blocked by the fence.
        /// </summary>
        /// <param name="x">The X-coordinate of the position to check.</param>
        /// <param name="y">The Y-coordinate of the position to check.</param>
        /// <returns><c>true</c> if the position is blocked by the fence; otherwise, <c>false</c>.</returns>
        public override bool IsPositionBlocked(int x, int y)
        {
            // Check if the fence is vertical
            if (X == EndX)
            {
                // Return true if the X-coordinate matches and the Y-coordinate lies between the start and end points.
                return x == X && y >= Math.Min(Y, EndY) && y <= Math.Max(Y, EndY);
            }
            // Check if the fence is horizontal
            else if (Y == EndY)
            {
                // Return true if the Y-coordinate matches and the X-coordinate lies between the start and end points.
                return y == Y && x >= Math.Min(X, EndX) && x <= Math.Max(X, EndX);
            }
            // Return false if the fence is neither vertical nor horizontal
            return false;
        }
    }
}
