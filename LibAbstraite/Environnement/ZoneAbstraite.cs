using System;
using System.Collections.Generic;

namespace AntBox.Environnement
{
	public abstract class ZoneAbstraite
	{
		public string Nom { get; protected set; }
		public List<ObjetAbstrait> ObjetList { get; protected set; }
		public List<PersonnageAbstrait> PersonnageList { get; protected set; }
		public List<AccesAbstrait> AccesList { get; protected set; }
        public int positionX { get; protected set; }
        public int positionY { get; protected set; }


        public abstract void AjouterAcces(AccesAbstrait acces);

		public abstract void AjouterObjet(ObjetAbstrait unObjet);

		public abstract void AjouterPersonnage(PersonnageAbstrait unPersonnage);
		public abstract void RetirerPersonnage(PersonnageAbstrait unPersonnage);

		public ZoneAbstraite(string unNom, int positionX, int positionY)
		{
			this.Nom        = unNom;
            this.positionX  = positionX;
            this.positionY  = positionY;
        }

        public override string ToString()
        {
            return this.Nom + "(" + positionX + " x " + positionY + ") " +
                "\n   " + string.Join("\n   ", AccesList) +
                "\n   " + PersonnageList.Count + " personnages"+
                "\n   " + ObjetList.Count + " objets";
        }

        public string PositionString
        {
            get
            {
                return "Zone : ("+ this.positionX + "," + this.positionY + ")";
            }
        }
    }
}