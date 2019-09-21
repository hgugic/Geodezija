using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geodezija.Tocke;
using Geodezija.Kutevi;

namespace Geodezija.LinearizacijaFunkcija
{
    /// <summary>
    /// Klasa <c>Pravac</c> predstavlja razvoj funkcije mjerenja pravaca u Taylorov red uz odbacivanje clanova drugog i visih redova (Linearizacija) 
    /// </summary>
    /// <remarks>
    /// <para/>Klasa sluzi za odredivanje clanova matrice koeficijenata jednadzbi popravaka (Matrica A)
    /// <para/>Najcesce se primanjuje izjednacenje u sekundama da bi prebacili u sekunde koristite metodu ToSeconds() 
    /// </remarks>
    public class Pravac : Azimut
    {
        /// <summary>
        /// Vrijednost razvoja u Taylorov red funkcije orijentacija limba - (z)
        /// </summary>
        public double zNepoznanicaOrijentacije { get; set; }

        /// <summary>
        /// <para/>Razvoj u Taylorov red funkcije pravca uz odbacivanje clanova viseg reda (Linearizacija)
        ///     <para/>Vrijednost razvoja u red data je u radijanima
        /// </summary>
        /// <param name="stajaliste">Priblizne koordinate tocke stajalista prilikom mjerenja (Od)</param>
        /// <param name="vizura">Priblizne koordinate tocke vizure (Do)</param>
        public Pravac(ITockaProjekcija stajaliste, ITockaProjekcija vizura) : base(stajaliste, vizura)
        {
            Radians smjerniKut = stajaliste.SmjerniKut(vizura);
            double duzina = stajaliste.Duzina(vizura);

            yStajaliste = new Radians(-Matematika.Cos(smjerniKut) / duzina);
            xStajaliste = new Radians(Matematika.Sin(smjerniKut) / duzina);

            yVizura = -yStajaliste;
            xVizura = -xStajaliste;

            zNepoznanicaOrijentacije = 1;
        }
    }
}
