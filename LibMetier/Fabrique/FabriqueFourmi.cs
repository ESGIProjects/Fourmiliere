using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntBox
{
    public class FabriqueFourmi : FabriquePersonnage
    {
        public const String Fourmi  = "fourmi";
        public const String Reine   = "reine";

        public override PersonnageAbstrait CreerPersonnage(string type)
        {
            switch(type)
            {
                case FabriqueFourmi.Fourmi : return new Fourmi("", 10);
                case FabriqueFourmi.Reine : return new Reine("", 99);
            }

            throw new NotImplementedException();
        }
    }
}
