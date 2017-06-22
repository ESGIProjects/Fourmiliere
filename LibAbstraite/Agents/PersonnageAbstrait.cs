using AntBox.Environnement;
using System;
using System.Collections.Generic;
using AntBox.Observateur;
using AntBox.Etat;

namespace AntBox
{
	public abstract class PersonnageAbstrait : Observateur.Observer

    {
		public string Nom { get; protected set; }
        public Subject Observe { get; protected set; }
        public EtatPersonnageAbstrait Etat { get;  set; }
        public ZoneAbstraite ZoneActuelle { get; set; }
        public ZoneAbstraite maison { get; protected set; }

        public virtual void AnalyseSituation()
        {
            Etat.AnalyseSituation(this);
        }

        public virtual ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle) {
            return Etat.ChoixZoneSuivante(accesList, zoneActuelle);
        }

		public virtual void Execution()
        {
            Etat.Execution();
        }

		public PersonnageAbstrait(string unNom, Subject observe, ZoneAbstraite maison, EtatPersonnageAbstrait etat) {
			Nom = unNom;
            Observe = observe;
            Etat = etat;
            observe.Attach(this);
            this.maison = maison;
		}
    }
}
