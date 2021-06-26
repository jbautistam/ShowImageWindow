using System;

using Bau.Libraries.SpritesEngine.Models.Definitions;

namespace Bau.Libraries.CharactersLemmings.Characters
{
	/// <summary>
	///		Gráficos del lemning
	/// </summary>
	public class LemmingsCharacter : SpritesEngine.Models.Gaming.VisualGameObjectModel
	{
		/// <summary>
		///		Tipos de movimientos
		/// </summary>
		public enum MovementType
		{
			/// <summary>Caída</summary>
			Falling,
			/// <summary>Andar a la izquierda</summary>
			Left,
			/// <summary>Andar a la derecha</summary>
			Right,
			/// <summary>Cavar a la izquierda</summary>
			DiggingLeft,
			/// <summary>Cavar a la derecha</summary>
			DiggingRight
		}
		// Variables privadas
		private MovementType _movement = MovementType.Falling;
		private DateTime _startDig;
		private bool _hasDig;

		public LemmingsCharacter(SpritesEngine.Models.EngineModel engine, SpriteModel sprite) : base(engine, sprite) {}

		/// <summary>
		///		Inicializa el personaje
		/// </summary>
		public override void Initialize()
		{
			// Inicializa la posición
			Top = 0;
			Left = Engine.Random.Next(0, Engine.Width);
			// Inicializa el movimiento
			_movement = MovementType.Falling;
		}

		/// <summary>
		///		Modifica el personaje
		/// </summary>
		public override void Update()
		{
			// Anima el personaje
			Animate(_movement.ToString());
			// Si no estamos en tierra ni cayendo, cambiamos el tipo de movimiento
			//? El usuario puede arrastrar el personaje hacia arriba y debería volver a caer
			if (_movement != MovementType.Falling && !IsGround())
				_movement = MovementType.Falling;
			// Añade el movimiento a las coordenadas
			switch (_movement)
			{
				case MovementType.Left:
						Left -= Speed;
					break;
				case MovementType.Right:
						Left += Speed;
					break;
				case MovementType.Falling:
						if (IsGround())
						{
							// Baja un poco el sprite (por la diferencia de altos entre la animación de caída y la de movimiento)
							Top += 25;
							// Mueve a la izquierda o a la derecha
							if (Engine.Random.Next(100) > 50)
								_movement = MovementType.Left;
							else
								_movement = MovementType.Right;
						}
						else
							Top += Speed;
					break;
			}
			// Pone el lemming a cavar
			if ((_movement == MovementType.Left || _movement == MovementType.Right) && Engine.Random.Next(10) > 8 && !_hasDig)
			{
				// Cambia el movimiento
				if (_movement == MovementType.Left)
					_movement = MovementType.DiggingLeft;
				else
					_movement = MovementType.DiggingRight;
				// Guarda el momento que ha empezado a cavar e indica que ya ha cavado para que no vuelva a hacerlo
				_startDig = DateTime.Now;
				_hasDig = true;
			}
			else if ((_movement == MovementType.DiggingLeft || _movement == MovementType.DiggingRight) && (DateTime.Now - _startDig) > TimeSpan.FromSeconds(10))
			{
				if (_movement == MovementType.DiggingLeft)
					_movement = MovementType.Left;
				else
					_movement = MovementType.Right;
			}
			// Recoloca el sprite si está fuera de la pantalla
			if (Top < 0)
				Top = 0;
			else if (Top + ActualBitmap.Height > Engine.Height)
				Top = Engine.Height - ActualBitmap.Height;
			// Si se sale de la pantalla, lo elimina
			if (Left <= 0 || Left + ActualBitmap.Width >= Engine.Width)
				Engine.Destroy(this);
		}

		/// <summary>
		///		Comprueba si el personaje está en el suelo
		/// </summary>
		private bool IsGround()
		{
			return Top + ActualBitmap.Height > Engine.Height - 50;
		}
	}
}
