using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Test;

namespace Test
{
    /// <summary>
    /// This is the main type for your game. 
    /// </summary>
    public class Game1 : Game
    {
        Texture2D ennemiTexture;
        Texture2D ennemiTextureTemp;
        Texture2D playerTexture;
        Texture2D playerTexturefront;
        Texture2D playerTextureback;
        Texture2D playerTextureleft;
        Texture2D playerTextureright;
        Texture2D objetCroissant;
        KeyboardState prevKstate;
        KeyboardState kstate;
        List<Ennemi> ennemis;
        TimeSpan ennemiSpawn;
        TimeSpan prevEnnemiSpawn;
        Random random;
        Inventaire[] inventaire;
        List<Objet> objets;
        List<TuileBase> mapLevel1;
        List<Grass_Tile> spawnable;
        List<Forest_Tile> dropLoot;
        List<Water_Tile> waterTiles;
        List<Rock_Tile> rockTiles;
        List<Projectile> projectiles;
        Texture2D projectileTexture;
        TimeSpan projectileSpawn;
        TimeSpan prevProjectileSpawn;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        float speedG;
        Player player;
        Texture2D textureTuile;
        Matrix cameraMatrix = Matrix.Identity;
        const double CameraSpeed = 200d;
        Rectangle playerRectangle;
        Rectangle ennemiRectangle;
        Rectangle projectileRectangle;
        Rectangle waterRectangle;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            IsMouseVisible = true;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            player = new Player();
            speedG = 200f;
            ennemis = new List<Ennemi>();
            prevEnnemiSpawn = TimeSpan.Zero;
            ennemiSpawn = TimeSpan.FromSeconds(1.0f);
            random = new Random();
            inventaire = new Inventaire[10];
            projectiles = new List<Projectile>();
            const float secondes = 60f;
            const float cadence_de_tir = 200f;

            prevProjectileSpawn = TimeSpan.Zero;
            projectileSpawn = TimeSpan.FromSeconds(secondes/cadence_de_tir);
            mapLevel1 = new List<TuileBase>();
            // on va instancier la map ci-dessus, pour la dessiner
            spawnable = new List<Grass_Tile>();
            dropLoot = new List<Forest_Tile>();
            objets = new List<Objet>();
            rockTiles = new List<Rock_Tile>();
            waterTiles = new List<Water_Tile>();
            
            //objets.Enqueue(Objet.Spawn(Objet.Types.Fromage));
            //objets.Enqueue(Objet.Spawn(Objet.Types.Graines));
            //objets.Enqueue(Objet.Spawn(Objet.Types.Lait));
            //objets.Enqueue(Objet.Spawn(Objet.Types.Soupe));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //textureAlphonseRight = Content.Load<Texture2D>("Alphonse-right");
            //textureAlphonseLeft = Content.Load<Texture2D>("Alphonse-left");
            //textureAlphonseAnimRight = Content.Load<Texture2D>("Alphonse-anim-right");
            //textureAlphonseAnimLeft = Content.Load<Texture2D>("Alphonse-anim-left");
            //textureAlphonsetempR = Content.Load<Texture2D>("Alphonse-right");
            //textureAlphonsetempL = Content.Load<Texture2D>("Alphonse-left");

