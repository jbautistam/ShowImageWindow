using System;

namespace Bau.Libraries.CharactersLemmings.Characters
{
	/// <summary>
	///		Clase para creación de Lemmings cada cierto tiempo
	/// </summary>
	public class LemmingsBrain : SpritesEngine.Models.Gaming.BrainObjectModel
	{
		// Variables privadas
		private DateTime _lastSpawn = DateTime.Now;

		public LemmingsBrain(LemmingsManager manager) : base(manager.Engine) 
		{
			Manager = manager;
		}

		/// <summary>
		///		Inicializa el motor
		/// </summary>
		public override void Initialize()
		{
			// ... no hace falta hacer nada
		}

		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public override void Update()
		{
			if ((DateTime.Now - _lastSpawn) > Manager.TimeSpawn && Engine.CountGameObjects(LemmingsManager.SpriteName) < Manager.Maximum)
			{
				// Añade un nuevo lemming
				Engine.Add(new LemmingsCharacter(Engine, Engine.Store.GetSprite(LemmingsManager.SpriteName)));
				// Guarda el tiempo de lanzamiento
				_lastSpawn = DateTime.Now;
			}
		}

		/// <summary>
		///		Manager
		/// </summary>
		internal LemmingsManager Manager { get; }
	}
}
