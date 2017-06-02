using System;
namespace AntBox
{
    public class Reine : PersonnageAbstrait
    {
        public Reine(string unNom, int desPointsDeVie) : base(unNom, desPointsDeVie)
        {
        }

        public override void AnalyseSituation()
        {
            throw new NotImplementedException();
        }

        public override void Execution()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Je suis la REINE " + Nom + " (PV : " + PointDeVie + ") ! ! !";
        }
    }
}