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

        Dictionary<string, Texture2D> tileTextures = new Dictionary<string, Texture2D>();
        Dictionary<string, Texture2D> peopleTextures = new Dictionary<string, Texture2D>();

        Tile[,] map = new Tile[16, 9];

        testMan dude;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 512;
            graphics.PreferredBackBufferHeight = 288;
        }

        protected override void Initialize()
        {        

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileTextures.Add("ground", Content.Load<Texture2D>("Tiles/ground"));

            peopleTextures.Add("testMan", Content.Load<Texture2D>("People/testMan"));

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    map[x, y] = new Tile(new Rectangle(x * 32, y * 32, 32, 32), tileTextures["ground"], x, y);
                }
            }

            dude = new testMan(new Rectangle(0, 0, 32, 32), peopleTextures["testMan"], map, map[10,7]);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            dude.Update(map);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
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
