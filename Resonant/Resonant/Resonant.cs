using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Resonant
{
    public class Resonant : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private readonly ScreenManager screenManager;

        public Resonant()
        {
            _graphics = new GraphicsDeviceManager(this);

            screenManager = new ScreenManager();
            Screen.ScreenManager = screenManager;

            //Monogame stuff
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            Globals.GraphicsDevice = GraphicsDevice;
            Globals.ScreenDims = new Vector2(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);
            Globals.arial = Content.Load<SpriteFont>("Fonts/Arial");

            //Setting up fullscreen
            _graphics.PreferredBackBufferWidth = (int)Globals.ScreenDims.X;
            _graphics.PreferredBackBufferHeight = (int)Globals.ScreenDims.Y;
            Window.IsBorderless = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager.LoadContent(Globals.Content);
            screenManager.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            screenManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumPurple);

            screenManager.Draw(spriteBatch);
        }
    }
}
