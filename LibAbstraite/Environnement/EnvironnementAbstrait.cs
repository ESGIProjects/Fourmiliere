using System;
using System.Collections.Generic;
using AntBox.Factory;
using System.Collections.ObjectModel;

namespace AntBox.Environnement
{
    /**
     * EnvironnementAbstrait représente l'environnement de simulation, mais il agit aussi comme un client d'une abstract factory
     */
	public abstract class EnvironnementAbstrait
	{
        //la fourmilière
        public ZoneAbstraite Fourmiliere { get; set; } = null;

        //les éléments composants l'environnement
        public List<ObjetAbstrait> ObjetList { get; protected set; }
		public List<AccesAbstrait> AccesList { get; protected set; }
		public List<ZoneAbstraite> ZoneList { get; protected set; }
        public ObservableCollection<PersonnageAbstrait> PersonnageList { get; protected set; }
        public FabriqueAbstraite fabriqueAbstraite;

        //le constructeur
        public EnvironnementAbstrait(FabriqueAbstraite fabrique)
        {
            this.fabriqueAbstraite = fabrique;
        }
        

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

        public abstract void Generation(int nbColonne, int nbLigne);

	}
}