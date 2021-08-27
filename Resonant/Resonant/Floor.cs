using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Resonant
{
    public class Floor : Platform
    {

        public Floor(Vector2 pos, Vector2 dims, Resonant g) : base(pos, dims, g)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(baseGame.Player.Left - (baseGame.ScreenDims.X / 2) + (baseGame.Player.Dims.X / 2), Position.Y);
        }
    }
}
