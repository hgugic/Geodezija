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
    /// Klasa <c>Duzina</c> predstavlja razvoj funkcije mjerenja duzina u Taylorov red uz odbacivanje clanova drugog i visih redova (Linearizacija) 
    /// </summary>
    /// <remarks>Klasa sluzi za odredivanje clanova matrice koeficijenata jednadzbi popravaka (Matrica A)</remarks>
    public class Duzina
    {
        #region Properties

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po x koordinati stajalista
        /// </summary>
        public double xStajaliste { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po y koordinati stajalista
        /// </summary>
        public double yStajaliste { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po x koordinati vizure
        /// </summary>
        public double xVizura { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po y koordinati vizure
        /// </summary>
        public double yVizura { get; set; }

        #endregion Properties

        /// <summary>
        /// <para/>Razvoj u Taylorov red funkcije duzine uz odbacivanje clanova viseg reda (Linearizacija) 
        /// </summary>
        /// <param name="stajaliste">Priblizne koordinate tocke stajalista prilikom mjerenja</param>
        /// <param name="vizura">Priblizne koordinate tocke vizure</param>
        public Duzina(ITockaProjekcija stajaliste, ITockaProjekcija vizura)
        {
            Radians smjerniKut = stajaliste.SmjerniKut(vizura);

            yStajaliste = -Matematika.Sin(smjerniKut);
            xStajaliste = -Matematika.Cos(smjerniKut);

            yVizura = -yStajaliste;
            xVizura = -xStajaliste;
        }
    }
}
