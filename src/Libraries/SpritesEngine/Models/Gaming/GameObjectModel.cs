using System;

namespace Bau.Libraries.SpritesEngine.Models.Gaming
{
	/// <summary>
	///		Modelo base para los objetos del motor
	/// </summary>
	public abstract class GameObjectModel
	{
		public GameObjectModel(EngineModel engine)
		{
			Engine = engine;
		}

		/// <summary>
		///		Inicializa el personaje en el juego
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public abstract void Update();

		/// <summary>
		///		Limita un valor entre un mínimo y un máximo
		/// </summary>
		protected int Clamp(int value, int minimum, int maximum)
		{
			if (value < minimum)
				return minimum;
			else if (value > maximum)
				return maximum;
			else
				return value;
		}

		/// <summary>
		///		Motor al que pertenece el objeto
		/// </summary>
		public EngineModel Engine { get; }

		/// <summary>
		///		Id del objeto
		/// </summary>
		public string Id { get; set; } = Guid.NewGuid().ToString();
	}
}
