using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntBox.Environnement;

namespace AntBox.Etat
{
    class EtatFourmiNourritureFound : EtatPersonnageAbstrait
    {
        ZoneAbstraite destination = null;

        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine("Je porte de la bouffe ! ! ! ! !");
            //this.destination = personnage.maison;
            Console.WriteLine("Je viens d'analyser la situation");
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {
            Console.WriteLine("Je porte de la bouffe ! ! ! ! !");
            Console.WriteLine("Je suis ici : " + zoneActuelle.Nom);
            Console.WriteLine("Je dois me rendre ici  : " + destination);



            return null;
        }

        public override void Execution()
        {
            Console.WriteLine("Je porte de la bouffe ! ! ! ! !");
        }
    }
}