            // TODO: use this.Content to load your game content here
            textureTuile = Content.Load<Texture2D>("grass_tile");
            Texture2D grassTexture = Content.Load<Texture2D>("grass_tile");
            Texture2D waterTexture = Content.Load<Texture2D>("water_tile");
            Texture2D rockTexture = Content.Load<Texture2D>("rock_tile");
            Texture2D sandTexture = Content.Load<Texture2D>("sand_tile");
            Texture2D forestTexture = Content.Load<Texture2D>("forest_tile");
            string[] ligneMap = System.IO.File.ReadAllLines(@"C: \Users\theog\source\repos\Test\Test\Content\maplevel1.txt");
            for (int x = 0; x < ligneMap.Length; x++)
            {
                for (int y = 0; y < ligneMap[x].Length; y++)
                {
                    int i = 0;
                    if (ligneMap[x].Substring(y,1) == "G")
                    {
                        mapLevel1.Add(new Grass_Tile(grassTexture, new Vector2(y * 32, x * 32)));
                        spawnable.Add(new Grass_Tile(grassTexture, new Vector2(y * 32, x * 32)));
                    }
                    if (ligneMap[x].Substring(y,1) == "W")
                    {
                        mapLevel1.Add(new Water_Tile(waterTexture, new Vector2(y * 32, x * 32)));
                        waterTiles.Add(new Water_Tile(waterTexture, new Vector2(y * 32, x * 32)));
                    }
                    if (ligneMap[x].Substring(y, 1) == "R")
                    {
                        mapLevel1.Add(new Rock_Tile(rockTexture, new Vector2(y * 32, x * 32)));
                    }
                    if (ligneMap[x].Substring(y, 1) == "S")
                    {
                        mapLevel1.Add(new Sand_Tile(sandTexture, new Vector2(y * 32, x * 32)));
                    }
                    if (ligneMap[x].Substring(y, 1) == "F")
                    {
                        mapLevel1.Add(new Forest_Tile(forestTexture, new Vector2(y * 32, x * 32)));
                        dropLoot.Add(new Forest_Tile(forestTexture, new Vector2(y * 32, x * 32)));
                    }
                    i++;
                }
            }
            //objets.Add(new Objet(Objet.Types.Croissant, new Rectangle((int)dropLoot[0].Position.X, (int)dropLoot[0].Position.Y, (int)dropLoot[0].LargeurTuile, (int)dropLoot[0].HauteurTuile)));
            //mapLevel1.Add(new TuileBase(textureTuile, new Vector2(0, 0)) { textureTuile = Content.Load<Texture2D>("grass_tile"), Position = new Vector2(0, 0) });
            //mapLevel1.Add(new Grass_Tile(textureTuile, new Vector2(0, 32)));
            playerTexture = Content.Load<Texture2D>("newAlphonse");
            playerTextureback = Content.Load<Texture2D>("newAlphonse-back");
            playerTexturefront = Content.Load<Texture2D>("newAlphonse-front");
            playerTextureleft = Content.Load<Texture2D>("newAlphonse-left");
            playerTextureright = Content.Load<Texture2D>("newAlphonse");
            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height / 2);
            Vector2 playerPosition = new Vector2(128, 128);
            player.Initialize(playerTexture, playerPosition);
            ennemiTexture = Content.Load<Texture2D>("ennemi");
            ennemiTextureTemp = Content.Load<Texture2D>("ennemi-bird");
            //Vector2 ennemiPosition = new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height / 2);
            projectileTexture = Content.Load<Texture2D>("projectile");
        }
        
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            kstate = Keyboard.GetState();
            prevKstate = kstate;
            if (player.Active)
            {
                for (var i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(gameTime);
                    if (!projectiles[i].Active || projectiles[i].Position.X > player.Position.X + projectiles[i].Range) 
                    {
                        projectiles.Remove(projectiles[i]);
                    }
                }
                UpdatePlayer(gameTime);
                UpdateEnnemis(gameTime);
                UpdateCollisions(gameTime);
                //foreach (var obj in objets)
                //{
                //    obj.Update(gameTime, playerRectangle);
                //}
            }
            /*
            player.Position.Y -= speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
            speedG -= Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            */
            base.Update(gameTime);
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            // rajouter les collisions 
            //UpdateCollisions(gameTime); 
            //Rectangle rectangleTemp = playerRectangle;
            float cameraDelta = (float)(CameraSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                if (player.MouvementBlocked == false)
                {
                    cameraMatrix *= Matrix.CreateTranslation(0, cameraDelta, 0);
                    player.Position.Y -= speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                //rectangleTemp.Y -= (int)speedG * (int)gameTime.ElapsedGameTime.TotalSeconds;
                //playerRectangle = rectangleTemp;

            }
            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                if (player.MouvementBlocked == false)
                {
                    cameraMatrix *= Matrix.CreateTranslation(0, -cameraDelta, 0);
                    player.Position.Y += speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                if (player.MouvementBlocked == false)
                {
                    cameraMatrix *= Matrix.CreateTranslation(cameraDelta, 0, 0);
                    player.Position.X -= speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                if (player.MouvementBlocked == false)
                {
                    cameraMatrix *= Matrix.CreateTranslation(-cameraDelta, 0, 0);
                    player.Position.X += speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            //projectiles 
            if (kstate.IsKeyDown(Keys.Space))
            {
                Tirer(gameTime);
            }

            //player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            //player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
        }
        
        public void Tirer(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - prevProjectileSpawn > projectileSpawn)
            {
                prevProjectileSpawn = gameTime.TotalGameTime;
                Projectile();
            }
        }

        public void Projectile()
        {
            Projectile projectile = new Projectile();
            var projectilePosition = player.Position;
            //position de depart du projectile
            projectilePosition.X += player.PlayerTexture.Width;
            projectilePosition.Y += player.PlayerTexture.Height;
            projectile.Initialize(projectileTexture, projectilePosition);
            projectiles.Add(projectile);
        }

        public void UpdateEnnemis(GameTime gameTime)
        {
            if (ennemis.Count < 6)
            {
                //prevEnnemiSpawn = gameTime.TotalGameTime;
                AjoutEnnemi();
            }

            for (int i = 0; i < ennemis.Count; i++)
            {
                ennemis[i].Update(gameTime, player, ennemis[i], ennemiTextureTemp, ennemiTexture);
                if (ennemis[i].Active == false)
                {
                    ennemis.RemoveAt(i);
                }
            }
        }

        public void AjoutEnnemi()
        {
            int r = random.Next(0, spawnable.Count);
            var ennemiPosition = new Vector2(spawnable[r].Position.X, spawnable[r].Position.Y);
            Ennemi ennemi = new Ennemi();
            ennemi.Initialize(ennemiTexture, ennemiPosition);
            ennemis.Add(ennemi);
        }

        //public void AjoutObjet(Objet.Types types)
        //{
        //    objets.Enqueue(Objet.Spawn(types));

        //}

        public void UpdateCollisions(GameTime gameTime)
        {
            /*var rect1 = { x:5, y:5, width:50, height:50 };
        var rect2 = { x:20, y:10, width:10, height:10 };
        if (rect1.x < rect2.x + rect2.width && rect1.x + rect1.width > rect2.x && rect1.y<rect2.y + rect2.height && rect1.height + rect1.y> rect2.y)
            ;*/

            playerRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Width - 10, player.Height - 10);
            //waterTiles.ForEach(w =>
            //{
            //    waterRectangle.Add(new Rectangle((int)w.Position.X, (int)w.Position.Y, w.textureTuile.Width, w.textureTuile.Height));
            //    if (!playerRectangle.Intersects(w.waterRectangle))
            //    {
            //        player.MouvementBlocked = true;
            //    }
            CollisionsMap(gameTime, playerRectangle);

            ennemis.ForEach(e =>
            {
                ennemiRectangle = new Rectangle((int)e.Position.X, (int)e.Position.Y, e.Width, e.Height);
                projectiles.ForEach(p =>
                {
                    projectileRectangle = new Rectangle((int)p.Position.X, (int)p.Position.Y, p.ProjectileWidth, p.ProjectileHeight);
                    if (projectileRectangle.Intersects(ennemiRectangle))
                    {
                        e.Health -= p.Damage;
                        p.Active = false;
                    }
                });
                if (playerRectangle.Intersects(ennemiRectangle))
                {
                    player.Health -= e.Damage;
                    e.Health = 0;
                    if (player.Health <= 0)
                    {
                        player.Active = false;
                    }
                }
            });
            //});
            foreach (var o in objets)
            {
                o.Update(gameTime, playerRectangle);
            }
        }
        //essayer de faire les collisions avec la map
        public void CollisionsMap(GameTime gameTime, Rectangle pPlayerRectangle)
        {
            waterTiles.ForEach(w =>
            {
                Rectangle playerRectTemp = pPlayerRectangle;
                waterRectangle = new Rectangle((int)w.Position.X, (int)w.Position.Y, w.textureTuile.Width, w.textureTuile.Height);
                if (!playerRectTemp.Intersects(waterRectangle))
                {
                    player.MouvementBlocked = false;
                    pPlayerRectangle = playerRectTemp;
                }
                else
                {
                    player.MouvementBlocked = true;
                    //playerRectTemp = pPlayerRectangle;
                }
            });
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cameraMatrix);
            foreach (var p in mapLevel1)
            {
                spriteBatch.Draw(p.textureTuile, p.Position, Color.White);
            }

            if (player.Active == true)
            {
                player.Draw(spriteBatch);
                if (kstate.IsKeyDown(Keys.Up))
                {
                    player.PlayerTexture = playerTextureback;
                }
                if (kstate.IsKeyDown(Keys.Down))
                {
                    player.PlayerTexture = playerTexturefront;
                }
                if (kstate.IsKeyDown(Keys.Left))
                {
                    player.PlayerTexture = playerTextureleft;
                }
                if (kstate.IsKeyDown(Keys.Right))
                {
                    player.PlayerTexture = playerTextureright;
                }

                for (int i = 0; i < ennemis.Count; i++)
                {
                    ennemis[i].Draw(spriteBatch);
                }

                foreach (var t in objets)
                {
                    t.Draw(spriteBatch);
                }

                foreach (var proj in projectiles)
                {
                    proj.Draw(spriteBatch);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
