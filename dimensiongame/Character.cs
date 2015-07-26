using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;




namespace dimensiongame
{
	//a base class for all NPCs and the player character. Handles collisions with enviroment and allows movements
	public class Character
	{	//rectangles are (more or less) an (x,y) point, a width and a height.
		//there are multiple ways to draw something to screen but I find a target rectangle easiest.
		//see the draw function for how it's called
		private int nfloor;
		private int temp;
		private Vector2 movegrav;
		private float grav,termv;
		private const float pi = 3.1416f;
		private char udir,ldir,ddir,rdir,tempdir;
		private bool wall;

		//stuff child classes need to mess with
		protected Rectangle collbox;
		protected Vector2 pos,movex,movey;
		protected Texture2D sprite;
		protected float jump,rot,xpace;
		protected bool ground;

		//stuff everything can access
		public bool dead;

		//creator obviously

		public Character ()
		{
			//all of the player stats:
			rot = 2*pi;
			grav = 1f;				//gravity strength
			termv = 20;				//terminal velocty from gravity
			ground = false;			//intialised to false incase player starts in air

			movegrav.X = grav * (float)Math.Sin (rot);
			movegrav.Y = grav*(float)Math.Cos (rot);


			//rotation stuff
			udir='u';
			ddir = 'd';
			ldir = 'l';
			rdir = 'r';
		}


		protected void Checkground (Level level)
		{	
			int[] tiles;
			float movelength;
			//check to see if player is in air
			//important because movever doesn't check if player walks off ledge
			ground=false;
			//find out which tiles are beneath player
			tiles = level.GetTile (collbox,ddir);
			//if any of them are floor, make nfloor!=0
			foreach (int tile in tiles) {
				if (tile == 1) {
					ground = true;
					movey.Y = 0;
					movey.X = 0;
				}
			}
			movelength = movey.Length();
			//if nfloor wasn't changed in above loop, player is in air
			if (ground ==false) {
				movey += movegrav;
				if(movey.Y > termv || movey.X > termv){
						movey.X = termv * (float)Math.Sin (rot);
						movey.Y = termv*(float)Math.Cos (rot);
					}
			} 

			pos = pos + movey;

			//check to see if embedded in ground by moving up 1 pixel.
			nfloor=0;
			pos.X+=20f*(float)(Math.Sin(rot));
			pos.Y-=20f*(float)(Math.Cos(rot));
			tiles = level.GetTile (collbox, ddir);
			//if any of them are floor, make nfloor!=0
			foreach (int tile in tiles) {
				if (tile == 1) {
					nfloor++;
					ground = true;
				}
			}

			//if no longer touching the floor, undo that otherwise stay 1 pixel higher
			if (nfloor == 0) {
				pos.X-=20f*(float)(Math.Sin(rot));
				pos.Y+=20f*(float)(Math.Cos(rot));
			}
		}

		protected void Moveleft(Level level)
		{
			int[] tiles;

			//check for L/R keypress and do correct tile check for direction.
			tiles = level.GetTile (collbox, ldir);
			wall = false;
			foreach (int tile in tiles) {
				if (tile == 1) {
					wall = true;
				}
			}

			if (wall == false) {
				pos = pos - movex;
			}
		}


		protected void Moveright(Level level)
		{	
			int[] tiles;
			int check = 0;
			//check for L/R keypress and do correct tile check for direction.
			tiles = level.GetTile (collbox, rdir);
			wall = false;
			foreach (int tile in tiles) {
				if (tile == 1) {
					check = 1;
				}
			}

			if (check == 0) {
				pos = pos + movex;
			}
		}

		protected void Moveup(Level level)
		{	
			int[] tiles;
			
			tiles = level.GetTile (collbox, udir);
			wall = false;
			foreach (int tile in tiles) {
				if (tile == 1) {
					wall = true;
				}
			}
			if (wall == false) {
				movey.X = jump * (float)Math.Sin (rot);
				movey.Y = -jump * (float)Math.Cos (rot);
			}

			pos = pos + movey;
		}

		protected void Moveupdate()
		{
			collbox.X = (int)pos.X;
			collbox.Y = (int)pos.Y;
		}

		protected void Rotate()
		{
			rot += pi / 2;
			if (rot > 2*pi){
				rot-=(2*pi);
			}

			movex.X = xpace*(float)Math.Cos (rot);
			movex.Y = xpace*(float)Math.Sin (rot);
			movey.X=jump*(float)Math.Sin (rot);
			movey.Y = -jump * (float)Math.Cos (rot);
			movegrav.X = grav * (float)Math.Sin (rot);
			movegrav.Y = grav*(float)Math.Cos (rot);
			tempdir = udir;
			udir = ldir;
			ldir = ddir;
			ddir = rdir;
			rdir = tempdir;

			temp=collbox.Width;
			collbox.Height = collbox.Width;
			collbox.Width = temp;
			
		}
	}	
}

