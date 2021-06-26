using System;

namespace Bau.Libraries.SpritesEngine.Models.Definitions
{
	/// <summary>
	///		Clase con la definición de un objeto del juego
	/// </summary>
	public class SpriteModel
	{
		public SpriteModel(string id)
		{
			Id = id;
		}

		/// <summary>
		///		Código de la animación
		/// </summary>
		public string Id { get; }

		/// <summary>
		///		Animaciones
		/// </summary>
		public Base.NormalizedDictionary<AnimationModel> Animations { get; } = new();
	}
}
