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
        private Character Player;
        private MusicManager MusicManager;
        private string bulletsLeft;
        private Text bulletsInMag, beatCounter, comboCounter;

        public HUD(Character p, MusicManager mm)
        {
            Player = p;
            MusicManager = mm;
            bulletsLeft = Player.Magazine.ToString();

            bulletsInMag = new Text(bulletsLeft, Color.White);
        }
        public void Initialize()
        {
            bulletsInMag.Initialize();
            Vector2 offset = new Vector2(Globals.ScreenDims.X * 0.05f, Globals.ScreenDims.Y * 0.05f) * -1f;
            bulletsInMag.Align(new Rectangle(new Point(0, 0), new Point((int)Globals.ScreenDims.X, (int)Globals.ScreenDims.Y)), offset, Text.Alignment.BottomRight);
            //beatCounter.Initialize();
            //comboCounter.Initialize();
        }
        public void Update()
        {
            if (Player.Magazine.ToString() != bulletsLeft)
            {
                bulletsInMag.UpdateText(bulletsLeft);
            }
            bulletsLeft = Player.Magazine.ToString();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Magazine counter
            bulletsInMag.Draw(spriteBatch);
            spriteBatch.DrawString(Globals.arial, "Beat: " + MusicManager.Beat, new Vector2(100, 900), Color.White);
            spriteBatch.DrawString(Globals.arial, "Combo: " + MusicManager.Combo, new Vector2(100, 300), Color.White);
        }
    }
}
