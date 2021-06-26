using System;

namespace Bau.Libraries.CharactersSpaceShips
{
	/// <summary>
	///		Manager para la preparación de los objetos de juego SpaceShips
	/// </summary>
	public class SpaceShipsManager
	{
		// Constantes internas
		internal const string SpriteName = "Ships";
		private const string ShipFlying = nameof(ShipFlying);

		public SpaceShipsManager(SpritesEngine.Models.EngineModel engine, int maximum, TimeSpan timeSpawn)
		{
			Engine = engine;
			Maximum = maximum;
			TimeSpawn = timeSpawn;
		}

		/// <summary>
		///		Carga las hojas de sprites y las animaciones
		/// </summary>
		public void Load()
		{
			Engine.Store.Add(LoadMovements(GetAtlasFileName("spaceship-sprite-sheet.png")));
		}

		/// <summary>
		///		Obtiene el nombre de un archivo Atlas del directorio de datos
		/// </summary>
		private string GetAtlasFileName(string fileName)
		{
			return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "Data", fileName);
		}

		/// <summary>
		///		Carga los sprites y las animaciones del archivo de movimiento
		/// </summary>
		private SpritesEngine.Models.Definitions.StoreModel LoadMovements(string spriteSheet)
		{
			return new SpritesEngine.Builders.StoreBuilder()
							.WithBitmaps(new SpritesEngine.Controllers.AtlasManager().Separe(ShipFlying, spriteSheet, 62, 62))
							.WithSprite(SpriteName)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.Right.ToString())
									.WithSteps(ShipFlying, 0, 3)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.Left.ToString())
									.WithSteps(ShipFlying, 4, 7)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.Up.ToString())
									.WithSteps(ShipFlying, 8, 11)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.Down.ToString())
									.WithSteps(ShipFlying, 12, 15)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.UpRight.ToString())
									.WithSteps(ShipFlying, 16, 19)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.UpLeft.ToString())
									.WithSteps(ShipFlying, 20, 23)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.DownLeft.ToString())
									.WithSteps(ShipFlying, 24, 27)
								.WithAnimation(Characters.SpaceShipsCharacter.MovementType.DownRight.ToString())
									.WithSteps(ShipFlying, 28, 31)
								.Back()
						.Back()
					.Build();
		}

		/// <summary>
		///		Añade el manejador al motor
		/// </summary>
		public void Start()
		{
			Engine.Add(new Characters.SpaceShipsBrain(this));
		}

		/// <summary>
		///		Motor del juego
		/// </summary>
		internal SpritesEngine.Models.EngineModel Engine { get; }

		/// <summary>
		///		Número máximo de lemmings
		/// </summary>
		public int Maximum { get; set; } 
		
		/// <summary>
		///		Tiempo entre lanzamiento de lemming
		/// </summary>
		public TimeSpan TimeSpawn { get; set; }
	}
}
