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
        Texture2D croissantTexture;
        Texture2D fromageTexture;
        Texture2D laitTexture;
        Texture2D soupeTexture;
        Texture2D grainesTexture;
        Vector2 positionTemporaire;
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
        //List<Rectangle> waterRectangle;
        //Rectangle waterRectangle;

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
            //waterRectangle = new List<Rectangle>();
            
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
                        mapLevel1.Add(new Water_Tile(waterTexture, new Vector2(y * 32, x * 32), new Rectangle(y * 32, x * 32, waterTexture.Width, waterTexture.Height)));
                        waterTiles.Add(new Water_Tile(waterTexture, new Vector2(y * 32, x * 32), new Rectangle(y * 32, x * 32, waterTexture.Width, waterTexture.Height)));
                    }
                    if (ligneMap[x].Substring(y, 1) == "R")
                    {
                        mapLevel1.Add(new Rock_Tile(rockTexture, new Vector2(y * 32, x * 32), new Rectangle(y * 32, x * 32, rockTexture.Width, rockTexture.Height)));
                        rockTiles.Add(new Rock_Tile(rockTexture, new Vector2(y * 32, x * 32), new Rectangle(y * 32, x * 32, rockTexture.Width, rockTexture.Height)));
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
            playerTexture = Content.Load<Texture2D>("newAlphonse");
            playerTextureback = Content.Load<Texture2D>("newAlphonse-back");
            playerTexturefront = Content.Load<Texture2D>("newAlphonse-front");
            playerTextureleft = Content.Load<Texture2D>("newAlphonse-left");
            playerTextureright = Content.Load<Texture2D>("newAlphonse");
            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height / 2);
            positionTemporaire = new Vector2(128, 128);
            Vector2 playerPosition = new Vector2(128, 128);
            player.Initialize(playerTexture, playerPosition);
            ennemiTexture = Content.Load<Texture2D>("ennemi");
            ennemiTextureTemp = Content.Load<Texture2D>("ennemi-bird");
            //Vector2 ennemiPosition = new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height / 2);
            projectileTexture = Content.Load<Texture2D>("projectile");
            croissantTexture = Content.Load<Texture2D>("croissant");
            fromageTexture = Content.Load<Texture2D>("fromage");
            laitTexture = Content.Load<Texture2D>("lait");
            soupeTexture = Content.Load<Texture2D>("soupe");
            grainesTexture = Content.Load<Texture2D>("graines");
            
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
                CollisionsMap(gameTime);
                UpdateObjets(gameTime);
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
                //position temporaire pour determiner si le mouvement est faisable
                positionTemporaire.Y -= speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (CollisionsMap(gameTime) == false)
                {
                    player.Position.Y = positionTemporaire.Y;
                    if (player.Position.Y > GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height / 2)
                    {
                        cameraMatrix *= Matrix.CreateTranslation(0, cameraDelta, 0);
                    }
                }
                else positionTemporaire.Y = player.Position.Y;
            }
            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                //position temporaire pour determiner si le mouvement est faisable
                positionTemporaire.Y += speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (CollisionsMap(gameTime) == false)
                {
                    player.Position.Y = positionTemporaire.Y;
                    if (player.Position.Y > GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height / 2)
                    {
                        cameraMatrix *= Matrix.CreateTranslation(0, -cameraDelta, 0);
                    }
                }
                else positionTemporaire.Y = player.Position.Y;
            }
            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                //position temporaire pour determiner si le mouvement est faisable
                positionTemporaire.X -= speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (CollisionsMap(gameTime) == false)
                {
                    player.Position.X = positionTemporaire.X;
                    if (player.Position.X > GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width / 2)
                    {
                        cameraMatrix *= Matrix.CreateTranslation(cameraDelta, 0, 0);
                    }
                }
                else positionTemporaire.X = player.Position.X;
            }
            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D)) //mouvement droite
            {
                //position temporaire pour determiner si le mouvement est faisable
                positionTemporaire.X += speedG * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (CollisionsMap(gameTime) == false)
                {
                    player.Position.X = positionTemporaire.X;
                    if (player.Position.X > GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width / 2)
                    {
                        cameraMatrix *= Matrix.CreateTranslation(-cameraDelta, 0, 0);
                    }
                }
                else positionTemporaire.X = player.Position.X;
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

        public void UpdateObjets(GameTime gameTime)
        {
            if (objets.Count < 5)
            {
                AjoutObjet();
            }
        }

        public void AjoutObjet()
        {
            //randomize la création d'objet
            int r = random.Next(0, dropLoot.Count);
            int max = random.Next(0, 5);
            switch (max)
            {
                case 0:
                    objets.Add(new Objet(croissantTexture, Objet.Types.Croissant, new Vector2(dropLoot[r].Position.X, dropLoot[r].Position.Y), new Rectangle((int)dropLoot[r].Position.X, (int)dropLoot[r].Position.Y, croissantTexture.Width, croissantTexture.Height)));
                    break;
                case 1:
                    objets.Add(new Objet(fromageTexture, Objet.Types.Fromage, new Vector2(dropLoot[r].Position.X, dropLoot[r].Position.Y), new Rectangle((int)dropLoot[r].Position.X, (int)dropLoot[r].Position.Y, fromageTexture.Width, fromageTexture.Height)));
                    break;
                case 2:
                    objets.Add(new Objet(laitTexture, Objet.Types.Lait, new Vector2(dropLoot[r].Position.X, dropLoot[r].Position.Y), new Rectangle((int)dropLoot[r].Position.X, (int)dropLoot[r].Position.Y, laitTexture.Width, laitTexture.Height)));
                    break;
                case 3:
                    objets.Add(new Objet(soupeTexture, Objet.Types.Soupe, new Vector2(dropLoot[r].Position.X, dropLoot[r].Position.Y), new Rectangle((int)dropLoot[r].Position.X, (int)dropLoot[r].Position.Y, soupeTexture.Width, soupeTexture.Height)));
                    break;
                case 4:
                    objets.Add(new Objet(grainesTexture, Objet.Types.Graines, new Vector2(dropLoot[r].Position.X, dropLoot[r].Position.Y), new Rectangle((int)dropLoot[r].Position.X, (int)dropLoot[r].Position.Y, grainesTexture.Width, grainesTexture.Height)));
                    break;
            }
            
        }

        public void UpdateCollisions(GameTime gameTime)
        {
            /*var rect1 = { x:5, y:5, width:50, height:50 };
        var rect2 = { x:20, y:10, width:10, height:10 };
        if (rect1.x < rect2.x + rect2.width && rect1.x + rect1.width > rect2.x && rect1.y<rect2.y + rect2.height && rect1.height + rect1.y> rect2.y)
            ;*/

            playerRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Width - 10, player.Height - 10);

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
            foreach (var o in objets)
            {
                o.Update(gameTime, playerRectangle);
            }
        }

        //essayer de faire les collisions avec la map
        public bool CollisionsMap(GameTime gameTime)
        {
            Rectangle playerRectTemp;
            
            foreach (var w in waterTiles)
            {
                playerRectTemp.X = (int)positionTemporaire.X;
                playerRectTemp.Y = (int)positionTemporaire.Y;
                playerRectTemp.Width = playerTexture.Width - 5;
                playerRectTemp.Height = playerTexture.Height - 5;
                if (playerRectTemp.Intersects(w.waterRectangle))
                {
                    //empecher le mouvement
                    return true;
                }
            }
            foreach (var r in rockTiles)
            {
                playerRectTemp.X = (int)positionTemporaire.X;
                playerRectTemp.Y = (int)positionTemporaire.Y;
                playerRectTemp.Width = playerTexture.Width - 5;
                playerRectTemp.Height = playerTexture.Height - 5;
                if (playerRectTemp.Intersects(r.rockRectangle))
                {
                    //empecher le mouvement
                    return true;
                }
            }
            // autoriser le mouvement
            return false;

            //for (int i = 0; i < waterTiles.Count; i++) 
            ////waterTiles.ForEach(w =>
            //{
            //    //instancier tous les rectangles TODO mettre dans une liste
            //    waterRectangle.Add(new Rectangle((int)waterTiles[i].Position.X, (int)waterTiles[i].Position.Y, waterTiles[i].textureTuile.Width, waterTiles[i].textureTuile.Height));
            //}
            //);
            //for (int i = 0; i < waterTiles.Count; i++) 
            ////foreach (var water in waterTiles)
            //{
            //    //verifier si le rectangle touche une tuile d'eau

            //    waterRectangle.Add(new Rectangle(waterTiles[i].waterRectangle.X, waterTiles[i].waterRectangle.Y, waterTiles[i].textureTuile.Width, waterTiles[i].textureTuile.Height));
            //}
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
            //foreach (var w in waterTiles)
            //{
            //    spriteBatch.Draw(w.textureTuile, w.Position, Color.White);
            //}

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
