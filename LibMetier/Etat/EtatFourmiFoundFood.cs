using System;
using System.Collections.Generic;
using AntBox.Environnement;

namespace AntBox.Etat
{
    public class EtatFourmiFoundFood : EtatPersonnageAbstrait
    {
        ZoneAbstraite Destination = null;
        ZoneAbstraite ZoneSuivante = null;

        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine(personnage.Nom + "(EtatFourmiFoundFood) se trouve sur la zone : " + personnage.ZoneActuelle.Nom);
            //Console.WriteLine(personnage.Nom + " à de la nourriture ");

            if ((ZoneSuivante == Destination) && (Destination != null)) {
                Console.WriteLine(personnage.Nom + " vient d'apporter la nourriture dans la fourmilière ! ! ! ! ");
                personnage.Etat = new EtatFourmiAleatoire();
            } else if (personnage.maison != null) {
                Console.WriteLine(personnage.Nom + " cherche à rapporter la nourriture dans la fourmilière ");
                Destination = personnage.maison;
            }
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {

            Console.WriteLine("ChoixZoneSuivante");

            if ((Destination != null) && (Destination == zoneActuelle))
            {
                Console.WriteLine("J'ai apporté la nourriture à la fourmilière ");
            }else  if (Destination != null)
            {
                foreach (AccesAbstrait accesSuivant in accesList)
                {
                    if (accesSuivant.ZoneDebut == zoneActuelle)
                    {
                        ZoneSuivante = accesSuivant.ZoneFin;
                    }
                    else
                    {
                        ZoneSuivante = accesSuivant.ZoneDebut;
                    }

                    if ((zoneActuelle.positionX > Destination.positionX) && (ZoneSuivante.positionX < zoneActuelle.positionX))
                    {
                        return ZoneSuivante;
                    }
                    else if ((zoneActuelle.positionX < Destination.positionX) && (ZoneSuivante.positionX > zoneActuelle.positionX))
                    {
                        return ZoneSuivante;
                    } else if ((zoneActuelle.positionY > Destination.positionY) && (ZoneSuivante.positionY < zoneActuelle.positionY))
                    {
                        return ZoneSuivante;
                    }
                    else if ((zoneActuelle.positionY < Destination.positionY) && (ZoneSuivante.positionY > zoneActuelle.positionY))
                    {
                        return ZoneSuivante;
                    }
                }
            } else
            {
                Console.WriteLine("Pas de destination... Houston il y a un problème ");
            }
            return null;
        }

        public override void Execution()
        {
            Console.WriteLine("execution");
        }
    }
}
