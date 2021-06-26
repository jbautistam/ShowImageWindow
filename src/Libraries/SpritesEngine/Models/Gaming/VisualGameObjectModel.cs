using System;

namespace Bau.Libraries.SpritesEngine.Models.Gaming
{
	/// <summary>
	///		Modelo base para los objetos del motor con componentes visuales
	/// </summary>
	public abstract class VisualGameObjectModel : GameObjectModel
	{
		public VisualGameObjectModel(EngineModel engine, Definitions.SpriteModel sprite) : base(engine)
		{
			Sprite = sprite;
		}

		/// <summary>
		///		Fuerza un cambio de posición
		/// </summary>
		public void SetPosition(int top, int left)
		{
			Top = top;
			Left = left;
		}

		/// <summary>
		///		Anima el sprite
		/// </summary>
		protected void Animate(string animation)
		{
			// Obtiene la animación
			if (ActualAnimation == null || !animation.Equals(ActualAnimation.Id, StringComparison.CurrentCultureIgnoreCase))
			{
				// Cambia la animación
				ActualAnimation = Sprite.Animations[animation];
				// y la establece en el movimiento inicial
				ActualStep = 0;
			}
			else
				ActualStep = (ActualStep + 1) % ActualAnimation.Steps.Count;
			// Obtiene el bitmap actual
			ActualBitmap = Engine.Store.Bitmaps[ActualAnimation.Steps[ActualStep].BitmapId];
		}

		/// <summary>
		///		Objeto del juego
		/// </summary>
		public Definitions.SpriteModel Sprite { get; }

		/// <summary>
		///		Posición superior
		/// </summary>
		public int Top { get; protected set; }

		/// <summary>
		///		Posición izquierda
		/// </summary>
		public int Left { get; protected set; }

		/// <summary>
		///		Velocidad
		/// </summary>
		public int Speed { get; set; } = 10;

		/// <summary>
		///		Bitmap actual
		/// </summary>
		public Definitions.BitmapModel ActualBitmap { get; protected set; }

		/// <summary>
		///		Animación actual
		/// </summary>
		protected Definitions.AnimationModel ActualAnimation { get; set; }

		/// <summary>
		///		Paso de animación actual
		/// </summary>
		protected int ActualStep { get; set; }
	}
}
