using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace dimensiongame
{
	public class Player:Character
	{
		//rectangles are (more or less) an (x,y) point, a width and a height.
		//there are multiple ways to draw something to screen but I find a target rectangle easiest.
		//see the draw function for how it's called
		private int rotcheck;
		private Rectangle spritebox;
		KeyboardState keystate;

		//creator obviously
		public Player():base()
		{
			//all of the player stats:
			jump = 20f;				//initial jump speed
			xpace = 10;				//top move speed horizontally
			collbox.X =60;		//intial pos
			collbox.Y = 860;
			collbox.Height = 80;	//size of player
			collbox.Width = 60;

			//movement stuff
			pos.X=collbox.X;
			pos.Y = collbox.Y;
			spritebox = collbox;
			movex.X = xpace*(float)Math.Cos (rot);
			movex.Y = xpace*(float)Math.Sin (rot);
			//movey.X=jump*(float)Math.Sin (rot);
			//movey.Y = -jump * (float)Math.Cos (rot);
		}

		//main required functions. LoadConent at start of game. Update and draw each timestep
		public void LoadContent(ContentManager content)
		{
			//player art.
			sprite=content.Load<Texture2D>("playsprite");
		}

		//update works out things like character movement
		public void Update(Level level, Camera camera)
		{
			//find out what player is pressing
			keystate = Keyboard.GetState ();
			//see if we're stood on ground
			Checkground (level);
			// movement
			Move(level);
			//general actions
			Moveupdate();
			Action(camera, level);
			//tell camera where we are
			camera.poscheck(pos);

		}

		//Draw puts out player on the screen. Nothing complicated to so far.
		public void Draw(SpriteBatch spritebatch)
		{
			spritebox.X = collbox.X;
			spritebox.Y = collbox.Y;
			//draw player in new position
			spritebatch.Draw (sprite,null,spritebox,null,null,rot,null,Color.White,SpriteEffects.None,0);
		}


#region internal functions
//Below are random functions to keep main class logic clear above 
		private void Move(Level level)
		{
			if (keystate.IsKeyDown (Keys.Left) ==true){
				base.Moveleft(level);
			}

			if (keystate.IsKeyDown (Keys.Right) ==true){
				base.Moveright(level);
			}

			if ((keystate.IsKeyDown (Keys.Up) ==true) && (ground ==true)){
				base.Moveup(level);
			}
		}

		private void Action(Camera camera, Level level)
		{
			//check if r is being pressed.
			if (keystate.IsKeyDown (Keys.R) == true) {
				rotcheck = 1;
			//perform rotate on release of R
			} else if (rotcheck == 1) {			
				camera.Rotate();
				base.Rotate ();
				rotcheck = 0;
			}
			/*if (keystate.IsKeyDown (Keys.F) == true) {
				flipcheck = 1;
			} else if (flipcheck == 1) {

				level.Flip ();
				flipcheck = 0;
				pos.Y = windowheight-pos.Y;
			}*/
		}
		#endregion internal functions

		public bool collcheck(Rectangle obj)
		{
			//coll is true if rectangles overlap
			bool coll = obj.Intersects (collbox);
			bool kill =false;
			if (coll == true) {
				//kill is true if bottom of player is no less than 5 pixels below top of enemy 
				kill = ((collbox.Top + collbox.Height) < (obj.Top + 20));
				if (kill == false) {
					dead = true;
				}
			}
			//kill remains false if no collision and is false if player dies. Returns true for dead enemy.
				return kill;
		}
	}
}
			
