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
        private Button characterSelect;
        public MainMenu() : base()
        {

        }
        public override void Initialize()
        {
            play = new Button(new Vector2(760, 390), "Let's go");
            play.Initialize();

            characterSelect = new Button(play.Position + new Vector2(0, 200), "Choose your character");
            characterSelect.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            mainmenu = content.Load<Texture2D>(@"Sprites\MainMenu");
        }

        public override void Update(GameTime gameTime)
        {
            if (play.IsClicked())
            {
                ScreenManager.ScreenTransition(1f);
                characterSelect.UpdateLabel("new text");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mainmenu, new Rectangle(new Point(0,0), new Point((int)Globals.ScreenDims.X, (int)Globals.ScreenDims.Y)), Color.White);
            play.Draw(spriteBatch);
            characterSelect.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
