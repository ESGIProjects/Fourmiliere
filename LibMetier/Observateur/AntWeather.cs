
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntBox.Observateur
{
    public class AntWeather : Subject
    {
        public const string RainIsComing = "Il va bientot pleuvoir.";
        public const string RainIsHere = "Il pleut.";
        public const string RainIsFinished = "il ne pleut plus.";

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
