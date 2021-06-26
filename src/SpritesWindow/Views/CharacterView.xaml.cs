using System;
using System.Threading;
using System.Windows;

using Bau.Libraries.SpritesEngine.Models.Gaming;

namespace ShowImageWindow.Views
{
	/// <summary>
	///		Ventana para mostrar un personaje del juego
	/// </summary>
	public partial class CharacterView : Window
	{
		// Variables privadas
		private object _state = new object();

		public CharacterView(VisualGameObjectModel gameObject)
		{
			// Inicializa la ventana
			InitializeComponent();
			// Inicializa el personaje
			GameObject = gameObject;
			// Inicializa el contexto de sincronización
			ContextUI = SynchronizationContext.Current;
		}

		/// <summary>
		///		Actualiza la ventana
		/// </summary>
		public void Update()
		{
			ContextUI.Send(_ => Move(), _state);
		}

		/// <summary>
		///		Desplaza la ventana
		/// </summary>
		private void Move()
		{
			if (GameObject.ActualBitmap != null)
			{
				// Asigna la imagen
				imgSprite.Source = GameObject.ActualBitmap.Image;
				// Posiciona la ventana
				Top = GameObject.Top;
				Left = GameObject.Left;
				// Cambia las dimensiones de la ventana
				Width = GameObject.ActualBitmap.Width;
				Height = GameObject.ActualBitmap.Height;
			}
		}

		/// <summary>
		///		Contexto de sincronización del interface de usuario
		/// </summary>
		private SynchronizationContext ContextUI { get; }

		/// <summary>
		///		Carácter
		/// </summary>
		public VisualGameObjectModel GameObject { get; }

		private void imgSprite_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
				GameObject.Engine.Destroy(GameObject);
			else
				DragMove();
		}

		/// <summary>
		///		Cuando se levanta el botón del ratón, se supone que se ha terminado el drag y se reposiciona el personaje
		/// </summary>
		private void imgSprite_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ClickCount == 1)
				GameObject.SetPosition((int) Top, (int) Left);
		}
	}
}
