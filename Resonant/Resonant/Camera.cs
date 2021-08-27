using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Resonant
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        //Function to move camera to centre on the player
        public void Follow(Player p)
        {
            var position = Matrix.CreateTranslation(
                -p.Position.X - ((p.Right - p.Left) / 2),
                -p.Position.Y - ((p.Bottom - p.Top) / 2),
                 0);

            var offset = Matrix.CreateTranslation(
                 Globals.ScreenDims.X / 2,
                 Globals.ScreenDims.Y / 2,
                 0);

            Transform = position * offset;
        }
    }
}
