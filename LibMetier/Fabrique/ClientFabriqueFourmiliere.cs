using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntBox
{
    internal class ClientFabriqueFourmiliere
    {
        //private readonly AccesAbstrait produitAbstraitA;
        //private readonly Zone produitAbstraitB;

        public ClientFabriqueFourmiliere(FabriqueAbstraite fabrique)
        {
            //produitAbstraitA = fabrique.CreerProduitA();
            //produitAbstraitB = fabrique.CreerProduitB();
        }

        public void Run()
        {
            //produitAbstraitB.Interagit(produitAbstraitA);
        }
    }
}
