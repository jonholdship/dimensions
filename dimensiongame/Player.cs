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
		private Texture2D playersprite;
		private Rectangle playerpos;
		KeyboardState keystate;

		public Player()
		{
			playerpos.X =100;
			playerpos.Y = 100;
			playerpos.Height = 100;
			playerpos.Width = 100;
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
				playerpos.Y--;
			}
			if (keystate.IsKeyDown (Keys.Down) == true) 
			{
				playerpos.Y++;
			}
			if (keystate.IsKeyDown (Keys.Left) == true) 
			{
				playerpos.X--;
			}
			if (keystate.IsKeyDown (Keys.Right) == true) 
			{
				playerpos.X++;
			}
		}

		public void Draw(SpriteBatch spritebatch)
		{	spritebatch.Begin ();
			spritebatch.Draw (playersprite,playerpos,Color.White);
			spritebatch.End();
		}
	}
}

