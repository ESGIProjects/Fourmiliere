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
            List<AccesAbstrait> accesListDisponible = new List<AccesAbstrait>();
            AccesAbstrait accesSuivant;
            ZoneAbstraite zoneSuivante = null;
            Random random = new Random();

            //pour éviter de se déplacer dans une zone avec une Fourmi copie la liste passée en paramêtre
            foreach (AccesAbstrait acces  in accesList) {
                accesListDisponible.Add(acces);
            }

            if (accesList.Count <= 0)
                throw new Exception("Fourmi ne peux pas se décider quand accesList est vide");

            

            while (accesListDisponible.Count > 0)
            {
                Console.WriteLine(zoneActuelle.Nom +" nb accès disponible " + accesListDisponible.Count);

                int tempo = random.Next(0, accesListDisponible.Count);
                Console.WriteLine("random : " + tempo);

                accesSuivant = accesList[tempo];

                if (accesSuivant.ZoneDebut == zoneActuelle) {
                    zoneSuivante = accesSuivant.ZoneFin;
                } else
                {
                    zoneSuivante = accesSuivant.ZoneDebut;
                }

                if (zoneSuivante.PersonnageList.Count > 0)
                {
                    Console.WriteLine(">0");
                    accesListDisponible.RemoveAt(tempo);

                    zoneSuivante = null;
                } else
                {
                    Console.WriteLine("<=0");
                    accesListDisponible.Clear();
                    Console.WriteLine(accesListDisponible.Count);
                }
            }

            return zoneSuivante;

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
