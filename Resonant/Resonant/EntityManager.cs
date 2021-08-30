using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class EntityManager
    {
        public Character Player { get; private set; }
        private readonly List<Enemy> Enemies;
        public EntityManager(BulletManager bm)
        {
            Enemies = new List<Enemy>();

            Vector2 pSize = new Vector2(60, 120);
            Vector2 pPos = new Vector2(500 + 7680, -120);
            Player = new Character(pPos, pSize, bm);

            var eSize = new Vector2(60, 120);
            var ePos = new Vector2(800 + 7680, -120);
            Enemies.Add(new Enemy(ePos, eSize));
        }

        public void Initialize()
        {
            //Initialising all enemies
            foreach (Enemy e in Enemies)
            {
                e.Initialize();
            }
        }

        public void UpdateEntities(GameTime gameTime)
        {
            Player.Update(gameTime);
            foreach (Enemy e in Enemies)
            {
                e.Update(gameTime);
            }
        }

        public void UpdateGuns(GameTime gameTime)
        {
            Player.Gun.Update();
        }

        public void LoadContent(ContentManager content)
        {
            Player.LoadContent(content);
            //foreach (Enemy e in Enemies)
            //{
            //    e.LoadContent(content);
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            foreach (Enemy e in Enemies)
            {
                e.Draw(spriteBatch);
            }
        }

        public void DeleteEnemy(Enemy e)
        {
            Enemies.Remove(e);
        }
    }
}
