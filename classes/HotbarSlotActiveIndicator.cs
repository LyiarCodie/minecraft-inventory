using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraft_inventory;

internal class HotbarSlotActiveIndicator
{
    private readonly Texture2D texture;
    public Vector2 Position;
    public HotbarSlotActiveIndicator(Game1 game, Vector2 position)
    {
        Position = position;

        texture = game.Content.Load<Texture2D>("hotbar_current_slot_active");
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
    }

    public void Dispose()
    {
        texture.Dispose();
    }
}
