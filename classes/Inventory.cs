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
        public List<Observer> HotbarObservers = new List<Observer>();

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

        public void LoadContent()
        {
            Slot[] slots = new Slot[hotbarSlots.Length];

            for (int i = 0; i < slots.Length; i++)
            {
                if (hotbarSlots[i].Item != null)
                {
                    slots[i].Item = hotbarSlots[i].Item.ShallowCopy();
                }
            }

            foreach (var observer in HotbarObservers)
            {
                observer.UpdateByNotifier(new Message(MessageOrder.GET_CURRENT, slots));
            }
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

            hotbarSlots[0].Item = new GoldBar(itemsTexture, Vector2Int.Parse(hotbarSlots[0].Position));
            hotbarSlots[3].Item = new BoneMeal(itemsTexture, Vector2Int.Parse(hotbarSlots[3].Position));
        }

        public void RestoreItemPositionOnClosing(Cursor cursor)
        {
            if (cursor.OriginalSlotId >= 0)
            {
                cursor.OriginalSlots[cursor.OriginalSlotId].Item = cursor.Item;
                cursor.OriginalSlots[cursor.OriginalSlotId].Item.Position = Vector2Int.Parse(cursor.OriginalSlots[cursor.OriginalSlotId].Position);
                cursor.Item = null;
                cursor.OriginalSlotId = -1;
                cursor.OriginalSlots = null;
            }
        }

        public void HoverSlot(byte slotId)
        {
            if (lastHoveredSlotId != slotId)
            {
                lastHoveredSlotId = slotId;
            }
        }

        private void TakeSlotItem(Slot[] slots, byte slotId, Cursor cursor)
        {
            if (slots[slotId].Item != null && cursor.Item == null)
            {
                cursor.Item = slots[slotId].Item;
                slots[slotId].Item = null;
                cursor.OriginalSlotId = slotId;
                cursor.OriginalSlots = slots;
            }
        }
        private void SwapSlotItem(Slot[] slots, byte slotId, Cursor cursor)
        {
            if (slots[slotId].Item != null && cursor.Item != null)
            {
                var slotItem = slots[slotId].Item;
                slots[slotId].Item = cursor.Item;
                slots[slotId].Item.Position = Vector2Int.Parse(slots[slotId].Position);
                cursor.Item = slotItem;
            }
        }

        private void PutItemOnSlot(Slot[] slots, byte slotId, Cursor cursor)
        {
            if (slots[slotId].Item == null && cursor.Item != null)
            {
                slots[slotId].Item = cursor.Item;
                slots[slotId].Item.Position = Vector2Int.Parse(slots[slotId].Position);
                cursor.Item = null;
                cursor.OriginalSlotId = -1;
                cursor.OriginalSlots = null;
            }
        }

        private void StorageSlotsClick(Cursor cursor, MouseManager mouse)
        {
            for (byte i = 0; i < storageSlots.Length; i++)
            {
                if (storageSlots[i].Intersects(cursor.Position))
                {
                    HoverSlot(i);

                    if (mouse.IsButtonPress("Left"))
                    {
                        if (storageSlots[i].Item != null && cursor.Item == null) TakeSlotItem(storageSlots, i, cursor);
                        else if (storageSlots[i].Item != null && cursor.Item != null) SwapSlotItem(storageSlots, i, cursor);
                        else if (storageSlots[i].Item == null && cursor.Item != null) PutItemOnSlot(storageSlots, i, cursor);
                    }
                }
            }
        }

        private void hotbarSlotsClick(Cursor cursor, MouseManager mouse)
        {
            for (byte i = 0; i < hotbarSlots.Length; i++)
            {
                if (hotbarSlots[i].Intersects(cursor.Position))
                {
                    HoverSlot(i);

                    if (mouse.IsButtonPress("Left"))
                    {
                        if (hotbarSlots[i].Item != null && cursor.Item == null) 
                        {
                            TakeSlotItem(hotbarSlots, i, cursor);
                            foreach (var observer in HotbarObservers)
                            {
                                observer.UpdateByNotifier(new Message(MessageOrder.PICK_UP, i));
                            }
                        }
                        else if (hotbarSlots[i].Item != null && cursor.Item != null)
                        {
                            foreach (var observer in HotbarObservers)
                            {
                                observer.UpdateByNotifier(new Message(MessageOrder.EXCHANGE, i, cursor.Item.ShallowCopy()));
                            }
                            SwapSlotItem(hotbarSlots, i, cursor);
                        }
                        else if (hotbarSlots[i].Item == null && cursor.Item != null)
                        {
                            foreach (var observer in HotbarObservers)
                            {
                                observer.UpdateByNotifier(new Message(MessageOrder.PUT, i, cursor.Item.ShallowCopy()));
                            }
                            PutItemOnSlot(hotbarSlots, i, cursor);
                        }
                    }
                }
            }
        }

        public void Update(MouseManager mouse, Cursor cursor)
        {
            StorageSlotsClick(cursor, mouse);
        
            hotbarSlotsClick(cursor, mouse);
        }

        public void Dispose()
        {
            foreach (var slot in storageSlots) { slot.Dispose(); }
            foreach (var slot in hotbarSlots) { slot.Dispose(); }
            texture.Dispose();
        }
    }
}