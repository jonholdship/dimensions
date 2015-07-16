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
					if (i > 0 && j>0 && j<49 && i<49) {
						layout [i][j] = 0;
					} else {
						layout [i][j] = 1;
					}

				}
			}


			tile.Width = 10;
			tile.Height = 10;
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
					tile.X = tile.Width * i;
					tile.Y = tile.Height * j;
					spritebatch.Draw (tiles[layout[i][j]],tile,Color.White);
				}
			}
		}

		//function that checks along the edges of a recieved rectangle and checks what tile they are touching.
		public int[] GetTile(Rectangle thing)
		{
			int[] intersects=new int[20];
			int i,x,y,nw,nh;
			//number of tiles wide and tall rectangle is
			nw = thing.Width / tile.Width;
			nh = thing.Height / tile.Height;
			i = 0;
			//x and y indices of the element of the level array which corresponds to the position of the rectangle
			x=thing.X/tile.Width;
			y=thing.Y/tile.Height;
			//from the top left of rectangle check every tile along to top right corner
			for (int w=0; w<nw;w++)
			{
				x = x + 1;
				intersects[i]=layout[x][y];
				i++;
			}
			//same again but from bottom edge
			x = thing.X /tile.Height;
			y = (thing.Y + thing.Height) / tile.Height;
			for (int w=0; w<nw;w++)
			{
				x = x + 1;
				intersects[i]=layout[x][y];
				i++;
			//left edge
			}x=thing.X/tile.Height;
			y=thing.Y/tile.Height;
			for (int h=0; h<nh;h++)
			{
				y = y + 1;
				intersects[i]=layout[x][y];
				i++;
			}
			//right edge
			x = (thing.X+thing.Width) / tile.Height;
			y = thing.Y/ tile.Height;
			for (int h=0; h<nh;h++)
			{
				y = y + 1;
				intersects[i]=layout[x][y];
				i++;
			}
			return intersects;
		}
	}
}

