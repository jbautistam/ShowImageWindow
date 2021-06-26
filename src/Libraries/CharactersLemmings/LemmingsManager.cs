using System;

namespace Bau.Libraries.CharactersLemmings
{
	/// <summary>
	///		Manager para la preparación de los objetos de juego Lemmings
	/// </summary>
	public class LemmingsManager
	{
		// Constantes internas
		internal const string SpriteName = "Lemmings";
		private const string LemmingsFalling = nameof(LemmingsFalling);
		private const string LemmingsWalking = nameof(LemmingsWalking);
		private const string LemmingsDigging = nameof(LemmingsDigging);

		public LemmingsManager(SpritesEngine.Models.EngineModel engine, int maximum, TimeSpan timeSpawn)
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
			Engine.Store.Add(LoadWalking(GetAtlasFileName("lemmings_walking_spritesheet.png")));
			Engine.Store.Add(LoadFalling(GetAtlasFileName("lemmings_falling_spritesheet.png")));
			Engine.Store.Add(LoadDigging(GetAtlasFileName("lemmings_digging_spritesheet.png")));
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
		private SpritesEngine.Models.Definitions.StoreModel LoadWalking(string spriteSheet)
		{
			return new SpritesEngine.Builders.StoreBuilder()
							.WithBitmaps(new SpritesEngine.Controllers.AtlasManager().Separe(LemmingsWalking, spriteSheet, 25, 40))
							.WithSprite(SpriteName)
								.WithAnimation(Characters.LemmingsCharacter.MovementType.Right.ToString())
									.WithSteps(LemmingsWalking, 0, 7)
								.WithAnimation(Characters.LemmingsCharacter.MovementType.Left.ToString())
									.WithSteps(LemmingsWalking, 8, 15)
								.Back()
						.Back()
					.Build();
		}

		/// <summary>
		///		Carga los sprites y las animaciones del archivo para las caídas
		/// </summary>
		private SpritesEngine.Models.Definitions.StoreModel LoadFalling(string spriteSheet)
		{
			return new SpritesEngine.Builders.StoreBuilder()
							.WithBitmaps(new SpritesEngine.Controllers.AtlasManager().Separe(LemmingsFalling, spriteSheet, 42, 65))
							.WithSprite(SpriteName)
								.WithAnimation(Characters.LemmingsCharacter.MovementType.Falling.ToString())
									.WithSteps(LemmingsFalling, 0, 9)
								.Back()
						.Back()
					.Build();
		}

		/// <summary>
		///		Carga los sprites y las animaciones del archivo de movimiento
		/// </summary>
		private SpritesEngine.Models.Definitions.StoreModel LoadDigging(string spriteSheet)
		{
			return new SpritesEngine.Builders.StoreBuilder()
							.WithBitmaps(new SpritesEngine.Controllers.AtlasManager().Separe(LemmingsDigging, spriteSheet, 64, 56))
							.WithSprite(SpriteName)
								.WithAnimation(Characters.LemmingsCharacter.MovementType.DiggingRight.ToString())
									.WithSteps(LemmingsDigging, 0, 23)
								.WithAnimation(Characters.LemmingsCharacter.MovementType.DiggingLeft.ToString())
									.WithSteps(LemmingsDigging, 24, 47)
								.Back()
						.Back()
					.Build();
		}

		/// <summary>
		///		Añade el manejador al motor
		/// </summary>
		public void Start()
		{
			Engine.Add(new Characters.LemmingsBrain(this));
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
