using System;

using System.Windows.Media.Imaging;

namespace Bau.Libraries.SpritesEngine.Models.Definitions
{
	/// <summary>
	///		Bitmap asociado a un sprite
	/// </summary>
	public class BitmapModel
	{
		public BitmapModel(string id, BitmapSource image, int width, int height)
		{
			Id = id;
			Image = image;
			Width = width;
			Height = height;
		}

		/// <summary>
		///		Id de la imagen
		/// </summary>
		public string Id { get; }

		/// <summary>
		///		Imagen
		/// </summary>
		public BitmapSource Image { get; }

		/// <summary>
		///		Ancho de la imagen
		/// </summary>
		public int Width { get; }
		
		/// <summary>
		///		Alto de la imagen
		/// </summary>
		public int Height { get; }
	}
}
