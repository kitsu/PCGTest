﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using PCGTest.Display;
using System;
using PCGTest.Display.MonoGame;
using PCGTest.Director;

namespace PCGTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MonoGameSetup : Game
    {
        const int VIRTUAL_WIDTH = 800;
        const int VIRTUAL_HEIGHT = 480;
        RenderTarget2D VirtualTarget;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager gameMan;
        ViewManager viewMan;

        public MonoGameSetup()
        {
            graphics = new GraphicsDeviceManager(this);
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
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap;
            // Cap frame rate at 60hz
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f);
            IsFixedTimeStep = true;
            // Set size to largest whole multiple of 800x480
            var screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            var scale = screenWidth / VIRTUAL_WIDTH;
            graphics.PreferredBackBufferWidth = scale * VIRTUAL_WIDTH;
            graphics.PreferredBackBufferHeight = scale * VIRTUAL_HEIGHT;
            graphics.ApplyChanges();
            // Setup render target for drawing at virtual width/height
            VirtualTarget = new RenderTarget2D(GraphicsDevice, VIRTUAL_WIDTH, VIRTUAL_HEIGHT);
            // Adjust touch event handling to scale and enable touch emulation
            TouchPanel.DisplayWidth = VIRTUAL_WIDTH;
            TouchPanel.DisplayHeight = VIRTUAL_HEIGHT;
            TouchPanel.EnableMouseTouchPoint = true;
            // Reposition window
            Window.Position = new Point(0, 0);
            base.Initialize(); // base.Initialize calls LoadContent
            // Create game manager (Could define startup config)
            gameMan = new GameManager(viewMan);
#if !__MOBILE__
            // Show mouse cursor (should only happen when mouse attached...)
            IsMouseVisible = true;
#endif
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            viewMan = new ViewManager(GraphicsDevice, VIRTUAL_WIDTH, VIRTUAL_HEIGHT);
            viewMan.LoadContent(Content);
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
            base.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            viewMan.Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            // Virtual drawing block
            GraphicsDevice.SetRenderTarget(VirtualTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            viewMan.Draw(gameTime.ElapsedGameTime.Milliseconds);
            GraphicsDevice.SetRenderTarget(null);

            // Draw to screen
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.PointClamp);
            spriteBatch.Draw(VirtualTarget, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();
        }
    }
}
