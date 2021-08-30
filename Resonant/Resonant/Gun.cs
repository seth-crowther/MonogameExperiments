using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Resonant
{
    public class Gun
    {
        private Character Player;
        private BulletManager BulletManager;
        private Texture2D sprite;
        private Vector2 position;
        private Vector2 rotationOrigin;
        private Vector2 mouse;
        private const float pi = (float)Math.PI;

        public Vector2 AimingAt { get { return mouse; } }
        public float Rotation { get; private set; }
        public Gun(Character p, BulletManager bm)
        {
            Player = p;
            BulletManager = bm;
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(@"Sprites\Gun");
            rotationOrigin = new Vector2(sprite.Width / 3, sprite.Height / 2);
            Rotation = 3 * pi / 2;
        }
        public void Update()
        {
            mouse = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - new Vector2(960, 540);
            position = new Vector2(Player.Position.X, Player.Position.Y + 37) + rotationOrigin;
            Rotation = (float)Math.Atan2(mouse.Y, mouse.X);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                BulletManager.ShootBullet(Player);
            }
            else
            {
                BulletManager.shot = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Player.Direction)
            {
                spriteBatch.Draw(sprite, position, null, Color.White, Rotation, rotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
            else
            {
                spriteBatch.Draw(sprite, position, null, Color.White, Rotation, rotationOrigin, 1.0f, SpriteEffects.FlipVertically, 0.0f);
            }
        }
    }
}
