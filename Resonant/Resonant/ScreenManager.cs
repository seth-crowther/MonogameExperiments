using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class ScreenManager
    {
        private readonly Stack<Screen> ScreenStack;
        private Texture2D black;
        private bool transitionIsActive;
        private bool fadingOut;
        private float alpha;
        private float fadeSpeed;
        public Screen TopScreen { get { if (ScreenStack.Count > 0) { return ScreenStack.Peek(); } else { return null; } } }
        public ScreenManager()
        {
            alpha = 0.0f;
            transitionIsActive = false;
            fadingOut = true;
            ScreenStack = new Stack<Screen>();
            PushScreen(new GameScreen());
            PushScreen(new MainMenu());
        }

        public void PushScreen(Screen screen)
        {
            ScreenStack.Push(screen);
        }

        public void PopScreen()
        {
            ScreenStack.Pop();
            if (!TopScreen.IsLoaded)
            {
                TopScreen.Initialize();
                TopScreen.LoadContent(Globals.Content);
                TopScreen.IsLoaded = true;
            }
        }

        public void Initialize()
        {
            black = new Texture2D(Globals.GraphicsDevice, 1, 1);
            black.SetData(new[] { Color.Black });
            TopScreen.Initialize();
        }

        public void LoadContent(ContentManager content)
        {
            TopScreen.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            TopScreen.Update(gameTime);
            UpdateTransition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TopScreen.Draw(spriteBatch);
            if (transitionIsActive)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(black, new Rectangle(new Point(0, 0), new Point((int)Globals.ScreenDims.X, (int)Globals.ScreenDims.Y)), Color.White * alpha);
                spriteBatch.End();
            }
        }

        public void ScreenTransition(float speed)
        {
            transitionIsActive = true;
            fadeSpeed = speed;
        }

        public void UpdateTransition(GameTime gameTime)
        {
            if (transitionIsActive)
            {
                if (fadingOut)
                {
                    alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                alpha = Math.Clamp(alpha, 0.0f, 1.0f);

                if (alpha == 1.0f)
                {
                    PopScreen();
                    fadingOut = false;
                }
                else if (alpha == 0.0f)
                {
                    transitionIsActive = false;
                }
            }
        }
    }
}
