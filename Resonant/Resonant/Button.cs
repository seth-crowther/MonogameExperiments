using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class Button
    {
        private Vector2 dimensions;
        private Text label;
        private readonly Texture2D fill;
        private readonly float rotation;
        private Vector2 rotationOrigin;
        private Vector2 scale;

        private float Right { get { return Position.X + dimensions.X; } }
        private float Left { get { return Position.X; } }
        private float Top { get { return Position.Y; } }
        private float Bottom { get { return Position.Y + dimensions.Y; } }
        public Vector2 Position { get; private set; }

        public Button(Vector2 pos, string text)
        {
            Position = pos;
            label = new Text(text, Color.Black);
            dimensions = Globals.arial.MeasureString(label.text) + new Vector2(50, 50);
            Rectangle bounds = new Rectangle(new Point((int)Position.X, (int)Position.Y), new Point((int)dimensions.X, (int)dimensions.Y));
            label.Align(bounds, Vector2.Zero, Text.Alignment.Centre);

            fill = new Texture2D(Globals.GraphicsDevice, 1, 1);
            fill.SetData(new[] { Color.Bisque });

            rotation = 0.0f;
            rotationOrigin = Vector2.Zero;
            scale = dimensions;
        }

        public void Initialize()
        {
            label.Initialize();
        }

        public bool IsClicked()
        {
            Vector2 mouse = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (mouse.X >= Left && mouse.X <= Right && mouse.Y >= Top && mouse.Y <= Bottom)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fill, Position, null, Color.White, rotation, rotationOrigin, scale, SpriteEffects.None, 0.0f);
            label.Draw(spriteBatch);
        }
    }
}
