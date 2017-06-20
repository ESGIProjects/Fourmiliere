﻿using AntBox.Environnement;
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
        protected EtatPersonnageAbstrait Etat;
        public ZoneAbstraite ZoneActuelle { get; set; }

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

		public PersonnageAbstrait(string unNom, Subject observe, EtatPersonnageAbstrait etat) {
			Nom = unNom;
            Observe = observe;
            Etat = etat;
            observe.Attach(this);
		}
    }
}
