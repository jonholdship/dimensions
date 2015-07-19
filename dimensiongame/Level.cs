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

		public Level (int windowwidth,int windowheight)
		{
			string[] temp,values;
			temp = File.ReadAllLines (levelfile);
			values=temp[0].Split(',');

			//levelwidth is width in tiles
			levelwidth = values.GetLength (0);
			levelheight = temp.GetLength (0);

			//layout is an array of integer arrays. need levelheight rows.
			layout = new int[levelheight][];

			//assign the size of each row to be the number of tiles across the level is
			for (int i = 0; i<50; i++) {
				layout[i]= new int [levelwidth];
			}

			//loop to assign the  actual tile values to layout
			for (int i = 0; i<50; i++) {
				values=temp[i].Split(',');
				for (int j = 0;j< 50; j++) {
					layout [j] [i] = Convert.ToInt16 (values [j]);
				}
			}

			//size of tiles in pixels = window size in pixels/ number of tiles in window
			tile.Width = windowwidth/levelwidth;
			tile.Height = windowheight/levelheight;
		}

		public void LoadContent(ContentManager content)
		{
			//load the images needed for the tiles
			//number is same as integers in layout
			tiles[0]=content.Load<Texture2D>("background");
			tiles[1]=content.Load<Texture2D>("wall");
			tiles[2]=content.Load<Texture2D>("lava");
		}

		public void Update()
		{
		}

		//we could call this conditionally. So that it draws only when it changes.
		public void Draw(SpriteBatch spritebatch)
		{
			//loop over every element in layout. tile is a rectangle that we use to decide where to draw
			//tile.X=pixels per tile * column of element
			//by updating like this, one rectangle serves for all the tiles.
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
			//intersects is difficult to dynamically size. So is just too large and therefore will send zeros. (hopefully?)
			//could do another case statement with jsut the n= line from each of the ones below.
			//then initialise intersects to new int [n] and do case again with rest of each statement.
			int[] intersects=new int[10];
			int x,y,n;
			switch (dir)
			{
			//case is left,right,top,bottom so only one edge is checked per call
			case 'l':
				//number of tiles to check.
				n = thing.Height / tile.Height;
				//current tile position of object=pixel position/ pixels per tile
				x=thing.X/tile.Width;
				y=thing.Y/tile.Height;
				//don't allow to go off edge of array
				if (x < 0)
					x = 0;
				//give intersects the value of every tile in contact with object
				for (int ni=0; ni<n;ni++)
				{
					y++;
					intersects[ni]=layout[x][y];
				}
				break;
			//other cases same as above. note below x=thing.x+thing.width.
			//this is x position of right edge of object as thing.x and thing.y
			//always refer to top left corner.
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

