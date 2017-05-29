using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace AntBox
{
	public class BoutDeTerrain : ZoneAbstraite
	{
		public BoutDeTerrain(string unNom) : base(unNom)
		{
			ObjetList = new List<ObjetAbstrait>();
			PersonnageList = new List<PersonnageAbstrait>();
			AccesList = new List<AccesAbstrait>();
		}

		public override void AjouterAcces(AccesAbstrait acces)
		{
			//si l'accès transmis n'est pas déjà relié à ce terrain, on l'ajoute
			if (!AccesList.Contains(acces))
			{
				Debug.WriteLine("J'ajoute l'accès");
				AccesList.Add(acces);
			}
			else
			{
				Debug.WriteLine("impossible d'ajouter l'accès");
			}
		}

		public override void AjouterObjet(ObjetAbstrait unObjet)
		{
			//si l'objet n'est pas déjà sur le terrain, on l'ajoute
			if (!ObjetList.Contains(unObjet))
			{
				Debug.WriteLine("J'ajoute un objet");
				ObjetList.Add(unObjet);
			}
			else
			{
				Debug.WriteLine("impossible d'ajouter l'accès");
			}
		}

		public override void AjouterPersonnage(PersonnageAbstrait unPersonnage)
		{
			//si le personnage n'est pas déjà sur le terrain, on l'ajoute
			if (!PersonnageList.Contains(unPersonnage))
			{
				Debug.WriteLine("J'ajoute un personnage");
				PersonnageList.Add(unPersonnage);
			}
			else
			{
				Debug.WriteLine("impossible d'ajouter ce personnage");
			}
		}

		public override void RetirerPersonnage(PersonnageAbstrait unPersonnage)
		{
			//si le personnage n'est pas déjà sur le terrain, on l'ajoute
			if (!PersonnageList.Contains(unPersonnage))
			{
				Debug.WriteLine("Je retire un personnage");
				PersonnageList.Remove(unPersonnage);
			}
			else
			{
				Debug.WriteLine("impossible de retirer ce personnage");
			}
		}
	}
}
