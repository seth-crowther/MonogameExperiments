using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class MainMenu : Screen
    {
        private Texture2D mainmenu;
        private Button play;
        public MainMenu() : base()
        {

        }
        public override void Initialize()
        {
            play = new Button(new Vector2(760, 390), new Vector2(400, 300), "play gaem");
        }

        public override void LoadContent(ContentManager content)
        {
            mainmenu = content.Load<Texture2D>(@"Sprites\MainMenu");
        }

        public override void Update(GameTime gameTime)
        {
            if (play.IsClicked())
            {
                ScreenManager.PopScreen();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mainmenu, new Rectangle(new Point(0,0), new Point((int)Globals.ScreenDims.X, (int)Globals.ScreenDims.Y)), Color.White);
            play.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
