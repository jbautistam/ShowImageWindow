using System;

namespace Bau.Libraries.SpritesEngine.EventArguments
{
	/// <summary>
	///		Argumentos de los eventos relacionados con <see cref="Models.Gaming.GameObjectModel"/>
	/// </summary>
	public class GameObjectEventArgs : EventArgs
	{
		public GameObjectEventArgs(Models.Gaming.GameObjectModel gameObject)
		{
			GameObject = gameObject;
		}

		/// <summary>
		///		Objeto del juego
		/// </summary>
		public Models.Gaming.GameObjectModel GameObject { get; }
	}
}
