using System;
using System.IO;
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
		private Texture2D[] tiles = new Texture2D[3];
		private int[][] layout ;
		private int levelwidth,levelheight;
		private Rectangle tile;
		private string levelfile="level1.csv";
		private string[] temp,values;

		public Level (int windowwidth,int windowheight)
		{
			temp = File.ReadAllLines (levelfile);
			values=temp[0].Split(',');

			levelwidth = values.GetLength (0);
			levelheight = temp.GetLength (0);

			layout = new int[levelheight][];

			for (int i = 0; i<50; i++) {
				layout[i]= new int [levelwidth];
			}

			for (int i = 0; i<50; i++) {
				values=temp[i].Split(',');
				for (int j = 0;j< 50; j++) {
					layout [j] [i] = Convert.ToInt16 (values [j]);
				}
			}

			layout [10] [49] = 2;
			tile.Width = windowwidth/levelwidth;
			tile.Height = windowheight/levelheight;
		}

		public void LoadContent(ContentManager content)
		{
			tiles[0]=content.Load<Texture2D>("background");
			tiles[1]=content.Load<Texture2D>("wall");
			tiles[2]=content.Load<Texture2D>("lava");
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
		public int[] GetTile(Rectangle thing, char dir)
		{
			int[] intersects=new int[10];
			int x,y,n;
			switch (dir)
			{
			case 'l':
				n = thing.Height / tile.Height;
				x=thing.X/tile.Width;
				y=thing.Y/tile.Height;
				if (x < 0)
					x = 0;
				for (int ni=0; ni<n;ni++)
				{
					y++;
					intersects[ni]=layout[x][y];
				}
				break;
			case 'r':
				n = thing.Height / tile.Height;
				x = (thing.X + thing.Width) / tile.Width;
				y = thing.Y / tile.Height;
				if (x > (levelwidth-1))
					x = levelwidth-1;
				for (int ni=0; ni<n;ni++)
				{	
					y++;
					intersects[ni]=layout[x][y];
				}		
				break;
			case 't':
				n = thing.Width / tile.Width;
				x = thing.X / tile.Width;
				y = thing.Y / tile.Height;
				if (y < 0)
					y = 0;
				for (int ni = 0; ni < n; ni++) {
					x++;
					intersects [ni] = layout [x] [y];
				}
				break;
			case 'b':
				n = thing.Width / tile.Width;
				x = thing.X / tile.Width;
				y = (thing.Y + thing.Height) / tile.Height;
				if (y > (levelheight-1))
					y = levelheight-1;
				for (int ni = 0; ni < n; ni++) {
					x++;
					intersects [ni] = layout [x] [y];
				}
				break;	
			}
	
			return intersects;
		}
	}
}

