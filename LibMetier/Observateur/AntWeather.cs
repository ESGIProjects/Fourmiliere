
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntBox.Observateur
{
    public class AntWeather : Subject
    {
        //instance privée partagée de AntWeather
        private static AntWeather sharedAntWeather;

        public static AntWeather SharedAntWeather
        {
            get {
                if (sharedAntWeather == null)
                {
                    sharedAntWeather = new AntWeather();
                }
                return sharedAntWeather;
            }
        }


        public AntWeather()
        {

        }
    }
}
