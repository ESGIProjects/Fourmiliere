using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntBox.Environnement;

namespace AntBox.Etat
{
    class EtatFourmiAleatoire : EtatPersonnageAbstrait
    {
        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine("Je viens d'analyser la situation");
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {
            List<AccesAbstrait> accesListDisponible = new List<AccesAbstrait>();
            AccesAbstrait accesSuivant;
            ZoneAbstraite zoneSuivante = null;
            Random random = new Random();

            //pour éviter de se déplacer dans une zone avec une Fourmi copie la liste passée en paramêtre
            foreach (AccesAbstrait acces in accesList)
            {
                accesListDisponible.Add(acces);
            }

            if (accesList.Count <= 0)
                throw new Exception("Fourmi ne peux pas se décider quand accesList est vide");



            while (accesListDisponible.Count > 0)
            {
                Console.WriteLine(zoneActuelle.Nom + " nb accès disponible " + accesListDisponible.Count);

                int tempo = random.Next(0, accesListDisponible.Count);
                Console.WriteLine("random : " + tempo);

                accesSuivant = accesList[tempo];

                if (accesSuivant.ZoneDebut == zoneActuelle)
                {
                    zoneSuivante = accesSuivant.ZoneFin;
                }
                else
                {
                    zoneSuivante = accesSuivant.ZoneDebut;
                }

                if (zoneSuivante.PersonnageList.Count > 0)
                {
                    Console.WriteLine(">0");
                    accesListDisponible.RemoveAt(tempo);

                    zoneSuivante = null;
                }
                else
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
            Console.WriteLine("execution");
        }
    }
}
