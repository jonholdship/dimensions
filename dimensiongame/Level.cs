using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace dimensiongame
{


	public class Level
	{
		private Texture2D[] tiles = new Texture2D[2];
		private int[][] layout = new int[50][];
		private Rectangle tile;

		public Level ()
		{
			for (int i = 0; i<50; i++) {
				layout [i] = new int [50];
				for (int j = 0;j< 50; j++) {
					if (i > 0 && j>0) {
						layout [i][j] = 0;
					} else {
						layout [i][j] = 1;
					}
				}
			}

			tile.Width = 500;
			tile.Height = 500;
		}

		public void LoadContent(ContentManager content)
		{
			tiles[0]=content.Load<Texture2D>("background");
			tiles[1]=content.Load<Texture2D>("wall");
		}

		public void Update()
		{
		}

		public void Draw(SpriteBatch spritebatch)
		{
			for (int i = 0; i<50; i++) {
				for (int j = 0;j< 50; j++) {
					tile.X = 10 * i;
					tile.Y = -10 * j;
					spritebatch.Draw (tiles[layout[i][j]],tile,Color.White);
				}
			}
		}

		public int GetTile(Rectangle thing)
		{
			int x,y;
			x=thing.X;
			y=thing.Y;
			x/=10;
			y/=10;
			x=layout[x][y];
			return x;
		}
	}
}

