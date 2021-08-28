using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class Text
    {
        private string text;
        private Rectangle bounds;
        private Alignment alignment;
        private Vector2 drawPos;
        private Vector2 textSize;

        public enum Alignment
        {
            Centre,
            Left,
            Right,
            Top,
            Bottom
        }

        public Text(string t, Rectangle b, Alignment a)
        {
            text = t;
            bounds = b;
            alignment = a;
        }

        public void Initialize()
        {
            textSize = Globals.arial.MeasureString(text);

            switch (alignment)
            {
                case Alignment.Centre:
                    drawPos = new Vector2(
                        bounds.Left + ((bounds.Width - textSize.X) / 2),
                        bounds.Top + ((bounds.Height - textSize.Y) / 2));
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.arial, text, drawPos, Color.Black);
        }
    }
}
