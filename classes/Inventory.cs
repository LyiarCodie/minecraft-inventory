using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minecraft_inventory.classes;
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

        private short lastHoveredSlotId = -1;
        
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
            storageSlots[2].Item = new Stick(itemsTexture, Vector2Int.Parse(storageSlots[2].Position));
            storageSlots[15].Item = new Coal(itemsTexture, Vector2Int.Parse(storageSlots[15].Position));
        }

        public void RestoreItemPositionOnClosing(Cursor cursor)
        {
            if (cursor.OriginalSlotId >= 0)
            {
                storageSlots[cursor.OriginalSlotId].Item = cursor.Item;
                storageSlots[cursor.OriginalSlotId].Item.Position = Vector2Int.Parse(storageSlots[cursor.OriginalSlotId].Position);
                cursor.Item = null;
                cursor.OriginalSlotId = -1;
            }
        }

        public void HoverSlot(byte slotId)
        {
            if (lastHoveredSlotId != slotId)
            {
                lastHoveredSlotId = slotId;
            }
        }

        public void TakeSlotItem(byte slotId, Cursor cursor)
        {
            if (storageSlots[slotId].Item != null && cursor.Item == null)
            {
                cursor.Item = storageSlots[slotId].Item;
                storageSlots[slotId].Item = null;
                cursor.OriginalSlotId = slotId;
            }
        }
        public void SwapSlotItem(byte slotId, Cursor cursor)
        {
            if (storageSlots[slotId].Item != null && cursor.Item != null)
            {
                var slotItem = storageSlots[slotId].Item;
                storageSlots[slotId].Item = cursor.Item;
                storageSlots[slotId].Item.Position = Vector2Int.Parse(storageSlots[slotId].Position);
                cursor.Item = slotItem;
                cursor.OriginalSlotId = slotId;
            }
        }

        public void PutItemOnSlot(byte slotId, Cursor cursor)
        {
            if (storageSlots[slotId].Item == null && cursor.Item != null)
            {
                storageSlots[slotId].Item = cursor.Item;
                storageSlots[slotId].Item.Position = Vector2Int.Parse(storageSlots[slotId].Position);
                cursor.Item = null;
                cursor.OriginalSlotId = -1;
            }
        }

        public void Update(MouseManager mouse, Cursor cursor)
        {
            for (byte i = 0; i < storageSlots.Length; i++)
            {
                if (storageSlots[i].Intersects(cursor.Position))
                {
                    HoverSlot(i);

                    if (mouse.IsButtonPress("Left"))
                    {
                        if (storageSlots[i].Item != null && cursor.Item == null) TakeSlotItem(i, cursor);
                        else if (storageSlots[i].Item != null && cursor.Item != null) SwapSlotItem(i, cursor);
                        else if (storageSlots[i].Item == null && cursor.Item != null) PutItemOnSlot(i, cursor);
                        
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var slot in storageSlots) { slot.Dispose(); }
            foreach (var slot in hotbarSlots) { slot.Dispose(); }
            texture.Dispose();
        }
    }
}