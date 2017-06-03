using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntBox.Factory;

namespace AntBox.Environnement
{
    public class Jardin : EnvironnementAbstrait
    {
        public Jardin()
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

        public override string Simuler()
        {
            //TODO
            throw new NotImplementedException();
        }

        public override string Statistiques()
        {   
            return string.Join("\n", ZoneList);
        }
    }
}
