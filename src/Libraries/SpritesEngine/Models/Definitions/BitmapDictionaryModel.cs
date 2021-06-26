using System;
using System.Collections.Generic;

namespace Bau.Libraries.SpritesEngine.Models.Definitions
{
	/// <summary>
	///		Diccionario de <see cref="BitmapModel"/>
	/// </summary>
	public class BitmapDictionaryModel : Base.NormalizedDictionary<BitmapModel>
	{
		/// <summary>
		///		Añade una serie de imágenes
		/// </summary>
		public void AddRange(List<BitmapModel> bitmaps)
		{
			foreach (BitmapModel bitmap in bitmaps)
				Add(bitmap.Id, bitmap);
		}
	}
}
