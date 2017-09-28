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
            tileTextures.Add("wall", Content.Load<Texture2D>("Tiles/wall"));

            peopleTextures.Add("testMan", Content.Load<Texture2D>("People/testMan"));

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    map[x, y] = new Tile(new Rectangle(x * 32, y * 32, 32, 32), tileTextures["ground"], x, y);
                }
            }

            map[2, 0].setTexture(tileTextures["wall"]);
            map[2, 0].walkable = false;

            map[2, 1].setTexture(tileTextures["wall"]);
            map[2, 1].walkable = false;

            map[2, 2].setTexture(tileTextures["wall"]);
            map[2, 2].walkable = false;

            map[2, 3].setTexture(tileTextures["wall"]);
            map[2, 3].walkable = false;

            map[2, 4].setTexture(tileTextures["wall"]);
            map[2, 4].walkable = false;

            map[2, 5].setTexture(tileTextures["wall"]);
            map[2, 5].walkable = false;

            map[2, 6].setTexture(tileTextures["wall"]);
            map[2, 6].walkable = false;

            map[2, 7].setTexture(tileTextures["wall"]);
            map[2, 7].walkable = false;

            map[9, 8].setTexture(tileTextures["wall"]);
            map[9, 8].walkable = false;

            map[9, 7].setTexture(tileTextures["wall"]);
            map[9, 7].walkable = false;

            map[9, 6].setTexture(tileTextures["wall"]);
            map[9, 6].walkable = false;

            map[9, 5].setTexture(tileTextures["wall"]);
            map[9, 5].walkable = false;

            map[9, 4].setTexture(tileTextures["wall"]);
            map[9, 4].walkable = false;

            map[9, 3].setTexture(tileTextures["wall"]);
            map[9, 3].walkable = false;

            map[9, 0].setTexture(tileTextures["wall"]);
            map[9, 0].walkable = false;

            map[13, 0].setTexture(tileTextures["wall"]);
            map[13, 0].walkable = false;

            map[13, 1].setTexture(tileTextures["wall"]);
            map[13, 1].walkable = false;

            map[13, 2].setTexture(tileTextures["wall"]);
            map[13, 2].walkable = false;

            map[13, 3].setTexture(tileTextures["wall"]);
            map[13, 3].walkable = false;

            dude = new testMan(new Rectangle(0, 0, 32, 32), peopleTextures["testMan"], map);
            dude.setTarget(map[15, 0], map);
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
