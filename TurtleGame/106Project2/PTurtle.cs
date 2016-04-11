using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _106Project2 {
    class PTurtle : Turtle {

        int playerNum;
        //properties for playerNum
        public int PlayerNum {
            get { return playerNum; }
        }

        long timeout = GameVariables.CommandTimeout;

        //CONSTRUCTOR
        public PTurtle(Bullet bullet, Arena arena, String TStats, int QueueSize, int num,
            Direction dir, Dictionary<Game1.TurtleTextures, Texture2D> TextureDictionary, string name)
            :base(bullet, arena, TStats, QueueSize, dir, TextureDictionary, name) {

            //initialize
            this.playerNum = num;
        }
        //ADDCOMMANDS METHOD
        public override void GetCommands() {
            throw new NotImplementedException("PTurtle.GetCommands()");
        }
    }
}
