using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectUno
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState msNow, msPrev;
        KeyboardState kbNow, kbPrev;
        Viewport view1;

        Dictionary<string, Texture2D> tileTextures = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> peopleTextures = new Dictionary<string, Texture2D>();
        Dictionary<string, TileType> tileTypes = new Dictionary<string, TileType>();

        int mapLength = 36;//TILES IN THE Y AXIS
        int mapWidth = 64;//TILES IN THE X AXIS
        Tile[,] map;

        testMan dude;

        TileType testType;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferWidth = 1024;
            //graphics.PreferredBackBufferHeight = 576;
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            view1 = new Viewport();
            view1.X = 0;
            view1.Y = 0;
            view1.Width = 1024;
            view1.Height = 576;
            view1.MinDepth = 0;
            view1.MaxDepth = 1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            msNow = Mouse.GetState();
            msPrev = msNow;
            kbNow = Keyboard.GetState();
            kbPrev = kbNow;

            tileTextures.Add("ground", Content.Load<Texture2D>("Tiles/ground"));
            tileTextures.Add("wall", Content.Load<Texture2D>("Tiles/wall"));
            tileTextures.Add("concrete", Content.Load<Texture2D>("Tiles/concrete"));//ADDS CONCRETE TEXTURE

            peopleTextures.Add("testMan", Content.Load<Texture2D>("People/testMan"));

            //
            tileTypes.Add("ground", new TileType(tileTextures["ground"], true, 0, 1));
            tileTypes.Add("wall", new TileType(tileTextures["wall"], false, 0, 0));
            tileTypes.Add("concrete", new TileType(tileTextures["concrete"], true, 40, 2));

            map = new Tile[mapWidth, mapLength];
            for (int y = 0; y < mapLength; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    map[x, y] = new Tile(new Rectangle(x * 32, y * 32, 32, 32), tileTextures["ground"], x, y);
                }
            }



            dude = new testMan(new Rectangle(0, 0, 32, 32), peopleTextures["testMan"], map);

            testType = tileTypes["wall"];
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            dude.Update(map);

            foreach(Tile t in map)
            {
                if(t.checkClicked(msNow, msPrev, view1))
                {
                    t.setType(testType);
                }
            }

            if(kbNow.IsKeyUp(Keys.Space) && kbPrev.IsKeyDown(Keys.Space))
            {
                if(testType == tileTypes["wall"])
                {
                    testType = tileTypes["concrete"];
                }
                else
                {
                    testType = tileTypes["wall"];
                }
            }

            if(kbNow.IsKeyDown(Keys.D))
            {
                if(view1.X > -(mapWidth*32)+2048)
                {
                    view1.X -= 5;
                }      
            }
            if(kbNow.IsKeyDown(Keys.A))
            {
                if(view1.X < 0)
                {
                    view1.X += 5;
                }   
            }
            if (kbNow.IsKeyDown(Keys.S))
            {
                if (view1.Y > -1024)
                {
                    view1.Y -= 5;
                }
            }
            if (kbNow.IsKeyDown(Keys.W))
            {
                if (view1.Y < 0)
                {
                    view1.Y += 5;
                }
            }

            if (msNow.RightButton == ButtonState.Released && msPrev.RightButton == ButtonState.Pressed)
            {
                dude.setTarget(map[(msNow.X-view1.X) / 32, (msNow.Y-view1.Y) / 32], map);
            }

            kbPrev = kbNow;
            kbNow = Keyboard.GetState();
            msPrev = msNow;
            msNow = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            graphics.GraphicsDevice.Viewport = view1;
            spriteBatch.Begin();

            foreach(Tile t in map)
            {
                t.Draw(spriteBatch);
            }

            dude.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
