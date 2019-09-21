using System;
using Geodezija.Tocke;
using Geodezija.Kutevi;

namespace Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja
{
    /// <summary>
    /// Klasa <c>SlobodanClanPravac</c> racuna slobodan clan (prikraceno mjerenja) pravca
    /// </summary>
    public class PravacSlobodanClan : SlobodanClan
    {
        Radians F;

        /// <summary>
        /// Slobodan clan (Prikraceno mjerenja)
        /// </summary>
        public Radians f
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
        /// Izracunava slobodan clan
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="vizura">Tocka vizure instrumenta</param>
        /// <param name="izmjereniPravac">Pravac izmjeren na terenu</param>
        /// <param name="orijentacija">Pravac orijentacije (nule) limba</param>
        /// <returns>Rad</returns>
        private Radians slobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, IRadian izmjereniPravac, IRadian orijentacija)
        {
            if ((orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura)) - izmjereniPravac.ToRadians() > new Radians(3 * Math.PI / 2))
            {
                return new Radians((orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura) - izmjereniPravac.ToRadians() - 2 * Math.PI));
            }
            else if (((orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura)) - izmjereniPravac.ToRadians()).Angle < -3 * Math.PI / 2)
            {
                return new Radians((orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura) - izmjereniPravac.ToRadians() + 2 * Math.PI));
            }

            return orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura) - izmjereniPravac.ToRadians();
        }

        /// <summary>
        /// Izracunava slobodan clan
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="vizura">Tocka vizure instrumenta</param>
        /// <param name="izmjereniPravac">Pravac izmjeren na terenu</param>
        /// <param name="orijentacija">Pravac orijentacije (nule) limba</param>
        /// <param name="tolerancija">Tolerancija kada je izmjereni pravac plus orijentacija blizu 2 PI, ali je manji</param>
        /// <returns>Radians</returns>
        private Radians slobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, IRadian izmjereniPravac, IRadian orijentacija, IRadian tolerancija)
        {
            if ((orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura)) - izmjereniPravac.ToRadians() > 2 * Math.PI - tolerancija.ToRadians())
            {
                return new Radians((orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura) - izmjereniPravac.ToRadians() - 2 * Math.PI));
            }
            else
                return orijentacija.ToRadians() + stajaliste.SmjerniKut(vizura) - izmjereniPravac.ToRadians();
        }

        /// <summary>
        /// Baca exception kada je vrijednost pravca veca od 2*PI ili manja od nule
        /// </summary>
        /// <param name="izmjereniPravac">Duzina izmjerena na terenu</param>
        protected void exceptionOdNulaDo2PI(IRadian izmjereniPravac)
        {
            if (izmjereniPravac.ToRadians() < 0 || izmjereniPravac.ToRadians() >= 2 * Math.PI)
                throw new ArgumentOutOfRangeException("izmjereniPravac ili orijentacija", izmjereniPravac, "Mjerenje mora biti izmedu 0 <= mjerenje < 2*PI");
        }

        /// <summary>
        /// <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerenog pravca i orijentacije i smjernog kuta (direkcionog ugla) izracunatog iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="vizura">Tocka vizure instrumenta</param>
        /// <param name="izmjereniPravac">Pravac izmjeren na terenu</param>
        /// <param name="orijentacija">Pravac orijentacije (nule) limba</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada mjereni pravac nije u intervalu od 0 do 2*PI</exception>
        public PravacSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, IRadian izmjereniPravac, IRadian orijentacija)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionOdNulaDo2PI(izmjereniPravac);
            exceptionOdNulaDo2PI(orijentacija);

            f = slobodanClan(stajaliste, vizura, izmjereniPravac, orijentacija);
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerenog pravca i orijentacije i smjernog kuta (direkcionog ugla) izracunatog iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="vizura">Tocka vizure instrumenta</param>
        /// <param name="izmjereniPravac">Pravac izmjeren na terenu</param>
        /// <param name="orijentacija">Pravac orijentacije (nule) limba</param>
        /// <param name="tolerancija">Vrijednost od koje slobodan clan mora biti manji (tolerancija na velike grube greske)</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada mjereni pravac nije u intervalu od 0 do 2*PI</exception>
        public PravacSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, IRadian izmjereniPravac, IRadian orijentacija, IRadian tolerancija)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionOdNulaDo2PI(izmjereniPravac);
            exceptionOdNulaDo2PI(orijentacija);

            f = slobodanClan(stajaliste, vizura, izmjereniPravac, orijentacija);
            tolerancijaZadovoljena = provjeraTolerancije(f.Angle, tolerancija.ToRadians().Angle);
        }
    }
}
