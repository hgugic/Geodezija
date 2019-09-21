using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Kutevi
{
    public class Matematika
    {
        #region Trigonometrijske funkcije

        /// <summary>
        /// Vraca sinus kuta
        /// </summary>
        /// <returns>double</returns>
        public static double Sin(IRadian kut)
        {
            return Math.Sin(kut.ToRadians().Angle);
        }

        /// <summary>
        /// Vraca kosinus kuta
        /// </summary>
        /// <returns>double</returns>
        public static double Cos(IRadian kut)
        {
            return Math.Cos(kut.ToRadians().Angle);
        }

        /// <summary>
        /// Vraca Tangens kuta
        /// </summary>
        /// <returns>double</returns>
        public static double Tan(IRadian kut)
        {
            return Math.Tan(kut.ToRadians().Angle);
        }

        /// <summary>
        /// Vraca Kotangens kuta
        /// </summary>
        /// <returns>double</returns>
        public static double Cot(IRadian kut)
        {
            return 1/Math.Tan(kut.ToRadians().Angle);
        }

        /// <summary>
        /// Vraca kosekans kuta
        /// </summary>
        /// <returns>double</returns>
        public static double Sec(IRadian kut)
        {
            return 1 / (Math.Cos(kut.ToRadians().Angle));
        }

        /// <summary>
        /// Vraca kosekans kuta
        /// </summary>
        /// <returns>double</returns>
        public static double Csc(IRadian kut)
        {
            return 1 / (Math.Sin(kut.ToRadians().Angle));
        }

        #endregion Trigonometrijske funkcije

        /// <summary>
        /// Vraca arkus sinus kuta
        /// </summary>
        /// <returns>Radians</returns>
        public static Radians Asin(double d)
        {
            return new Radians(Math.Asin(d));
        }

        /// <summary>
        /// Vraca arkus kosinus kuta
        /// </summary>
        /// <returns>Radians</returns>
        public static Radians Acos(double d)
        {
            return new Radians(Math.Acos(d));
        }

        /// <summary>
        /// Vraca arkus tangens kuta
        /// </summary>
        /// <returns>Radians</returns>
        public static Radians Atan(double d)
        {
            return new Radians(Math.Atan(d));
        }

        /// <summary>
        /// Vraca arkus cotangens kuta
        /// </summary>
        /// <returns>Radians</returns>
        public static Radians Acot(double d)
        {
            return new Radians(1/Math.Atan(d));
        }
    }
}
