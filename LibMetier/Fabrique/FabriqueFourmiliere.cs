using System;
namespace AntBox
{
	public class FabriqueFourmiliere : FabriqueAbstraite
	{
		public FabriqueFourmiliere()
		{
		}

		public override AccesAbstrait CreerAcces(ZoneAbstraite zoneDebut, ZoneAbstraite zoneFin)
		{
			throw new NotImplementedException();
		}

		public override EnvironnementAbstrait CreerEnvironnement()
		{
			throw new NotImplementedException();
		}

		public override ObjetAbstrait CreerObjet(string nom)
		{
			throw new NotImplementedException();
		}

		public override PersonnageAbstrait CreerPersonnage(string nom)
		{
			throw new NotImplementedException();
		}

		public override ZoneAbstraite CreerZone(string nom)
		{
			return new BoutDeTerrain(nom);
		}
	}
}
