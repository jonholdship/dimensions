using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace dimensiongame
{
	public class Player
	{
		//rectangles are (more or less) an (x,y) point, a width and a height.
		//there are multiple ways to draw something to screen but I find a target rectangle easiest.
		//see the draw function for how it's called
		private int[] tiles;
		private Vector2 movedir;
		private Vector2 movespeed;
		private float grav;
		private bool ground;
		private float jump, xpace;
		private Texture2D playersprite;
		private Rectangle playerpos;
		KeyboardState keystate;

		//creator obviously
		public Player()
		{
			xpace = 1;
			jump = 5;
			playerpos.X =100;
			playerpos.Y = 100;
			movespeed.X = xpace;
			movespeed.Y = 0;
			movedir.Y = 0;
			movedir.X = 0;
			grav = 1f;
			ground = false;
			playerpos.Height = 50;
			playerpos.Width = 50;
		}

		//main required functions. LoadConent at start of game. Update and draw each timestep
		public void LoadContent(ContentManager content)
		{
			playersprite=content.Load<Texture2D>("playsprite");
		}

		//update works out things like character movement
		public void Update(Level level)
		{
			//find out what player is pressing
			keystate = Keyboard.GetState ();
			//see if we're stood on ground
			Checkground (level);
			//simple horizontal movement
			Movehor(level);
			//jumping and falling
			Movever(level);

		}

		//Draw puts out player on the screen. Nothing complicated to so far.
		public void Draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw (playersprite,playerpos,Color.White);
		}


#region internal functions
//Below are random functions to keep main class logic clear above 
		//asks for keyboard state and sets movement speed.

		private void Checkground (Level level)
		{
			ground = false;
			playerpos.Y += (int)grav;
			tiles = level.GetTile (playerpos, 'b');
			foreach (int tile in tiles) {
				if (tile == 1) {
					ground = true;
					grav = 1f;
				}
			}
			if (ground == true) {
				playerpos.Y -= (int)grav;
			}
		}

		private void Movehor(Level level)
		{
			if (keystate.IsKeyDown (Keys.Left) == true) {
				movedir.X = -1;
			} else if (keystate.IsKeyDown (Keys.Right) == true) {
				movedir.X = 1;
			} else {
				movedir.X = 0;
			}

			if (movedir.X > 0) {
				tiles = level.GetTile (playerpos, 'r');
			} else {
				tiles = level.GetTile (playerpos, 'l');
			}
			foreach (int tile in tiles) {
				if (tile == 1) {
					playerpos.X -= (int)movedir.X;
					movedir.X = 0;
				}
			}
			playerpos.X += (int)(movedir.X * movespeed.X);
		}

		private void Movever(Level level)
		{
			if (ground == true) {
				movedir.Y = 0;
				movespeed.Y = jump;
				if (keystate.IsKeyDown (Keys.Up) == true) {
					movedir.Y = -1;
				}
			}
			else{
				if (movespeed.Y != 0) {
					movespeed.Y -= grav;
				}				
			}
		}
			
	}
}
			
#endregion internal functions