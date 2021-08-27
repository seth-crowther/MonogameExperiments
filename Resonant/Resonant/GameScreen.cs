using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class GameScreen : Screen
    {
        private HUD hud;
        private Camera Camera;

        private readonly BulletManager BulletManager;
        private readonly MusicManager MusicManager;
        private readonly FileLoader FileLoader;
        private readonly CollisionManager CollisionManager;
        private readonly RoomManager RoomManager;
        private readonly EntityManager EntityManager;

        public GameScreen() : base()
        {
            CollisionManager = new CollisionManager();
            Collider.CollisionManager = CollisionManager;

            BulletManager = new BulletManager();
            FileLoader = new FileLoader();

            EntityManager = new EntityManager(BulletManager);
            Entity.EntityManager = EntityManager;

            RoomManager = new RoomManager(EntityManager.Player, FileLoader.RoomLayouts);
            Entity.RoomManager = RoomManager;

            MusicManager = new MusicManager(EntityManager.Player, BulletManager);
        }

        public override void Initialize()
        {
            Camera = new Camera();

            RoomManager.Initialize();
            RoomManager.LoadNearbyRooms();
            EntityManager.Initialize();

            hud = new HUD(EntityManager.Player, MusicManager);
        }

        public override void LoadContent(ContentManager Content)
        {
            EntityManager.LoadContent(Content);
            hud.LoadContent(Content);
            MusicManager.LoadContent(Content);
            BulletManager.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            BulletManager.Update(gameTime);

            RoomManager.Update();
            EntityManager.UpdateEntities(gameTime);
            CollisionManager.CorrectAllCollisions();
            EntityManager.UpdateGuns(gameTime);
            Camera.Follow(EntityManager.Player);

            MusicManager.Update(gameTime);
            hud.Update();


            //NOTE: Camera.Follow must be after player.Update
            //NOTE: CollisionManager.Update must be after player.Update
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Drawing world
            spriteBatch.Begin(transformMatrix: Camera.Transform);
            BulletManager.Draw(spriteBatch);
            EntityManager.Draw(spriteBatch);
            RoomManager.Draw(spriteBatch);
            spriteBatch.End();

            //Drawing HUD
            spriteBatch.Begin();
            hud.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
