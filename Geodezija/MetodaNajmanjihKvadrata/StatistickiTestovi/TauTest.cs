using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Geodezija.MetodaNajmanjihKvadrata.StatistickiTestovi
{
    /// <summary>
    /// Klasa <c>TauTest</c> koristi se za otkrivanje grube greske u mjerenjima (jednovarijantni test)
    /// </summary>
    public class TauTest
    {
        double tauDistribucija;
        DenseVector tau;
        List<double> tauList = new List<double>();
        List<bool> rezultat = new List<bool>();

        /// <summary>
        /// Vrijednost Tau distribucije za broj prekobrojnosti i izabran nivo signifikantnosti
        /// </summary>
        public double TauDistribucija
        {
            get
            {
                return tauDistribucija;
            }

            set
            {
                tauDistribucija = value;
            }
        }

        /// <summary>
        /// Apsolutne vrijednosti standardizirane popravke tj. odnos apsolutne vrijednosti popravke i standardne devijacije mjerenja
        /// </summary>
        public DenseVector Tau
        {
            get
            {
                return tau;
            }

            set
            {
                tau = value;
            }
        }

        /// <summary>
        /// Apsolutne vrijednosti standardizirane popravke tj. odnos apsolutne vrijednosti popravke i standardne devijacije mjerenja
        /// </summary>
        public List<double> TauList
        {
            get
            {
                return tauList;
            }

            set
            {
                tauList = value;
            }
        }

        /// <summary>
        /// Rezultat statistickog testiranja mjerenja
        /// </summary>
        /// <returns>List(bool)</returns>
        public List<bool> RezultatTesta()
        {
            return rezultat;
        }

        /// <summary>
        /// <para/>Lokalni jednovarijantni test, otkrivanje grubih gresaka         
        /// </summary>
        /// <param name="sKvadrat">A posteriori varijanca</param>
        /// <param name="v">Vektor popravaka mjerenja</param>
        /// <param name="Qv">Matrica kofaktora popravaka mjerenja</param>
        /// <param name="alfa">Nivo signifikantnosti</param>
        /// <param name="f">Broj prekobrojnosti</param>
        /// <exception cref="ArgumentException">Baca se kada matrica ili vektor nema odgovarajuce dimenzije</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada nivo sifnifikantnosti nije u intervalu 0-1 ili je broj prekobrojnosti manji od 1</exception>
        public TauTest(double sKvadrat, DenseVector v, DenseMatrix Qv, double alfa, int f)
        {
            if (Qv.RowCount < Qv.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice Qv("
                    + Qv.RowCount.ToString() + "x" + Qv.ColumnCount.ToString() +
                    "). Broj redova mora biti veci od broja stupaca");
            }
            else if (v.Count != Qv.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice Qv("
                    + Qv.RowCount.ToString() + "x" + Qv.ColumnCount.ToString() +
                    ") ili vektora v(" + v.Count.ToString() + "x1)");
            }
            else if (alfa < 0 || alfa> 1)
            {
                throw new ArgumentOutOfRangeException("alfa", alfa, "Nivo signifikantnosti mora biti u intervalu 0 < alfa < 1");
            }
            else if (f < 1)
            {
                throw new ArgumentOutOfRangeException("f", f, "Broj prekobrojnosti mora biti veci od nule");
            }
            else
            {
                tauDistribucija = Distribucije.Tau(alfa, f);
                TestStatistika(sKvadrat, v, Qv);
            }
        }

        private void TestStatistika(double sKvadrat, DenseVector v, DenseMatrix Qv)
        {
            DenseVector qv = sKvadrat * (DenseVector)Qv.Diagonal();

            for (int i = 0; i < v.Count; i++)
            {
                double test = Math.Abs(v[i]) / Math.Sqrt(qv[i]);

                tauList.Add(test);

                if (test < tauDistribucija)
                    rezultat.Add(true);
                else
                    rezultat.Add(false);
            }

            tau = new DenseVector(tauList.ToArray());
        }


    }
}
