using System;
using AntBox.Environnement;

namespace AntBox.Factory
{
	public class FabriqueFourmiliere : FabriqueAbstraite
	{
        const string TypeObjetNourriture = "nourriture";
        const string TypeObjetOeuf       = "oeuf";
        const string TypeObjetPheromone  = "pheromone";

        public FabriqueFourmiliere()
		{
		}

        public override AccesAbstrait CreerAcces(ZoneAbstraite zoneDebut, ZoneAbstraite zoneFin)
		{
            return new Acces(zoneDebut, zoneFin);
        }


        public override EnvironnementAbstrait CreerEnvironnement()
		{
            return new Jardin();
		}



        //génère des Fourmi à partir d'une fabrique concrète
		public override ObjetAbstrait CreerObjet(string nom)
		{
            switch (nom)
            {
                case TypeObjetNourriture: return new Nourriture();
                case TypeObjetOeuf: return new Oeuf();
                case TypeObjetPheromone: return new Pheromone();
                default: throw new NotImplementedException();
            }      
		}

        //création d'une fourmi avec point de vie par défaut 
        public override PersonnageAbstrait CreerPersonnage(string nom)
		{
            return new Fourmi(nom);
        }

        //création d'une fourmi sans points de vie par défaut
        public override PersonnageAbstrait CreerPersonnage(string nom, int desPointsDeVie)
        {
            return new Fourmi(nom, desPointsDeVie);
        }

        //création d'un boutDeTerrain
        public override ZoneAbstraite CreerZone(string nom)
		{
            return new BoutDeTerrain(nom);
		}
	}
}
