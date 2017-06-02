using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntBox
{
    public abstract class FabriquePersonnage
    {
        public abstract PersonnageAbstrait CreerPersonnage(String type);
    }
}
