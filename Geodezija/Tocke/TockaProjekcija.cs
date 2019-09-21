using System;
using System.Collections.Generic;
using Geodezija.Kutevi;

namespace Geodezija.Tocke
{
    public class TockaProjekcija : ITockaProjekcija
    {
        /// <summary>
        /// Vrijednost koordinate na y-osi 
        /// </summary>
        double y;

        /// <summary>
        /// Vrijednost koordinate na y-osi 
        /// </summary>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Vrijednost koordinate na x-osi 
        /// </summary>
        double x;

        /// <summary>
        /// Vrijednost koordinate na x-osi 
        /// </summary>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public JedinicaDuzine Jedinica
        {
            get
            {
                return jedinica;
            }

            private set{ }
        }

        JedinicaDuzine jedinica;


        /// <summary>
        /// Koordinate tocke; lijevi koordinatnom u ravnini;   
        /// </summary>
        /// <remarks>Ovo su najcesce koordinate u projekciji. 
        ///     <para/>Easting  (E) = y
        ///     <para/>Northing (N) = x
        /// </remarks>
        /// <param name="y">Vrijednost koordinate na y-osi u lijevom 2D koordinatnom sustavu</param>
        /// <param name="x">Vrijednost koordinate na x-osi u lijevom 2D koordinatnom sustavu</param>
        public TockaProjekcija(double y, double x)
        {
            this.y = y;
            this.x = x;
            this.jedinica = JedinicaDuzine.metar;
        }

        /// <summary>
        /// Koordinate tocke; lijevi koordinatnom u ravnini;   
        /// </summary>
        /// <remarks>Ovo su najcesce koordinate u projekciji. 
        ///     <para/>Easting  (E) = y
        ///     <para/>Northing (N) = x
        /// </remarks>
        /// <param name="y">Vrijednost koordinate na y-osi u lijevom 2D koordinatnom sustavu</param>
        /// <param name="x">Vrijednost koordinate na x-osi u lijevom 2D koordinatnom sustavu</param>
        public TockaProjekcija(double y, double x, JedinicaDuzine jedinica)
        {
            this.y = y;
            this.x = x;
            this.jedinica = jedinica;
        }


        /// <summary>
        /// Kut u ravnini projekcije između paralele s apscisnom osi (os x) u zadanoj točki i pravca prema nekoj drugoj točki. 
        /// Mjeri se od sjevernog dijela paralele u smjeru kretanja kazaljke na satu.
        /// </summary>
        /// <param name="vizura">Koordinate tocke prema kojoj se zeli odrediti smjerni kut</param>
        /// <returns>Radians</returns>
        public Radians SmjerniKut(ITocka vizura)
        {
            double dy = vizura.Y - y;
            double dx = vizura.X - x;

            // Prva 4. slucaja odgovaraju za vise 99% racunanja
            if (dy >= 0 && dx > 0)
                return new Radians(Math.Atan(dy / dx));                 // 1. kvadrant

            else if (dy > 0 && dx < 0)
                return new Radians(Math.Atan(dy / dx) + Math.PI);       // 2. kvadrant

            else if (dy <= 0 && dx < 0)
                return new Radians(Math.Atan(dy / dx) + Math.PI);       // 3. kvadrant

            else if (dy < 0 && dx > 0)
                return new Radians(Math.Atan(dy / dx) + 2 * Math.PI);   // 4. kvadrant


            // Posebni slucajevi po y-osi, javlja se problem dijeljenja sa nulom 
            else if (dy > 0 && dx == 0)
                return new Radians(Math.PI / 2);

            else if (dy < 0 && dx == 0)
                return new Radians(3 * Math.PI / 2);

            // Obe tocke imaju iste koordinate
            else
                throw new ArgumentException("Nije moguce izracunati smjerni kut izmedu dvije tocke s istim koordinatama");
        }

        /// <summary>
        /// Udaljenost izmedu tocaka - Pripazite da su jedinice duzine u metrima
        /// </summary>
        /// <param name="vizura">Koordinate tocke preka kojoj se zeli odrediti udaljenost</param>
        /// <remarks>Racunanje se bazira na pitagorinom poucku</remarks>
        /// <returns>double</returns>
        public double Duzina(ITocka vizura)
        {
            double dy = vizura.Y - y;
            double dx = vizura.X - x;

            double d = Math.Sqrt(dy * dy + dx * dx);

            return d;
        }



        /// <summary>
        /// Racunanje koordinatnih razlika
        /// </summary>
        /// <param name="tocka1">Set koordinata prve tocke</param>
        /// <param name="tocka2">Set koordinata druge tocke</param>
        /// <returns>Tocka2D</returns>
        public static TockaProjekcija operator -(TockaProjekcija tocka1, TockaProjekcija tocka2)
        {
            double dy = tocka1.Y - tocka2.Y;
            double dx = tocka1.X - tocka2.X;

            TockaProjekcija tocka = new TockaProjekcija(dy, dx);

            return tocka;
        }

        /// <summary>
        /// Racunanje tezinskih (gravitacijskih) koordinata liste tockaka
        /// </summary>
        /// <param name="ListaKoordinata">Lista koordinata</param>
        /// <returns>Tocka2D</returns>
        public TockaProjekcija TezinskeKoordinate(List<TockaProjekcija> ListaKoordinata)
        {
            double dy = 0;
            double dx = 0;

            for (int i = 0; i < ListaKoordinata.Count; i++)
            {
                TockaProjekcija tocka = ListaKoordinata[i];
                dy += tocka.Y;
                dx += tocka.X;
            }

            dy = dy / ListaKoordinata.Count;
            dx = dx / ListaKoordinata.Count;

            TockaProjekcija tockaTezinska = new TockaProjekcija(dy, dx);
            return tockaTezinska;
        }

        //Mijenja postavljenujedinicu duzine u metre
        private void koverzijaJedinicaDuzineUMetre()
        {
            switch (this.jedinica)
            {
                case JedinicaDuzine.metar:
                    break;

                case JedinicaDuzine.decimetar:
                    this.y = this.y / 10;
                    this.x = this.x / 10;
                    break;

                case JedinicaDuzine.centimetar:
                    this.y = this.y / 100;
                    this.x = this.x / 100;
                    break;

                case JedinicaDuzine.milimetar:
                    this.y = this.y / 1000;
                    this.x = this.x / 1000;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Mijenja jedinicu duzine u zadanu i preracunava vrijednosti koordinata daodgovaraju zadanoj jednici
        /// </summary>
        /// <param name="jedinica">jedinica duzine</param>
        /// <returns>TockaProjekcija</returns>
        public TockaProjekcija PromjenaJediniceDuzine(JedinicaDuzine jedinica)
        {
            koverzijaJedinicaDuzineUMetre();

            this.jedinica = jedinica;

            switch (this.jedinica)
            {
                case JedinicaDuzine.metar:
                    break;

                case JedinicaDuzine.decimetar:
                    this.y = this.y * 10;
                    this.x = this.x * 10;
                    break;

                case JedinicaDuzine.centimetar:
                    this.y = this.y * 100;
                    this.x = this.x * 100;
                    break;

                case JedinicaDuzine.milimetar:
                    this.y = this.y * 1000;
                    this.x = this.x * 1000;
                    break;

                default:
                    break;                    
            }
            return this;
        }
    }
}
