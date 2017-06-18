using AntBox.Environnement;

namespace AntBox
{
	public abstract class ObjetAbstrait
	{
		public ZoneAbstraite Position { get; protected set; }
		public string Nom { get; protected set; }

		public abstract void MiseAJour();
	}
}