//------------------------------------------------------------------------------
// File: DVDLogo.cs
// Author: Aidan Harries
// Date: 9/1/23
// Course: CIS 580 Video Game Programming
// Description: Represents a DVD Logo object for Game0 project
//------------------------------------------------------------------------------
using Microsoft.Xna.Framework;

namespace Proj0
{
    /// <summary>
    /// Represents a DVD logo with position, velocity, and color properties.
    /// </summary>
    public class DVDLogo
    {
        /// <summary>
        /// Position of the DVD logo
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Velocity of the DVD logo
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Color of the DVD logo
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the DVDLogo class.
        /// </summary>
        /// <param name="position">The initial position of the DVD logo.</param>
        /// <param name="velocity">The initial velocity of the DVD logo.</param>
        /// <param name="color">The color of the DVD logo.</param>
        public DVDLogo(Vector2 position, Vector2 velocity, Color color)
        {
            Position = position;
            Velocity = velocity;
            Color = color;
        }
    }
}

