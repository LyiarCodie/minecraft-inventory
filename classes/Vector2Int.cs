using Microsoft.Xna.Framework;

namespace minecraft_inventory.classes
{
    public struct Vector2Int
    {
        public int X;
        public int Y;
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Vector2Int()
        {
            X = 0;
            Y = 0;
        }
        public readonly static Vector2Int Zero = new Vector2Int();

        public static Vector2Int Parse(Vector2 value)
        {
            return new Vector2Int((int) value.X, (int) value.Y);
        }
    }
}