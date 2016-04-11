using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _106Project2 {
    public abstract class GameObject {

        public enum Direction { UP=0, LEFT, DOWN, RIGHT };

        private Rectangle position;
        //properties for position
        public Rectangle Position {
            get { return position; }
            set { position = value; }
        }

        public int X {
            get { return position.X; }
            set { position.X = value; }
        }

        public int Y {
            get { return position.Y; }
            set { position.Y = value; }
        }

        private String name;
        //properties for name
        public String Name {
            get { return name; }
        }

        public Texture2D texture;

        //CONSTRUCTOR
        public GameObject(Point position, Point size, Texture2D texture, String name) {
            this.position = new Rectangle(position, size);
            this.texture = texture;
            this.name = name;
        }

        public void SetLocation(Vector2 to) {
            position.X = (int) to.X;
            position.Y = (int) to.Y;
        }

        //DRAW METHOD
        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(texture, position, Color.White);
        }
        //ISCOLLIDING METHOD
        public abstract bool isColliding(GameObject check);
    }
}
