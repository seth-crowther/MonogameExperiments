using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Resonant
{
    public class Enemy : Entity
    {
        private List<Node> PathToPlayer;
        private Node nextNode, currentNode, standingAbove, playerStandingAbove;
        public Enemy(Vector2 pos, Vector2 dims) : base(pos, dims, EntityManager)
        {
            hp = 100;
            scale = dims;
            Stationary = false;

            Position = pos;
            Acceleration = new Vector2(0, Globals.Gravity);

            MoveVel = 180f;
            JumpVel = -1000f;
            Airborne = false;
        }

        public void Initialize()
        {
            sprite = new Texture2D(Globals.GraphicsDevice, 1, 1);
            sprite.SetData(new[] { Color.Green });
        }

        public void Shoot()
        {
            //Shoot a bullet at the player
        }

        public void Update(GameTime gameTime)
        {

            standingAbove = GetClosestNode();
            playerStandingAbove = EntityManager.Player.GetClosestNode();

            if (!Airborne)
            {
                RoomManager.MapGraph.FindPath(standingAbove, playerStandingAbove);
            }

            PathToPlayer = RoomManager.MapGraph.Path;

            if (PathToPlayer.Count > 1)
            {
                nextNode = PathToPlayer[1];
                currentNode = PathToPlayer[0];

                //If the next node in the path is above the current node and the enemy is grounded, jump
                if (nextNode.Position.Y < currentNode.Position.Y && !Airborne)
                {
                    newVelocity.Y = JumpVel;
                }

                HandleHorizontalMovement();
            }
            else
            {
                newVelocity.X = 0;
            }

            Velocity = newVelocity;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            HandleCollisions();

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            newVelocity = new Vector2(0, Velocity.Y);
        }

        public void HandleHorizontalMovement()
        {
            //If next node is right of current node
            if (nextNode.Position.X > currentNode.Position.X)
            {
                newVelocity.X = MoveVel;
            }
            //If next node is left of current node
            else if (nextNode.Position.X < currentNode.Position.X)
            {
                newVelocity.X = -MoveVel;
            }
        }

        public override void Delete()
        {
            base.Delete();
            EntityManager.DeleteEnemy(this);
        }
    }
}
