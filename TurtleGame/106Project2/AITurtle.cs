using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _106Project2 {
    class AITurtle : Turtle {

        public AIHandler AI;

        //CONSTRUCTOR
        public AITurtle(Bullet bullet, Arena arena, String TStats, int QueueSize, Direction dir, Dictionary<Game1.TurtleTextures, Texture2D> TextureDictionary, string name, AIHandler ai)
            : base(bullet, arena, TStats, QueueSize, dir, TextureDictionary, name) {
            AI = ai;
        }

        //ADDCOMMANDS METHOD
        public override void GetCommands() {
            throw new NotImplementedException("AITurtle.GetCommands()");
            //    try
            //    {
            //        while (true)
            //        { //run until successful #badcode
            //            StreamReader reader = new StreamReader(
            //                new FileStream(StatFile, FileMode.Open));
            //            string read = "";
            //            while ((read = reader.ReadLine()) != null)
            //            {
            //                if (read.Equals("C" + CommandNumber)) break;
            //            }
            //            if (read == null) this.CommandNumber = 0;
            //            else
            //            {
            //                //TODO add command inputs here

            //                break;
            //            }
            //        }
            //    }
            //    catch (FileNotFoundException e)
            //    {
            //        //add better exception handling here
            //        //maybe quit and return to menu with a message

            //    }
        }
    }
}
