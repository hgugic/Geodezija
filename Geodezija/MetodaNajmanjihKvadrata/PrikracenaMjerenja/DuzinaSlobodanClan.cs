using System;
using Geodezija.Tocke;

namespace Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja
{
    /// <summary>
    /// Klasa <c>DuzinaSlobodanClan</c> racuna slobodan clan (prikraceno mjerenja) izmjerene duzine (u metrima)
    /// </summary>
    public class DuzinaSlobodanClan : SlobodanClan
    {
        double F;

        /// <summary>
        /// Slobodan clan (Prikraceno mjerenja)
        /// </summary>
        public double f
        {
            get
            {
                return F;
            }

            set
            {
                F = value;
            }
        }

        /// <summary>
        /// Vraca slobodan clan u odredenoj jedinici za duzinu
        /// </summary>
        /// <param name="f">Slobodan clan</param>
        /// <param name="jedinica">Jedinica za vracanje (enum Duzina)</param>
        /// <returns>double</returns>
        private double duzinaJedinica(double f, JedinicaDuzine jedinica)
        {
            switch (jedinica)
            {
                case JedinicaDuzine.metar:
                    return f;

                case JedinicaDuzine.decimetar:
                    return f / 10;
                    ;
                case JedinicaDuzine.centimetar:
                    return f / 100;

                case JedinicaDuzine.milimetar:
                    return f / 1000;

                default:
                    return f;
            }
        }

        /// <summary>
        /// Izracunava slobodan clan (prikraceno mjerenje)
        /// </summary>
        /// <param name="stajaliste"></param>
        /// <param name="vizura"></param>
        /// <param name="izmjerenaDuzina">Duzina izmjerena na terenu</param>
        /// <returns>double</returns>
        private double slobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, double izmjerenaDuzina)
        {
            return stajaliste.Duzina(vizura) - izmjerenaDuzina;
        }

        /// <summary>
        /// Baca exception kada je izmjerena duzina nula ili manja od nule 
        /// </summary>
        /// <param name="izmjerenaDuzina">Duzina izmjerena na terenu</param>
        private void exceptionIzmjerenaDuzinaManjaOdNule(double izmjerenaDuzina)
        {
            if (izmjerenaDuzina <= 0)
                throw new ArgumentException("Izmjerena duzina ne moze biti nula ili manja od nule");
        }

        #region Constructors

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerene duzine i duzine iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Pocetna tocka mjerenja duzine</param>
        /// <param name="vizura">Zavrsna tocka mjerenja duzine</param>
        /// <param name="izmjerenaDuzina">Izmjerena duzina u metrima</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste ili je duzina manja ili jednaka nuli</exception>
        public DuzinaSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, double izmjerenaDuzina)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionIzmjerenaDuzinaManjaOdNule(izmjerenaDuzina);

            f = slobodanClan(stajaliste, vizura, izmjerenaDuzina);
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerene duzine i duzine iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Pocetna tocka mjerenja duzine</param>
        /// <param name="vizura">Zavrsna tocka mjerenja duzine</param>
        /// <param name="izmjerenaDuzina">Izmjerena duzina u metrima</param>
        /// <param name="jedinica">Jedinica u kojoj se daje vrijednost slobodnog clana</param>
        public DuzinaSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, double izmjerenaDuzina, JedinicaDuzine jedinica)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionIzmjerenaDuzinaManjaOdNule(izmjerenaDuzina);

            f = slobodanClan(stajaliste, vizura, izmjerenaDuzina);
            f = duzinaJedinica(f, jedinica);
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerene duzine i duzine iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Pocetna tocka mjerenja duzine</param>
        /// <param name="vizura">Zavrsna tocka mjerenja duzine</param>
        /// <param name="izmjerenaDuzina">Izmjerena duzina u metrima</param>
        /// <param name="tolerancija">Vrijednost od koje slobodan clan mora biti manji (tolerancija na velike grube greske)</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste ili je duzina manja ili jednaka nuli</exception>
        public DuzinaSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, double izmjerenaDuzina, double tolerancija)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionIzmjerenaDuzinaManjaOdNule(izmjerenaDuzina);

            f = slobodanClan(stajaliste, vizura, izmjerenaDuzina);
            tolerancijaZadovoljena = provjeraTolerancije(f, tolerancija);
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerene duzine i duzine iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Pocetna tocka mjerenja duzine</param>
        /// <param name="vizura">Zavrsna tocka mjerenja duzine</param>
        /// <param name="izmjerenaDuzina">Izmjerena duzina u metrima</param>
        /// <param name="jedinica">Jedinica u kojoj se daje vrijednost slobodnog clana</param>
        /// <param name="tolerancija">Vrijednost od koje slobodan clan mora biti manji (tolerancija na velike grube greske)</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste ili je duzina manja ili jednaka nuli</exception>
        public DuzinaSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, double izmjerenaDuzina, JedinicaDuzine jedinica, double tolerancija)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionIzmjerenaDuzinaManjaOdNule(izmjerenaDuzina);

            f = slobodanClan(stajaliste, vizura, izmjerenaDuzina);
            tolerancijaZadovoljena = provjeraTolerancije(f, tolerancija);
            f = duzinaJedinica(f, jedinica);
        }

        #endregion Constructors
    }
}
