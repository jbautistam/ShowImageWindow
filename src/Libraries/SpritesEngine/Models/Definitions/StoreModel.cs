using System;
using System.Collections.Generic;

namespace Bau.Libraries.SpritesEngine.Models.Definitions
{
	/// <summary>
	///		Definición de sprites
	/// </summary>
	public class StoreModel
	{
		/// <summary>
		///		Añade los datos de un store a éste
		/// </summary>
		public void Add(StoreModel store)
		{
			// Añade las imágenes
			Bitmaps.AddRange(store.Bitmaps);
			// Añade los sprites
			foreach (SpriteModel sprite in store.Sprites)
			{
				SpriteModel oldSprite = GetSprite(sprite.Id);

					// Si ya había un sprite con el mismo Id, le añade animaciones, si no, añade todo el sprite
					if (oldSprite != null)
						foreach ((string key, AnimationModel animation) in sprite.Animations.Enumerate())
							oldSprite.Animations.Add(key, animation);
					else
						Sprites.Add(sprite);
			}
		}

		/// <summary>
		///		Obtiene un sprite por su nombre
		/// </summary>
		public SpriteModel GetSprite(string id)
		{
			// Busca el sprite en la colección
			foreach (SpriteModel sprite in Sprites)
				if (sprite.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase))
					return sprite;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return null;
		}

		/// <summary>
		///		Sprites del juego
		/// </summary>
		public List<SpriteModel> Sprites { get; } = new();

		/// <summary>
		///		Diccionario de imágenes 
		/// </summary>
		public BitmapDictionaryModel Bitmaps { get; } = new();
	}
}
