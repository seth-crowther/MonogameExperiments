using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Resonant
{
    public class MusicManager
    {
        private Song well_rested;
        private Player Player;
        private BulletManager BulletManager;

        private const float tolerance = 0.08f;
        private float period;

        private TimeSpan timePassed;
        private DateTime startTime;
        private DateTime onBeat;

        public int Combo { get; protected set; }
        public int Beat { get; protected set; }

        public MusicManager(Player p, BulletManager bm)
        {
            Player = p;
            BulletManager = bm;
            Combo = 0;
            period = 60f / 112f;
        }

        public void LoadContent(ContentManager content)
        {
            well_rested = content.Load<Song>("Music/WellRested");
            //MediaPlayer.Volume = 0.0f;
            MediaPlayer.Play(well_rested);
            MediaPlayer.Volume = 0.0f;
            startTime = DateTime.Now;
        }

        public void Update(GameTime gameTime)
        {
            timePassed = DateTime.Now - startTime.AddSeconds(period * Beat);

            if (timePassed.TotalSeconds > period)
            {
                //Gets the exact time the beat is hit
                onBeat = DateTime.Now.AddSeconds(-(timePassed.TotalSeconds - period));
                Beat += 1;
            }

            TimeSpan difference = Player.LastShootTime - onBeat;
            if (BulletManager.shot)
            {
                if (Math.Abs(difference.TotalSeconds) < tolerance)
                {
                    Combo += 1;
                }
                else
                {
                    Combo = 0;
                }
            }
        }
    }
}
