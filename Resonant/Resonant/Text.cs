using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class Text
    {
        public string text { get; private set; }
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

        public Text(string text, Color color)
        {
            this.text = text;
            this.color = color;
        }

        public void Initialize()
        {
            textSize = Globals.arial.MeasureString(text);

            
        }
        public void Align(Rectangle bounds, Vector2 offset, Alignment alignment)
        {
            this.alignment = alignment;
            this.bounds = bounds;
            this.offset = offset;
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
