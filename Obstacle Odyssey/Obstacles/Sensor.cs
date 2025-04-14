using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obstacle_Odyssey.Obstacles
{
    /// <summary>
    /// Represents a Sensor in the StealthQuest game.
    /// The Sensor detects entities within its specified range.
    /// </summary>
    public class Sensor : Obstacle
    {
        /// <summary>
        /// Gets or sets the detection range of the Sensor. 
        /// Entities within this range are detected by the Sensor.
        /// </summary>
        public double Range { get; set; }  // Using a double to accommodate floating-point range values.

        /// <summary>
        /// Initializes a new instance of the <see cref="Sensor"/> class with specified coordinates and detection range.
        /// </summary>
        /// <param name="x">The X-coordinate of the Sensor.</param>
        /// <param name="y">The Y-coordinate of the Sensor.</param>
        /// <param name="range">The detection range of the Sensor.</param>
        public Sensor(int x, int y, double range) : base(x, y)
        {
            Range = range;
            this.Symbol = 's';  // 's' represents a Sensor on the game map.
        }

        /// <summary>
        /// Determines whether the specified position is within the detection range of the Sensor.
        /// </summary>
        /// <param name="x">The X-coordinate of the position to check.</param>
        /// <param name="y">The Y-coordinate of the position to check.</param>
        /// <returns><c>true</c> if the position is within the Sensor's detection range; otherwise, <c>false</c>.</returns>
        public override bool IsPositionBlocked(int x, int y)
        {
            // Calculate the Euclidean distance between the Sensor and the specified position.
            double distance = Math.Sqrt(Math.Pow(X - x, 2) + Math.Pow(Y - y, 2));

            // Check if the distance is within the Sensor's detection range.
            return distance <= Range;
        }
    }
}
