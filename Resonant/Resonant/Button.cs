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
        private Vector2 position;
        private Vector2 dimensions;
        private readonly string label;

        private readonly Texture2D fill;
        private readonly float rotation;
        private Vector2 rotationOrigin;
        private Vector2 scale;

        private float Right { get { return position.X + dimensions.X; } }
        private float Left { get { return position.X; } }
        private float Top { get { return position.Y; } }
        private float Bottom { get { return position.Y + dimensions.Y; } }

        public Button(Vector2 pos, Vector2 dims, string text)
        {
            position = pos;
            dimensions = dims;
            label = text;
            fill = new Texture2D(Globals.GraphicsDevice, 1, 1);
            fill.SetData(new[] { Color.CornflowerBlue });

            rotation = 0.0f;
            rotationOrigin = Vector2.Zero;
            scale = dims;
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
            spriteBatch.Draw(fill, position, null, Color.White, rotation, rotationOrigin, scale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(Globals.arial, label, position, Color.White); //Fonts should be globally accessible
        }
    }
}
