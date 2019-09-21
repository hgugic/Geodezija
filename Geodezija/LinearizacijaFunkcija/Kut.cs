using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geodezija.Kutevi;
using Geodezija.Tocke;

namespace Geodezija.LinearizacijaFunkcija
{
    public class Kut
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
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po x koordinati prve tocke vizure Od koje se mjeri kut (u radijanima)
        /// </summary>
        public Radians xOd { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po y koordinati prve tocke vizure Od koje se mjeri kut (u radijanima)
        /// </summary>
        public Radians yOd { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po x koordinati prve tocke vizure Do koje se mjeri kut (u radijanima)
        /// </summary>
        public Radians xDo { get; set; }

        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije mjerenja po y koordinati prve tocke vizure Do koje se mjeri kut (u radijanima)
        /// </summary>
        public Radians yDo { get; set; }

        #endregion Properties
   
        /// <summary>
        /// <para/>Razvoj u Taylorov red funkcije kuta uz odbacivanje clanova viseg reda (Linearizacija) 
        /// <para/>Vrijednost razvoja u red data je u radijanima
        /// </summary>
        /// <param name="stajaliste">Priblizne koordinate tocke stajalista prilikom mjerenja</param>
        /// <param name="Od">Priblizne koordinate prve tocke vizure Od koje se mjeri kut</param>
        /// <param name="Do">Priblizne koordinate druge tocke vizure Do koje se mjeri kut</param>
        public Kut(ITockaProjekcija stajaliste, ITockaProjekcija Od, ITockaProjekcija Do)
        {
            Radians smjerniKutOd = stajaliste.SmjerniKut(Od);
            double duzinaOd = stajaliste.Duzina(Od);

            Radians smjerniKutDo = stajaliste.SmjerniKut(Do);
            double duzinaDo = stajaliste.Duzina(Do);


            yOd = new Radians(-Matematika.Cos(smjerniKutOd) / duzinaOd);
            xOd = new Radians(Matematika.Sin(smjerniKutOd) / duzinaOd);

            yDo = new Radians(Matematika.Cos(smjerniKutDo) / duzinaDo);
            xDo = new Radians(-Matematika.Sin(smjerniKutDo) / duzinaDo);

            yStajaliste = (-yDo) - yOd;
            xStajaliste = (-xDo) - xOd;
        }
    }
}
