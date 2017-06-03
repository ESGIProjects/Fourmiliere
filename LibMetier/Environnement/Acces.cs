using System;
namespace AntBox.Environnement
{
	public class Acces : AccesAbstrait
	{
		public Acces(ZoneAbstraite debut, ZoneAbstraite fin)
		{
			//on enregistre la zone de début et de fin de notre accès
			ZoneDebut = debut;
			ZoneFin = fin;

			//on indique à nos zone abstraite, qu'elle sont reliées par cet accès
			debut.AjouterAcces(this);
			fin.AjouterAcces(this);
		}
	}
}
