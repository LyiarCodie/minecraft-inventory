using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace minecraft_inventory;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Dimensions2 windowSize;

    private Hotbar hotbar;

    private KeyboardManager keyboard;    

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
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        hotbar = new Hotbar(this, new Vector2(windowSize.Width / 2f, windowSize.Height - 64f));
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        keyboard.PreUpdate();
        
        hotbar.Update(keyboard);
        // if (keyboard.IsKeyPress(Keys.E))
        // {
        //     Console.WriteLine("Open/close inventory");
        // }
        
        keyboard.PostUpdate();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        hotbar.Draw(_spriteBatch, gameTime);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
