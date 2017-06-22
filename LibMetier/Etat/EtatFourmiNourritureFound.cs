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
        protected ZoneAbstraite Destination = null;
        protected ZoneAbstraite ZoneSuivante = null;

        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine("Je porte de la bouffe ! ! ! ! !");
            this.Destination = personnage.maison;
            Console.WriteLine("Je viens d'analyser la situation");
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {
            Console.WriteLine("Je porte de la bouffe ! ! ! ! !");
            Console.WriteLine("Je suis ici : " + zoneActuelle.Nom);
            Console.WriteLine("Je dois me rendre ici  : " + destination);

            if (Destination == null)
                return null;

            foreach (AccesAbstrait accesSuivant in accesList)
            {
                if (accesSuivant.ZoneDebut == zoneActuelle)  {
                    ZoneSuivante = accesSuivant.ZoneFin;
                }
                else {
                    ZoneSuivante = accesSuivant.ZoneDebut;
                }

                if ((zoneActuelle.postionX > destination.positionX) && (ZoneSuivante.postionX < zoneActuelle.positionX))
                {
                    return ZoneSuivante;
                } else if ((zoneActuelle.postionX < destination.positionX) && (ZoneSuivante.postionX > zoneActuelle.positionX))
                {
                    return ZoneSuivante;
                }




            }


            return null;
        }

        public override void Execution()
        {
            Console.WriteLine("Je porte de la bouffe ! ! ! ! !");
        }
    }
}
