//------------------------------------------------------------------------------
// File: AnimatedButton.cs
// Author: Aidan Harries
// Date: 9/1/23
// Course: CIS 580 Video Game Programming
// Description: Represents an Animated Button object for Game0 project
//------------------------------------------------------------------------------
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Proj0
{
    /// <summary>
    /// Represents an animated button with a sprite sheet, position, and frame dimensions.
    /// </summary>
    public class AnimatedButton
    {
        /// <summary>
        /// Position of the AnimatedButton
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Width of each frame in the animation
        /// </summary>
        public int FrameWidth { get; private set; }

        /// <summary>
        /// Height of each frame in the animation
        /// </summary>
        public int FrameHeight { get; private set; }

        /// <summary>
        /// Value indicating whether the button is in its idle state
        /// </summary>
        public bool IsIdle => _currentFrame == 0;

        // Fields for internal state management
        private readonly Texture2D _texture;
        private int _currentFrame;
        private int _totalFrames;
        private float _timeSinceLastFrame;
        private float _timePerFrame;
        private bool _isPressed;

        /// <summary>
        /// Initializes a new instance of the AnimatedButton class
        /// </summary>
        /// <param name="texture">Texture used for the animated sprite.</param>
        /// <param name="position">Initial position of the button.</param>
        /// <param name="frameWidth">Width of each frame in the sprite sheet.</param>
        /// <param name="frameHeight">Height of each frame in the sprite sheet.</param>
        /// <param name="timePerFrame">Time each frame is shown for.</param>
        public AnimatedButton(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, float timePerFrame)
        {
            _texture = texture;
            Position = position;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            _currentFrame = 0;
            _totalFrames = texture.Width / frameWidth;
            _timePerFrame = timePerFrame;
        }

        /// <summary>
        /// Updates the animated button based on the game time.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (_isPressed)
            {
                _timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timeSinceLastFrame >= _timePerFrame)
                {
                    _currentFrame++;
                    _timeSinceLastFrame = 0f;

                    if (_currentFrame >= _totalFrames)
                    {
                        _currentFrame = 0;
                        _isPressed = false;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the animated button to the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing sprites.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRect = new Rectangle(_currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            spriteBatch.Draw(_texture, Position, sourceRect, Color.White);
        }

        /// <summary>
        /// Handles the button press action.
        /// </summary>
        public void Press()
        {
            if (!_isPressed)
            {
                _isPressed = true;
                _currentFrame = 1;
                _timeSinceLastFrame = 0f;
            }
        }
    }
}
