using Microsoft.Xna.Framework.Input;

namespace minecraft_inventory;

internal class KeyboardManager
{
    private struct Key
    {
        public bool press = false;
        public bool hold = false;
        public Keys key;

        public Key(Keys key)
        {
            this.key = key;
        }
    }

    private Key[] keys;

    public KeyboardManager()
    {
        keys = new Key[10];
        keys[0] = new Key(Keys.D1);
        keys[1] = new Key(Keys.D2);
        keys[2] = new Key(Keys.D3);
        keys[3] = new Key(Keys.D4);
        keys[4] = new Key(Keys.D5);
        keys[5] = new Key(Keys.D6);
        keys[6] = new Key(Keys.D7);
        keys[7] = new Key(Keys.D8);
        keys[8] = new Key(Keys.D9);
        keys[9] = new Key(Keys.E);
    }

    public void PreUpdate()
    {
        var kbstate = Keyboard.GetState();
        for (int i = 0; i < keys.Length; i++)
        {
            if (kbstate.IsKeyDown(keys[i].key)  && !keys[i].hold)
            {
                keys[i].hold = true;
                keys[i].press = true;
            }
        }
    }

    public void PostUpdate()
    {
        var kbstate = Keyboard.GetState();

        for (int i = 0; i < keys.Length; i++)
        {
            if (kbstate.IsKeyUp(keys[i].key) && keys[i].hold)
            {
                keys[i].hold = false;
                keys[i].press = false;
            }
        }

        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].press = false;
        }
    }

    public bool IsKeyPress(Keys key)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].key == key && keys[i].press)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsKeyDown(Keys key)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].key == key && keys[i].hold)
            {
                return true;
            }
        }
        return false;
    }
}