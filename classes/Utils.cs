using Microsoft.Xna.Framework;

namespace minecraft_inventory.classes
{
    internal class Utils
    {
        public static Vector2 Parse(Vector2Int value)
        {
            return new Vector2(value.X, value.Y);
        }
    }
}