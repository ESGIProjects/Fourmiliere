using System;
using System.Collections.Generic;
using AntBox.Environnement;
using AntBox.Observateur;

namespace AntBox
{
    public class Fourmi : PersonnageAbstrait
	{
        public Fourmi(string unNom, Sujet unObservé, int desPointsDeVie=10) : base(unNom, unObservé, desPointsDeVie )
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
			return "Je suis "+Nom+" la fourmi (PV : "+PointDeVie+")";
		}

        public override void Update()
        {
            Console.Write(Nom + "sait que " + Observe.Etat);
        }
    }
}
