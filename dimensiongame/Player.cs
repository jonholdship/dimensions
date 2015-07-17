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
		private Texture2D playersprite;
		private Rectangle playerpos;
		KeyboardState keystate;

		//creator obviously
		public Player()
		{
			playerpos.X =100;
			playerpos.Y = 100;
			movespeed.X = 1;
			movespeed.Y = 100;
			movedir.Y = 0;
			movedir.X = 0;
			grav = -0.3f;
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
			Getinput();
			Checkmove (level);

		}

		//Draw puts out player on the screen. Nothing complicated to so far.
		public void Draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw (playersprite,playerpos,Color.White);
		}


#region internal functions
//Below are random functions to keep main class logic clear above 
		//asks for keyboard state and sets movement speed.
		private void Getinput()
		{
			//reset x and check button presses
			movedir.X = 0;
			keystate = Keyboard.GetState ();

			//vertical motion. Allow jumps on ground and remove speed if not (ie gravity).
			if (ground == true) {
				grav = -Math.Abs (grav);
				movedir.Y = 0;
				movespeed.Y = 3;
				if (keystate.IsKeyDown (Keys.Up) == true) {
					movedir.Y = -1;
				}
				if (keystate.IsKeyDown (Keys.Space) == true) {
					movedir.Y = -1;
				}
			}
			else{
				movespeed.Y += grav;
				if (movespeed.Y < 0) {
					movedir.Y = 1;
					movespeed.Y *= -1;
					grav *= -1;
				}
			}

			//more simple for x motion. just move in direction of button press
			if (keystate.IsKeyDown (Keys.Left) == true) {
				movedir.X = -1;
			}
			if (keystate.IsKeyDown (Keys.Right) == true) {
				movedir.X = 1;
			}
		}

		private void Checkmove(Level level)
		{
			playerpos.X += (int)movedir.X;
			playerpos.Y += (int)movedir.Y;

			//this is messy as shit.
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

			if (movedir.Y > 0) {
				tiles = level.GetTile (playerpos, 'b');
				foreach (int tile in tiles) {
					if (tile == 1) {
						playerpos.Y -= (int)movedir.Y;
						ground = true;
					}
				}
			} else {
				if (movedir.Y < 0) {
					ground = false;
				}
				tiles = level.GetTile (playerpos, 't');
				foreach (int tile in tiles) {
					if (tile == 1) {
						playerpos.Y -= (int)movedir.Y;
					}
				}
			}
			playerpos.X += (int)(movedir.X * movespeed.X);
			playerpos.Y += (int)(movedir.Y * movespeed.X);
		}
	}
}

#endregion internal functions