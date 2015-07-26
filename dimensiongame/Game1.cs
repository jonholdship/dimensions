#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace dimensiongame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spritebatch;
		//we separately define size of window and size of total world in the level
		const int windowwidth = 1000;
		const int windowheight = 1000;
		int worldwidth,worldheight;
		//Player and level need to know size of window.
		Level level = new Level(windowwidth,windowheight);
		Player player = new Player();
		Enemy enemy = new Enemy();
		Camera camera;


		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			base.Initialize ();
			graphics.PreferredBackBufferHeight = windowheight ;
			graphics.PreferredBackBufferWidth = windowwidth;
			Viewport viewport = new Viewport ();
			viewport.Height = windowheight;
			viewport.Width = windowwidth;
			viewport.X = 0;
			viewport.Y = 0;

			worldheight = (int)level.GetLevelSize ().X;
			worldwidth = (int)level.GetLevelSize ().Y;
			camera = new Camera (viewport, worldwidth, worldheight);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spritebatch = new SpriteBatch (GraphicsDevice);
			player.LoadContent (Content);
			level.LoadContent (Content);
			enemy.LoadContent (Content);
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif
			// TODO: Add your update logic here	
			player.Update(level,camera);
			enemy.Update (level,player);
			base.Update (gameTime);

			if (player.dead == true) {
				Exit ();
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
			spritebatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,camera.GetTransformation());
			//spritebatch.Begin();
			//TODO: Add your drawing code here
			level.Draw (spritebatch);
			player.Draw(spritebatch);
			enemy.Draw (spritebatch);
			spritebatch.End();
			base.Draw (gameTime);
		}
	}
}



