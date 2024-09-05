using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory.classes
{
    internal class Cursor
    {
        public Vector2Int Position;
        public short OriginalSlotId = -1;
        public Slot[] OriginalSlots;
        public Item Item = null;
        public void Update(MouseManager mouse)
        {
            Position = mouse.Position;

            if (Item != null)
            {
                Item.Position.X = Position.X - Item.Width / 2;
                Item.Position.Y = Position.Y - Item.Height / 3;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (Item != null) Item.Draw(sb);
        }

        public void Dispose()
        {
            if (Item != null) Item.Dispose();
        }
    }
}