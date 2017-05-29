using System;
using System.Collections.Generic;

namespace AntBox
{
    public class Fourmi : PersonnageAbstrait
	{
        public Fourmi(string unNom, int desPointsDeVie) : base(unNom, desPointsDeVie)
		{
		}

		public override void AnalyseSituation()
		{
			throw new NotImplementedException();
		}

		public ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList)
		{
			throw new NotImplementedException();
		}

		public override void Execution()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return "Je suis une fourmi parmis tant d'autre";
		}

	}
}
