using System;

using Bau.Libraries.SpritesEngine.Models.Definitions;

namespace Bau.Libraries.SpritesEngine.Builders
{
	/// <summary>
	///		Generador de <see cref="SpriteModel"/>
	/// </summary>
	public class SpriteBuilder
	{
		public SpriteBuilder(StoreBuilder builder, StoreModel store)
		{
			Builder = builder;
			Store = store;
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteBuilder WithSprite(string name)
		{
			// Añade el personaje
			Store.Sprites.Add(new SpriteModel(name));
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Crea una animación
		/// </summary>
		public AnimationBuilder WithAnimation(string name)
		{
			return new AnimationBuilder(this, Sprite).WithAnimation(name);
		}

		/// <summary>
		///		Pasa al <see cref="StoreBuilder"/>
		/// </summary>
		public StoreBuilder Back()
		{
			return Builder;
		}

		/// <summary>
		///		Generador padre
		/// </summary>
		private StoreBuilder Builder { get; }

		/// <summary>
		///		Almacén
		/// </summary>
		private StoreModel Store { get; }

		/// <summary>
		///		Personaje actual
		/// </summary>
		private SpriteModel Sprite
		{
			get { return Store.Sprites[Store.Sprites.Count - 1]; }
		}
	}
}
