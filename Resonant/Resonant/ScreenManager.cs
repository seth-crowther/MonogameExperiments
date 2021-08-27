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
        public Screen TopScreen { get { if (ScreenStack.Count > 0) { return ScreenStack.Peek(); } else { return null; } } }
        public ScreenManager()
        {
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
            TopScreen.Initialize();
        }

        public void LoadContent(ContentManager content)
        {
            TopScreen.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            TopScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TopScreen.Draw(spriteBatch);
        }
    }
}
