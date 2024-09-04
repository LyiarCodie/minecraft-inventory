using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace minecraft_inventory;

internal class Hotbar : Observer
{
    private Vector2 position;
    private Texture2D texture;
    public int Width { get => texture.Width; }
    public int Height { get => texture.Height; }
    private Slot[] slots;

    private HotbarSlotActiveIndicator slotActiveIndicator;
    private byte slotActiveId;

    private Keys[] digitKeys;

    // observer notification
    public void UpdateHotbar(string message)
    {

    }
    
    public Hotbar(Game1 game, Vector2 position)
    {
        this.position = position;
        texture = game.Content.Load<Texture2D>("hotbar");
        slots = new Slot[9];
        float slotWidth = 16 * 2;
        float slotHeight = 16 * 2;
        for (int i = 0; i < slots.Length;i++)
        {
            slots[i] = new Slot(
                game, 
                new Vector2(position.X - texture.Width * 2f / 2f + 3 * 2 + (slotWidth * i + 4 * 2 * i), 
                            position.Y- texture.Height * 2f / 2f + 3 * 2),
                new Dimensions2((int) slotWidth, (int) slotHeight),
                Color.Red
            );
        }

        slotActiveId = 0;
        slotActiveIndicator = new HotbarSlotActiveIndicator(
            game, 
            new Vector2(
                slots[slotActiveId].Position.X - 4f * 2f,
                slots[slotActiveId].Position.Y - 4f * 2f
            )
        );

        digitKeys = new Keys[9];
        digitKeys[0] = Keys.D1;
        digitKeys[1] = Keys.D2;
        digitKeys[2] = Keys.D3;
        digitKeys[3] = Keys.D4;
        digitKeys[4] = Keys.D5;
        digitKeys[5] = Keys.D6;
        digitKeys[6] = Keys.D7;
        digitKeys[7] = Keys.D8;
        digitKeys[8] = Keys.D9;
    }

    public void Update(KeyboardManager kb)
    {
        for (byte i = 0; i < digitKeys.Length; i++)
        {
            if (kb.IsKeyPress(digitKeys[i]))
            {
                MoveSlotIndicator(i);
            }
        }
    }

    private void MoveSlotIndicator(byte id)
    {
        if (id != slotActiveId)
        {
            slotActiveId = id;

            slotActiveIndicator.Position.X = slots[slotActiveId].Position.X - 4f * 2f;
            slotActiveIndicator.Position.Y = slots[slotActiveId].Position.Y - 4f * 2f;
        }
    }

    public void Draw(SpriteBatch sb, GameTime gameTime)
    {
        sb.Draw(
            texture, 
            position, 
            null, 
            Color.White, 
            0f, 
            new Vector2(texture.Width / 2f, texture.Height / 2f),
            2f,
            SpriteEffects.None,
            0f
        );

        slotActiveIndicator.Draw(sb);

        foreach (var slot in slots)
        {
            slot.Draw(sb);
        }

    }

    public void Dispose()
    {
        texture.Dispose();
        slotActiveIndicator.Dispose();

        foreach (var slot in slots)
        {
            slot.Dispose();
        }
    }

    public void UpdateByNotifier()
    {
        throw new NotImplementedException();
    }
}