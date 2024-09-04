using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes.items
{
    public class GoldNugget : Item
    {
        public GoldNugget(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            
            sourceRectangle = new Rectangle(16 + 1, 16 + 1, 16, 16);
        }
    }
}