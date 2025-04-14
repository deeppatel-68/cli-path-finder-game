using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.Obstacles
{
    public class Camera : Obstacle
    {

        /// <summary>
        /// Gets or sets the direction in which the camera is facing, represented in degrees.
        /// 0 - East, 90 - South, 180 - West, 270 - North.
        /// </summary>
        public int Direction { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class with specified position and direction.
        /// </summary>
        /// <param name="x">The X-coordinate of the camera.</param>
        /// <param name="y">The Y-coordinate of the camera.</param>
        /// <param name="direction">The direction in which the camera is facing.</param>
        public Camera(int x, int y, int direction) : base(x, y)
        {
            Direction = direction;
            // Setting the symbol for the camera
            this.Symbol = 'c';
        }


        /// <summary>
        /// Determines whether the specified position is detected/blocked by the camera's line of sight.
        /// </summary>
        /// <param name="x">The X-coordinate of the position to check.</param>
        /// <param name="y">The Y-coordinate of the position to check.</param>
        /// <returns><c>true</c> if the position is blocked by the camera; otherwise, <c>false</c>.</returns>
        public override bool IsPositionBlocked(int x, int y)
        {
            // If the position is exactly where the camera is
            if (x == this.X && y == this.Y) return true;

            // Calculate the differences in the X and Y coordinates
            int dx = x - this.X;
            int dy = y - this.Y;

            // Calculate the angle of the point relative to the camera's position
            double angle = Math.Atan2(dy, dx) * 180.0 / Math.PI;

            // Normalize negative angles
            if (angle < 0)
            {
                angle += 360.0;
            }

            // Calculate the difference in angles between the point and the camera's direction
            double angleDifference = Math.Abs(angle - this.Direction);

            // Ensure that the angleDifference is within 0 - 360 degrees
            if (angleDifference >= 180)
            {
                angleDifference = 360 - angleDifference;
            }

            // Check if the point is in the cone
            if (angleDifference <= 45.0)
            {
                return true;
            }

            return false;
        }


    }
}
