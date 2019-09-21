using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Geodezija.MetodaNajmanjihKvadrata.StatistickiTestovi
{
    /// <summary>
    /// Klasa <c>DataSnooping</c> koristi se za otkrivanje grube greske u mjerenjima poznat je i kao B-metoda (W. Baarda)
    /// </summary>
    public class DataSnooping
    {

        double lambdaKorjen;
        DenseVector T;
        List<double> TList = new List<double>();
        List<bool> rezultat = new List<bool>();


        /// <summary>
        /// Korijen iz parametra necentralnosti 
        /// </summary>
        /// <remarks>
        /// Odreden je iz normirane noramalne razdiobe (0,1) sa nivoom signifikantnosti alfa (greska I tipa) i snagom testa beta (greska II tipa)
        /// </remarks>
        public double LambdaKorijen
        {
            get
            {
                return lambdaKorjen;
            }

            set
            {
                lambdaKorjen = value;
            }
        }

        /// <summary>
        /// Vrijednosti za testiranje hipoteze
        /// </summary>
        public DenseVector t
        {
            get
            {
                return T;
            }

            set
            {
                T = value;
            }
        }

        /// <summary>
        /// Vrijednosti za testiranje hipoteze
        /// </summary>
        public List<double> tList
        {
            get
            {
                return TList;
            }

            set
            {
                TList = value;
            }
        }

        /// <summary>
        /// <para/>Data Snooping (B-metoda), otkrivanje grubih gresaka 
        /// </summary>
        /// <param name="sigmaNulaKvadrat">A priori varijanca izjednacenja</param>
        /// <param name="v">Vektor popravaka mjerenja</param>
        /// <param name="Ql">Matrica kofaktora mjerenja</param>
        /// <param name="R">Matrica unutrasnje pouzdanosti</param>
        /// <param name="alfa">Nivo signifikantnosti</param>
        /// <param name="beta">Snaga testa</param>
        /// <exception cref="ArgumentException">Baca se kada matrica ili vektor nema odgovarajuce dimenzije</exception>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada nivo sifnifikantnosti ili snaga testa nije u intervalu 0-1</exception>
        public DataSnooping(double sigmaNulaKvadrat, DenseVector v, DenseMatrix Ql, DenseMatrix R, double alfa, double beta)
        {
            if (Ql.RowCount < Ql.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice Ql("
                    + Ql.RowCount.ToString() + "x" + Ql.ColumnCount.ToString() +
                    "). Broj redova mora biti veci od broja stupaca");
            }
            else if (R.RowCount != R.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice R("
                    + R.RowCount.ToString() + "x" + R.ColumnCount.ToString() +
                    "). Matrica mora biti kvadratna");
            }
            else if (Ql.RowCount != R.RowCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice Ql("
                    + Ql.RowCount.ToString() + "x" + Ql.ColumnCount.ToString() +
                    ") ili R(" + R.RowCount.ToString() + "x" + R.ColumnCount.ToString() + ")");
            }
            else if (v.Count != R.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice R("
                    + R.RowCount.ToString() + "x" + R.ColumnCount.ToString() +
                    ") ili vektora v(" + v.Count.ToString() + "x1)");
            }
            else if (alfa < 0 || alfa > 1)
            {
                throw new ArgumentOutOfRangeException("alfa", alfa, "Nivo signifikantnosti mora biti u intervalu 0 < alfa < 1");
            }
            else if (beta < 0 || beta > 1)
            {
                throw new ArgumentOutOfRangeException("beta", beta, "Snaga testa mora biti u intervalu 0 < beta < 1");
            }
            else
            {
                lambdaKorjen = Math.Sqrt(Distribucije.ParametarNecentralnosti(alfa, beta));
                tVrijednost(sigmaNulaKvadrat, v, Ql, R);

            }


        }

        private void tVrijednost(double sigmaNulaKvadrat, DenseVector v, DenseMatrix Ql, DenseMatrix R)
        {
            DenseVector ql = sigmaNulaKvadrat * (DenseVector)Ql.Diagonal();
            DenseVector r = (DenseVector)R.Diagonal();


            for (int i = 0; i < v.Count; i++)
            {
                double test = Math.Abs(v[i]) / Math.Sqrt(r[i] * ql[i]);

                TList.Add(test);

                if (test < lambdaKorjen)
                    rezultat.Add(true);
                else
                    rezultat.Add(false);
            }

            T = new DenseVector(TList.ToArray());
        }

        /// <summary>
        /// Rezultat statistickog testiranja mjerenja
        /// </summary>
        /// <returns>List(bool)</returns>
        public List<bool> RezultatTesta()
        {
            return rezultat;
        }

    }
}
