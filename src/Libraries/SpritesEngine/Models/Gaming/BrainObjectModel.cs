using System;

namespace Bau.Libraries.SpritesEngine.Models.Gaming
{
	/// <summary>
	///		Modelo base para los objetos del motor que no tienen información visual
	/// </summary>
	public abstract class BrainObjectModel : GameObjectModel
	{
		public BrainObjectModel(EngineModel engine) : base(engine) {}
	}
}
