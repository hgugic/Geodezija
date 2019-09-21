using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Kutevi
{
    /// <summary>
    /// Klasa <c>Degrees</c> predstavlja kut ili pravac u stupnjevima (dekadski sustav)
    /// </summary>
    public class Degrees : IRadian, IComparable, IEquatable<IRadian>
    {
        /// <summary>
        /// Mjera (vrijednost) kuta u stupnjevima (dekadski sustav)
        /// </summary>
        public double Angle { get; set; }

        #region Konstruktori

        public Degrees(double angle)
        {
            Angle = angle;
        }

        public Degrees(IRadian angle)
        {
            Angle = angle.ToRadians().Angle * 180 / Math.PI;
        }

        #endregion Konstruktori

        public static implicit operator Radians(Degrees kut)
        {
            return kut.ToRadians();
        }

        public static implicit operator DMS(Degrees kut)
        {
            return kut.ToRadians().ToDMS();
        }

        #region Arithmetic operators

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima (dekadski sustav)</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>Degrees</returns>
        public static Degrees operator +(Degrees a, IRadian b)
        {
            return new Degrees(a.ToRadians() + b.ToRadians());
        }

        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima (dekadski sustav)</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>Degrees</returns>
        public static Degrees operator -(Degrees a, IRadian b)
        {
            return new Degrees(a.ToRadians() - b.ToRadians());
        }

        /// <summary>
        /// Matematicka operacija promjene smjera (predznaka) kuta ili pravca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima (dekadski sustav)</param>
        /// <returns>Degrees</returns>
        public static Degrees operator -(Degrees a)
        {
            return new Degrees((-1) * a.Angle);
        }

        #region Degrees, double 

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac u stupnjevima</param>
        /// <returns>Degrees</returns>
        public static Degrees operator +(Degrees a, double b)
        {
            return new Degrees(a.Angle + b);
        }

        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac u stupnjevima</param>
        /// <returns>Degrees</returns>
        public static Degrees operator -(Degrees a, double b)
        {
            return new Degrees(a.Angle - b);
        }

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac u stupnjevima</param>
        /// <returns>Degrees</returns>
        public static Degrees operator +(double a, Degrees b)
        {
            return new Degrees(a + b.Angle);
        }

        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac u stupnjevima</param>
        /// <returns>Degrees</returns>
        public static Degrees operator -(double a, Degrees b)
        {
            return new Degrees(a - b.Angle);
        }

        /// <summary>
        /// Matematicka operacija množenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="k">Koeficijent (broj)</param>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <returns>Degrees</returns>
        public static Degrees operator *(double k, Degrees a)
        {
            return new Degrees(a.Angle * k);
        }

        /// <summary>
        /// Matematicka operacija množenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="k">Koeficijent (broj)</param>
        /// <returns>Degrees</returns>
        public static Degrees operator *(Degrees a, double k)
        {
            return new Degrees(a.Angle * k);
        }

        /// <summary>
        /// Matematicka operacija dijeljenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="k">Koeficijent (broj)</param>
        /// <returns>Degrees</returns>
        public static Degrees operator /(Degrees a, double k)
        {
            return new Degrees(a.Angle / k);
        }

        #endregion Degrees, double 

        #endregion Arithmetic operators

        #region Comparison operators

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator <(Degrees a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) < Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator >(Degrees a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) > Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator >=(Degrees a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) >= Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac </param>
        /// <returns>bool</returns>
        public static bool operator <=(Degrees a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) <= Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator ==(Degrees a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) == Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator !=(Degrees a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) != Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        #endregion Comparison operators

        #region Parse

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u stupnjevima
        /// </summary>
        /// <exception cref="FormatException">String nije u odgovarajucem formatu</exception>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <returns>Degrees</returns>
        public static Degrees Parse(string s)
        {
            string str = s.Substring(s.Length - 1, 1);

            if (str != "°")
            {
                if (str != "d")
                {
                    if (str != "D")
                    {
                        throw new FormatException("Unesen string nije u odgovarajucem formatu");
                    }
                }

            }

            try
            {
                return new Degrees(double.Parse(s.Substring(0, s.Length - 1)));
            }
            catch
            {
                throw new FormatException("Unesen string nije u odgovarajucem formatu");
            }
        }

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u stupnjevima
        /// </summary>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <param name="kut">Vraca vrijednost kuta u stupnjevima, ako je konverzija neuspjesna vraca vrijednost 0</param>
        /// <returns>bool</returns>
        public static bool TryParse(string s, out Degrees kut)
        {
            try
            {
                kut = new Degrees(double.Parse(s));
                return true;
            }
            catch
            {
                kut = new Degrees(0);
                return false;
            }
        }

        #endregion Parse

        /// <summary>
        /// Vraca vrijednost kuta u radijanima
        /// </summary>
        /// <returns>Radians</returns>
        public Radians ToRadians()
        {
            return new Radians(Angle * Math.PI / 180);
        }

        /// <summary>
        /// Usporeduje ovu instancu sa odredenim objektom i vraca integer koju ukazuje da li je vrijednost instance veca, manja ili jednaka objektu 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public int CompareTo(object obj)
        {
            return new Radians(this).CompareTo(obj);
        }

        /// <summary>
        /// Vraća vrijednost koja ukazuje da li je instanca jednaka odredenom kutu
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(IRadian other)
        {
            if (other == null)
                return false;

            if (this == other)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Vraća vrijednost koja ukazuje da li je instanca jednaka odredenom objektu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            IRadian Obj = obj as IRadian;
            if (Obj == null)
                return false;
            else
                return Equals(Obj);
        }

        /// <summary>
        /// Vraca hash kod instance
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return this.Angle.GetHashCode();
        }

        /// <summary>
        /// Vraca vrijednost kuta u obliku stringa
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Angle.ToString());
            builder.Append("°");

            return builder.ToString();
        }
    }
}
