using AntBox.Environnement;

namespace AntBox
{
	public abstract class ObjetAbstrait
	{
		public ZoneAbstraite Position { get; protected set; }
		public string Nom { get; set; }
        public int HPMax { get; set; } = 7777;
        public int HP { get; set; } = 7777;

        public virtual void MiseAJour()
        {
            this.HP--;
        }
	}
}