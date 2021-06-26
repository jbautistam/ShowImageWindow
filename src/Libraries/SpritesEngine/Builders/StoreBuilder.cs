using System;
using System.Collections.Generic;

using Bau.Libraries.SpritesEngine.Models.Definitions;

namespace Bau.Libraries.SpritesEngine.Builders
{
	/// <summary>
	///		Generador para <see cref="StoreModel"/>
	/// </summary>
	public class StoreBuilder
	{
		// Variables privadas
		private StoreModel _store = new();

		/// <summary>
		///		Añade un <see cref="SpriteModel"/>
		/// </summary>
		public SpriteBuilder WithSprite(string name)
		{
			return new SpriteBuilder(this, _store).WithSprite(name);
		}

		/// <summary>
		///		Añade una lista de <see cref="BitmapModel"/>
		/// </summary>
		public StoreBuilder WithBitmaps(List<BitmapModel> bitmaps)
		{
			// Añade las imágenes
			foreach (BitmapModel bitmap in bitmaps)
				_store.Bitmaps.Add(bitmap.Id, bitmap);
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade un <see cref="BitmapModel"/>
		/// </summary>
		public StoreBuilder WithBitmap(BitmapModel bitmap)
		{
			// Añade la imagen
			_store.Bitmaps.Add(bitmap.Id, bitmap);
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Genera el <see cref="StoreModel"/>
		/// </summary>
		public StoreModel Build()
		{
			return _store;
		}
	}
}
