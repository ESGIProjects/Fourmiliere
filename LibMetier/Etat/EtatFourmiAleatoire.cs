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
        protected List<ZoneAbstraite> ZonesPrecedentes = new List<ZoneAbstraite>();
        protected ZoneAbstraite ZoneSuivante = null;

        public override void AnalyseSituation(PersonnageAbstrait personnage)
        {
            Console.WriteLine("Je me trouve sur la zone : " + personnage.ZoneActuelle);


           foreach(ZoneAbstraite z in  ZonesPrecedentes)
            {
                Console.WriteLine(z.Nom + " déjà parcouru");
            }

            foreach (ObjetAbstrait objet in personnage.ZoneActuelle.ObjetList)
            {

                if (objet is Nourriture)
                {
                    Console.WriteLine("IL Y A DE LA BOUFFE OU JE SUIS ! ! ! ! ! ! ! ");

                    //TODO changer d'ETAT
                    //TODO changer d'état 
                    //TODO changer d'ETAT
                    //TODO changer d'état 
                    //TODO changer d'ETAT
                    //TODO changer d'état 
                    //TODO changer d'ETAT
                    //TODO changer d'ETAT
                    //TODO changer d'état 
                    //TODO changer d'état 

                }
            }

            Console.WriteLine("Je viens d'analyser la situation");
        }

        public override ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle)
        {
            List<AccesAbstrait> accesListDisponible = new List<AccesAbstrait>();
            AccesAbstrait accesSuivant;

            Random random = new Random();

            if (accesList.Count <= 0)
                throw new Exception("Fourmi ne peux pas se décider quand accesList est vide");

            //si la zone en cours n'est pas dans la liste des zones parcourues on l'ajoute à la listes des zones connues par notre etat
            if (!ZonesPrecedentes.Contains(zoneActuelle))
            {
                Console.WriteLine(zoneActuelle.Nom + " Ajouté dans ma fourmis");
                ZonesPrecedentes.Add(zoneActuelle);
            }

            foreach (AccesAbstrait acces in accesList)
            {
                accesListDisponible.Add(acces);
            }

            Console.WriteLine("Nombre d'accès disponible non utilisés pour le moment : " + accesListDisponible.Count);


            while (accesListDisponible.Count > 0)
            {
                Console.WriteLine(zoneActuelle.Nom + " nb accès disponible " + accesListDisponible.Count);

                int tempo = random.Next(0, accesListDisponible.Count);
                Console.WriteLine("random : " + tempo);

                accesSuivant = accesList[tempo];

                if (accesSuivant.ZoneDebut == zoneActuelle)
                {
                    ZoneSuivante = accesSuivant.ZoneFin;
                }
                else
                {
                    ZoneSuivante = accesSuivant.ZoneDebut;
                }

                Console.WriteLine(ZoneSuivante.Nom + " est la zone suivante");
                //Si il y a plusieurs fourmis sur la zone suivante, ou qu'on est déjà allé sur cette zone on regarde l'accès suivant
                if ((ZoneSuivante.PersonnageList.Count > 0) || (ZonesPrecedentes.Contains(ZoneSuivante)))
                {
                    Console.WriteLine(ZoneSuivante.Nom + " annulée");
                    Console.WriteLine(">0");
                    accesListDisponible.RemoveAt(tempo);

                    ZoneSuivante = null;
                }
                else
                {
                    Console.WriteLine("<=0");
                    accesListDisponible.Clear();
                    Console.WriteLine(accesListDisponible.Count);
                }
            }

            
            return ZoneSuivante;
        }

        public override void Execution()
        {
            this.ZonesPrecedentes.Add(ZoneSuivante);
            ZoneSuivante = null;
            //pour éviter de bloquer une fourmis trop longtemps, pour une
            if (ZonesPrecedentes.Count > 5)
            {
                ZonesPrecedentes.RemoveAt(0);
            }

            Console.WriteLine("execution");
        }
    }
}
