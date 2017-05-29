using System;
using System.Collections.Generic;

namespace AntBox
{
	public abstract class PersonnageAbstrait
	{
		public string Nom { get; protected set; }
		public int PointDeVie { get; protected set; }
		public ZoneAbstraite Position { get; protected set; }

		public abstract void AnalyseSituation();

		public abstract void Execution();

		public PersonnageAbstrait(string unNom, int desPointsDeVie) {
			PointDeVie = desPointsDeVie;
			Nom = unNom;
		}

	}
}
