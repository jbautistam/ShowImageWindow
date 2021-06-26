using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

using Bau.Libraries.SpritesEngine.Models.Gaming;
using Bau.Libraries.SpritesEngine.Models;

namespace ShowImageWindow
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Variables privadas
		private EngineModel _engine;
		private object _state = new object();

		public MainWindow()
		{
			// Inicializa los componentes
			InitializeComponent();
			// Inicializa el contexto de sincronización
			ContextUI = SynchronizationContext.Current;
		}

		/// <summary>
		///		Inicializa la ventana
		/// </summary>
		private void InitWindow()
		{
			// Crea el motor de juego y asigna los manejadores de eventos
			_engine = new EngineModel((int) SystemParameters.PrimaryScreenWidth, (int) SystemParameters.PrimaryScreenHeight, TimeSpan.FromMilliseconds(100));
			_engine.Updated += (sender, args) => UpdateCharacters();
			_engine.Created += (sender, args) => CreateCharacterView(args.GameObject);
			_engine.Removed += (sender, args) => RemoveCharacterView(args.GameObject);
			// Añade los personajes
			AddLemmingsEngine(_engine);
			AddSpaceShipsEngine(_engine);
			// Arranca el motor
			_engine.Start();
		}

		/// <summary>
		///		Añade el motor de los Lemmings
		/// </summary>
		private void AddLemmingsEngine(EngineModel engine)
		{
			Bau.Libraries.CharactersLemmings.LemmingsManager manager = new(engine, 5, TimeSpan.FromSeconds(3));

				// Carga el spritesheet
				manager.Load();
				// Inicializa el motor
				manager.Start();
		}

		/// <summary>
		///		Añade el motor de las naves
		/// </summary>
		private void AddSpaceShipsEngine(EngineModel engine)
		{
			//Bau.Libraries.CharactersSpaceShips.SpaceShipsManager manager = new(engine, 5, TimeSpan.FromSeconds(3));

			//	// Carga el spritesheet
			//	manager.Load();
			//	// Inicializa el motor
			//	manager.Start();
		}

		/// <summary>
		///		Obtiene el nombre de un archivo Atlas del directorio de datos
		/// </summary>
		private string GetAtlasFileName(string fileName)
		{
			return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "Data", fileName);
		}

		/// <summary>
		///		Crea la ventana asociada a un <see cref="GameObjectModel"/>
		/// </summary>
		private void CreateCharacterView(GameObjectModel gameObject)
		{
			if (gameObject is VisualGameObjectModel character)
				ContextUI.Send(_ => {
										Views.CharacterView view = new(character);

											// Añade la ventana a la colección
											CharacterWindows.Add(view);
											// Muestra la ventana
											view.Show();
									}, 
							   _state);
		}

		/// <summary>
		///		Elimina la ventana asociada a un <see cref="GameObjectModel"/>
		/// </summary>
		private void RemoveCharacterView(GameObjectModel gameObject)
		{
			if (gameObject is VisualGameObjectModel character)
				ContextUI.Send(_ => {
										for (int index = CharacterWindows.Count - 1; index >= 0; index--)
											if (CharacterWindows[index].GameObject.Id.Equals(gameObject.Id, StringComparison.CurrentCultureIgnoreCase))
											{
												// Cierra la ventana
												CharacterWindows[index].Close();
												// Quita la ventana de la lista
												CharacterWindows.RemoveAt(index);
											}
									}, 
							   _state);
		}

		/// <summary>
		///		Actualiza las ventanas de personajes
		/// </summary>
		private void UpdateCharacters()
		{
			foreach (Views.CharacterView window in CharacterWindows)
				window.Update();
		}

		/// <summary>
		///		Cierra la aplicación
		/// </summary>
		private void ExitApp()
		{
			// Detiene el motor
			_engine.Stop();
			// Cierra las ventanas
			try
			{
				foreach (Views.CharacterView view in CharacterWindows)
					view.Close();
			}
			catch {}
			// Cierra la aplicación
			App.Current.Shutdown();
		}

		/// <summary>
		///		Ventanas de personajes
		/// </summary>
		private List<Views.CharacterView> CharacterWindows { get; } = new();

		/// <summary>
		///		Contexto de sincronización del interface de usuario
		/// </summary>
		private SynchronizationContext ContextUI { get; }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			InitWindow();
		}

		private void cmdAddCharacter_Click(object sender, RoutedEventArgs e)
		{
			//AddCharacter("sheep");
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			ExitApp();
		}
	}
}
