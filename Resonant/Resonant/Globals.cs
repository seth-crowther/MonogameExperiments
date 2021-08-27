using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public static class Globals
    {
        public const float Gravity = 1500f;
        public static Vector2 ScreenDims;
        public static GraphicsDevice GraphicsDevice;
        public static ContentManager Content;
        public static SpriteFont arial;
        public static float epsilon = 0.001f;
    }
}
