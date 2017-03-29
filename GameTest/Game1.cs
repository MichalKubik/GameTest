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
using MonoGame.Extended.Entities;
using GameTest.Entities;
using GameTest.Components;
using GameTest.Entities.Systems;
using MonoGame.Extended.Entities.Systems;
using GameTest.Entities.Components;

namespace GameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        
        private ViewportAdapter _viewportAdapter;
        private TiledMap _map;
        private TiledMapRenderer _mapRenderer;
        private Camera2D _camera;

        private EntityComponentSystem _entityComponentSystem;
        private EntityFactory _entityFactory;
        private TiledObjectToEntityService _objectToEntityService;

        Vector2 position;

        private static Game1 instance;
        public static Game1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game1();
                }
                return instance;
            }
        }



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

            _entityComponentSystem = new EntityComponentSystem();
            _entityFactory = new EntityFactory(_entityComponentSystem);
            _objectToEntityService = new TiledObjectToEntityService(_entityFactory);

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

            _map = Content.Load<TiledMap>("level01");

            _entityComponentSystem.RegisterSystem(new CollisionSystem());
            _entityComponentSystem.RegisterSystem(new SpriteBatchSystem(GraphicsDevice, _camera));
            _entityComponentSystem.RegisterSystem(new PlayerMovementSystem());

            _objectToEntityService.createEntities(_map.GetLayer<TiledMapObjectLayer>("entities").Objects);
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
            _entityComponentSystem.Update(gameTime);
            _camera.LookAt(_entityComponentSystem.GetEntity("Player").Position);
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

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            var viewMatrix = _camera.GetViewMatrix();
            var projectionMatrix = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0f, -1f);

            
            _mapRenderer.Draw(_map, viewMatrix, projectionMatrix, null);
            _entityComponentSystem.Draw(gameTime);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
