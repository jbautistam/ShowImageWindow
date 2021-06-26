using System;

namespace Bau.Libraries.SpritesEngine.Models.Definitions
{
	/// <summary>
	///		Clase con la definición de un paso animación
	/// </summary>
	public class AnimationStepModel
	{
		public AnimationStepModel(string bitmapId, TimeSpan time)
		{
			BitmapId = bitmapId;
			Time = time;
		}

		/// <summary>
		///		Código del bitmap
		/// </summary>
		public string BitmapId { get; }

		/// <summary>
		///		Tiempo que debe durar la animación
		/// </summary>
		public TimeSpan Time { get; }
	}
}
