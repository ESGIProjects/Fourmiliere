using System;
using AntBox.Environnement;

namespace AntBox.Factory
{
	public abstract class FabriqueAbstraite
	{
		public string Titre { get; }

		public abstract AccesAbstrait CreerAcces(ZoneAbstraite zoneDebut, ZoneAbstraite zoneFin);
		public abstract EnvironnementAbstrait CreerEnvironnement();
		public abstract ObjetAbstrait CreerObjet(string nom);
        public abstract PersonnageAbstrait CreerPersonnage(string nom, Observateur.Sujet unObservé);
        public abstract PersonnageAbstrait CreerPersonnage(string nom, Observateur.Sujet unObservé, int desPointsDeVie);
        public abstract ZoneAbstraite CreerZone(string nom);

	}
}
