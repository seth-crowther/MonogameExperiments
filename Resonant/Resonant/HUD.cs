using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Resonant
{
    public class HUD
    {
        private Player Player;
        private MusicManager MusicManager;
        private Vector2 pos;
        private string bulletsLeft;
        private Vector2 numberSize;

        public HUD(Player p, MusicManager mm)
        {
            Player = p;
            MusicManager = mm;
            pos = new Vector2(Globals.ScreenDims.X - 50, Globals.ScreenDims.Y - 50);
            bulletsLeft = Player.Magazine.ToString();
        }
        public void LoadContent(ContentManager content)
        {
            Globals.arial = content.Load<SpriteFont>("Fonts/Arial");
            numberSize = Globals.arial.MeasureString(bulletsLeft);
        }
        public void Update()
        {
            //MeasureString function is quite expensive so this block means it is only calculated when the text changes.
            if (Player.Magazine.ToString() != bulletsLeft)
            {
                numberSize = Globals.arial.MeasureString(bulletsLeft);
            }
            bulletsLeft = Player.Magazine.ToString();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Magazine counter
            spriteBatch.DrawString(Globals.arial, bulletsLeft, pos - numberSize, Color.White);
            spriteBatch.DrawString(Globals.arial, "Beat: " + MusicManager.Beat, new Vector2(100, 900), Color.White);
            spriteBatch.DrawString(Globals.arial, "Combo: " + MusicManager.Combo, new Vector2(100, 300), Color.White);
        }
    }
}
