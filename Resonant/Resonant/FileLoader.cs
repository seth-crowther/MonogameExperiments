using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Resonant
{
    public class FileLoader
    {
        public dynamic[] RoomLayouts { get; private set; }
        private readonly int NumRoomLayouts = 2;
        public FileLoader()
        {
            RoomLayouts = new dynamic[NumRoomLayouts];
            LoadRoomLayouts();
        }

        public void LoadRoomLayouts()
        {
            for (int i = 0; i < NumRoomLayouts; i++)
            {
                string platforms = File.ReadAllText(@"C:\Users\sethc\Desktop\Resonant Git Repo\Resonant\Resonant\Content\Levels\Room" + i + ".json");
                dynamic jsonFile = JsonConvert.DeserializeObject(platforms);

                if (RoomLayouts[i] == null)
                {
                    RoomLayouts[i] = jsonFile;
                }
            }
        }
    }
}
