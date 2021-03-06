﻿using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace AntBox.Environnement
{
	public class BoutDeTerrain : ZoneAbstraite
	{
        public BoutDeTerrain(string unNom, int positionX, int positionY) : base(unNom, positionX, positionY)
		{
			ObjetList = new List<ObjetAbstrait>();
			PersonnageList = new List<PersonnageAbstrait>();
			AccesList = new List<AccesAbstrait>();
		}

		public override void AjouterAcces(AccesAbstrait acces)
		{
			//si l'accès transmis n'est pas déjà relié à ce terrain, on l'ajoute
			if (!AccesList.Contains(acces)) {
				AccesList.Add(acces);
			}
		}

		public override void AjouterObjet(ObjetAbstrait unObjet)
		{
			//si l'objet n'est pas déjà sur le terrain, on l'ajoute
			if (!ObjetList.Contains(unObjet)) {
				ObjetList.Add(unObjet);
			}
		}

		public override void AjouterPersonnage(PersonnageAbstrait unPersonnage)
		{
			//si le personnage n'est pas déjà sur le terrain, on l'ajoute
			if (!PersonnageList.Contains(unPersonnage)) {
				PersonnageList.Add(unPersonnage);
                unPersonnage.ZoneActuelle = this;
			}
		}

		public override void RetirerPersonnage(PersonnageAbstrait unPersonnage)
		{
			//si le personnage n'est pas déjà sur le terrain, on l'ajoute
			if (PersonnageList.Contains(unPersonnage)) {
                unPersonnage.ZoneActuelle = null;
                PersonnageList.Remove(unPersonnage);
			}
		}
	}
}
