using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntBox.Factory;
using AntBox;
using AntBox.Observateur;

namespace AntBox.Environnement
{
    public class Jardin : EnvironnementAbstrait
    {
        public Jardin(FabriqueAbstraite fabrique) :base(fabrique)
        {
            ObjetList = new List<ObjetAbstrait>();
            AccesList = new List<AccesAbstrait>();
            ZoneList = new List<ZoneAbstraite>();
         }

        public override void AjouteChemins(/*FabriqueAbstraite fabrique, */params AccesAbstrait[] accesArray)
        {
            AccesList.AddRange(accesArray);
        }

        public override void AjouteObjet(ObjetAbstrait unObjet)
        {
            ObjetList.Add(unObjet);
            //TODO peut être positionner cet objet sur une zone ?
        }

        public override void AjoutePersonnage(PersonnageAbstrait unPersonnage)
        {
            throw new NotImplementedException();
            //TODO positionner ce personnage sur une zone ? 
        }

        public override void AjouteZoneAbstraites(params ZoneAbstraite[] zoneAbstraitesArray)
        {
            ZoneList.AddRange(zoneAbstraitesArray);
        }

        public override void ChargerEnvironnement(FabriqueAbstraite fabrique)
        {
            throw new NotImplementedException();
        }

        public override void ChargerObjets(FabriqueAbstraite fabrique)
        {
            throw new NotImplementedException();
        }

        public override void ChargerPersonnages(FabriqueAbstraite fabrique)
        {
            throw new NotImplementedException();
        }

        public override void DeplacerPersonnage(PersonnageAbstrait unPersonnage, ZoneAbstraite zoneSource, ZoneAbstraite zoneFin)
        {
            //TODO
            throw new NotImplementedException();
        }


        /**
         * 
         */
        public override string Simuler()
        {
            ZoneAbstraite zoneSelectionne;
            String simulation = "";

           foreach (ZoneAbstraite zone in ZoneList) {
                foreach(PersonnageAbstrait personnage in zone.PersonnageList) {
                    zoneSelectionne = personnage.ChoixZoneSuivante(zone.AccesList, zone);
                    simulation += "\n" + personnage.Nom + " devrait se rendre sur : " + zoneSelectionne.Nom;
                }
            }
            return simulation;
        }



         /**
          * 
          */
        public override string Statistiques()
        {   
            return string.Join("\n", ZoneList);
        }



        /**
         * Cette méthode permet de générer automatiquement une grille 
         */
        public override void Generation(int nbColonne, int nbLigne)
        {
            //nom temporaire d'une des zones 
            String tempoNom = "";

            //nombre de fourmi
            int nbFourmiAGenerer = ((nbColonne * nbLigne) / 10);
            
            //nécessaire pour la génération aléatoire des fourmis
            Random random = new Random();

            //permet de connaitre le nombre de chiffre dans nbColonne et nbLigne pour l'affichage
            int nbCharColonne = nbColonne.ToString().Length;
            int nbCharLigne = nbLigne.ToString().Length;

            //les objets temporaires
            ZoneAbstraite zoneDebut;
            ZoneAbstraite zoneFin;
            AccesAbstrait acces;

            //compteur du nombre de zone (nécessaire quand on génère nos cases)
            int nombreZone = 0;

            //////génération du jardin
            for (int y = 1; y <= nbLigne; y++)
            {
                for (int x = 1; x <= nbColonne; x++)
                {
                    //création du nom de la zone
                    tempoNom = "zone " +
                        new string('0', nbCharColonne - x.ToString().Length) + x.ToString() +
                        "x" +
                        new string('0', nbCharLigne - y.ToString().Length) + y.ToString();

                    //création de la zone et ajout dans le jardin
                    this.AjouteZoneAbstraites(this.fabriqueAbstraite.CreerZone(tempoNom, x, y));

                    nombreZone = this.ZoneList.Count;

                    //si on a 2 zones ou plus, alors la dernière zone à une case voisine à sa gauche
                    if (nombreZone >= 2)
                    {
                        zoneDebut = this.ZoneList[nombreZone - 2];
                        zoneFin = this.ZoneList[nombreZone - 1];
                        acces = this.fabriqueAbstraite.CreerAcces(zoneDebut, zoneFin);

                        this.AjouteChemins(acces);
                    }

                    //si en soustrayant le nombre max de colonne au nombre de case on a une valeur positive, alors la dernière case à une case voisine au dessus
                    if ((nombreZone - nbColonne) >= 1)
                    {
                        zoneDebut = this.ZoneList[nombreZone - nbColonne - 1];
                        zoneFin = this.ZoneList[nombreZone - 1];
                        acces = this.fabriqueAbstraite.CreerAcces(zoneDebut, zoneFin);
                        this.AjouteChemins(acces);
                    }
                }
            }


            //////Génération des fourmis
            for (int a = 0; a < nbFourmiAGenerer; a++)
            {
                int colX = random.Next(1, nbColonne);
                int colY = random.Next(1, nbLigne);

                var zone = this.ZoneList[((colY - 1) * nbColonne + colX - 1)];

                zone.AjouterPersonnage(this.fabriqueAbstraite.CreerPersonnage("Fourmi " + a, AntWeather.SharedAntWeather ));
            }


        }
    }
}
