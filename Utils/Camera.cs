using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaEngine
{
	public sealed class Camera : IDisposable
	{
		public Transform Transform;

		public Vector2 Position { get { return Transform.Position; } set { Transform.Position = value; } }
		public Vector2 Scale { get { return Transform.Scale; } set { Transform.Scale = value; } }
		public float Rotation { get { return Transform.Rotation; } set { Transform.Rotation = value; } }

		public Viewport Viewport;

		public bool Disposed { get; private set; }

		public Camera()
		{
			Transform = new Transform(Vector2.Zero);
			Viewport = new();
		}

		public Camera(Transform trans, int width, int height)
		{
			Transform = trans;
			Viewport = new();
			Viewport.Widt = width;
			Viewport.Height = height;
		}

		public Camera(Vector2 pos, int width, int height)
		{
			Transform = new Transform(pos);
			Viewport = new();
			Viewport.Widt = width;
			Viewport.Height = height;
		}

		public Matrix GetMatrix()
		{
			return Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
				Matrix.CreateTranslation(Viewport.Width , Viewport.Height, 0f);
		}

		public Matrix GetMatrixOnCenter()
		{
			return Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
				Matrix.CreateTranslation(Viewport.Width * .5f , Viewport.Height * .5f, 0f);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					Disposed = true;
				}
			}
		}
	}
}
