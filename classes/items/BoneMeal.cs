using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes.items
{
    public class BoneMeal : Item
    {
        public BoneMeal(Texture2D texture, Vector2Int position)
        {
            this.texture = texture;
            Position = position;
            
            sourceRectangle = new Rectangle(16 + 1, 32 + 2, 16, 16);
        }
    }
}