using AntBox.Environnement;

namespace AntBox
{
	public abstract class ObjetAbstrait
	{
		public ZoneAbstraite Position { get; protected set; }
		public string Nom { get; protected set; }
        public int HPMax { get; protected set; } = 7777;
        public int HP { get; protected set; } = 7777;

        public virtual void MiseAJour()
        {
            this.HP--;
        }
	}
}