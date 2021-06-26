using System;
using System.Collections.Generic;

using System.Windows.Media.Imaging;
using Bau.Libraries.SpritesEngine.Models.Definitions;

namespace Bau.Libraries.SpritesEngine.Controllers
{
	/// <summary>
	///		Manager para archivos Atlas
	/// </summary>
	public class AtlasManager
	{
		/// <summary>
		///		Separa los sprites de un archivo
		/// </summary>
		public List<BitmapModel> Separe(string characterName, string fileName, int width, int height)
		{
			return Separe(characterName, Load(fileName), width, height);
		}

		/// <summary>
		///		Separa los sprites de una imagen
		/// </summary>
		public List<BitmapModel> Separe(string prefix, BitmapImage image, int width, int height)
		{
			List<BitmapModel> bitmaps = new();
			int index = 0;

				// Crea las imágenes cortadas
				for (int top = 0; top <= image.PixelHeight - height; top += height)
					for (int left = 0; left <= image.PixelWidth - width; left += width)
						bitmaps.Add(new BitmapModel($"{prefix}_{index++}", CopyImage(image, top, left, width, height), width, height));
				// Devuelve la lista
				return bitmaps;
		}

		/// <summary>
		///		Carga una imagen de un archivo
		/// </summary>
		private BitmapImage Load(string fileName)
		{
			BitmapImage bitmap = new();

				// Comienza las modificaciones
				bitmap.BeginInit();
				// Carga el archivo
				bitmap.UriSource = new Uri(fileName, UriKind.RelativeOrAbsolute);
				// Finaliza las modificaciones
				bitmap.EndInit();
				// Devuelve la imagen cargada
				return bitmap;
		}

		/// <summary>
		///		Copia una parte de la imagen
		/// </summary>
		private CroppedBitmap CopyImage(BitmapImage image, int top, int left, int width, int height)
		{
			return new CroppedBitmap(image, new System.Windows.Int32Rect(left, top, width, height));
		}
	}
}