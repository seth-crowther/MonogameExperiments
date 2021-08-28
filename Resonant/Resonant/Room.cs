using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class Room
    {
        #region Getters and Setters
        public List<Platform> Platforms { get; private set; }
        public Vector2 Size { get; private set; }
        public bool Loaded { get; set; }
        public bool HasConnector { get; set; }
        public int RoomIndex { get; private set; }
        #endregion Getters and Setters
        #region private attributes

        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly Random rnd;
        private readonly RoomManager RoomManager;
        #endregion private attributes
        public Room(int x, int y, RoomManager roomManager)
        {
            Size = new Vector2(2020, 1080);
            Platforms = new List<Platform>();

            X = x;
            Y = y;
            RoomManager = roomManager;

            Loaded = false;
            rnd = new Random();
            RoomIndex = rnd.Next(0, 2);
        }

        public void AddPlatform(Platform p)
        {
            Platforms.Add(p);
        }

        public void LoadPlatforms()
        {
            if (!Loaded)
            {
                dynamic roomLayout = RoomManager.RoomLayouts[RoomIndex];
                if (Y == 0)
                {
                    Platform floor = new Platform(new Vector2(X * (Globals.ScreenDims.X + 100), 0), new Vector2(Globals.ScreenDims.X, Globals.ScreenDims.Y / 2), Platform.Tags.Floor);
                    AddPlatform(floor);
                    RoomManager.LoadedPlatforms.Add(floor);
                }
                for (int i = 0; i < roomLayout["Platforms"].Count; i++)
                {
                    Vector2 pos = new Vector2((int)roomLayout["Platforms"][i]["position"]["x"] + (X * (Globals.ScreenDims.X + 100)), (int)roomLayout["Platforms"][i]["position"]["y"] - (Y * Globals.ScreenDims.Y));
                    Vector2 dims = new Vector2((int)roomLayout["Platforms"][i]["size"]["x"], (int)roomLayout["Platforms"][i]["size"]["y"]);
                    Platform.Tags tag = roomLayout["Platforms"][i]["tag"];

                    Platform p = new Platform(pos, dims, tag);
                    AddPlatform(p);
                    RoomManager.LoadedPlatforms.Add(p);
                }
                InitializeAdjacency();
                Loaded = true;
            }
        }

        public void InitializeAdjacency()
        {
            for (int i = 0; i < Platforms.Count; i++)
            {
                //Hard coding adjacency between different platforms
                if (Y == 0)
                {
                    if (Platforms[i].Tag == Platform.Tags.Floor)
                    {
                        //Left platform
                        Platforms[i].SurfaceNodes[0].AddAdjNode(Platforms[i + 1].SurfaceNodes[0]);
                        Platforms[i].SurfaceNodes[64].AddAdjNode(Platforms[i + 1].SurfaceNodes[^1]);

                        //Right platform
                        Platforms[i].SurfaceNodes[^1].AddAdjNode(Platforms[i + 3].SurfaceNodes[^1]);
                        Platforms[i].SurfaceNodes[128].AddAdjNode(Platforms[i + 3].SurfaceNodes[0]);

                    }
                }
            }
        }

        public bool Contains(Platform.Tags tag)
        {
            foreach (Platform p in Platforms)
            {
                if (p.Tag == tag)
                {
                    return true;
                }
            }
            return false;
        }

        public void Unload() //Doesn't remove room from memory - should do this eventually
        {
            if (Loaded)
            {
                //Removing room's platforms from loadedplatforms
                foreach (Platform p in Platforms)
                {
                    foreach (Node n in p.SurfaceNodes)
                    {
                        //Removing room's nodes from the NavGraph
                        RoomManager.MapGraph.Nodes.Remove(n);
                    }
                    RoomManager.LoadedPlatforms.Remove(p);
                }
                Loaded = false;
            }
        }
    }
}
