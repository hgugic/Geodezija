using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Kutevi
{
    /// <summary>
    /// Klasa <c>Hours</c> predstavlja kut ili pravac u satima (dekadski sustav)
    /// </summary>
    /// <remarks>Koristi se najcesce za nebeske koordinate sisteme (npr. Mjesni ekvatorski koordinatni sustav)</remarks>
    public class Hours : IRadian, IComparable, IEquatable<IRadian>
    {
        /// <summary>
        /// Mjera (vrijednost) kuta u satima (dekadski sustav)
        /// </summary>
        public double Angle { get; set; }

        #region Konstruktori

        public Hours(double angle)
        {
            Angle = angle;
        }

        public Hours(IRadian angle)
        {
            Angle = angle.ToRadians().Angle * 12 / Math.PI;
        }

        #endregion Konstruktori

        #region Implicit operators

        public static implicit operator Radians(Hours kut)
        {
            return kut.ToRadians();
        }

        public static implicit operator HMS(Hours kut)
        {
            return kut.ToRadians().ToHMS();
        }

        #endregion Implicit operators

        #region Arithmetic operators

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima (dekadski sustav)</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>Hours</returns>
        public static Hours operator +(Hours a, IRadian b)
        {
            return new Hours(a.ToRadians() + b.ToRadians());
        }

        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima (dekadski sustav)</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>Hours</returns>
        public static Hours operator -(Hours a, IRadian b)
        {
            return new Hours(a.ToRadians() - b.ToRadians());
        }

        /// <summary>
        /// Matematicka operacija promjene smjera (predznaka) kuta ili pravca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima (dekadski sustav)</param>
        /// <returns>Hours</returns>
        public static Hours operator -(Hours a)
        {
            return new Hours(-1 * a.Angle);
        }

        #region Hours, double 

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac u satima</param>
        /// <returns>Hours</returns>
        public static Hours operator +(Hours a, double b)
        {
            return new Hours(a.Angle + b);
        }

        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac u satima</param>
        /// <returns>Hours</returns>
        public static Hours operator -(Hours a, double b)
        {
            return new Hours(a.Angle - b);
        }

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac u satima</param>
        /// <returns>Hours</returns>
        public static Hours operator +(double a, Hours b)
        {
            return new Hours(a + b.Angle);
        }

        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac u satima</param>
        /// <returns>Hours</returns>
        public static Hours operator -(double a, Hours b)
        {
            return new Hours(a - b.Angle);
        }

        /// <summary>
        /// Matematicka operacija množenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="k">Koeficijent (broj)</param>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <returns>Hours</returns>
        public static Hours operator *(double k, Hours a)
        {
            return new Hours(a.Angle * k);
        }

        /// <summary>
        /// Matematicka operacija množenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="k">Koeficijent (broj)</param>
        /// <returns>Hours</returns>
        public static Hours operator *(Hours a, double k)
        {
            return new Hours(a.Angle * k);
        }

        /// <summary>
        /// Matematicka operacija dijeljenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="k">Koeficijent (broj)</param>
        /// <returns>Hours</returns>
        public static Hours operator /(Hours a, double k)
        {
            return new Hours(a.Angle / k);
        }

        #endregion Hours, double 

        #endregion Arithmetic operators

        #region Comparison operators

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator <(Hours a, IRadian b)
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
        public static bool operator >(Hours a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) > Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator >=(Hours a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) >= Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator <=(Hours a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) <= Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator ==(Hours a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) == Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u satima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>bool</returns>
        public static bool operator !=(Hours a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) != Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        #endregion Comparison operators

        #region Parse

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u satima
        /// </summary>
        /// <exception cref="FormatException">String nije u odgovarajucem formatu</exception>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <returns>Hours</returns>
        public static Hours Parse(string s)
        {
            string str = s.Substring(s.Length - 1, 1);

            if (str != "h")
            {
                if (str != "H")
                {
                    throw new FormatException("Unesen string nije u odgovarajucem formatu");
                }

            }

            try
            {
                return new Hours(double.Parse(s.Substring(0, s.Length - 1)));
            }
            catch
            {
                throw new FormatException("Unesen string nije u odgovarajucem formatu");
            }
        }

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u satima
        /// </summary>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <param name="kut">Vraca vrijednost kuta u satima, ako je konverzija neuspjesna vraca vrijednost 0</param>
        /// <returns>bool</returns>
        public static bool TryParse(string s, out Hours kut)
        {
            try
            {
                kut = new Hours(double.Parse(s));
                return true;
            }
            catch
            {
                kut = new Hours(0);
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
            return new Radians(Angle * Math.PI / 12);
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
            builder.Append("h");

            return builder.ToString();
        }

        /// <summary>
        /// Zaokruzivanje mjere kuta na zadan broj decimala
        /// </summary>
        /// <param name="decimals"></param>
        /// <returns>Hours</returns>
        public Hours Round(int decimals)
        {
            Math.Round(Angle, decimals);

            return this;
        }
    }
}
