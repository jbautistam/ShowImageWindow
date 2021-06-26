using System;

namespace Bau.Libraries.CharactersSpaceShips.Characters
{
	/// <summary>
	///		Clase para creación de naves cada cierto tiempo
	/// </summary>
	public class SpaceShipsBrain : SpritesEngine.Models.Gaming.BrainObjectModel
	{
		// Variables privadas
		private DateTime _lastSpawn = DateTime.Now;

		public SpaceShipsBrain(SpaceShipsManager manager) : base(manager.Engine) 
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
			if ((DateTime.Now - _lastSpawn) > Manager.TimeSpawn && Engine.CountGameObjects(SpaceShipsManager.SpriteName) < Manager.Maximum)
			{
				// Añade un nuevo lemming
				Engine.Add(new SpaceShipsCharacter(Engine, Engine.Store.GetSprite(SpaceShipsManager.SpriteName)));
				// Guarda el tiempo de lanzamiento
				_lastSpawn = DateTime.Now;
			}
		}

		/// <summary>
		///		Manager
		/// </summary>
		internal SpaceShipsManager Manager { get; }
	}
}
