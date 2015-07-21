using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace dimensiongame
{
	public class Enemy
	{
		private int[] tiles;
		private int nfloor;
		private int windowheight,windowwidth;
		private int spritepos;
		private Vector2 movespeed;
		private float grav;
		private bool ground,dead;
		private float xpace,termv;
		private Texture2D lsprite,rsprite,Esprite;
		private Rectangle targetpos,pos,sprite;
		private bool goodtarget;

		public Enemy (int ww,int wh)
		{
			//all of the player stats:
			xpace =5;				//top move speed horizontally
			pos.X =100;		//intial pos
			pos.Y = 290;
			pos.Height = 80;	//size
			pos.Width = 60;
			targetpos.Height = pos.Height;
			targetpos.Width = pos.Width;

			sprite.Width = 56;
			sprite.Height = 80;
			spritepos = 1;
			grav = 1f;				//gravity strength
			termv = 20;				//terminal velocty from gravity
			movespeed.X=xpace;

			//stuff about the world
			windowheight=wh;
			windowwidth = ww;
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
			//see if it's stood on ground
			Checkground (level);
			//AI for horizontal movement
			Movever(level);
			Movehor(level);
			if (dead == false) {				
				dead = player.collcheck (pos);
			}
		}

		//Draw puts out player on the screen. Nothing complicated to so far.
		public void Draw(SpriteBatch spritebatch)
		{
			if (dead == false) {
				if (movespeed.X > 0) {
					Esprite = rsprite;
				} else {
					Esprite = lsprite;
				}
				if (spritepos < 21) {
					spritepos++;
				} else {
					spritepos = 1;
				}
				sprite.Y = (spritepos / 3) + 1;
				sprite.X = spritepos / sprite.Y;
				sprite.Y = sprite.Y * sprite.Height;
				sprite.X = sprite.X * sprite.Width;
				//draw pin new position
				spritebatch.Draw (Esprite, pos, sprite, Color.White);
			}
		}


		#region internal functions
		//Below are random functions to keep main class logic clear above 

		private void Checkground (Level level)
		{	
			//check to see if player is in air
			//important because movever doesn't check if player walks off ledge
			nfloor=0;
			pos.Y++;
			//find out which tiles are beneath player
			tiles = level.GetTile (pos, 'b');
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
			} else{
				pos.Y--;
			}

			//check to see if embedded in ground by moving up 1 pixel.
			nfloor=0;
			pos.Y--;
			tiles = level.GetTile (pos, 'b');
			//if any of them are floor, make nfloor!=0
			foreach (int tile in tiles) {
				if (tile == 1) {
					nfloor++;
					ground = true;
				}
			}

			//if no longer touching the floor, undo that otherwise stay 1 pixel higher
			if (nfloor == 0) {
				pos.Y++;
			}
		}


		private void Movehor(Level level)
		{
			//check for L/R keypress and do correct tile check for direction.
			if (movespeed.X > 0) {
				tiles = level.GetTile (pos, 'r');
			}
			else if (movespeed.X<0) {
				tiles = level.GetTile (pos, 'l');
			}

			//Change  position by movespeed
			goodtarget = true;
			//check results of GetTile and undo position change if it goes through wall.
			foreach (int tile in tiles) {
				if (tile ==1) {
					goodtarget = false;
				}
			}

			targetpos.X = pos.X+(int)movespeed.X;
			targetpos.Y = pos.Y;

			//check enemy won't fall if it moves to new place
			//but don't bother if target is already bad
			if (goodtarget == true) {
				nfloor = 0;
				targetpos.Y++;
				//checks floor under target if there is some, it will still go there
				tiles = level.GetTile (targetpos, 'b');
				foreach (int tile in tiles) {
					if (tile == 1) {
						nfloor++;
					}
				}
			}

			if (nfloor == 0) {
				goodtarget = false;
			}

			if (goodtarget == true) {
				pos.X = targetpos.X;
			} else {
				movespeed.X = -movespeed.X;
			}
		}

		private void Movever(Level level)
		{
			//as a soon as we hit ground, ground becomes true. this allows player to jump
			//if statement therefore blocks double jumping
			if (ground == false) {
				if (movespeed.Y < termv) {
					movespeed.Y += grav;
				}				
			}

			//change player position by move speed
			pos.Y += (int)movespeed.Y;

			//do the correct tile check depending on move direction
			if (movespeed.Y < 0) {
				tiles = level.GetTile (pos, 't');
			} else {
				tiles = level.GetTile (pos, 'b');
			}

			//check results of GetTile and undo change to position if we hit a wall
			foreach (int tile in tiles) {
				if (tile == 1) {
					pos.Y -= (int)movespeed.Y;
					movespeed.Y = 0;
					ground = true;
				}
			}
		}
		#endregion private function
	}
}



