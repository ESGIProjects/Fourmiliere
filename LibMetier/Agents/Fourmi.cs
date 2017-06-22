using System;
using System.Collections.Generic;
using AntBox.Environnement;
using AntBox.Observateur;
using AntBox.Etat;

namespace AntBox
{
    public class Fourmi : PersonnageAbstrait
	{

        public Fourmi(string unNom, Subject unObservé, ZoneAbstraite maison, EtatPersonnageAbstrait etat) : base(unNom, unObservé, maison, etat) { }

		public override string ToString()
		{
			return "Je suis "+Nom+" la fourmi";
		}

        public override void Update()
        {
            Console.WriteLine(Nom + " sait que " + Observe.Etat);
        }
    }
}
