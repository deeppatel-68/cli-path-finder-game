using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.Obstacles
{
    /// <summary>
    /// Represents a laser beam obstacle in the StealthQuest game.
    /// A laser beam can be either horizontal or vertical and blocks positions along its direction.
    /// </summary>
    public class LaserBeam : Obstacle
    {
        /// <summary>
        /// Gets the starting Y-coordinate of the laser beam.
        /// </summary>
        public int StartY { get; set; }
        /// <summary>
        /// Gets the starting X-coordinate of the laser beam.
        /// </summary>
        public int StartX { get; set; }
        /// <summary>
        /// Gets the direction of the laser beam. 'H' for horizontal and 'V' for vertical.
        /// </summary>
        public char Direction { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaserBeam"/> class with specified coordinates and direction.
        /// </summary>
        /// <param name="startX">The starting X-coordinate of the laser beam.</param>
        /// <param name="startY">The starting Y-coordinate of the laser beam.</param>
        /// <param name="laser_direction">The direction of the laser beam. 'H' for horizontal and 'V' for vertical.</param>

        public LaserBeam(int startX, int startY, char laser_direction) : base(startX, startY)
        {
            this.StartX = startX;
            this.StartY = startY;
            this.Direction = laser_direction;
            this.Symbol = 'l';  // Setting the symbol for the laser beam
        }

        /// <summary>
        /// Determines whether the specified position is blocked by the laser beam.
        /// </summary>
        /// <param name="x">The X-coordinate of the position to check.</param>
        /// <param name="y">The Y-coordinate of the position to check.</param>
        /// <returns><c>true</c> if the position is blocked by the laser beam; otherwise, <c>false</c>.</returns>
        public override bool IsPositionBlocked(int x, int y)
        {
            // Assuming laser beam is either vertical or horizontal

            if (Direction == 'H' && y == StartY)
            {
                // Horizontal laser, blocks all x positions on its row
                return true;
            }
            else if (Direction == 'V' && x == StartX)
            {
                // Vertical laser, blocks all y positions on its column
                return true;
            }

            return false;
        }
    }
}
