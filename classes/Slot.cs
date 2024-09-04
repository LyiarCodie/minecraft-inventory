using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using minecraft_inventory.classes;

namespace minecraft_inventory;

internal struct Slot
{
    public Rectangle rect;
    public Item Item = null;

    private Color[] colorData;
    private Texture2D texture;
    private Color _color;
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            for (int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = Color.Transparent;
            }

            //horizontal borders
            for (int i = 0; i < rect.Width;i++)
            {
                // top
                colorData[i] = value;
                // bottom
                colorData[(rect.Height * rect.Width) - rect.Width + i] = value;
            }

            // vertical borders
            for (int i = 0; i < rect.Height; i++)
            {
                // right
                colorData[rect.Width - 1 + (rect.Width * i)] =value;
                // left
                colorData[rect.Width * i] = value;
            }
            texture.SetData(colorData);
        }
    }
    private Vector2 _position;
    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            rect.X = (int) value.X;
            rect.Y = (int) value.Y;
        }
    }

    public Slot(Game1 game, Vector2 position, Dimensions2 size, Color color)
    {
        rect = new Rectangle((int) position.X, (int)position.Y, size.Width, size.Height);
        texture = new Texture2D(game.GraphicsDevice, rect.Width, rect.Height);
        colorData = new Color[size.Width * size.Height];
    
        Position = position;
        Color = color;
    }

    public bool Intersects(Vector2Int position)
    {
        return position.X > rect.Left && position.X < rect.Right && position.Y > rect.Top && position.Y < rect.Bottom;
    }

    public bool Intersects(int x, int  y)
    {
        return x > rect.Left && x < rect.Right && y > rect.Top && y < rect.Bottom;
    }

    public void Update()
    {
        Console.WriteLine("OLHA A XERECA!");
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, Position, Color);

        if (Item != null)
        {
            Item.Draw(sb);
        }
    }

    public void Dispose()
    {
        texture.Dispose();
        if (Item != null)
        {
            Item.Dispose();
        }
    }
}