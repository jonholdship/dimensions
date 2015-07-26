using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace dimensiongame
{
	public class Enemy:Character
	{
		private int[] tiles;
		private int sourcepos;
		private Texture2D lsprite,rsprite,Esprite;
		private Rectangle targetpos,sourcebox;
		private bool goodtarget,right;

		public Enemy ():base()
		{
			//all of the player stats:
			xpace =5;				//top move speed horizontally
			collbox.X =200;		//intial pos
			collbox.Y = 300;

			collbox.Height = 80;	//size
			collbox.Width = 60;
			//movement stuff
			pos.X=collbox.X;
			pos.Y = collbox.Y;
			movex.X = xpace*(float)Math.Cos (rot);
			movex.Y = xpace*(float)Math.Sin (rot);
			movey.X=jump*(float)Math.Sin (rot);
			movey.Y = -jump * (float)Math.Cos (rot);

			targetpos=collbox;
			right = true;
			//stuff for animated sprite
			sourcebox.Width = 56;
			sourcebox.Height = 80;
			sourcepos = 1;
		}

		//main required functions. LoadConent at start of game. Update and draw each timestep
		public void LoadContent(ContentManager content)
		{
			lsprite=content.Load<Texture2D>("kitleft");
			rsprite = content.Load<Texture2D> ("kitright");
		}

		//update works out things like character movement
		public void Update(Level level, Player player)
		{
			if (dead == false) {
				//see if it's stood on ground
				Checkground (level);
				//AI for horizontal movement
				Movehor (level);
				if (dead == false) {				
					dead = player.collcheck (collbox);
				}

				Moveupdate ();
			}
		}

		//Draw puts out player on the screen. Nothing complicated to so far.
		public void Draw(SpriteBatch spritebatch)
		{
			if (dead == false) {
				if (right==true) {
					Esprite = rsprite;
				} else {
					Esprite = lsprite;
				}
				if (sourcepos < 21) {
					sourcepos++;
				} else {
					sourcepos = 1;
				}

			}
			else{
				sourcepos++;
				if( sourcepos>24)
				{sourcepos = 21;}
			
			}
			sourcebox.Y = (sourcepos / 3) + 1;
			sourcebox.X = sourcepos / sourcebox.Y;
			sourcebox.Y = sourcebox.Y * sourcebox.Height;
			sourcebox.X = sourcebox.X * sourcebox.Width;
			//draw pin new position
			spritebatch.Draw (Esprite,pos,sourcebox, Color.White);
		}
			


		private void Movehor(Level level)
		{
			//clumsy, but eaesy to have big if for each direction
			if (right == true) {
				//assume this check fails for ease of logic
				goodtarget = false;
				right = false;
				//check if we move one whole tile, will there be floor
				targetpos.X = collbox.X+20;
				//check enemy won't fall if it moves to new place
				tiles = level.GetTile (targetpos, 'd');

				//go through results and if there is floor, continue to move
				foreach (int tile in tiles) {
					if (tile == 1) {
						goodtarget = true;
						right = true;
					}
				}
			} else {
				goodtarget = false;
				right = true;
				targetpos.X = collbox.X-20;
				tiles = level.GetTile (targetpos, 'd');
				foreach (int tile in tiles) {
					if (tile == 1) {
						goodtarget = true;
						right = false;
					}
				}
			}

			//if we're statisfied moving forward will be ok, move
			if (goodtarget == true) {
				if (right == true) {
					Moveright (level);
					//if we hit a wall, these two values are the same and need to turn around
					if (collbox.X == (int)pos.X) {
						right = false;
					}
				} else {
					Moveleft (level);
					if (collbox.X == (int)pos.X) {
						right = true;
					}
				}
			}
		}
	}
}



