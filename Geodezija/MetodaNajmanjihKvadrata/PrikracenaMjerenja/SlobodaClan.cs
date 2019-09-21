using System;
using Geodezija.Tocke;

namespace Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja
{
    /// <summary>
    ///     <para/>Klasa <c>SlobodanClanAbstract</c> sadrzi zajednicke metode za sve ostale klase SlobodanClan
    /// </summary>
    public abstract class SlobodanClan
    {

        /// <summary>
        ///     <para/> Provjera da li je razlika izmjerene velicine i izracunate (koordinate, visina i sl.) veca od zadane tolerancije 
        ///     <para/> Sluzi za eliminaciju grubih gresaka unosa (nije dostupno za sve konstruktore)
        /// </summary>
        protected Nullable<bool> tolerancijaZadovoljena;

        /// <summary>
        ///     <para/> Provjera da li je razlika izmjerene velicine i izracunate (koordinate, visina i sl.) veca od zadane tolerancije 
        ///     <para/> Sluzi za eliminaciju grubih gresaka unosa (nije dostupno za sve konstruktore)
        /// </summary>
        public Nullable<bool> TolerancijaZadovoljena
        {
            get
            {
                return tolerancijaZadovoljena;
            }

            set
            {
                tolerancijaZadovoljena = value;
            }
        }

        #region Private methods

        /// <summary>
        /// Provjera da li je razlika izmjerene velicine i izracunate (koordinate, visina i sl.) veca od zadane tolerancije 
        /// </summary>
        /// <param name="f">Slobodan clan (prikraceno mjerenje)</param>
        /// <param name="tolerancija"></param>
        /// <returns>Nullable bool</returns>
        protected bool provjeraTolerancije(double f, double tolerancija)
        {
            if (Math.Abs(f) > Math.Abs(tolerancija))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Provjerada li tocke imaju iste koordinate - ako imaju baca se exception
        /// </summary>
        /// <param name="stajaliste">Pocetna tocka mjerenja duzine</param>
        /// <param name="vizura">Zavrsna tocka mjerenja duzine</param>
        protected void exceptionIsteKoordinateTocke(ITockaProjekcija stajaliste, ITockaProjekcija vizura)
        {
            if ((stajaliste.X == vizura.X) && (stajaliste.Y == vizura.Y))
                throw new ArgumentException("Koordinate dvije tocke su iste");
        }

        #endregion Private methods
    }
}
