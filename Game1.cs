using System;
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

    private KeyboardManager keyboard;
    private MouseManager mouse;

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
        mouse = new MouseManager();
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
                windowSize.Height / 2f - inventoryTex.Height * 2f / 2f
            )
        );

        inventory.Observers.Add(hotbar);
    }

    protected override void Update(GameTime gameTime)
    {
        keyboard.PreUpdate();
        mouse.PreUpdate();
        
        if (!inventory.IsOpen)
        {
            hotbar.Update(keyboard);
        }

        if (keyboard.IsKeyPress(Keys.E))
        {
            inventory.IsOpen = !inventory.IsOpen;
        }
        
        keyboard.PostUpdate();
        mouse.PostUpdate();

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
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        hotbar.Dispose();
        inventory.Dispose();
        base.Dispose(disposing);
    }
}
