using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Resonant
{
    public class Character : Entity
    {
        public bool Direction { get; private set; }
        public Gun Gun { get; private set; }

        public Character(Vector2 pos, Vector2 dims, BulletManager bm) : base(pos, dims, EntityManager)
        {
            hp = 1000;
            maxMag = 500;
            Magazine = maxMag;
            Reloading = false;

            JumpVel = -1000f;
            MoveVel = 200f;

            Position = pos;
            Velocity = Vector2.Zero;
            Stationary = false;
            scale = Vector2.One;
            rotation = 0;
            rotationOrigin = Vector2.Zero;
            LastShootTime = DateTime.Now;
            Direction = false;

            Gun = new Gun(this, bm);
        }
        public void Update(GameTime gameTime)
        {
            HandleReload();

            //Orienting player depending on where gun is aimed
            if (Gun.AimingAt.X > 0)
            {
                Direction = true;
            }
            else
            {
                Direction = false;
            }

            //Moves players left and right
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                newVelocity.X = MoveVel;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                newVelocity.X = -MoveVel;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && !Airborne)
            {
                newVelocity.Y = JumpVel;
            }

            Velocity = newVelocity;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Resets vertical velocity if you bang your head on a ceiling or are on the floor
            HandleCollisions();

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            newVelocity = new Vector2(0, Velocity.Y);
        }

        public void HandleReload()
        {
            if (Magazine <= 0 && !Reloading)
            {
                reload = DateTime.Now;
                Reloading = true;
            }

            TimeSpan reloadTime = DateTime.Now - reload;

            if (reloadTime.TotalSeconds >= 2 && Reloading)
            {
                Magazine = maxMag;
                Reloading = false;
                LastShootTime = DateTime.Now;
            }

            //Reloading
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                reload = DateTime.Now;
                Reloading = true;
            }
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(@"Sprites\SargeRight");
            Gun.LoadContent(content);
        }

        public void UnloadContent()
        {
            sprite.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!(hp <= 0))
            {
                if (Direction)
                {
                    spriteBatch.Draw(sprite, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(sprite, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0);
                }
            }
            else
            {
                CollisionManager.DeleteCollider(this);
            }
            Gun.Draw(spriteBatch);
        }
    }
}
