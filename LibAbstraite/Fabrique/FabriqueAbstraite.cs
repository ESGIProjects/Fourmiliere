using System;

namespace AntBox
{
	public abstract class FabriqueAbstraite
	{
		public string Titre { get; }

		public abstract AccesAbstrait CreerAcces(ZoneAbstraite zoneDebut, ZoneAbstraite zoneFin);
		public abstract EnvironnementAbstrait CreerEnvironnement();
		public abstract ObjetAbstrait CreerObjet(string nom);	//TODO chercher si l'objet à créer peur varier (avec un param en plus)
		public abstract PersonnageAbstrait CreerPersonnage(string nom); //TODO idem objet
		public abstract ZoneAbstraite CreerZone(string nom);

	}
}
