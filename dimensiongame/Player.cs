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
		private int nfloor,rotcheck,flipcheck;
		private int windowheight,windowwidth,temp;
		private Vector2 movespeed;
		private float grav;
		private bool ground;
		private float jump, xpace,termv;
		private Texture2D playersprite;
		private Rectangle playerpos;
		KeyboardState keystate;

		//creator obviously
		public Player(int ww,int wh)
		{
			//all of the player stats:
			xpace = 10;				//top move speed horizontally
			jump = 20f;				//initial jump speed
			playerpos.X =50;		//intial pos
			playerpos.Y = 900;
			playerpos.Height = 80;	//size of player
			playerpos.Width = 50;

			grav = 1f;				//gravity strength
			termv = 20;				//terminal velocty from gravity
			ground = false;			//intialised to false incase player starts in air

			//stuff about the world
			windowheight=wh;
			windowwidth = ww;
		}

		//main required functions. LoadConent at start of game. Update and draw each timestep
		public void LoadContent(ContentManager content)
		{
			//player art.
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
			//general actions
			Action(level);
		}

		//Draw puts out player on the screen. Nothing complicated to so far.
		public void Draw(SpriteBatch spritebatch)
		{
			//draw player in new position
			spritebatch.Draw (playersprite,playerpos,Color.White);
		}


#region internal functions
//Below are random functions to keep main class logic clear above 

		private void Checkground (Level level)
		{	
			//check to see if player is in air
			//important because movever doesn't check if player walks off ledge
			nfloor=0;
			playerpos.Y++;
			//find out which tiles are beneath player
			tiles = level.GetTile (playerpos, 'b');
			//if any of them are floor, make nfloor!=0
			foreach (int tile in tiles) {
				if (tile == 1) {
					ground = true;
					nfloor++;
				}
			}
			//if nfloor wasn't changed in above loop, player is in air
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

		private void Action(Level level)
		{
			//check if r is being pressed.
			if (keystate.IsKeyDown (Keys.R) == true) {
				rotcheck = 1;
			//perform rotate on release of R
			} else if (rotcheck == 1) {			
				level.Rotate ();
				rotcheck = 0;
				//move player to their new position.
				temp = playerpos.X;
				playerpos.X = playerpos.Y;
				playerpos.Y = windowheight - temp-1;
			}
			if (keystate.IsKeyDown (Keys.F) == true) {
				flipcheck = 1;
			} else if (flipcheck == 1) {

				level.Flip ();
				flipcheck = 0;
				playerpos.Y = windowheight-playerpos.Y;
			}
		}
	}
}
			
#endregion internal functions