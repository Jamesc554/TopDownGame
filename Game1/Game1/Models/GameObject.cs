﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Game1
{
    public class GameObject
    {
        private Texture2D sprite;
        public string spriteLocation;
        public Vector2 position;
        private float angle; // Value between 0 - 360 (Degrees)
        public string name;

        public static object TextureUsage { get; private set; }

        public GameObject(string _spriteLocation, Vector2 _position, float _angle = 0)
        {
            spriteLocation = _spriteLocation;
            position = _position;
            angle = _angle;
        }

        public void LoadContent(ContentManager content)
        {
            using (FileStream filestream = new FileStream(spriteLocation, FileMode.Open))
            {
                sprite = Texture2D.FromStream(Game1.graphics.GraphicsDevice, filestream);
            };
        }

        public void UnloadContent(ContentManager content)
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 newPos = new Vector2(position.X + (sprite.Width / 2), position.Y + (sprite.Height / 2));
            spriteBatch.Draw(sprite, newPos, new Rectangle(0, 0, sprite.Width, sprite.Height), Color.White, angle, new Vector2(sprite.Width / 2, sprite.Height / 2), 1.0f, SpriteEffects.None, 1);
        }
    }
}
