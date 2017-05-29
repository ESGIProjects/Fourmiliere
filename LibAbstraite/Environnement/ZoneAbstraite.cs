using System.Collections.Generic;

namespace AntBox
{
	public abstract class ZoneAbstraite
	{
		public string Nom { get; protected set; }
		public List<ObjetAbstrait> ObjetList { get; protected set; }
		public List<PersonnageAbstrait> PersonnageList { get; protected set; }
		public List<AccesAbstrait> AccesList { get; protected set; }


		public abstract void AjouterAcces(AccesAbstrait acces);

		public abstract void AjouterObjet(ObjetAbstrait unObjet);

		public abstract void AjouterPersonnage(PersonnageAbstrait unPersonnage);
		public abstract void RetirerPersonnage(PersonnageAbstrait unPersonnage);

		public ZoneAbstraite(string unNom)
		{
			this.Nom = unNom;
		}
	}
}