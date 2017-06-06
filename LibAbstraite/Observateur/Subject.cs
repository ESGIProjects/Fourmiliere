using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntBox.Observateur
{
    public abstract class Subject
    {
        public List<Observer> ListObservateur { get; protected set; } = new List<Observer>();

        private string etat;
        public string Etat
        {
            get {
                return etat;
            }
            set {
                etat = value;
                Notify();
            }
        }

        public void Attach(Observer observateur) {
            ListObservateur.Add(observateur);
        }

        public void Detach(Observer observateur) {
            ListObservateur.Remove(observateur);
        }

        public void Notify() {
            foreach (Observer observateur in ListObservateur) {
                observateur.Update();
            }
        }
    }


}
