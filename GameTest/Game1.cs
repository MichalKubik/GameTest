using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace GameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite magus;

        private ViewportAdapter _viewportAdapter;
        private TiledMap _map;
        private TiledMapRenderer _mapRenderer;
        private Camera2D _camera;

        Vector2 position;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 768;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1024, 768);
            _mapRenderer = new TiledMapRenderer(GraphicsDevice);
            _camera = new Camera2D(_viewportAdapter);

            position = Vector2.Zero;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            magus = new Sprite(Content.Load<Texture2D>("magus_small"), new Vector2(512, 384));

            _map = Content.Load<TiledMap>("level01");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A))
            {
                position.X -= 600 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.D))
            {
                position.X += 600 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.S))
            {
                position.Y += 600 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.W))
            {
                position.Y -= 600 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }

            _camera.LookAt(position);
            _mapRenderer.Update(_map, gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            var viewMatrix = _camera.GetViewMatrix();
            var projectionMatrix = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0f, -1f);
            _mapRenderer.Draw(_map, viewMatrix, projectionMatrix, null);

            magus.draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
