using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minecraft_inventory.classes.items;

namespace minecraft_inventory
{
    internal class Inventory
    {
        public List<Observer> Observers = new List<Observer>();

        public bool IsOpen = false;

        private Texture2D texture;
        private Texture2D itemsTexture;
        public Vector2 Position;

        private Slot[] storageSlots;
        private Slot[] hotbarSlots;
        
        public Inventory(Game1 game, Texture2D texture, Texture2D itemsTexture, Vector2 position)
        {
            Position = position;
            this.texture = texture;
            this.itemsTexture = itemsTexture;

            storageSlots = new Slot[27];
            hotbarSlots = new Slot[9];

            float currentRow = 0f;
            float currentCol = 0f;
            for (byte i = 0; i < storageSlots.Length; i++)
            {
                float scaleFactor = 2f;
                float horizontalGap = 2f * scaleFactor * currentCol;
                float nextSlotX = 16f * scaleFactor * currentCol;
                float actuallySlotX = 8f * scaleFactor;

                float verticalGap = 2f * scaleFactor * currentRow;
                float nextSlotY = 16f * scaleFactor * currentRow;
                float actuallySlotY = 84f * 2f;

                storageSlots[i] = new Slot(
                    game, 
                    new Vector2(
                        position.X + actuallySlotX + horizontalGap + nextSlotX,
                        position.Y + actuallySlotY + verticalGap + nextSlotY
                    ),
                    new Dimensions2(16*(int)scaleFactor, 16*(int)scaleFactor),
                    Color.Orange
                );

                currentCol++;

                if ((i + 1) % 9f == 0) {
                    currentRow++;
                    currentCol = 0f;
                }

            }
        
            for (byte i = 0; i < hotbarSlots.Length; i++)
            {
                hotbarSlots[i] = new Slot(
                    game,
                    new Vector2(
                        position.X + 8 * 2 + (2 * 2 * i) + (16 * 2 * i),
                        position.Y + 142 * 2f
                    ),
                    new Dimensions2(16 * 2, 16 * 2),
                    Color.Green
                );
            }
        
            PopulateInventory();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                texture, 
                new Rectangle((int) Position.X, (int) Position.Y, texture.Width * 2, texture.Height * 2),
                Color.White
            );
            foreach (var slot in storageSlots) { slot.Draw(sb); }
            foreach (var slot in hotbarSlots) { slot.Draw(sb); }
        }

        private void PopulateInventory()
        {
            storageSlots[2].Item = new Stick(itemsTexture, storageSlots[2].Position);
        }

        public void Dispose()
        {
            foreach (var slot in storageSlots) { slot.Dispose(); }
            foreach (var slot in hotbarSlots) { slot.Dispose(); }
            texture.Dispose();
        }
    }
}