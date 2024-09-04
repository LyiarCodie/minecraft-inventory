using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes.items
{
    public class Stick : Item
    {
        public Stick(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            
            sourceRectangle = new Rectangle(0, 0, 16, 16);
        }
    }
}