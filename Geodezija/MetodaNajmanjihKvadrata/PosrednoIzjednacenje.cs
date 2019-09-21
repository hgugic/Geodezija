using System;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geodezija.MetodaNajmanjihKvadrata
{
    /// <summary>
    /// Klasa <c>PosrednoIzjednacenje</c> predstavlja izjednacenje posrednom metodom i posrednom metodom sa datumskim uvjetima.  
    /// </summary>
    /// <remarks>Sustav oznaka je sustav kakav se koristi na Fakultetu Tehnickih Nauka Novi Sad.</remarks>
    public class PosrednoIzjednacenje
    {
        /// <summary>
        /// Matrica koeficijenata jednadzbi popravaka
        /// </summary>
        public DenseMatrix A { get; private set; }

        /// <summary>
        /// Matrica tezina mjerenja
        /// </summary>
        public DenseMatrix P { get; private set; }

        /// <summary>
        /// Vektor slobodnih clanova
        /// </summary>
        public DenseVector f { get; private set; }

        /// <summary>
        /// Matrica datumskih uvjeta
        /// </summary>
        public DenseMatrix G { get; private set; }

        /// <summary>
        /// Matrica koeficijenata normalnih jednadzbi
        /// </summary>
        public DenseMatrix N { get; private set; }

        /// <summary>
        /// Vektor slobodnih (apsolutnih) clanova normarnih jednadzbi
        /// </summary>
        public DenseVector n { get; private set; }

        /// <summary>
        /// Matrica kofaktora slobodnih članova (nepoznanica)
        /// </summary>
        public DenseMatrix Qx { get; private set; }

        /// <summary>
        /// Vektor izjednacenih nepoznanica (nepoznatih parametara)
        /// </summary>
        public DenseVector x { get; private set; }

        /// <summary>
        /// Vektor popravaka mjerenja
        /// </summary>
        public DenseVector v { get; private set; }

        /// <summary>
        /// Eksperimentalna standardna devijacija (referentno standardno odstupanje)
        /// </summary>
        public double sKvadrat { get; private set; }

        /// <summary>
        /// Matrica kofaktora izjednacenih mjerenja
        /// </summary>
        public DenseMatrix Qlcap { get; private set; }

        /// <summary>
        /// Matrica kofaktora popravaka mjerenja
        /// </summary>
        public DenseMatrix Qv { get; private set; }

        /// <summary>
        /// Matrica kofaktora mjerenja
        /// </summary>
        public DenseMatrix Ql { get; private set; }

        /// <summary>
        /// Matrica unutrasnje pouzdanosti
        /// </summary>
        public DenseMatrix R { get; private set; }

        /// <summary>
        /// Matrica vanjske pouzdanosti
        /// </summary>
        public DenseMatrix U { get; private set; }




        /// <summary>
        ///     <para/>Inicijalizira novu instancu klase Geodezija.MetodaNajmanjihKvadrata.PosrednoIzjednacenje 
        ///     <para/>Posredno regularno izjednacenje
        /// </summary>
        /// <param name="A">Matrica koeficijenata jednadzbi popravaka</param>
        /// <param name="P">Matrica tezina mjerenja</param>
        /// <param name="f">Vektor slobodnih clanova (prikracenih mjerenja)</param>
        /// <exception cref="ArgumentException">Baca se kada matrica nema odgovarajuce dimenzije</exception>
        /// <remarks>Posredno regularno izjednacenje</remarks>
        public PosrednoIzjednacenje(DenseMatrix A, DenseMatrix P, DenseVector f)
        {
            this.A = A;
            this.P = P;
            this.f = f;

            if (A.RowCount < A.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice A("
                    + A.RowCount.ToString() + "x" + A.ColumnCount.ToString() +
                    "). Broj redova mora biti veci od broja stupaca");
            }
            else if (P.RowCount != P.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice P("
                    + P.RowCount.ToString() + "x" + P.ColumnCount.ToString() +
                    "). Matrica mora biti kvadratna");
            }
            else if (A.RowCount != P.RowCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice A("
                    + A.RowCount.ToString() + "x" + A.ColumnCount.ToString() +
                    ") ili P(" + P.RowCount.ToString() + "x" + P.ColumnCount.ToString() + ")");
            }
            else if (f.Count != P.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice P("
                    + A.RowCount.ToString() + "x" + A.ColumnCount.ToString() +
                    ") ili vektora f(" + f.Count.ToString() + "x1)");
            }
            else
            {
                this.N = (DenseMatrix)(A.Transpose() * P * A);
                this.n = (DenseVector)(A.Transpose() * P * f);
                this.Qx = (DenseMatrix)(N.Inverse());
                this.x = (DenseVector)(-Qx * n);
                this.v = (DenseVector)(A * x + f);
                this.sKvadrat = (double)((v * P * v.ToColumnMatrix())[0] / (A.RowCount - A.ColumnCount));
                this.Qlcap = (DenseMatrix)(A * Qx * A.Transpose());
                this.Qv = (DenseMatrix)(P.Inverse() - Qlcap);
                this.Ql = (DenseMatrix)(Qv + Qlcap);
                this.R = (DenseMatrix)(Qv * Ql.Inverse());
                this.U = (DenseMatrix)(A * Qx * A.Transpose() * Ql.Inverse());
            }
        }

        /// <summary>
        ///     <para/>Inicijalizira novu instancu klase Geodezija.MetodaNajmanjihKvadrata.PosrednoIzjednacenje 
        ///     <para/>Posredno izjednacenje sa datumskum uvjetima 
        /// </summary>
        /// <param name="A">Matrica koeficijenata jednadzbi popravaka</param>
        /// <param name="P">Matrica tezina mjerenja</param>
        /// <param name="f">Vektor slobodnih clanova</param>
        /// <param name="G">Matrica datumskih uvjeta</param>
        /// <exception cref="ArgumentException">Baca se kada matrica nema odgovarajuce dimenzije</exception>
        /// <remarks>
        ///     <para/>Posredno izjednacenje sa datumskum uvjetima
        ///     <para/>Rjesavanje problema inverzije matrice koeficijenata normalnih jednadzbi (singularna matrica)
        ///     <para/>Pseudoinverzija (N+GG^t)N(N+GG^t)
        ///     <para/>Ne koristi se prosirenje matrice koeficijenata normalnih jer je ovaj sistem pogodniji za programiranje, a rezultat je isti
        /// </remarks>
        public PosrednoIzjednacenje(DenseMatrix A, DenseMatrix P, DenseVector f, DenseMatrix G)
        {
            this.A = A;
            this.P = P;
            this.f = f;
            this.G = G;

            if (A.RowCount < A.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice A("
                    + A.RowCount.ToString() + "x" + A.ColumnCount.ToString() +
                    "). Broj redova mora biti veci od broja stupaca");
            }
            else if (P.RowCount != P.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice P("
                    + P.RowCount.ToString() + "x" + P.ColumnCount.ToString() +
                    "). Matrica mora biti kvadratna");
            }
            else if (A.RowCount != P.RowCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice A("
                    + A.RowCount.ToString() + "x" + A.ColumnCount.ToString() +
                    ") ili P(" + P.RowCount.ToString() + "x" + P.ColumnCount.ToString() + ")");
            }
            else if (f.Count != P.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice P("
                    + A.RowCount.ToString() + "x" + A.ColumnCount.ToString() +
                    ") ili vektora f(" + f.Count.ToString() + "x1)");
            }
            else if (f.Count != P.ColumnCount)
            {
                throw new ArgumentException("Greska u dimenzijama matrice P("
                    + A.RowCount.ToString() + "x" + A.ColumnCount.ToString() +
                    ") ili vektora f(" + f.Count.ToString() + "x1)");
            }
            else
            {
                this.N = (DenseMatrix)(A.Transpose() * P * A);
                this.n = (DenseVector)(A.Transpose() * P * f);
                this.Qx = (DenseMatrix)(((N + G * G.Transpose()).Inverse()) * N * ((N + G * G.Transpose()).Inverse()));
                this.x = (DenseVector)(-Qx * n);
                this.v = (DenseVector)(A * x + f);
                this.sKvadrat = (double)((v * P * v.ToColumnMatrix())[0] / (A.RowCount - A.ColumnCount + G.ColumnCount));
                this.Qlcap = (DenseMatrix)(A * Qx * A.Transpose());
                this.Qv = (DenseMatrix)(P.Inverse() - Qlcap);
                this.Ql = (DenseMatrix)(Qv + Qlcap);
                this.R = (DenseMatrix)(Qv * Ql.Inverse());
                this.U = (DenseMatrix)(A * Qx * A.Transpose() * Ql.Inverse());
            }
        }
    }
}
