using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Resonant
{
    public class RoomManager
    {
        #region Getters and Setters
        public int PlayerX { get; private set; }
        public int PlayerY { get; private set; }
        public Room[,] Rooms { get; private set; }
        public NavGraph MapGraph { get; private set; }
        public List<Platform> LoadedPlatforms { get; private set; }
        public Room CurrentRoom { get { return Rooms[PlayerX, PlayerY]; } } //TODO make this be able to take in an entity and return the room that the entity is in (not just player)
        #endregion Getters and Setters
        #region private attributes
        private readonly Character player;
        private const int mapDimsX = 9;
        private const int mapDimsY = 9;
        public dynamic[] RoomLayouts;
        #endregion private attributes

        public RoomManager(Character p, dynamic[] RoomLayouts)
        {
            player = p;
            Rooms = new Room[mapDimsX + 1,mapDimsY + 1];
            CreateAllRooms();

            LoadedPlatforms = new List<Platform>();
            this.RoomLayouts = RoomLayouts;

            MapGraph = new NavGraph();
        }

        public void CreateAllRooms()
        {
            for (int i = 0; i <= mapDimsX; i++)
            {
                for (int j = 0; j <= mapDimsY; j++)
                {
                    Rooms[i, j] = new Room(i, j, this);
                }
            }
        }

        public void Initialize()
        {
            PlayerX = (int)player.Position.X / (int)(Globals.ScreenDims.X + 100);
            PlayerY = (int)player.Position.Y / (int)(Globals.ScreenDims.Y + 100);
        }

        public void Update()
        {
            PlayerX = (int)player.Position.X / (int)(Globals.ScreenDims.X + 100);
            PlayerY = (int)player.Position.Y / (int)(Globals.ScreenDims.Y + 100);

            LoadNearbyRooms();
            UnloadFarRooms();
        }

        public void LoadNearbyRooms()
        {
            foreach (Room r in Rooms)
            {
                //Load unloaded rooms in a 1 room radius from player
                if (Math.Abs(r.Y - PlayerY) <= 1 && Math.Abs(r.X - PlayerX) <= 1)
                {
                    if (!r.Loaded)
                    {
                        r.LoadPlatforms();
                        MapGraph.Update(r.Platforms);
                    }
                }
            }

            LoadGroundConnectors();
        }

        public void LoadGroundConnectors()
        {
            for (int i = 0; i < mapDimsX; i++)
            {
                if (!Rooms[i, 0].HasConnector && Rooms[i, 0].Loaded && Rooms[i + 1, 0].Loaded)
                {
                    CreateGroundConnector(Rooms[i, 0], Rooms[i + 1, 0]);
                    Rooms[i, 0].HasConnector = true;
                }
            }
        }

        public void UnloadFarRooms()
        {
            //if 2 rooms away, unload
            foreach (Room r in Rooms)
            {
                if (Math.Abs(r.Y - PlayerY) > 1 && Math.Abs(r.X - PlayerX) > 1)
                {
                    r.Unload();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform p in LoadedPlatforms)
            {
                p.Draw(spriteBatch);
            }
        }

        public void CreateGroundConnector(Room roomLeft, Room roomRight)
        {
            Vector2 pos = new Vector2(1920 + (2020 * roomLeft.X), 0);
            Vector2 dims = new Vector2(100, 30);
            GroundConnector connector = new GroundConnector(pos, dims, Platform.Tags.Connector, roomLeft, roomRight);

            connector.InitializeAdjacency();
            MapGraph.Update(new List<Platform>() { connector });
            roomLeft.AddPlatform(connector);

            LoadedPlatforms.Add(connector);
        }

        public bool ComparePlatformList(List<Platform> one, List<Platform> two)
        {
            if (one.Count == two.Count)
            {
                for (int i = 0; i < one.Count; i++)
                {
                    if (!one[i].Equals(two[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
