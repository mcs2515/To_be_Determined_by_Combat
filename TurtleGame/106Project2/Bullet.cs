using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _106Project2 {
    class Bullet : GameObject{

        private int speed;
        public int Speed {
            get { return speed; }
            set { speed = value; }
        }

        private bool active;
        public bool Active {
            get { return active; }
            set { active = value; }
        }

        Direction dir;
        public Direction Dir {
            get { return dir; }
            set { dir = value; }
        }

        public Bullet(int speed, Direction dir, Texture2D texture, string name) 
            : base(new Point(0, 0), new Point(GameVariables.BulletWidth, GameVariables.BulletHeight), texture, name) { //change size to texture
            this.speed = speed;
            this.active = false;
        }

        public void Update() {
            if (active) {
                switch (Dir) {
                    case (Direction.UP):
                        this.Y += speed; //this may be backwards 
                        break;
                    case (Direction.DOWN):
                        this.Y -= speed; //this may be backwards
                        break;
                    case (Direction.RIGHT):
                        this.X += speed;
                        break;
                    case (Direction.LEFT):
                        this.X -= speed;
                        break;
                }
                if (Game1.isScreenEdgeAll(Position)) active = false;
            }
        }

        public override void Draw(SpriteBatch sb) {
            if (active) sb.Draw(texture, Position, Color.White);
        }

        public override bool isColliding(GameObject check) {
            return check.Position.Intersects(this.Position);
        }
    }
}
