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
		private Vector2 speed;
		private Texture2D playersprite;
		private Rectangle playerpos;
		KeyboardState keystate;

		public Player()
		{
			playerpos.X =100;
			playerpos.Y = 100;
			playerpos.Height = 100;
			playerpos.Width = 100;
			speed = Vector2.Zero;
		}
		public void LoadContent(ContentManager content)
		{
			playersprite=content.Load<Texture2D>("playsprite");
		}

		public void Update()
		{
			keystate = Keyboard.GetState ();

			if (keystate.IsKeyDown (Keys.Up) == true) 
			{
				speed.Y=-1;
			}
			if (keystate.IsKeyDown (Keys.Down) == true) 
			{
				speed.Y=1;
			}
			if (keystate.IsKeyDown (Keys.Left) == true) 
			{
				speed.X=-1;
			}
			if (keystate.IsKeyDown (Keys.Right) == true) 
			{
				speed.X=1;
			}
			if (keystate.IsKeyDown (Keys.Space) == true) 
			{
				speed.Y = 2;
			}

			playerpos.X += (int)speed.X;
			playerpos.Y+=(int)speed.Y;

		}




		public void Draw(SpriteBatch spritebatch)
		{	spritebatch.Begin ();
			spritebatch.Draw (playersprite,playerpos,Color.White);
			spritebatch.End();
		}
	}
}

