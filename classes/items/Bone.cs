using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes.items
{
    public class Bone : Item
    {
        public Bone(Texture2D texture, Vector2Int position)
        {
            this.texture = texture;
            Position = position;
            
            sourceRectangle = new Rectangle(0, 32 + 1, 16, 16);
        }
    }
}