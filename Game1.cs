using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using minecraft_inventory.classes;

namespace minecraft_inventory;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Dimensions2 windowSize;

    private Hotbar hotbar;
    private Inventory inventory;
    private Cursor cursor;

    private KeyboardManager keyboard;
    private MouseManager mouseManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        windowSize = new Dimensions2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
    }

    protected override void Initialize()
    {
        keyboard = new KeyboardManager();
        mouseManager = new MouseManager();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        hotbar = new Hotbar(this, new Vector2(windowSize.Width / 2f, windowSize.Height - 64f));
        
        Texture2D inventoryTex = Content.Load<Texture2D>("inventory");
        inventory = new Inventory(
            this, 
            inventoryTex,
            Content.Load<Texture2D>("items"),
            new Vector2(
                windowSize.Width / 2f - inventoryTex.Width * 2f / 2f,
                windowSize.Height / 2f - inventoryTex.Height * 2f / 1.5f 
            )
        );
        cursor = new Cursor();

        inventory.HotbarObservers.Add(hotbar);

        inventory.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        keyboard.PreUpdate();
        mouseManager.PreUpdate();
        
        if (!inventory.IsOpen)
        {
            hotbar.Update(keyboard);
        }
        else
        {
            cursor.Update(mouseManager);
            inventory.Update(mouseManager, cursor);
        }

        if (keyboard.IsKeyPress(Keys.E))
        {
            if (!inventory.IsOpen) inventory.IsOpen = true;
            else if (inventory.IsOpen)
            {   
                inventory.RestoreItemPositionOnClosing(cursor);
                inventory.IsOpen = false;
            }
        }
        
        keyboard.PostUpdate();
        mouseManager.PostUpdate();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        hotbar.Draw(_spriteBatch, gameTime);

        if (inventory.IsOpen)
        {
            inventory.Draw(_spriteBatch);
            cursor.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        hotbar.Dispose();
        inventory.Dispose();
        cursor.Dispose();
        base.Dispose(disposing);
    }
}
