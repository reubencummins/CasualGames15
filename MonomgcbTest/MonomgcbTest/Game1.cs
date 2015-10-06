using GameEngine3D.Engine.Engines;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonomgcbTest
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        HubConnection Connection;
        IHubProxy proxy;

        Player _currentPlayer;
        List<Player> Players = new List<Player>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            Connection = new HubConnection("http://localhost:15366/");
            proxy = Connection.CreateHubProxy("GameHub");
            
        }
        
        
        protected override void Initialize()
        {

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (InputEngine.IsKeyPressed(Keys.D1) && _currentPlayer == null)
            {
                Task<Player> joinTask = Task.Factory.StartNew<Player>(() =>
                {
                    return proxy.Invoke<Player>("join", new object[] { "p1" }).Result;
                });
                joinTask.Wait();
                _currentPlayer = joinTask.Result;
            }
            


            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}
