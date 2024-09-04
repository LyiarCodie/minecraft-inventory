using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes.items
{
    public class Stick : Item
    {
        public Stick(Texture2D texture, Vector2Int position)
        {
            this.texture = texture;
            Position = position;
            
            sourceRectangle = new Rectangle(0, 0, 16, 16);
        }
    }
}