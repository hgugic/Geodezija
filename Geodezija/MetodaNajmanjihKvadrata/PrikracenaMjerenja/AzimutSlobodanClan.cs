using System;
using Geodezija.Kutevi;
using Geodezija.Tocke;
namespace Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja
{
    /// <summary>
    ///     <para/>Klasa <c>AzimutSlobodanClan</c> racuna slobodan clan (prikraceno mjerenja) Azimuta
    ///     <para/>Tehnicki ovo nije azimut (nema racunanja kovergencije meridijana sl.) nego smjerni kut (direkcioni ugao)
    /// </summary>
    public class AzimutSlobodanClan : SlobodanClan
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
        /// Izracunava slobodan clan (prikraceno mjerenje)
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="vizura">Tocka vizure instrumenta</param>
        /// <param name="izmjereniAzimut">Azimut (smjerni kut, direkcioni ugao) izmjeren na terenu</param>
        /// <returns>Rad</returns>
        private Radians slobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, IRadian izmjereniAzimut)
        {
            return stajaliste.SmjerniKut(vizura) - izmjereniAzimut.ToRadians();
        }

        /// <summary>
        /// Baca exception kada je vrijednost azimuta (smjernog kuta, direkcionog ugla) veca/jednaka od 2*PI ili manja od nule
        /// </summary>
        /// <param name="izmjereniAzimut">Azimut (smjerni kut, direkcioni ugao) izmjeren na terenu</param>
        protected void exceptionOdNulaDo2PI(IRadian izmjereniAzimut)
        {
            if (izmjereniAzimut.ToRadians().Angle < 0 || izmjereniAzimut.ToRadians().Angle >= 2 * Math.PI)
                throw new ArgumentOutOfRangeException("izmjereniAzimut", izmjereniAzimut, "Mjerenje mora biti izmedu 0 < mjerenje < 2*PI");
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerenog azimuta i azimuta izracunatog iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="vizura">Tocka vizure instrumenta</param>
        /// <param name="izmjereniAzimut">Azimut (smjerni kut, direkcioni ugao) izmjeren na terenu</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada mjereni azimut nije u intervalu od 0 do 2*PI</exception>
        public AzimutSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, Radians izmjereniAzimut)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionOdNulaDo2PI(izmjereniAzimut);

            f = slobodanClan(stajaliste, vizura, izmjereniAzimut);
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerenog azimuta i azimuta izracunatog iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="vizura">Tocka vizure instrumenta</param>
        /// <param name="izmjereniAzimut">Azimut (smjerni kut, direkcioni ugao) izmjeren na terenu</param>
        /// <param name="tolerancija">Vrijednost od koje slobodan clan mora biti manji (tolerancija na velike grube greske)</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada mjereni azimut nije u intervalu od 0 do 2*PI</exception>
        public AzimutSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, IRadian izmjereniAzimut, IRadian tolerancija)
        {
            exceptionIsteKoordinateTocke(stajaliste, vizura);
            exceptionOdNulaDo2PI(izmjereniAzimut);

            f = slobodanClan(stajaliste, vizura, izmjereniAzimut);
            TolerancijaZadovoljena = provjeraTolerancije(f.ToRadians().Angle, tolerancija.ToRadians().Angle);
        }
    }
}
