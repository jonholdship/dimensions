using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dimensiongame
{
	public class Camera
	{
		private Matrix transform;
		private Vector2 pos;
		private float rotation;
		private int viewportwidth;
		private int viewportheight;
		private int worldwidth;
		private int worldheight;
		private const float pi = 3.1416f;

		public Camera(Viewport viewport, int worldWidth, 
			int worldHeight)
		{
			rotation = 0.0f;
			pos = Vector2.Zero;
			viewportwidth = viewport.Width;
			viewportheight = viewport.Height;
			worldwidth = worldWidth;
			worldheight = worldHeight;
		}



		public void poscheck(Vector2 playerpos)
		{
			pos.X = playerpos.X;
			pos.Y = playerpos.Y;
			float leftBarrier = viewportwidth/2;
			float rightBarrier = worldwidth - viewportwidth / 2;
			float topBarrier = viewportheight / 2;
			float bottomBarrier = worldheight-viewportheight/2;
			if (pos.X < leftBarrier)
				pos.X = leftBarrier;
			if (pos.X > rightBarrier)
				pos.X = rightBarrier;
			if (pos.Y < topBarrier)
				pos.Y = topBarrier;
			if (pos.Y > bottomBarrier)
				pos.Y = bottomBarrier;
		}



		public void Rotate()
		{	
			rotation += pi / 2;
			if (rotation > 2*pi)
				rotation -= 2*pi;
		}

		public Matrix GetTransformation()
		{
			transform = Matrix.CreateTranslation (new Vector3 (-pos.X, -pos.Y, 0)) *
			Matrix.CreateRotationZ (rotation) *
			Matrix.CreateTranslation (new Vector3 (viewportwidth * 0.5f,
				viewportheight * 0.5f, 0));// *
				Matrix.CreateReflection (new Plane(1,1,0,viewportheight/2));
			return transform;
		}
	}
}
