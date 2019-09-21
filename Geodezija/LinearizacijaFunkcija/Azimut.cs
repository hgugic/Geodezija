using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geodezija.Kutevi;
using Geodezija.Tocke;

namespace Geodezija.LinearizacijaFunkcija
{
    /// <summary>
    /// Klasa <c>Azimut</c> predstavlja razvoj funkcije mjerenja pravaca u Taylorov red uz odbacivanje clanova drugog i visih redova (Linearizacija) 
    /// </summary>
    /// <remarks>
    /// <para/>Klasa sluzi za odredivanje clanova matrice koeficijenata jednadzbi popravaka (Matrica A)
    /// <para/>Najcesce se primanjuje izjednacenje u sekundama da bi prebacili u sekunde koristite metodu ToSeconds() 
    /// </remarks>
    public class Azimut
    {
        #region Properties

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po x koordinati stajalista (u radijanima)
        /// </summary>
        public Radians xStajaliste { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po y koordinati stajalista (u radijanima)
        /// </summary>
        public Radians yStajaliste { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po x koordinati stajalista (u radijanima)
        /// </summary>
        public Radians xVizura { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po y koordinati stajalista (u radijanima)
        /// </summary>
        public Radians yVizura { get; set; }

        #endregion Properties

        /// <summary>
        /// <para/>Razvoj u Taylorov red funkcije pravca uz odbacivanje clanova viseg reda (Linearizacija)
        /// <para/>Vrijednost razvoja u red data je u radijanima
        /// </summary>
        /// <param name="stajaliste">Priblizne koordinate tocke stajalista prilikom mjerenja (Od)</param>
        /// <param name="vizura">Priblizne koordinate tocke vizure (Do)</param>
        public Azimut(ITockaProjekcija stajaliste, ITockaProjekcija vizura)
        {
            Radians smjerniKut = stajaliste.SmjerniKut(vizura);
            double duzina = stajaliste.Duzina(vizura);

            yStajaliste = new Radians(-Matematika.Cos(smjerniKut) / duzina);
            xStajaliste = new Radians(Matematika.Sin(smjerniKut) / duzina);

            yVizura = -yStajaliste;
            xVizura = -xStajaliste;
        }
    }
}
