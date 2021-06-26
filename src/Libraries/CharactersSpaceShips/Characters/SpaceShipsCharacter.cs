using System;

using Bau.Libraries.SpritesEngine.Models.Definitions;

namespace Bau.Libraries.CharactersSpaceShips.Characters
{
	/// <summary>
	///		Manejador de los personajes de naves epseciales
	/// </summary>
	public class SpaceShipsCharacter : SpritesEngine.Models.Gaming.VisualGameObjectModel
	{
		// Tipo de movimiento
		public enum MovementType
		{
			Up,
			Down,
			Left,
			Right,
			UpLeft,
			UpRight,
			DownLeft,
			DownRight
		}
		// Variables privadas
		private MovementType _movement;
		private int _speedX, _speedY;
		private DateTime _lastMovementUpdate = DateTime.Now.AddDays(-1);

		public SpaceShipsCharacter(SpritesEngine.Models.EngineModel engine, SpriteModel sprite) : base(engine, sprite) {}

		/// <summary>
		///		Inicializa el personaje
		/// </summary>
		public override void Initialize()
		{
			switch (Engine.Random.Next(4))
			{
				case 0: // arranca desde la parte superior de la pantalla
						Top = 0;
						Left = Engine.Random.Next(0, Engine.Width);
						_movement = MovementType.Down;
					break;
				case 1: // arranca desde la parte inferior de la pantalla
						Top = Engine.Height - 20;
						Left = Engine.Random.Next(0, Engine.Width);
						_movement = MovementType.Up;
					break;
				case 2: // arranca desde la parte izquierda de la pantalla
						Top = Engine.Random.Next(0, Engine.Height);
						Left = 0;
						_movement = MovementType.Right;
					break;
				case 3: // arranca desde la parte derecha de la pantalla
						Top = Engine.Random.Next(0, Engine.Height);
						Left = Engine.Width;
						_movement = MovementType.Left;
					break;
			}
		}

		/// <summary>
		///		Modifica el personaje
		/// </summary>
		public override void Update()
		{
			// Obtiene la dirección del movimiento
			if ((DateTime.Now - _lastMovementUpdate).TotalSeconds > 5)
				_movement = (MovementType) Engine.Random.Next(8);
			// Asigna la velocidad
			switch (_movement)
			{
				case MovementType.Down:
						_speedX = 0;
						_speedY = 10;
					break;
				case MovementType.Up:
						_speedX = 0;
						_speedY = -10;
					break;
				case MovementType.Left:
						_speedX = -10;
						_speedY = 0;
					break;
				case MovementType.Right:
						_speedX = 10;
						_speedY = 0;
					break;
				case MovementType.DownLeft:
						_speedX = -10;
						_speedY = 10;
					break;
				case MovementType.DownRight:
						_speedX = 10;
						_speedY = 10;
					break;
				case MovementType.UpLeft:
						_speedX = -10;
						_speedY = -10;
					break;
				case MovementType.UpRight:
						_speedX = 10;
						_speedY = -10;
					break;
			}
			// Anima el personaje
			Animate(_movement.ToString());
			// Incrementa la posición
			Top += _speedY;
			Left += _speedX;
			// Si se sale de la pantalla, lo elimina
			if (ActualBitmap != null && (Left < 0 || Left + ActualBitmap.Width > Engine.Width || Top < 0 || Top + ActualBitmap.Height > Engine.Height))
				Engine.Destroy(this);
		}
	}
}
