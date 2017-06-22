using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntBox.Factory;
using AntBox;
using AntBox.Observateur;
using System.Collections.ObjectModel;

namespace AntBox.Environnement
{
    public class Jardin : EnvironnementAbstrait
    {
        public Jardin(FabriqueAbstraite fabrique) : base(fabrique)
        {
            ObjetList = new List<ObjetAbstrait>();
            AccesList = new List<AccesAbstrait>();
            ZoneList = new List<ZoneAbstraite>();
            PersonnageList = new ObservableCollection<PersonnageAbstrait>();
        }

        public override void AjouteChemins(/*FabriqueAbstraite fabrique, */params AccesAbstrait[] accesArray)
        {
            this.AccesList.AddRange(accesArray);
        }

        public override void AjouteObjet(ObjetAbstrait unObjet)
        {
            this.ObjetList.Add(unObjet);
            //TODO peut être positionner cet objet sur une zone ?
        }

        public override void AjoutePersonnage(PersonnageAbstrait unPersonnage)
        {
            this.PersonnageList.Add(unPersonnage);
        }

        public override void AjouteZoneAbstraites(params ZoneAbstraite[] zoneAbstraitesArray)
        {
            this.ZoneList.AddRange(zoneAbstraitesArray);
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
          
            zoneSource.RetirerPersonnage(unPersonnage);
            zoneFin.AjouterPersonnage(unPersonnage);

        }


         /**
          * Méthode permettant d'afficher toute les données du jeu
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

            //nombre de fourmi et de nourriture
            int nbFourmiAGenerer        = ((nbColonne * nbLigne) / 10);
            int nbNourritureAGenerer    = ((nbColonne * nbLigne) / 20);

            //poour avoir au moins une fourmis et un gateau
            nbFourmiAGenerer        = (nbFourmiAGenerer != 0) ? nbFourmiAGenerer : 1;
            nbNourritureAGenerer    = (nbNourritureAGenerer != 0) ? nbNourritureAGenerer : 1;

            //nécessaire pour la génération aléatoire des fourmis
            Random random = new Random();

            //permet de connaitre le nombre de chiffre dans nbColonne et nbLigne pour l'affichage
            int nbCharColonne = nbColonne.ToString().Length;
            int nbCharLigne = nbLigne.ToString().Length;

            //détermination de l'emplacement de la fourmilière
            int positionXFourmiliere = (nbColonne%2 == 0)? (nbColonne / 2) : ((nbColonne / 2) +1);
            int positionYFourmiliere = (nbLigne % 2 == 0) ? (nbLigne / 2) : ((nbLigne / 2) + 1);

            //les objets temporaires
            ZoneAbstraite zoneDebut;
            ZoneAbstraite zoneFin;
            AccesAbstrait acces;

            //la fourmilière
            ZoneAbstraite fourmiliere = null;

            //compteur du nombre de zone (nécessaire quand on génère nos cases)
            int nombreZone = 0;

            //////génération du jardin
            for (int y = 1; y <= nbLigne; y++)
            {
                for (int x = 1; x <= nbColonne; x++)
                {
                    //création du nom de la zone
                    tempoNom =  new string('0', nbCharColonne - x.ToString().Length) + x.ToString() + "x" +  new string('0', nbCharLigne - y.ToString().Length) + y.ToString();

                    //création de la zone et ajout dans le jardin
                    if ((x ==  positionXFourmiliere) && (y == positionYFourmiliere))
                    {
                        fourmiliere = this.fabriqueAbstraite.CreerZoneSpeciale("Fourmiliere " + tempoNom, x, y);
                        this.AjouteZoneAbstraites(fourmiliere);
                    } else  {
                        this.AjouteZoneAbstraites(this.fabriqueAbstraite.CreerZone("Zone " + tempoNom, x, y));
                    }
                    

                    nombreZone = this.ZoneList.Count;

                    //si on a 2 zones ou plus, alors la dernière zone à une case voisine à sa gauche (sauf si la première colonne)
                    if ((nombreZone >= 2) && (x > 1))
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
                int colX = random.Next(1, nbColonne+1);
                int colY = random.Next(1, nbLigne+1);

                var zone        = this.ZoneList[((colY - 1) * nbColonne + colX - 1)];
                var personnage = this.fabriqueAbstraite.CreerPersonnage("Fourmi " + a,  AntWeather.SharedAntWeather, fourmiliere);

                zone.AjouterPersonnage(personnage); //ajout du personnage sur une zone
                this.AjoutePersonnage(personnage);  //ajout du personnage dans le jardin pour faciliter le binding

            }

            //////Génération de la nourriture
            Console.WriteLine("Nourriture : " + nbNourritureAGenerer);
            for (int a = 0; a < nbNourritureAGenerer; a++)
            {
                int colX = random.Next(1, nbColonne + 1);
                int colY = random.Next(1, nbLigne + 1);

                var zone        = this.ZoneList[((colY - 1) * nbColonne + colX - 1)];
                var nourriture  = this.fabriqueAbstraite.CreerObjet(FabriqueFourmiliere.TypeObjetNourriture);

                zone.AjouterObjet(nourriture);  //ajouter de la nourriture sur la zone
                this.AjouteObjet(nourriture);   //ajout de l'objet dans le jardin pour faciliter le binding
            }
        }

        
        /**
         * 
         */
        public override string Simuler()
        {
            ZoneAbstraite zoneSelectionne;
            String simulation = "";
            PersonnageAbstrait personageEnCours;
            List<PersonnageAbstrait> personnageAyantDejaBouge = new List<PersonnageAbstrait>();

            foreach (ZoneAbstraite zone in ZoneList)
            {
                for (int a =0; a < zone.PersonnageList.Count; a++)
                {
                    personageEnCours = zone.PersonnageList[a];

                    if (!personnageAyantDejaBouge.Contains(personageEnCours)) {
                        simulation += "\n" + personageEnCours.Nom + " (sur "+zone.Nom+") n'a pas encore bougée";

                        personageEnCours.AnalyseSituation();

                        zoneSelectionne = personageEnCours.ChoixZoneSuivante(zone.AccesList, zone);

                        if (zoneSelectionne != null)
                        {
                            simulation += "\n" + personageEnCours.Nom + " souhaite se rendre sur : " + zoneSelectionne.Nom;
                            this.DeplacerPersonnage(personageEnCours, zone, zoneSelectionne);
                            simulation += "\n" + personageEnCours.Nom + " vient de se déplacer sur "+ zoneSelectionne.Nom;


                            personageEnCours.Execution();
                            personnageAyantDejaBouge.Add(personageEnCours);
                        } else
                        {
                            simulation += "\n" + personageEnCours.Nom + " n'a pas trouvé de zone sur laquelle se déplacer";
                        }
                    } else {
                        simulation += "\n" + personageEnCours.Nom + " a déjà bougée";
                    }
                }
            }
            return simulation;
        }
    }
}
