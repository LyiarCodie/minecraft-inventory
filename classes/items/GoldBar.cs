using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes.items
{
    public class GoldBar : Item
    {
        public GoldBar(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            
            sourceRectangle = new Rectangle(0, 16 + 1, 16, 16);
        }
    }
}