using AntBox.Environnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntBox.Etat
{
    public abstract class EtatPersonnageAbstrait
    {
        public abstract ZoneAbstraite ChoixZoneSuivante(List<AccesAbstrait> accesList, ZoneAbstraite zoneActuelle);

        public abstract void AnalyseSituation(PersonnageAbstrait personnage);

        public abstract void Execution();
    }
}
