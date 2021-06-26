using System;
using System.Collections.Generic;

namespace Bau.Libraries.SpritesEngine.Models.Definitions
{
	/// <summary>
	///		Clase con la definición de una animación
	/// </summary>
	public class AnimationModel
	{
		public AnimationModel(string id)
		{
			Id = id;
		}

		/// <summary>
		///		Código del tipo de animación
		/// </summary>
		public string Id { get; }

		/// <summary>
		///		Pasos de animación
		/// </summary>
		public List<AnimationStepModel> Steps { get; } = new();
	}
}
