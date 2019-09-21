using System;
using MathNet.Numerics.Distributions;

namespace Geodezija.MetodaNajmanjihKvadrata
{
    /// <summary>
    ///  Klasa <c>Distribucije</c> sadrzi statisticke distribucije (razdiobe) koje se najcesce koriste u geodeziji
    /// </summary>
    public class Distribucije
    {

        /// <summary>
        /// Inverzna Fisherova (F) distribucija (razdioba)
        /// </summary>
        /// <param name="alfa">Nivo signifikantnosti (znacajnosti) -> 1 > alfa > 0 </param>
        /// <param name="f1">Stupanj slobode</param>
        /// <param name="f2">Stupanj slobode (ako uvrstite veliki broj dvostruka Fisherova prelazi u Chi^2)</param>
        /// <returns>double</returns>
        public static double Fisher(double alfa, int f1, int f2)
        {
            return FisherSnedecor.InvCDF(f1, f2, 1 - alfa);
        }

        /// <summary>
        /// Inverzna Chi kvadrat (X^2) distribucija (razdioba)
        /// </summary>
        /// <param name="alfa">Nivo signifikantnosti (znacajnosti) -> 1 > alfa > 0</param>
        /// <param name="f">Stupanj slobode</param>
        /// <returns>double</returns>
        public static double ChiKvadrat(double alfa, int f)
        {
            return ChiSquared.InvCDF(f, 1 - alfa);
        }

        /// <summary>
        /// Inverzna Student (T) distribucija (razdioba) 
        /// </summary>
        /// <param name="alfa">Nivo signifikantnosti (znacajnosti) -> 1 > alfa > 0</param>
        /// <param name="f">Stupanj slobode</param>
        /// <returns>double</returns>
        public static double Student(double alfa, int f)
        {
            return StudentT.InvCDF(0, 1, f, 1 - alfa);
        }

        /// <summary>
        /// Tau distribucija (razdioba) 
        /// </summary>
        /// <param name="alfa">Nivo signifikantnosti (znacajnosti) -> 1 > alfa > 0</param>
        /// <param name="f">Stupanj slobode</param>
        /// <returns>double</returns>
        /// <remarks>
        ///     <para/>Distribucija interno studentiziranih mjerenja
        ///     <para/>Popravci i njihove standardne su korelirani
        ///     <para/>Allan J. Pope 1976.
        /// </remarks>
        public static double Tau(double alfa, int f)
        {
            double t = StudentT.InvCDF(0, 1, f - 1, 1 - alfa / 2);

            double tau = t * Math.Sqrt(f) / Math.Sqrt(f - 1 + Math.Pow(t, 2));

            return tau;
        }

        /// <summary>
        /// Parametar necentralbnosti (lambda) za testiranje B-metodom (Baarda, Data snooping)
        /// </summary>
        /// <param name="alfa">Nivo signifikantnosti (znacajnosti) -> 1 > alfa > 0</param>
        /// <param name="beta">Snaga testa -> 1 > alfa > 0</param>
        /// <returns>double</returns>
        /// <remarks>
        ///     <para/> W. Baarda
        ///     <para/> Math.Pow(Normal.InvCDF(0, 1, 1 - alfa / 2) + Normal.InvCDF(0, 1, 1 - beta), 2);
        /// </remarks>
        public static double ParametarNecentralnosti(double alfa, double beta)
        {
            return Math.Pow(Normal.InvCDF(0, 1, 1 - alfa / 2) + Normal.InvCDF(0, 1, beta), 2);
        }
    }
}
