using AntBox.Environnement;
using System;
using System.Collections.Generic;
using AntBox.Observateur;

namespace AntBox
{
	public abstract class PersonnageAbstrait : Observateur.Observer

    {
		public string Nom { get; protected set; }
		public int PointDeVie { get; protected set; }
        public Subject Observe { get; protected set; }

        public ZoneAbstraite Position { get; protected set; }

		public abstract void AnalyseSituation();

		public abstract void Execution();

		public PersonnageAbstrait(string unNom, Subject observe, int desPointsDeVie) {
			PointDeVie = desPointsDeVie;
			Nom = unNom;
            Observe = observe;
            observe.Attach(this);
		}

	}
}
