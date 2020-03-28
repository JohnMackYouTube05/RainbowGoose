using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 1. Added the "GooseModdingAPI" project as a reference.
// 2. Compile this.
// 3. Create a folder with this DLL in the root, and *no GooseModdingAPI DLL*
using GooseShared;
using SamEngine;
using System.Windows;
using System.Timers;

namespace DefaultMod
{
    public class ModEntryPoint : IMod
    {
        // Gets called automatically, passes in a class that contains pointers to
        // useful functions we need to interface with the goose.
        private int current;
        private float nextChangeTime = -1f;
        private int rainbowIndex;
        private float colorSpeed = .15f;

        void IMod.Init()
        {
            // Subscribe to whatever events we want
            InjectionPoints.PostTickEvent += PostTick;
            InjectionPoints.PreRenderEvent += new InjectionPoints.PreRenderEventHandler(PreRenderEvent);
        }

        public void PreRenderEvent(GooseEntity goose, Graphics gfx)
        {
            if (Time.time >= nextChangeTime)
            { 
                goose.renderData.brushGooseWhite.Color = rainbowColors[rainbowIndex++]; 
                this.rainbowIndex = this.rainbowIndex % this.rainbowColors.Count;

                nextChangeTime = Time.time + colorSpeed;
            }
        }

        public List<Color> rainbowColors = new List<Color>()
        {
            Color.Red,
            Color.DarkRed,
            Color.DarkOrange,
            Color.Orange,
            Color.Gold,
            Color.Yellow,
            Color.GreenYellow,
            Color.Chartreuse,
            Color.Lime,
            Color.Green,
            Color.SeaGreen,
            Color.Teal,
            Color.Cyan,
            Color.SkyBlue,
            Color.Blue,
            Color.Purple,
            Color.Magenta,
            Color.Pink,
            Color.HotPink,
            Color.LightPink
        };


        public void PostTick(GooseEntity g)
        {
            // Do whatever you want here.

            // If we're running our mod's task
            if (g.currentTask == API.TaskDatabase.getTaskIndexByID("FollowMouseDrifty"))
            {
                // Lock our goose facing one direction for some reason?
                g.renderData.brushGooseWhite.Color = this.rainbowColors[this.rainbowIndex++];
                this.rainbowIndex = this.rainbowIndex % this.rainbowColors.Count;
                
            }
        }
    }
}
