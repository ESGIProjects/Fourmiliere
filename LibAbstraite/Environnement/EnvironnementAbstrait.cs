using System;
using System.Collections.Generic;
using AntBox.Factory;

namespace AntBox.Environnement
{
	public abstract class EnvironnementAbstrait
	{
		public List<ObjetAbstrait> ObjetList { get; protected set; }
		public List<AccesAbstrait> AccesList { get; protected set; }
		public List<ZoneAbstraite> ZoneList { get; protected set; }

        //TODO comprendre l'intéret de FabriqueAbstraite fabrique ici je ne vois pas
		public abstract void AjouteChemins(/*FabriqueAbstraite fabrique,*/ params AccesAbstrait[] accesArray);
		public abstract void AjouteObjet (ObjetAbstrait unObjet);
		public abstract void AjoutePersonnage(PersonnageAbstrait unPersonnage);
		public abstract void AjouteZoneAbstraites (params ZoneAbstraite[] zoneAbstraitesArray);

        public abstract void ChargerEnvironnement (FabriqueAbstraite fabrique);
		public abstract void ChargerObjets (FabriqueAbstraite fabrique);
		public abstract void ChargerPersonnages (FabriqueAbstraite fabrique);
		public abstract void DeplacerPersonnage(PersonnageAbstrait unPersonnage, ZoneAbstraite zoneSource, ZoneAbstraite zoneFin);

        public abstract string Simuler();
		public abstract string Statistiques();

	}
}