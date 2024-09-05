
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes
{
    public class Item
    {
        protected Texture2D texture;
        public Vector2Int Position;
        protected Rectangle sourceRectangle;

        public int Width => texture.Width;
        public int Height => texture.Height;

        public Item ShallowCopy()
        {
            return (Item)MemberwiseClone();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                texture, 
                new Rectangle(Position.X, Position.Y, 16 * 2, 16 * 2), 
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