using System;
using System.Collections.Generic;
using AntBox.Environnement;

namespace AntBox.Etat
{
    class EtatFourmiFoundFood : EtatPersonnageAbstrait
    {
        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine(personnage.Nom + "A DE LA BOUFFE ");

        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {

            Console.WriteLine("ChoixZoneSuivante");
            return null;
        }

        public override void Execution()
        {


            Console.WriteLine("execution");
        }
    }
}
