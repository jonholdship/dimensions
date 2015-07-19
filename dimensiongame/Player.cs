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
		private int nfloor;
		private Vector2 movedir;
		private Vector2 movespeed;
		private float grav;
		private bool ground;
		private float jump, xpace,termv;
		private Texture2D playersprite;
		private Rectangle playerpos;
		KeyboardState keystate;

		//creator obviously
		public Player()
		{
			xpace = 10;
			jump = 20f;
			playerpos.X =50;
			playerpos.Y = 900;
			movespeed.X = xpace;
			movespeed.Y = 0;
			movedir.Y = 0;
			movedir.X = 0;
			grav = 1f;
			termv = 20;
			ground = false;
			playerpos.Height = 80;
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

		//really think this function is redundant due to movever
		private void Checkground (Level level)
		{	
			//assume player is in air, then check to see if it isn't
			nfloor=0;
			playerpos.Y++;
			tiles = level.GetTile (playerpos, 'b');
			foreach (int tile in tiles) {
				if (tile == 1) {
					ground = true;
					nfloor++;
				}
			}
			if (nfloor == 0) {
				ground = false;
			} else {
				playerpos.Y--;
			}
		}

		private void Movehor(Level level)
		{
			//check for L/R keypress and do correct tile check for direction.
			if (keystate.IsKeyDown (Keys.Left) == true) {
				tiles = level.GetTile (playerpos, 'l');
				movespeed.X = -xpace;
			} else if (keystate.IsKeyDown (Keys.Right) == true) {
				tiles = level.GetTile (playerpos, 'r');
				movespeed.X = xpace;
			} else {
				movespeed.X = 0;
			}

			//change player position by movespeed
			playerpos.X += (int)(movespeed.X);

			//check results of GetTile and undo position change if it goes through wall.
			foreach (int tile in tiles) {
				if (tile == 1) {
					playerpos.X -= (int)movespeed.X;
					movespeed.X = 0;
				}
			}
		}

		private void Movever(Level level)
		{
			//as a soon as we hit ground, ground becomes true. this allows player to jump
			//if statement therefore blocks double jumping
			if (ground == true) {
				if (keystate.IsKeyDown (Keys.Up) == true) {
					movespeed.Y = -jump;
					ground = false;
				}
				//if we're not on ground, gravity accelerated down up to terminal velocity
			} else {
				if (movespeed.Y < termv) {
					movespeed.Y += grav;
				}				
			}

			//change player position by move speed
			playerpos.Y += (int)movespeed.Y;
		
			//do the correct tile check depending on move direction
			if (movespeed.Y < 0) {
				tiles = level.GetTile (playerpos, 't');
			} else {
				tiles = level.GetTile (playerpos, 'b');
			}

			//check results of GetTile and undo change to position if we hit a wall
			foreach (int tile in tiles) {
				if (tile == 1) {
					playerpos.Y -= (int)movespeed.Y;
					movespeed.Y = 0;
					ground = true;
				}
			}
		}			
	}
}
			
#endregion internal functions