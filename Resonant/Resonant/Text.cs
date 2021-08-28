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
        private Vector2 offset;
        private Color color;

        public enum Alignment
        {
            Centre,
            Left,
            Right,
            Top,
            Bottom,
            BottomRight
        }

        public Text(string text, Rectangle bounds, Alignment alignment, Vector2 offset, Color color)
        {
            this.text = text;
            this.bounds = bounds;
            this.alignment = alignment;
            this.offset = offset;
            this.color = color;
        }

        public void Initialize()
        {
            textSize = Globals.arial.MeasureString(text);

            switch (alignment)
            {
                case Alignment.Centre:
                    drawPos = new Vector2(
                        bounds.Left + ((bounds.Width - textSize.X) / 2),
                        bounds.Top + ((bounds.Height - textSize.Y) / 2)) + offset;
                    break;

                case Alignment.BottomRight:
                    drawPos = new Vector2(
                        bounds.Right - textSize.X,
                        bounds.Bottom - textSize.Y) + offset;
                    break;
            }
        }

        public void UpdateText(string newText)
        {
            text = newText;
            Initialize();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.arial, text, drawPos, color);
        }
    }
}
