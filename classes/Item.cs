
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes
{
    public class Item
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle sourceRectangle;

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                texture, 
                new Rectangle((int) position.X, (int)position.Y, 16 * 2, 16 * 2), 
                sourceRectangle, 
                Color.White
            );
        }

        public void Dispose()
        {
            texture.Dispose();
        }
    }
}