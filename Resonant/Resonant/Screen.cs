using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public abstract class Screen
    {
        public bool IsLoaded = false;
        public static ScreenManager ScreenManager;
        public Screen()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent(ContentManager Content)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
