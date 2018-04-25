using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test
{
    class Sprites
    {
        Texture2D textureSprite;
        float scale;
        int tempsEcoule;
        int frameTime;
        int frameCount;
        int currentFrame;
        Color couleur;
        Rectangle sourceRect = new Rectangle();
        Rectangle destinationRect = new Rectangle();
        public int FrameWidth;
        public int FrameHeight;
        public bool Active;
        public bool Looping;
        public Vector2 Position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            textureSprite = texture;
            Position = position;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            couleur = color;
            this.scale = scale;
            Looping = looping;
            tempsEcoule = 0;
            currentFrame = 0;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (Active == false) return;
            tempsEcoule += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (tempsEcoule > frameTime)
            {
                currentFrame++;
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    if (Looping == false) Active = false;
                }
                tempsEcoule = 0;
            }
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2, (int)Position.Y - (int)(FrameHeight * scale) / 2, (int)(FrameWidth * scale), (int)(FrameHeight * scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
                spriteBatch.Draw(textureSprite, destinationRect, sourceRect, couleur);
        }
    }
}
