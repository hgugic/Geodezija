using System;
using Geodezija.Kutevi;
using Geodezija.Tocke;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja
{
    /// <summary>
    /// Klasa <c>SlobodanClanKut</c> racuna slobodan clan (prikraceno mjerenja) kuta
    /// </summary>
    public class KutSlobodanClan : SlobodanClan
    {
        Radians F;

        /// <summary>
        /// Slobodan clan (Prikraceno mjerenja) kuta
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
        /// Izracunava slobodan clan (prikraceno mjerenje) kuta
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="Od">Tocka prve vizure (Od) </param>
        /// <param name="VizuraOd">Pravac vizure prema prvoj tocki</param>
        /// <param name="Do">Tocka druge vizure(Do)</param>
        /// <param name="VizuraDo">Pravac vizure prema drugoj tocki</param>
        /// <returns>Radians</returns>
        private Radians slobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija Od, IRadian VizuraOd, ITockaProjekcija Do, IRadian VizuraDo)
        {
            Radians koordinate = stajaliste.SmjerniKut(Do) - stajaliste.SmjerniKut(Od);
            if (koordinate < 0)
                koordinate = koordinate + 2 * Math.PI;

            Radians mjerenje = VizuraDo.ToRadians() - VizuraOd.ToRadians();
            if (mjerenje < 0)
                mjerenje = mjerenje + 2 * Math.PI;

            return koordinate - mjerenje;
        }

        /// <summary>
        /// Baca exception kada je vrijednost pravca veca od 2*PI ili manja od nule
        /// </summary>
        /// <param name="izmjereniPravac">Duzina izmjerena na terenu</param>
        protected void exceptionOdNulaDo2PI(IRadian izmjereniPravac)
        {
            if (izmjereniPravac.ToRadians() < 0 || izmjereniPravac.ToRadians() >= 2 * Math.PI)
                throw new ArgumentOutOfRangeException("Vizura", izmjereniPravac, "Mjerenje mora biti izmedu 0 < mjerenje < 2*PI");
        }

        /// <summary>
        ///     <para/>Inicijalizira novu instancu klase Geodezija.MetodaNajmanjihKvadrata.SlobodanClanKut
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerenog kuta i smjernih kuta (direkcionih ugla) izracunatih iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="Od">Tocka prve vizure koja cini kut (lijevi pravac)</param>
        /// <param name="Do">Tocka druge vizure koja cini kut (desni pravac)</param>
        /// <param name="izmjereniKut">izmjereni kut</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada mjereni kut nije u intervalu od 0 do 2*PI</exception>
        public KutSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija Od, ITockaProjekcija Do, IRadian izmjereniKut)
        {
            exceptionIsteKoordinateTocke(stajaliste, Od);
            exceptionIsteKoordinateTocke(stajaliste, Do);
            exceptionIsteKoordinateTocke(Do, Od);
            exceptionOdNulaDo2PI(izmjereniKut);

            Radians koordinate = stajaliste.SmjerniKut(Do) - stajaliste.SmjerniKut(Od);
            if (koordinate < 0)
                koordinate = koordinate + 2 * Math.PI;

            f = koordinate - izmjereniKut.ToRadians();
        }

        /// <summary>
        /// <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerena dva pravca i smjernih kuta (direkcionih ugla) izracunatih iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="Od">Tocka prve vizure koja cini kut (lijevi pravac)</param>
        /// <param name="VizuraOd">Pravac izmjeren na terenu prema prvoj tocki</param>
        /// <param name="Do">Tocka druge vizure koja cini kut (desni pravac)</param>
        /// <param name="VizuraDo">Pravac izmjeren na terenu prema drugoj tocki</param>
        ///  <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada mjereni pravac nije u intervalu od 0 do 2*PI</exception>
        public KutSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija Od, IRadian VizuraOd, ITockaProjekcija Do, IRadian VizuraDo)
        {
            exceptionIsteKoordinateTocke(stajaliste, Od);
            exceptionIsteKoordinateTocke(stajaliste, Do);
            exceptionIsteKoordinateTocke(Do, Od);
            exceptionOdNulaDo2PI(VizuraOd);
            exceptionOdNulaDo2PI(VizuraDo);

            f = slobodanClan(stajaliste, Od, VizuraOd, Do, VizuraDo);
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana za matricu slobodnih clanova, preko izmjerena dva pravca i smjernih kuta (direkcionih ugla) izracunatih iz koordinata 
        /// </summary>
        /// <param name="stajaliste">Tocka stajalista instrumenta</param>
        /// <param name="Od">Tocka prve vizure koja cini kut (lijevi pravac)</param>
        /// <param name="VizuraOd">Pravac izmjeren na terenu prema prvoj tocki</param>
        /// <param name="Do">Tocka druge vizure koja cini kut (desni pravac)</param>
        /// <param name="VizuraDo">Pravac izmjeren na terenu prema drugoj tocki</param>
        /// <param name="tolerancija">Vrijednost od koje slobodan clan mora biti manji (tolerancija na velike grube greske)</param>
        /// <exception cref="ArgumentException">Baca se kada su koordinate obe tocke iste</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada mjereni pravac nije u intervalu od 0 do 2*PI</exception>
        public KutSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija Od, IRadian VizuraOd, ITockaProjekcija Do, IRadian VizuraDo, IRadian tolerancija)
        {
            exceptionIsteKoordinateTocke(stajaliste, Od);
            exceptionIsteKoordinateTocke(stajaliste, Do);
            exceptionIsteKoordinateTocke(Do, Od);
            exceptionOdNulaDo2PI(VizuraOd);
            exceptionOdNulaDo2PI(VizuraDo);
            
            f = slobodanClan(stajaliste, Od, VizuraOd, Do, VizuraDo);
            tolerancijaZadovoljena = provjeraTolerancije(f.Angle, tolerancija.ToRadians().Angle);
        }
    }
}
