using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.LinearizacijaFunkcija
{
    /// <summary>
    /// <para/>Klasa <c>BazniVektor</c> predstavlja razvoj funkcije mjerenja duzina u Taylorov red uz odbacivanje clanova drugog i visih redova (Linearizacija) 
    /// <para/>Za 2D i 3D bazne vektore
    /// </summary>
    /// <remarks>
    ///     <para/>Klasa sluzi za odredivanje clanova matrice koeficijenata jednadzbi popravaka (Matrica A)
    ///     <para/>Klasa nema ulaznih parametara jer izlazne vrijednosti su uvijek iste (1 i -1)
    /// </remarks>
    public class BazniVektor
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
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po z koordinati stajalista
        /// </summary>
        public double zStajaliste { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po x koordinati vizure
        /// </summary>
        public double xVizura { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po y koordinati vizure
        /// </summary>
        public double yVizura { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po z koordinati vizure
        /// </summary>
        public double zVizura { get; set; }

        #endregion Properties

        /// <summary>
        /// <para/>Razvoj u Taylorov red funkcije duzine uz odbacivanje clanova viseg reda (Linearizacija) 
        /// </summary>
        public BazniVektor()
        {
            xStajaliste = -1;
            yStajaliste = -1;
            zStajaliste = -1;

            xVizura = 1;
            yVizura = 1;
            zVizura = 1;
        }
    }
}
