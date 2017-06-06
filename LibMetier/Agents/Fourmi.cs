using System;
using System.Collections.Generic;
using AntBox.Environnement;
using AntBox.Observateur;

namespace AntBox
{
    public class Fourmi : PersonnageAbstrait
	{
        public Fourmi(string unNom, Subject unObservé, int desPointsDeVie=10) : base(unNom, unObservé, desPointsDeVie )
		{
		}

		public override void AnalyseSituation()
		{
			throw new NotImplementedException();
		}

		public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
		{
            if (accesList.Count <= 0)
                throw new Exception("Fourmi ne peux pas se décider quand accesList est vide");

            AccesAbstrait accesSuivant;
            Random random = new Random();

            accesSuivant = accesList[random.Next(1, accesList.Count)-1];

            return (accesSuivant.ZoneDebut == zoneActuelle) ? accesSuivant.ZoneFin : accesSuivant.ZoneDebut;
		}

		public override void Execution()
		{
            //TODOvoir si on implémente état ou stratégie... stratégie me semble plus adéquat
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return "Je suis "+Nom+" la fourmi (PV : "+PointDeVie+")";
		}

        public override void Update()
        {
            Console.WriteLine(Nom + " sait que " + Observe.Etat);
        }
    }
}
