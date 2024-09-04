using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace minecraft_inventory.classes
{
    internal class MouseManager
    {
        public Vector2Int Position { get; private set; } = Vector2Int.Zero;

        private struct Button
        {
            public bool hold;
            public bool press;
        }

        private Button leftButton = new Button();
        private Button rightButton = new Button();

        public void PreUpdate()
        {
            var mstate = Mouse.GetState();
            if (mstate.LeftButton == ButtonState.Pressed && !leftButton.hold)
            {
                leftButton.hold = true;
                leftButton.press = true;
            }
            if (mstate.RightButton == ButtonState.Pressed && !rightButton.hold)
            {
                rightButton.hold = true;
                rightButton.press = true;
            }

            Position = new Vector2Int(mstate.X, mstate.Y);
        }

        // button:
        //   "Left" | "Right"
        public bool IsButtonPress(string button)
        {
            if (button == "Left") {
                return leftButton.press;
            }
            else if (button == "Right") {
                return rightButton.press;
            }

            return false;
        }

        public void PostUpdate()
        {
            var mstate = Mouse.GetState();

            if (mstate.LeftButton == ButtonState.Released && leftButton.hold)
            {
                leftButton.hold = false;
                leftButton.press = false;
            }

            if (mstate.RightButton == ButtonState.Released && rightButton.hold)
            {
                rightButton.hold = false;
                rightButton.press = false;
            }

            rightButton.press = false;
            leftButton.press = false;
        }
    }
}