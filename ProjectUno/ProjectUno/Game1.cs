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

        Dictionary<string, Texture2D> tileTextures = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> peopleTextures = new Dictionary<string, Texture2D>();
        Dictionary<string, TileType> tileTypes = new Dictionary<string, TileType>();

        Tile[,] map;
        int mapWidth, mapHeight, cameraX, cameraY, cameraWidth, cameraHeight;

        testMan dude;

        TileType testType;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 576;
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            mapWidth = 64;
            mapHeight = 36;
            cameraX = 0;
            cameraY = 0;
            cameraWidth = 32 * 32;
            cameraHeight = 18 * 32;
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

            map = new Tile[64, 36];
            for (int y = 0; y < 36; y++)
            {
                for (int x = 0; x < 64; x++)
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
                if(t.checkClicked(msNow, msPrev, cameraX, cameraY))
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

            if (msNow.RightButton == ButtonState.Released && msPrev.RightButton == ButtonState.Pressed)
            {
                dude.setTarget(map[(msNow.X-cameraX) / 32, (msNow.Y-cameraY) / 32], map);
            }

            if (kbNow.IsKeyDown(Keys.W) && cameraY < 0)
            {
                cameraY += 8;
            }
            if (kbNow.IsKeyDown(Keys.A) && cameraX < 0)
            {
                cameraX += 8;
            }
            if (kbNow.IsKeyDown(Keys.S) && cameraY > -(32*mapHeight)+cameraHeight)
            {
                cameraY += -8;
            }
            if (kbNow.IsKeyDown(Keys.D) && cameraX > -(32*mapWidth)+cameraWidth)
            {
                cameraX += -8;
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Matrix.CreateTranslation(cameraX, cameraY, 0));

            foreach (Tile t in map)
            {
                t.Draw(spriteBatch);
            }

            dude.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
