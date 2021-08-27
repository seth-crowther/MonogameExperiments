using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Resonant
{
    public class BulletManager
    {
        public List<Bullet> bullets;
        private const int speed = 500;
        private const float gunOffset = 88;
        private Texture2D bulletSprite;
        
        public bool shot { get; set; }

        public BulletManager()
        {
            bullets = new List<Bullet>();
            shot = false;
        }

        public void LoadContent(ContentManager content)
        {
            bulletSprite = content.Load<Texture2D>(@"Sprites\Bullet");
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Update(gameTime))
                {
                    i -= 1;
                }
                else
                {
                    Vector2 change = bullets[i].Direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    bullets[i].Position += change;
                }
            }
        }

        //Instantiates a new bullet and shoots
        public void ShootBullet(Entity p)
        {
            TimeSpan time = DateTime.Now - p.LastShootTime;

            //Checking for firerate
            if (time.TotalMilliseconds > 250 && !p.Reloading)
            {
                shot = true;

                Vector2 mousePos = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
                var bSize = new Vector2(8, 8);

                //Getting direction of the mousepointer
                var dir = mousePos - Globals.ScreenDims / 2;
                dir.Normalize();

                //Setting the position of the bullet outside of the hitbox of the player
                var bPos = p.Position + (p.Dims / 2) + (dir * gunOffset);

                Bullet b = new Bullet(bPos, bSize, this);
                b.Direction = dir;
                b.Initialize(bulletSprite);
                p.Magazine--;
                p.LastShootTime = DateTime.Now;
            }
            else
            {
                shot = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws all bullets
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(spriteBatch);
            }
        }

        public void AddBullet(Bullet b)
        {
            bullets.Add(b);
        }

        public void DeleteBullet(Bullet b)
        {
            bullets.Remove(b);
        }
    }
}
