using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes.items
{
    public class Coal : Item
    {
        public Coal(Texture2D texture, Vector2Int position)
        {
            this.texture = texture;
            Position = position;
            
            sourceRectangle = new Rectangle(16 + 1, 0, 16, 16);
        }
    }
}