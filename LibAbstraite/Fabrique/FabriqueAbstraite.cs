using System;
using AntBox.Environnement;
using AntBox.Observateur;

namespace AntBox.Factory
{
	public abstract class FabriqueAbstraite
	{
		public string Titre { get; }

		public abstract AccesAbstrait CreerAcces(ZoneAbstraite zoneDebut, ZoneAbstraite zoneFin);
		public abstract EnvironnementAbstrait CreerEnvironnement();
		public abstract ObjetAbstrait CreerObjet(string nom);
        public abstract PersonnageAbstrait CreerPersonnage(string nom, Subject unObservé);
        public abstract PersonnageAbstrait CreerPersonnage(string nom, Subject unObservé, int desPointsDeVie);
        public abstract ZoneAbstraite CreerZone(string nom, int positionX, int positionY);


    }
}
