using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _106Project2 {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public enum GameTextures { UPARROW, DOWNARROW, LEFTARROW, RIGHTARROW, ACTIONBAR, FIREICON, HEARTICON, RWIN, BWIN, PLAN, FIGHT, MUTUALDESTRUCTION, DRAW }
        public enum TurtleTextures { /*Still,*/ FaceLeft, FaceRight, Jump, Shoot, Block} //ADD ADDITIONAL TEXTURES AS NEEDED
        public enum Result { Draw = -1, BlueWin = 0, RedWin = 1, MutualDestruction = 2 }

        Dictionary<GameTextures, Texture2D> GameTextureHolder = new Dictionary<GameTextures, Texture2D>();
        Dictionary<TurtleTextures, Texture2D> turtleRTextures = new Dictionary<TurtleTextures, Texture2D>();
        Dictionary<TurtleTextures, Texture2D> turtleBTextures = new Dictionary<TurtleTextures, Texture2D>();

        public const int MAXROUNDS = 12;
        public const int INIT_HEALTH = 3;
        public const int MOVE_DISTANCE = 10;
        public const int JUMP_HEIGHT = 10;

        //compensate for bullet deactivation bug
        bool canTakeHitR = false;
        bool canTakeHitB = false;

        AITurtle TurtleR;
        Turtle TurtleB;
        Arena DebugArena;
        SpriteFont font;

        //menus
        Texture2D homeMenu;
        Texture2D creditsMenu;
        Texture2D directionsMenu;
        Texture2D pauseMenu;

        //backgrounds
        double CommandTimer;
        double PlanTimer;
        Result result;
        Menu currentState;

        //PauseMenu pauseState;
        public static KeyboardState kbState;
        public static KeyboardState previousKbState;

        enum Menu
        {
            Home,
            Play,
            Directions,
            Credits,
            Pause,
            //Exit,
        }

        GameState currentGameState;
        enum GameState { Planning, Fighting, Result }
        
        int currentRound;
        //enum PauseMenu
        //{
        //    Pause,
        //    Play,
        //    Exit,
        //}

        //CONSTRUCTOR
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1200;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 700;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        //SET STAGE METHOD
        public void SetStage() {
            //configure turtles
            //place all objects
            //prepare variables

            //TurtleR.CurrentCommand = Turtle.Command.RIGHT;
            //TurtleB.CurrentCommand = Turtle.Command.LEFT;

            TurtleR.SetLocation(new Vector2(150, GraphicsDevice.Viewport.Height - 150));
            TurtleB.SetLocation(new Vector2(GraphicsDevice.Viewport.Width - 350, GraphicsDevice.Viewport.Height - 150));
           
            TurtleR.SetHealth(INIT_HEALTH);
            TurtleB.SetHealth(INIT_HEALTH);

            TurtleR.CommandQueue.Clear();
            TurtleB.CommandQueue.Clear();

            TurtleR.Bullet.Active = false;
            TurtleB.Bullet.Active = false;

            TurtleR.AI.ResetAI();
        }

       
        //SINGLE KEYPRESS METHOD
        public bool SingleKeyPress(Keys key)
        {
            //check to see if this is the first frame that the key was pressed
            return (kbState.IsKeyDown(key) && previousKbState.IsKeyUp(key));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            //read data files

            /////////////////////////////////////FILE MANAGER//////////////////////////////////////////////
            //GameVariables.WriteAllData("../../../Data/Data.dat");
            GameVariables.ReadAllData("../../../Data/Data.dat");

            //setting initial menu
            //currentState= HomeMenu.Home;
            currentState = Menu.Home;
            currentGameState = GameState.Planning;
            result = Result.Draw;
            CommandTimer = 1;
            PlanTimer = 5;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Arena
            this.DebugArena = new Arena(Content.Load<Texture2D>("GameStage"), "DebugArena", Arena.Layouts.Flat, 550, GraphicsDevice.Viewport);

            //TURTLE R
            turtleRTextures.Add(TurtleTextures.FaceLeft, Content.Load<Texture2D>("turtleR"));
            turtleRTextures.Add(TurtleTextures.FaceRight, Content.Load<Texture2D>("turtleR"));
            turtleRTextures.Add(TurtleTextures.Jump, Content.Load<Texture2D>("Rjump"));
            turtleRTextures.Add(TurtleTextures.Shoot, Content.Load<Texture2D>("bulletR"));
            turtleRTextures.Add(TurtleTextures.Block, Content.Load<Texture2D>("Rblock"));
            this.TurtleR = new AITurtle(
                           new Bullet(4, Bullet.Direction.LEFT, Content.Load<Texture2D>("bulletR"), "bulletR"),
                           DebugArena, null, 8, Turtle.Direction.LEFT, turtleRTextures, "TurtleR", new AIHandler("../../../Data/AIScripts", false));

            //TURTLE B
            turtleBTextures.Add(TurtleTextures.FaceLeft, Content.Load<Texture2D>("turtleB"));
            turtleBTextures.Add(TurtleTextures.FaceRight, Content.Load<Texture2D>("turtleB"));
            turtleBTextures.Add(TurtleTextures.Jump, Content.Load<Texture2D>("Bjump"));
            turtleBTextures.Add(TurtleTextures.Shoot, Content.Load<Texture2D>("bulletB"));
            turtleBTextures.Add(TurtleTextures.Block, Content.Load<Texture2D>("Bblock"));

            this.TurtleB = new PTurtle(
                           new Bullet(4, Bullet.Direction.LEFT, Content.Load<Texture2D>("bulletB"), "bulletB"),
                           DebugArena, null, 8, 1, Turtle.Direction.LEFT, turtleBTextures, "TurtleB");
            
            //GAME TEXTURES
            this.GameTextureHolder[GameTextures.ACTIONBAR] = Content.Load<Texture2D>("SetStage");
            this.GameTextureHolder[GameTextures.FIREICON] = Content.Load<Texture2D>("action");
            this.GameTextureHolder[GameTextures.RIGHTARROW] = Content.Load<Texture2D>("right");
            this.GameTextureHolder[GameTextures.LEFTARROW] = Content.Load<Texture2D>("left");
            this.GameTextureHolder[GameTextures.UPARROW] = Content.Load<Texture2D>("up");
            this.GameTextureHolder[GameTextures.DOWNARROW] = Content.Load<Texture2D>("down");
            this.GameTextureHolder[GameTextures.HEARTICON] = Content.Load<Texture2D>("hearts");
            this.GameTextureHolder[GameTextures.PLAN] = Content.Load<Texture2D>("PlanMessage");
            this.GameTextureHolder[GameTextures.BWIN] = Content.Load<Texture2D>("BWin");
            this.GameTextureHolder[GameTextures.RWIN] = Content.Load<Texture2D>("Rwin");
            this.GameTextureHolder[GameTextures.FIGHT] = Content.Load<Texture2D>("FightMessage");
            this.GameTextureHolder[GameTextures.DRAW] = Content.Load<Texture2D>("Draw");
            this.GameTextureHolder[GameTextures.MUTUALDESTRUCTION] = Content.Load<Texture2D>("MutualDistruction");

            //Font
            this.font = this.Content.Load<SpriteFont>("Arial14");

            //Menus
            homeMenu = this.Content.Load<Texture2D>("Home");
            creditsMenu = this.Content.Load<Texture2D>("Credits");
            directionsMenu = this.Content.Load<Texture2D>("Directions");
            pauseMenu = this.Content.Load<Texture2D>("Pause");
          
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //set current keypress to previous
            previousKbState = kbState;
            //update the keystate
            kbState = Keyboard.GetState();

            //CHECK GAME STATE AND ENTER LOGIC HERE
            switch (currentState)
            {
                case Menu.Home:
                    if (SingleKeyPress(Keys.Enter))
                    {                       
                        currentState = Menu.Play; //change home menu to play
                        currentRound = 0;
                        currentGameState = GameState.Planning;
                        //Console.WriteLine("Current state: Plan");
                        SetStage();
                    }
                    if (SingleKeyPress(Keys.D))
                    {                
                        currentState = Menu.Directions; //change home menu to directions
                    }
                    if (SingleKeyPress(Keys.C))
                    {                     
                        currentState = Menu.Credits; //change home menu to credits                       
                    }
                    break;
                case Menu.Play:
                    //Console.WriteLine(TurtleB.Bullet.Active);
                    switch (currentGameState) {
                        case (GameState.Planning):
                            //manual add commands TurtleR
                            foreach (Keys key in kbState.GetPressedKeys()) {
                                if (!previousKbState.IsKeyDown(key))
                                    switch (key) {
                                        case (Keys.W):
                                        case (Keys.A):
                                        case (Keys.S):
                                        case (Keys.D):
                                        case (Keys.Space):
                                            TurtleR.AddSingleCommand(TurtleR.AI.NextCommand());
                                            break;
                                    }
                            } 

                            //manual add commands TurtleB
                            foreach (Keys key in kbState.GetPressedKeys()) {
                                if(!previousKbState.IsKeyDown(key))
                                    switch (key) {
                                        case (Keys.W):
                                            TurtleB.AddSingleCommand(Turtle.Command.JUMP);
                                            break;
                                        case (Keys.A):
                                            //CommandQueue.Enqueue(Command.LEFT);
                                            TurtleB.AddSingleCommand(Turtle.Command.LEFT);
                                            break;
                                        case (Keys.S):
                                            //CommandQueue.Enqueue(Command.BLOCK);
                                            TurtleB.AddSingleCommand(Turtle.Command.BLOCK);
                                            break;
                                        case (Keys.D):
                                            //CommandQueue.Enqueue(Command.RIGHT);
                                            TurtleB.AddSingleCommand(Turtle.Command.RIGHT);
                                            break;
                                        case (Keys.Space):
                                            //CommandQueue.Enqueue(Command.FIRE);
                                            TurtleB.AddSingleCommand(Turtle.Command.FIRE);
                                            break;
                                        case (Keys.Enter):
                                            //end
                                            PlanTimer -= 1000;
                                            break;
                                    }
                            }

                            //check for timeout, start the game
                            PlanTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                            if(PlanTimer <= 0) {
                                currentRound++;
                                PlanTimer = 5;
                                currentGameState = GameState.Fighting;
                                //Console.WriteLine("Current state: Fighting");
                            } 
                            
                            break;
                        case (GameState.Fighting):
                            //update the two turtles
                            TurtleB.Update();
                            TurtleR.Update();

                            //check for collisions, canTakeHit compensates for bullet lag
                            if (TurtleB.isColliding(TurtleR.Bullet) && canTakeHitB) {
                                TurtleR.Bullet.Active = false;
                                if(TurtleB.CurrentCommand != Turtle.Command.BLOCK) TurtleB.TakeHit();
                                canTakeHitB = false;
                            }
                            if (TurtleR.isColliding(TurtleB.Bullet) && canTakeHitR) {
                                TurtleB.Bullet.Active = false;
                                if(TurtleR.CurrentCommand != Turtle.Command.BLOCK) TurtleR.TakeHit();
                                canTakeHitR = false;
                            }

                            //check for results
                            if (TurtleR.KO && TurtleB.KO) result = Result.MutualDestruction;
                            else if (TurtleR.KO) result = Result.BlueWin;
                            else if (TurtleB.KO) result = Result.RedWin;
                            else result = Result.Draw;

                            //check for maxrounds
                            if (currentRound > MAXROUNDS) {
                                currentGameState = GameState.Result;
                                //Console.WriteLine("Current state: Result");
                                currentRound = 0;
                            }

                            //check for fighting timeout, swith to planning
                            CommandTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                            if (CommandTimer <= 0) {
                                if (!TurtleB.HasCommands && !TurtleR.HasCommands) {
                                    currentGameState = GameState.Planning;
                                    //Console.WriteLine("Current state: Planning");
                                }
                                TurtleB.ExecuteNextCommand();
                                TurtleR.ExecuteNextCommand();
                                //so because of weird bullet issues, i have to compensate for the 
                                //time it takes a bullet to deactivate
                                if (TurtleB.Bullet.Active) canTakeHitR = true;
                                if (TurtleR.Bullet.Active) canTakeHitB = true;
                                CommandTimer = 1;
                            }                         
                            
                            break;
                        case (GameState.Result):
                            if (SingleKeyPress(Keys.Enter)) currentState = Menu.Home;
                            break;
                        
                    }
                    
                    if (SingleKeyPress(Keys.P))
                    {                       
                        currentState = Menu.Pause; //pause game
                    }
                    break;
                case Menu.Pause:
                    if (SingleKeyPress(Keys.P))
                    {
                        currentState = Menu.Play; //resume game
                    }                   
                    if (SingleKeyPress(Keys.E)) //return to main menu
                    {
                        currentState = Menu.Home;
                        //clear the turtles commands
                        TurtleB.CommandQueue.Clear();
                        TurtleR.CommandQueue.Clear();
                    }
                    break;
                case Menu.Directions:
                    if (SingleKeyPress(Keys.Enter))
                    {  
                        currentState = Menu.Home; //change home menu to play
                    }
                    if (SingleKeyPress(Keys.D))
                    {
                        currentState = Menu.Home; //return to main menu
                    }
                    break;
                case Menu.Credits:
                    if (SingleKeyPress(Keys.Enter))
                    {
                        currentState = Menu.Home; //change home menu to play
                    }
                    if (SingleKeyPress(Keys.C))
                    {
                        currentState = Menu.Home; //return to main menu
                    }
                    break;
            }

            Draw(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //draw the arena
            switch (currentState) {

                case Menu.Home:
                    spriteBatch.Draw(homeMenu, new Vector2(0, 0), Color.White);
                    break;
                case (Menu.Play):
                    //draw the game background
                    DebugArena.Draw(spriteBatch);
                    //draw the queue bar/action bar
                    spriteBatch.Draw(GameTextureHolder[GameTextures.ACTIONBAR], new Vector2(0, 0), Color.White);
                    //draw the turtles to screen
                    TurtleB.Draw(spriteBatch);
                    TurtleR.Draw(spriteBatch);
                    TurtleB.Bullet.Draw(spriteBatch);
                    TurtleR.Bullet.Draw(spriteBatch);

                    switch (currentGameState) {
                        case (GameState.Planning):
                            //draw the Plan message to screen
                            spriteBatch.Draw(GameTextureHolder[GameTextures.PLAN], new Vector2(0, 0), Color.White);
                            //set where first image will go
                            int i = 9 * 45;
                            //foreach turtle command in its command queue
                            foreach (Turtle.Command cmd in TurtleB.CommandQueue)
                            {
                                //call the draw command method
                                DrawCommand(cmd, GraphicsDevice.Viewport.Width - 88, GraphicsDevice.Viewport.Height - i);
                                //allows an image to be drawn under other images
                                i -= 45;
                            }
                            int j = 9 * 45;
                            //do the same for the RTurtle
                            foreach (Turtle.Command cmd in TurtleR.CommandQueue)
                            {
                                //call the draw command method
                                DrawCommand(cmd, 37, GraphicsDevice.Viewport.Height - j);
                                //allows an image to be drawn under other images
                                j -= 45;
                            }
                            break;
                        case (GameState.Fighting):
                            //draw the Fight message to screen
                            spriteBatch.Draw(GameTextureHolder[GameTextures.FIGHT], new Vector2(0, 0), Color.White);
                            //draw the current command at the top of the command bar
                            DrawCommand(TurtleB.CurrentCommand, GraphicsDevice.Viewport.Width - 88, GraphicsDevice.Viewport.Height - 10 * 46);
                            //set where first image will go
                            i = 9 * 45;
                            //foreach turtle command in its command queue
                            foreach (Turtle.Command cmd in TurtleB.CommandQueue)
                            {
                                //call the draw command method
                                DrawCommand(cmd, GraphicsDevice.Viewport.Width - 88, GraphicsDevice.Viewport.Height - i);
                                //allows an image to be drawn under other images
                                i -= 45;
                            }
                            j = 9 * 45;
                            //do the same for the RTurtle
                            //draw the current command at the top of the command bar
                            DrawCommand(TurtleR.CurrentCommand, 37, GraphicsDevice.Viewport.Height - 10 * 46);
                            //foreach turtle command in its command queue
                            foreach (Turtle.Command cmd in TurtleR.CommandQueue)
                            {
                                //call the draw command method
                                DrawCommand(cmd, 37, GraphicsDevice.Viewport.Height - j);
                                //allows an image to be drawn under other images
                                j -= 45;
                            }
                            break;
                        case (GameState.Result):
                            switch (result) {
                                case (Result.BlueWin):
                                    spriteBatch.Draw(GameTextureHolder[GameTextures.BWIN], new Vector2(0, 0), Color.White);
                                    break;
                                case (Result.RedWin):
                                    spriteBatch.Draw(GameTextureHolder[GameTextures.RWIN], new Vector2(0, 0), Color.White);
                                    break;
                                case (Result.Draw):
                                    spriteBatch.Draw(GameTextureHolder[GameTextures.DRAW], new Vector2(0, 0), Color.White);
                                    break;
                                case (Result.MutualDestruction):
                                    spriteBatch.Draw(GameTextureHolder[GameTextures.MUTUALDESTRUCTION], new Vector2(0, 0), Color.White);
                                    break;
                                ///add others here
                                /// draw
                                /// mutual destruction
                            }
                            break;
                    }

                    //check players turtle's hp and draw hearts
                    for (int i = 0; i < TurtleR.Health; i++)
                    {
                        int x = 15+70*i;
                        spriteBatch.Draw(GameTextureHolder[GameTextures.HEARTICON], new Rectangle(x, 55, 75, 75), Color.White);
                    }

                    //check ai turtle's hp and draw hearts
                    for (int i = 0; i < TurtleB.Health; i++)
                    {
                        int x = 1105-70*i;
                        spriteBatch.Draw(GameTextureHolder[GameTextures.HEARTICON], new Rectangle(x, 55, 75, 75), Color.White);
                    }
                   
                    break;
                case (Menu.Pause):
                    //draw the game background
                    DebugArena.Draw(spriteBatch);
                    spriteBatch.Draw(pauseMenu, new Vector2(0, 0), Color.White);                   
                    //draw the turtles to screen
                    TurtleB.Draw(spriteBatch);
                    TurtleR.Draw(spriteBatch);                    
                    //draw the queue bar/action bar
                    spriteBatch.Draw(GameTextureHolder[GameTextures.ACTIONBAR], new Vector2(0,0), Color.White);

                    //check players turtle's hp and draw hearts
                    for (int i = 0; i < TurtleB.Health; i++)
                    {
                        int x = 15 + 70 * i;
                        spriteBatch.Draw(GameTextureHolder[GameTextures.HEARTICON], new Rectangle(x, 55, 75, 75), Color.White);
                    }

                    //check ai turtle's hp and draw hearts
                    for (int i = 0; i < TurtleR.Health; i++)
                    {
                        int x = 1105 - 70 * i;
                        spriteBatch.Draw(GameTextureHolder[GameTextures.HEARTICON], new Rectangle(x, 55, 75, 75), Color.White);
                    }

                    break;
                case (Menu.Directions):
                    //insert background here
                    spriteBatch.Draw(directionsMenu, new Vector2(0, 0), Color.White);
                    break;
                case (Menu.Credits):
                    //insert background here
                    spriteBatch.Draw(creditsMenu, new Vector2(0, 0), Color.White);
                    break;
            }
       
            spriteBatch.End();            
            base.Draw(gameTime);
        }

        //CRAWCOMMAND METHOD
        void DrawCommand(Turtle.Command cmd, int x, int y)
        {
            //depening on the command
            switch (cmd)
            {
                //draw the corresponding command images to screen
                case (Turtle.Command.RIGHT):
                    spriteBatch.Draw(GameTextureHolder[GameTextures.RIGHTARROW], new Vector2(x, y+7), Color.White);
                    break;
                case (Turtle.Command.LEFT):
                    spriteBatch.Draw(GameTextureHolder[GameTextures.LEFTARROW], new Vector2(x, y+7), Color.White);
                    break;
                case (Turtle.Command.JUMP):
                    spriteBatch.Draw(GameTextureHolder[GameTextures.UPARROW], new Vector2(x+5, y-3), Color.White);
                    break;
                case (Turtle.Command.BLOCK):
                    spriteBatch.Draw(GameTextureHolder[GameTextures.DOWNARROW], new Vector2(x+5, y-3), Color.White);
                    break;
                case (Turtle.Command.FIRE):
                    spriteBatch.Draw(GameTextureHolder[GameTextures.FIREICON], new Vector2(x-3, y-3), Color.White);
                    break;
            }
        }

        public static bool isScreenEdge(GameObject.Direction Dir, Rectangle Position) {
            switch (Dir) {
                case (GameObject.Direction.UP):
                    return (Position.Y <= 0);
                case (GameObject.Direction.DOWN):
                    return (Position.Y + Position.Height >= graphics.GraphicsDevice.Viewport.Height);
                case (GameObject.Direction.LEFT):
                    return (Position.X <= 0);
                case (GameObject.Direction.RIGHT):
                    return (Position.X + Position.Width >= graphics.GraphicsDevice.Viewport.Width);
            }
            return false;
        }

        public static bool isScreenEdgeAll(Rectangle Position) {
                return (Position.Y <= 0) ||
                (Position.Y + Position.Height >= graphics.GraphicsDevice.Viewport.Height) ||
                (Position.X <= 0) ||
                (Position.X + Position.Width >= graphics.GraphicsDevice.Viewport.Width);
        }
    }
}
