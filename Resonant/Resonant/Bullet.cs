using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Resonant
{
    public class Bullet : Collider
    {
        private readonly TimeSpan bulletLife = new TimeSpan(0, 0, 2);
        private BulletManager BulletManager;
        private readonly DateTime spawnTime;
        public bool collided;
        private Vector2 direction;

        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Bullet(Vector2 pos, Vector2 dims, BulletManager bm) : base(pos, dims)
        {
            spawnTime = DateTime.Now;

            dimensions = dims;
            position = pos;
            BulletManager = bm;
            Stationary = false;
            scale = 8 * Vector2.One;
            rotation = 0.0f;
            //rotation = baseGame.Gun.Rotation;

            rotationOrigin = Vector2.Zero;
            //rotationOrigin = scale / 2;

            BulletManager.AddBullet(this);
        }

        public void Initialize(Texture2D bulletSprite)
        {
            sprite = new Texture2D(GraphicsDevice, 1, 1);
            sprite.SetData(new[] { Color.Blue });
        }

        public bool Update(GameTime gameTime)
        {
            //Deletes bullet after it reachs its max range
            if ((DateTime.Now - spawnTime).TotalSeconds > bulletLife.TotalSeconds)
            {
                Delete();
                return true;
            }
            return false;
        }

        public override void Delete()
        {
            base.Delete();
            BulletManager.DeleteBullet(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, rotationOrigin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
