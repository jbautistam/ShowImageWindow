using System;
using System.Collections.Generic;

using Bau.Libraries.SpritesEngine.Models.Definitions;
using Bau.Libraries.SpritesEngine.Models.Gaming;

namespace Bau.Libraries.SpritesEngine.Models
{
	/// <summary>
	///		Motor del juego
	/// </summary>
	public class EngineModel
	{
		// Eventos públicos
		public event EventHandler Updated;
		public event EventHandler<EventArguments.GameObjectEventArgs> Created;
		public event EventHandler<EventArguments.GameObjectEventArgs> Removed;
		// Variables privadas
		private System.Timers.Timer _timer;

		public EngineModel(int width, int height, TimeSpan loopTime)
		{
			Width = width;
			Height = height;
			LoopTime = loopTime;
		}

		/// <summary>
		///		Añade un carácter a la definición
		/// </summary>
		public void Add(GameObjectModel gameObject)
		{
			// Añade el personaje a la colección
			GameObjects.Add(gameObject);
			// Lo inicializa
			gameObject.Initialize();
			// y lanza un evento indicando que se ha creado
			Created?.Invoke(this, new EventArguments.GameObjectEventArgs(gameObject));
		}

		/// <summary>
		///		Arranca el motor
		/// </summary>
		public void Start()
		{
			// Crea el temporizador si es necesario
			if (_timer == null)
			{
				// Crea el temporizador
				_timer = new System.Timers.Timer(LoopTime.TotalMilliseconds);
				// Asigna el manejador de eventos
				_timer.Elapsed += (sender, args) => Update();
			}
			// Arranca el temporizador
			_timer.Start();
		}

		/// <summary>
		///		Detiene el motor
		/// </summary>
		public void Stop()
		{
			_timer.Stop();
		}

		/// <summary>
		///		Modifica los datos del motor
		/// </summary>
		private void Update()
		{
			// Detiene el temporizador
			Stop();
			// Actualiza primero los objetos del juego sin información visual
			//? No utiliza foreach porque puede que se estén creando objetos entre medias
			for (int index = 0; index < GameObjects.Count; index++)
				if (GameObjects[index] is BrainObjectModel brain)
					brain.Update();
			// y después, los objetos con información visual
			//? No utiliza foreach porque puede que se estén creando objetos entre medias
			for (int index = 0; index < GameObjects.Count; index++)
				if (GameObjects[index] is VisualGameObjectModel visual)
					visual.Update();
			// Indica que se han terminado de modificar los datos
			Updated?.Invoke(this, EventArgs.Empty);
			// Arranca el temporizador
			Start();
		}

		/// <summary>
		///		Destruye un objeto
		/// </summary>
		public void Destroy(GameObjectModel gameObject)
		{
			// Llama al evento
			Removed?.Invoke(this, new EventArguments.GameObjectEventArgs(gameObject));
			// Quita el objeto de la lista
			GameObjects.Remove(gameObject);
		}

		/// <summary>
		///		Cuenta los objetos de juego de determinado sprite
		/// </summary>
		public int CountGameObjects(string spriteName)
		{
			int count = 0;

				// Cuenta los elementos
				foreach (GameObjectModel gameObject in GameObjects)
					if (gameObject is VisualGameObjectModel visual)
						if (visual.Sprite.Id.Equals(spriteName))
							count++;
				// Devuelve el número de elementos
				return count;
		}

		/// <summary>
		///		Almacén de las definiciones
		/// </summary>
		public StoreModel Store { get; } = new();

		/// <summary>
		///		Ancho de la pantalla
		/// </summary>
		public int Width { get; }

		/// <summary>
		///		Alto de la pantalla
		/// </summary>
		public int Height { get; }

		/// <summary>
		///		Tiempo entre ejecuciones del bucle de movimientos
		/// </summary>
		public TimeSpan LoopTime { get; }

		/// <summary>
		///		Lista de objetos del juego
		/// </summary>
		public List<GameObjectModel> GameObjects { get; } = new();

		/// <summary>
		///		Objeto para obtener datos aleatorios
		/// </summary>
		public Random Random { get; } = new();
	}
}
