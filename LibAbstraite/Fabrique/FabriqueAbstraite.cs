using System;
using AntBox.Environnement;
using AntBox.Observateur;
using AntBox.Etat;

namespace AntBox.Factory
{
	public abstract class FabriqueAbstraite
	{
		public string Titre { get; }

		public abstract AccesAbstrait CreerAcces(ZoneAbstraite zoneDebut, ZoneAbstraite zoneFin);
		public abstract EnvironnementAbstrait CreerEnvironnement();
		public abstract ObjetAbstrait CreerObjet(string nom);
        public abstract PersonnageAbstrait CreerPersonnage(string nom, Subject unObservé, ZoneAbstraite maison);
        public abstract PersonnageAbstrait CreerPersonnage(string nom, Subject unObservé, ZoneAbstraite maison, EtatPersonnageAbstrait etat);
        public abstract ZoneAbstraite CreerZone(string nom, int positionX, int positionY);
        public abstract ZoneAbstraite CreerZoneSpeciale(string nom, int positionX, int positionY);


    }
}
