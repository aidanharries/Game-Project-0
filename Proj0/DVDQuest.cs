//------------------------------------------------------------------------------
// File: DVDQuest.cs
// Author: Aidan Harries
// Date: 9/1/23
// Course: CIS 580 Video Game Programming
// Description: Main game loop for DVD Quest game.
//------------------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Proj0
{
    /// <summary>
    /// Main game loop for DVD Quest.
    /// </summary>
    public class DVDQuest : Game
    {
        // Class level variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Constants and settings
        private readonly int _spriteWidth = 128;
        private readonly int _spriteHeight = 64;
        private readonly Vector2 _menuSize = new Vector2(384, 480);

        // Textures and graphical elements
        private Texture2D _menuTexture;
        private Vector2 _menuPosition;
        private Texture2D _spriteTexture;
        private List<DVDLogo> _dvdLogos;
        private AnimatedButton _animatedButton;
        private Texture2D _buttonSpriteSheet;

        // Mouse state variables
        private ButtonState _previousMouseState = ButtonState.Released;

        // Random number generator
        private Random _random;

        // Fonts and text
        private SpriteFont _pixelFont;
        private bool _buttonPressed = false;

        // UI elements and their properties
        private Vector2 _startGamePosition;
        private Vector2 _exitGamePosition;
        private Color _startGameColor = Color.White;
        private Color _exitGameColor = Color.White;

        /// <summary>
        /// Constructor initializes graphics and sets root directory.
        /// </summary>
        public DVDQuest()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _random = new Random();
        }

        /// <summary>
        /// Initialize the game state.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Load textures, fonts, and initial game settings.
        /// </summary>
        protected override void LoadContent()
        {
            // Initialize sprite batch
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Menu and inital position
            _menuTexture = Content.Load<Texture2D>("dvd_quest_menu");
            _menuPosition = new Vector2(0, 0);

            // Load DVD logo texture and inital position
            _spriteTexture = Content.Load<Texture2D>("dvd_quest");
            var initialPosX = _random.Next((int)_menuSize.X, GraphicsDevice.Viewport.Width - _spriteWidth);
            var initialPosY = _random.Next(0, GraphicsDevice.Viewport.Height - _spriteHeight);
            _dvdLogos = new List<DVDLogo> { new DVDLogo(new Vector2(initialPosX, initialPosY), new Vector2(3, 2), Color.Blue) };

            // Load the sprite sheet for the animated button and set its position
            _buttonSpriteSheet = Content.Load<Texture2D>("button_animation");
            Vector2 buttonPosition = new Vector2(_menuPosition.X + _menuSize.X - 100, _menuPosition.Y + _menuSize.Y - 90);
            _animatedButton = new AnimatedButton(_buttonSpriteSheet, buttonPosition, 80, 80, 0.1f);

            // Load the sprite font for text rendering
            _pixelFont = Content.Load<SpriteFont>("LanaPixel");

            // Set the positions for the "Start Game" and "Exit Game" text on the menu
            _startGamePosition = new Vector2(_menuPosition.X + 20, _menuPosition.Y + _menuSize.Y - 240);
            _exitGamePosition = new Vector2(_menuPosition.X + 20, _menuPosition.Y + _menuSize.Y - 200);
        }

        /// <summary>
        /// Main game logic update.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected override void Update(GameTime gameTime)
        {
            // Get the current state of the mouse
            var currentMouseState = Mouse.GetState();

            // Update the animated button's state
            _animatedButton.Update(gameTime);

            // Check if the animated button is idle (not pressed)
            if (_animatedButton.IsIdle)
            {
                // Define the clickable rectangle for the animated button
                Rectangle buttonRect = new Rectangle((int)_animatedButton.Position.X, (int)_animatedButton.Position.Y, _animatedButton.FrameWidth, _animatedButton.FrameHeight);

                // Detect if the button is clicked
                if (currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState == ButtonState.Released && buttonRect.Contains(currentMouseState.X, currentMouseState.Y))
                {
                    // Simulate button press
                    _animatedButton.Press();

                    // Mark the button as pressed
                    _buttonPressed = true;

                    // Generate a random angle for initial velocity
                    double angle = _random.NextDouble() * Math.PI * 2;
                    float velocityX = (float)Math.Cos(angle) * 3;
                    float velocityY = (float)Math.Sin(angle) * 3;

                    // Create a new random velocity vector
                    Vector2 randomVelocity = new Vector2(velocityX, velocityY);

                    // Generate random initial position for new DVD logo
                    var posX = _random.Next((int)_menuSize.X, GraphicsDevice.Viewport.Width - _spriteWidth);
                    var posY = _random.Next(0, GraphicsDevice.Viewport.Height - _spriteHeight);

                    // Create a new DVD logo with random attributes
                    var newLogo = new DVDLogo(new Vector2(posX, posY), randomVelocity, new Color(_random.Next(256), _random.Next(256), _random.Next(256)));

                    // Add the new DVD logo to the list of logos
                    _dvdLogos.Add(newLogo);
                }
            }

            // Define clickable rectangles for start and exit game texts
            Rectangle startGameRect = new Rectangle((int)_startGamePosition.X, (int)_startGamePosition.Y, 155, 40);
            Rectangle exitGameRect = new Rectangle((int)_exitGamePosition.X, (int)_exitGamePosition.Y, 205, 40);

            // Check if the mouse is over the start game rectangle
            if (startGameRect.Contains(currentMouseState.X, currentMouseState.Y))
            {
                _startGameColor = Color.Gray;

                // Detect if the start game text is clicked
                if (currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState == ButtonState.Released)
                {
                    // Placeholder for handling start game click
                }
            }
            else
            {
                _startGameColor = Color.White;
            }

            // Check if the mouse is over the exit game rectangle
            if (exitGameRect.Contains(currentMouseState.X, currentMouseState.Y))
            {
                _exitGameColor = Color.Gray;

                // Detect if the exit game text is clicked
                if (currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState == ButtonState.Released)
                {
                    // Exit the game
                    Exit();
                }
            }
            else
            {
                _exitGameColor = Color.White;
            }

            // Allow the game to exit if the escape key is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Update the state of the previous mouse button for future use
            _previousMouseState = currentMouseState.LeftButton;

            // Calculate elapsed time since the last frame
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move each DVD logo based on its velocity and the elapsed time
            foreach (var logo in _dvdLogos)
            {
                logo.Position += logo.Velocity * elapsed * 60;

                // Handle collision with horizontal boundaries
                if (logo.Position.X < _menuSize.X || logo.Position.X + _spriteWidth > GraphicsDevice.Viewport.Width)
                {
                    logo.Velocity = new Vector2(-logo.Velocity.X, logo.Velocity.Y);
                }

                // Handle collision with vertical boundaries
                if (logo.Position.Y < 0 || logo.Position.Y + _spriteHeight > GraphicsDevice.Viewport.Height)
                {
                    logo.Velocity = new Vector2(logo.Velocity.X, -logo.Velocity.Y);
                }
            }

            // Call the base class update method
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the game content.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Set black background
            GraphicsDevice.Clear(Color.Black);

            // Begin a new sprite batch for drawing
            _spriteBatch.Begin();

            // Draw each DVD logo stored in the list
            foreach (var logo in _dvdLogos)
            {
                // Calculate the destination rectangle for the logo sprite
                Rectangle destinationRect = new Rectangle((int)logo.Position.X, (int)logo.Position.Y, _spriteWidth, _spriteHeight);
                // Draw the sprite at the calculated destination rectangle
                _spriteBatch.Draw(_spriteTexture, destinationRect, logo.Color);
            }

            // Draw the menu texture at the specified position
            _spriteBatch.Draw(_menuTexture, _menuPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            // Draw the animated button
            _animatedButton.Draw(_spriteBatch);

            // Draw text based on whether the button has been pressed or not
            if (!_buttonPressed)
            {
                // Text to display when the button has not been pressed
                string text1 = "Do ";
                string text2 = "NOT";
                string text3 = " Press...";

                // Calculate the position for the text
                Vector2 textPosition = new Vector2(_menuPosition.X + 20, _menuPosition.Y + _menuSize.Y - 64);

                // Measure the size of each text segment
                Vector2 text1Size = _pixelFont.MeasureString(text1);
                Vector2 text2Size = _pixelFont.MeasureString(text2);
                Vector2 text3Size = _pixelFont.MeasureString(text3);

                // Draw the text segments
                _spriteBatch.DrawString(_pixelFont, text1, textPosition, Color.White);
                _spriteBatch.DrawString(_pixelFont, text2, textPosition + new Vector2(text1Size.X, 0), Color.Red);
                _spriteBatch.DrawString(_pixelFont, text3, textPosition + new Vector2(text1Size.X + text2Size.X, 0), Color.White);
            }
            else
            {
                // Text to display when the button has been pressed
                string text1 = "Oh great... See?!";
                string text2 = "Look what you did!";

                // Calculate the positions for each text segment
                Vector2 textPosition1 = new Vector2(_menuPosition.X + 20, _menuPosition.Y + _menuSize.Y - 100);
                Vector2 textPosition2 = new Vector2(_menuPosition.X + 20, _menuPosition.Y + _menuSize.Y - 64);

                // Draw the text
                _spriteBatch.DrawString(_pixelFont, text1, textPosition1, Color.White);
                _spriteBatch.DrawString(_pixelFont, text2, textPosition2, Color.White);
            }

            // Draw Start and Exit game options
            _spriteBatch.DrawString(_pixelFont, "Start Game", _startGamePosition, _startGameColor);
            _spriteBatch.DrawString(_pixelFont, "Exit Game [ESC]", _exitGamePosition, _exitGameColor);

            // End the sprite batch
            _spriteBatch.End();

            // Call the base class Draw method
            base.Draw(gameTime);
        }
    }
}