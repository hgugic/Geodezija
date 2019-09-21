using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.MetodaNajmanjihKvadrata.StatistickiTestovi
{
    /// <summary>
    /// Klasa <c>GlobalniTest</c> koristi se za otkrivanje postojanja grubih (ili sistematsih) greska - nakon izjednacenja 
    /// </summary>        
    /// <remarks>
    ///     <para/>Pad globalnog testa moze uzrokovati:
    ///     <para/> - Prisutnost ne stohastickih gresaka u mjerenjima
    ///     <para/> - Pogresna procjena a priori varijance
    ///     <para/> - Greska matematickog modela
    /// </remarks>
    public class GlobalniTest
    {
        double chiKvadratAlfaPola;
        double chiKvadratJedanMinusAlfaPola;
        bool testProlazi;
        double minimum;
        double maximum;

        /// <summary>
        /// Vrijednost Chi Kvadrat razdiobe za nivo signifikantnosti (alfa/2) i broj stupnjeva slobode f (prekobrojnosti mjerenja)
        /// </summary>
        public double ChiKvadratAlfaPola
        {
            get
            {
                return chiKvadratAlfaPola;
            }

            set
            {
                chiKvadratAlfaPola = value;
            }
        }

        /// <summary>
        /// Vrijednost Chi Kvadrat razdiobe za nivo signifikantnosti (1-alfa/2) i broj stupnjeva slobode f (prekobrojnosti mjerenja)
        /// </summary>
        public double ChiKvadratJedanMinusAlfaPola
        {
            get
            {
                return chiKvadratJedanMinusAlfaPola;
            }

            set
            {
                chiKvadratJedanMinusAlfaPola = value;
            }
        }

        /// <summary>
        /// Vrijednosti statistickog testiranja (da li prolazi ili pada test)
        /// </summary>
        public bool TestProlazi
        {
            get
            {
                return testProlazi;
            }

            set
            {
                testProlazi = value;
            }
        }

        /// <summary>
        /// Donja granica testa
        /// </summary>
        public double Minimum
        {
            get
            {
                return minimum;
            }

            set
            {
                minimum = value;
            }
        }

        /// <summary>
        /// Gornja granica testa
        /// </summary>
        public double Maximum
        {
            get
            {
                return maximum;
            }

            set
            {
                maximum = value;
            }
        }

        /// <summary>
        /// <para/>Otkrivanje postojanja grubih (ili sistematsih) greska - nakon izjednacenja u cijeloj mrezi   
        /// </summary>
        /// <param name="sigmaNulaKvadrat">A priori varijanca (varijanca jedinice tezine)</param>
        /// <param name="sKvadrat">A posteriori varijanca</param>
        /// <param name="f">Broj prekobrojnosti</param>
        /// <param name="alfa">Nivosignifikantnosti</param>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada nivo sifnifikantnosti nije u intervalu 0-1 ili je broj prekobrojnosti manji od 1</exception>
        public GlobalniTest(double sigmaNulaKvadrat, double sKvadrat, int f, double alfa)
        {
            if (alfa < 0 || alfa > 1)
                throw new ArgumentOutOfRangeException("alfa", alfa, "Nivo signifikantnosti mora biti u intervalu 0 < alfa < 1");
            if (f < 1)
                throw new ArgumentOutOfRangeException("f", f, "Broj prekobrojnosti mora biti veci od nule");

            test(sigmaNulaKvadrat, sKvadrat, f, alfa);
        }


        private double min(double sKvadrat, double alfa, int f)
        {
            chiKvadratAlfaPola = Distribucije.ChiKvadrat(alfa / 2, f);
            minimum = f * sKvadrat / chiKvadratAlfaPola;

            return minimum;
        }

        private double max(double sKvadrat, double alfa, int f)
        {
            chiKvadratJedanMinusAlfaPola = Distribucije.ChiKvadrat(1 - alfa / 2, f);
            maximum = f * sKvadrat / chiKvadratJedanMinusAlfaPola;

            return maximum;
        }

        private void test(double sigmaNulaKvadrat, double sKvadrat, int f, double alfa)
        {
            if (min(sKvadrat, alfa, f) < sigmaNulaKvadrat && sigmaNulaKvadrat < max(sKvadrat, alfa, f))
                testProlazi = true;
            else
                testProlazi = false;
        }
    }
}
