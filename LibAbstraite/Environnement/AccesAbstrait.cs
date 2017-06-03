namespace AntBox.Environnement
{
	public abstract class AccesAbstrait
	{
		public ZoneAbstraite ZoneDebut { get; protected set; }
		public ZoneAbstraite ZoneFin { get; protected set; }
        public override string ToString()
        {
            return "Accès " + ZoneDebut.Nom + " <=> " + ZoneFin.Nom;
        }


    }
}