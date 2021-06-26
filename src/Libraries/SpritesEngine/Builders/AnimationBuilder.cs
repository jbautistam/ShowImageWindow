using System;

using Bau.Libraries.SpritesEngine.Models.Definitions;

namespace Bau.Libraries.SpritesEngine.Builders
{
	/// <summary>
	///		Generador de <see cref="AnimationModel"/>
	/// </summary>
	public class AnimationBuilder
	{
		public AnimationBuilder(SpriteBuilder builder, SpriteModel character)
		{
			Builder = builder;
			Character = character;
		}

		/// <summary>
		///		Añade una animación
		/// </summary>
		public AnimationBuilder WithAnimation(string name)
		{
			// Crea la animación y la añade al diccionario del personaje
			Animation = new AnimationModel(name);
			Character.Animations.Add(name, Animation);
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade un paso a la animación actual
		/// </summary>
		public AnimationBuilder WithStep(string bitmapId, TimeSpan? time = null)
		{
			// Añade el paso de animación
			Animation.Steps.Add(new AnimationStepModel(bitmapId, time ?? TimeSpan.FromMilliseconds(10)));
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade una serie de pasos a la animación actual
		/// </summary>
		public AnimationBuilder WithSteps(string bitmapPrefix, int startIndex, int endIndex, TimeSpan? time = null)
		{
			// Añade el paso de animación
			for (int index = startIndex; index <= endIndex; index++)
				Animation.Steps.Add(new AnimationStepModel($"{bitmapPrefix}_{index}", time ?? TimeSpan.FromMilliseconds(10)));
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Vuelve al generador anterior
		/// </summary>
		public SpriteBuilder Back()
		{
			return Builder;
		}

		/// <summary>
		///		Generador padre
		/// </summary>
		private SpriteBuilder Builder { get; }

		/// <summary>
		///		Definición de personaje
		/// </summary>
		private SpriteModel Character { get; }

		/// <summary>
		///		Animación que se está definiendo
		/// </summary>
		private AnimationModel Animation { get; set; }
	}
}
