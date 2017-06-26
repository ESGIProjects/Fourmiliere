using System;
using System.Collections.Generic;
using AntBox.Environnement;

namespace AntBox.Etat
{
    public class EtatFourmiFuirPluie : EtatPersonnageAbstrait
    {
        ZoneAbstraite Destination = null;
        ZoneAbstraite ZoneSuivante = null;

        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine(personnage.Nom + " cherche à rentrer rapidement dans la fourmillière pour cause de prévision de pluie ");

            if ((ZoneSuivante == Destination) && (Destination != null))
            {
                Console.WriteLine(personnage.Nom + " est à l'abri et ne bouge pas ! ! ! ! ");
                //personnage.Etat = new EtatFourmiAbri();
            }
            else if (personnage.maison != null)
            {
                Destination = personnage.maison;
            }
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {

            Console.WriteLine("ChoixZoneSuivante");

            if ((Destination != null) && (Destination == zoneActuelle))
            {
                Console.WriteLine("Je suis à l'abri  ! ! ! ");
            }
            else if (Destination != null)
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
                    }
                    else if ((zoneActuelle.positionY > Destination.positionY) && (ZoneSuivante.positionY < zoneActuelle.positionY))
                    {
                        return ZoneSuivante;
                    }
                    else if ((zoneActuelle.positionY < Destination.positionY) && (ZoneSuivante.positionY > zoneActuelle.positionY))
                    {
                        return ZoneSuivante;
                    }
                }
            }
            else
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
