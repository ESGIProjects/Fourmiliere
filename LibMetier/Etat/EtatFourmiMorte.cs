using System;
using System.Collections.Generic;
using AntBox.Environnement;

namespace AntBox.Etat
{
    public class EtatFourmiMorte : EtatPersonnageAbstrait
    {
        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine(personnage.Nom + "(EtatFourmiMorte) se trouve sur la zone : " + personnage.ZoneActuelle.Nom);
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {
            return null;
        }

        public override void Execution()
        {
        }
    }
}
