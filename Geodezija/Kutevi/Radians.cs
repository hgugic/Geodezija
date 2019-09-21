using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Kutevi
{
    public class Radians : IRadian, IComparable, IEquatable<IRadian>
    {
        public double  Angle { get; set; }

        public Radians(double angle = 0)
        {
            Angle = angle;
        }

        public Radians(IRadian kut)
        {
            Angle = kut.ToRadians().Angle;
        }

        public double GlavnaMjeraKuta()
        {
            int k = (int)(Angle / (2 * Math.PI));
            return Angle - 2 * Math.PI * k;
        }

        public double GlavnaMjeraKutaPozitivanSmjer()
        {
            if (GlavnaMjeraKuta() < 0) return GlavnaMjeraKuta() + 2 * Math.PI;

            return GlavnaMjeraKuta();

        }

        #region Arithmetic operators

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>Radians</returns>
        public static Radians operator +(Radians a, IRadian b)
        {
            return new Radians(a.Angle + b.ToRadians().Angle);           
        }


        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>Radians</returns>
        public static Radians operator -(Radians a, IRadian b)
        {
            return new Radians(a.Angle - b.ToRadians().Angle);
        }

        /// <summary>
        /// Matematicka operacija promjene smjera (predznaka) kuta ili pravca
        /// </summary>
        /// <remarks>Promjena predznaka kuta ili pravca</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <returns>Radians</returns>
        public static Radians operator -(Radians a)
        {
            return new Radians(-1 * a.Angle);         
        }

        #region Radians, double 

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (double)</param>
        /// <returns>Radians</returns>
        public static Radians operator +(Radians a, double b)
        {
            return new Radians(a.Angle + b);
        }


        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (double)</param>
        /// <returns>Radians</returns>
        public static Radians operator -(Radians a, double b)
        {
            return new Radians(a.Angle - b);
        }

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac u radijanima</param>
        /// <returns>Radians</returns>
        public static Radians operator +(double a, Radians b)
        {
            return new Radians(a + b.Angle);
        }


        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac u radijanima</param>
        /// <returns>Radians</returns>
        public static Radians operator -(double a, Radians b)
        {
            return new Radians(a - b.Angle);
        }

        /// <summary>
        /// Matematicka operacija množenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="k">Koeficijent (broj)</param>
        /// <returns>Radians</returns>
        public static Radians operator *(Radians a, double k)
        {
            return new Radians(a.Angle * k);
        }

        /// <summary>
        /// Matematicka operacija množenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="k">Koeficijent (broj)</param>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <returns>Radians</returns>
        public static Radians operator *(double k, Radians a)
        {
            return new Radians(a.Angle * k);
        }

        /// <summary>
        /// Matematicka operacija dijeljenja kuteva ili pravaca nekim koeficijentom (brojem)
        /// </summary>
        /// <remarks>Matematicka operacija drugog stupnja</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="k">Koeficijent (broj)</param>
        /// <returns>Radians</returns>
        public static Radians operator /(Radians a, double k)
        {
            return new Radians(a.Angle / k);
        }

        #endregion Radians, double 

        #endregion Arithmetic operators

        #region Comparison operators

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator <(Radians a, IRadian b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) < Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac u (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator >(Radians a, IRadian b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) > Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator >=(Radians a, IRadian b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) >= Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator <=(Radians a, IRadian b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) <= Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator ==(Radians a, IRadian b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) == Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator !=(Radians a, IRadian b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) != Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        #region Radians, double

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (double)</param>
        /// <returns>bool</returns>
        public static bool operator <(Radians a, double b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) < Math.Abs(b)) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac u (double)</param>
        /// <returns>bool</returns>
        public static bool operator >(Radians a, double b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) > Math.Abs(b)) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (double)</param>
        /// <returns>bool</returns>
        public static bool operator >=(Radians a, double b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) >= Math.Abs(b)) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u radijanima</param>
        /// <param name="b">Kut ili pravac (double)</param>
        /// <returns>bool</returns>
        public static bool operator <=(Radians a, double b)
        {
            if (Math.Abs(a.GlavnaMjeraKuta()) <= Math.Abs(b)) return true;

            return false;
        }


        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac</param>
        /// <param name="b">Kut ili pravac u radijanima</param>
        /// <returns>bool</returns>
        public static bool operator <(double a, Radians b)
        {
            if (Math.Abs(a) < Math.Abs(b.GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac</param>
        /// <param name="b">Kut ili pravacu radijanima</param>
        /// <returns>bool</returns>
        public static bool operator >(double a, Radians b)
        {
            if (Math.Abs(a) > Math.Abs(b.GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac</param>
        /// <param name="b">Kut ili pravac u radijanima</param>
        /// <returns>bool</returns>
        public static bool operator >=(double a, Radians b)
        {
            if (Math.Abs(a) >= Math.Abs(b.GlavnaMjeraKuta())) return true;

            return false;
        }

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac </param>
        /// <param name="b">Kut ili pravac u radijanima</param>
        /// <returns>bool</returns>
        public static bool operator <=(double a, Radians b)
        {
            if (Math.Abs(a) <= Math.Abs(b.GlavnaMjeraKuta())) return true;

            return false;
        }

        #endregion Radians, double

        #endregion Comparison operators

        #region Parse

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u radijanima
        /// </summary>
        /// <exception cref="FormatException">String nije u odgovarajucem formatu</exception>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <returns>Radians</returns>
        public static Radians Parse(string s)
        {
            try
            {
                return new Radians(double.Parse(s));
            }
            catch
            {
                throw new FormatException("Unesen string nije u odgovarajucem formatu");
            }
        }

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u radijanima
        /// </summary>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <param name="kut">Vraca vrijednost kuta u radijanima, ako je konverzija neuspjesna vraca vrijednost 0</param>
        /// <returns>bool</returns>
        public static bool TryParse(string s, out Radians kut)
        {
            try
            {
                kut = new Radians(double.Parse(s));
                return true;
            }
            catch
            {
                kut = new Radians();
                return false;
            }
        }

        #endregion Parse

        #region Conversion to other units

        public Radians ToRadians()
        {
            return this;
        }

        public Degrees ToDegrees()
        {
            return new Degrees(this);
        }

        public DMS ToDMS()
        {
            return new DMS(this);
        }

        public Seconds ToSeconds()
        {
            return new Seconds(this);
        }

        public Hours ToHours()
        {
            return new Hours(this);
        }

        public HMS ToHMS()
        {
            return new HMS(this);
        }

        public Gradians ToGradians()
        {
            return new Gradians(this);
        }

        #endregion Conversion to other units

        /// <summary>
        /// Usporeduje ovu instancu sa odredenim objektom i vraca integer koju ukazuje da li je vrijednost instance veca, manja ili jednaka objektu 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public int CompareTo(object obj)
        {
            IRadian angle = obj as IRadian;

            if (angle != null)
            {
                return this.GlavnaMjeraKuta().CompareTo(angle.ToRadians().GlavnaMjeraKuta());
            }

            throw new ArgumentException("Objekt ne implementira IRadian interface");
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
            return Angle.ToString();
        }

        /// <summary>
        /// Zaokruzivanje mjere kuta na zadan broj decimala
        /// </summary>
        /// <param name="decimals"></param>
        /// <returns>Radians</returns>
        public Radians Round(int decimals)
        {
            Math.Round(Angle, decimals);

            return this;
        }
    }
}
