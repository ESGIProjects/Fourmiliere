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



		public override void AnalyseSituation(ZoneAbstraite zoneActuelle)
		{
            //Cette méthode va permettre d'analyser la situation et changer d'état par la suite
		}


		public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
		{
            AnalyseSituation(zoneActuelle);

            if (accesList.Count <= 0)
                throw new Exception("Fourmi ne peux pas se décider quand accesList est vide");

            AccesAbstrait accesSuivant;
            Random random = new Random();

            accesSuivant = accesList[random.Next(1, accesList.Count)-1];

            return (accesSuivant.ZoneDebut == zoneActuelle) ? accesSuivant.ZoneFin : accesSuivant.ZoneDebut;
		}

		public override void Execution()
		{
            //Cette méthode  sera lancé après le déplacement (utile pour prendre de la nourriture par exemple)
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
