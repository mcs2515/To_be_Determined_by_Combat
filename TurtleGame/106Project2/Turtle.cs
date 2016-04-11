using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _106Project2 {
    abstract class Turtle : GameObject{

        public enum Command {
            //Moves
            JUMP,
            LEFT,
            RIGHT,
            //Attacks
            BLOCK,
            FIRE,
            //add others here
            NULL
        }

        public Queue<Command> CommandQueue;
        protected string StatFile;
        private Dictionary<Game1.TurtleTextures, Texture2D> textureDictionary;
        private Bullet bullet;
        public Bullet Bullet {
            get { return bullet; }
        }

        private Arena arena;
        private int qSize;

        private enum jumpState { start=0, hover=1, drop=2 }
        private jumpState currentJumpState;

        Direction LastDirection;
        Direction dir;
        //propeties for dir
        public Direction Dir {
            get { return dir; }
            set { dir = value; }
        }

        Command currentCommand;
        //propeties for currentCommand
        public Command CurrentCommand
        {
            get { return currentCommand; }
            set { currentCommand = value; }
        }

        public bool HasCommands {
            get { return CommandQueue.Count > 0; }
        }

        private int health;
        public int Health {
            get { return health; }
        }

        public bool KO {
            get { return Health <= 1; }
        }

        //CONSTRUCTOR
        //MSpeed = movement speed, ExDelay execuation delay
        //other such as jump height? nah. keep it simple for now
        public Turtle(Bullet bullet, Arena arena, String TStats, int QueueSize, Direction dir, Dictionary<Game1.TurtleTextures, Texture2D> TextureDictionary, string name)
            :base(new Point(0, 0), new Point(GameVariables.TurtleWidth, GameVariables.TurtleHeight), null, name) {

            //other initializers here
            this.textureDictionary = TextureDictionary;
            this.CommandQueue = new Queue<Command>(QueueSize);
            this.currentJumpState = jumpState.drop;
            this.CurrentCommand = Command.NULL;
            this.LastDirection = Direction.LEFT;
            this.StatFile = TStats;
            this.dir = dir;
            this.bullet = bullet;
            this.arena = arena;
            this.qSize = QueueSize;
        }

        public abstract void GetCommands();

        public int AddSingleCommand(Command add) {
            if (CommandQueue.Count >= qSize) return -1;
            else {
                CommandQueue.Enqueue(add);
                Console.WriteLine("Added Command: " + add);
                return 0;
            }
        }

        //EXECUTE METHOD
        public void ExecuteNextCommand() {
            if (currentCommand != Command.JUMP && currentJumpState < jumpState.drop) currentJumpState++;
            if (HasCommands) {
                currentCommand = CommandQueue.Dequeue();
                //sleep ExDelay
            }
            else currentCommand = Command.NULL;
        }

        public void Update() {
            this.Bullet.Update();
            switch (currentCommand) {
                case (Command.JUMP):
                    if(!Game1.isScreenEdge(Direction.UP, Position)) currentJumpState = jumpState.start;
                    else { currentJumpState = jumpState.hover; }
                    break;
                case (Command.LEFT):
                    if (!Game1.isScreenEdge(Direction.LEFT, Position)) {
                        this.X -= 3;
                        LastDirection = Direction.LEFT;
                    }
                    break;
                case (Command.RIGHT):
                    if (!Game1.isScreenEdge(Direction.RIGHT, Position)) {
                        this.X += 3;
                        LastDirection = Direction.RIGHT;
                    }
                    break;
                case (Command.BLOCK):
                    //ADD BLOCK FUNCTIONALITY HERE
                    break;
                case (Command.FIRE):
                    //ADD FIRE FUNCTIONALITY HERE
                    bullet.Active = true;
                    bullet.X = this.X - 8;
                    bullet.Y = this.Y + 10;
                    bullet.Dir = LastDirection;
                    bullet.Update();
                    break;
            }
            switch (currentJumpState) {
                case (jumpState.start):
                    this.Y -= 3;
                    break;
                case (jumpState.hover):
                    //hover broken skip for now
                    currentJumpState++;
                    break;
                case (jumpState.drop):
                    if (this.Y < arena.FloorHeight) this.Y += 5;
                    break;
            }
        }

        //DRAW METHOD
        public override void Draw(SpriteBatch sb) {
            //Do not call spritebatch.Begin() let Game1 handle it
            //Check the current command to draw correct image
            switch (CurrentCommand) {
                case Command.LEFT: //left
                    sb.Draw(textureDictionary[Game1.TurtleTextures.FaceLeft], Position, Color.White); //default image
                    break;
                case Command.RIGHT: //right
                    sb.Draw(textureDictionary[Game1.TurtleTextures.FaceRight], Position, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.0f); //image flipped
                    break;
                case Command.JUMP: //jump
                    sb.Draw(textureDictionary[Game1.TurtleTextures.Jump], Position, Color.White); //check to see what direction before jump to get right direction for jump?
                    break;
                case Command.BLOCK:
                    ///default turtle image better than no image
                    sb.Draw(textureDictionary[Game1.TurtleTextures.Block], Position, Color.White);
                    break;
                case Command.FIRE: //fire
                    sb.Draw(textureDictionary[Game1.TurtleTextures.FaceLeft], Position, Color.White); //default image
                    break;
                case Command.NULL:
                    sb.Draw(textureDictionary[Game1.TurtleTextures.FaceLeft], Position, Color.White); //default image
                    break;
            }
            bullet.Draw(sb);           
        }

        public void TakeHit() { this.health--; }
        public void SetHealth(int hearts) { this.health = hearts; }

        public override bool isColliding(GameObject check) {
            return check.Position.Intersects(this.Position);
        }
    }
}
