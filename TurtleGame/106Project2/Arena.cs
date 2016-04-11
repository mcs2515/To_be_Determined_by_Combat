using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _106Project2 {
    class Arena : GameObject {
        //
        public enum Layouts { Flat=0, NoFloor }; //add additional layouts
        public int minY = 0; //check this and update as necessary, should be floor

        private Layouts layout;
        //properties for layout
        public Layouts Layout {
            get { return Layout; }
        }

        private int floorHeight;
        public int FloorHeight {
            get { return floorHeight; }
        }

        //CONSTRUCTOR
        public Arena(Texture2D texture, string name, Layouts layout, int minFloorHeight, Viewport view) 
            //set below arena size and position
            : base(new Point(0, 0), new Point(view.Width, view.Height), texture, name) {
            this.layout = layout;
            this.floorHeight = minFloorHeight;
        }
        //ISCOLLIDING METHOD FOR GAMEOBJECTS
        public override bool isColliding(GameObject check) {
            throw new NotImplementedException("Arena.isColliding()");
        }
        
    }
}
