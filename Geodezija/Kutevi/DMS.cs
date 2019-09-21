using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Geodezija.Kutevi
{
    /// <summary>
    /// Klasa <c>DMS</c> predstavlja kut ili pravac u stupnjevima (seksagezimalni sustav)
    /// </summary>
    public class DMS : IRadian, IComparable, IEquatable<IRadian>
    {
        #region Properties

        public int Sign { get; set; }

        public int Degrees { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada je broj minuta veci od 59.</exception>
        public int Minutes
        {
            get
            {
                return minutes;
            }

            set
            {
                if (Math.Abs(value) > 59)
                {
                    throw new ArgumentOutOfRangeException("Minutes", Minutes, "DMS: Broj minuta ne moze biti veci od 59");
                }

                minutes = Math.Abs(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Baca se kada je broj sekunda veci ili jednak 60.</exception>
        public double Seconds
        {
            get
            {
                return seconds;
            }

            set
            {
                if (Math.Abs(value) >= 60)
                {
                    throw new ArgumentOutOfRangeException("Seconds", Seconds, "DMS: Broj sekunda ne moze biti veci ili jednak 60");
                }
                seconds = Math.Abs(value);
            }
        }

        int minutes;
        double seconds;

        #endregion Properties

        #region Kontruktori

        /// <summary>
        /// Inicijalizira novu instancu klase Geodezija.Kutevi.DMS
        /// </summary>
        /// <param name="Degrees">Dio vrijednost kuta ili pravca u stupnjevima</param>
        /// <param name="Minutes">Dio vrijednost kuta ili pravca u minutama</param>
        /// <param name="Seconds">Dio vrijednost kuta ili pravca u sekundama</param>
        /// <remarks>
        /// U slucaju da kut ili pravac ima negativan predznak, predznak se moze postaviti na bilo koji argument.
        /// </remarks>
        public DMS(int Degrees, int Minutes, double Seconds)
        {
            if (Degrees < 0 || Minutes<0 || Seconds < 0)
            {
                Sign = -1;
                this.Degrees = Math.Abs(Degrees);
                this.Minutes = Math.Abs(Minutes);
                this.Seconds = Math.Abs(Seconds);
            }
            else
            {
                Sign = 1;
                this.Degrees = Math.Abs(Degrees);
                this.Minutes = Math.Abs(Minutes);
                this.Seconds = Math.Abs(Seconds);
            }
        }

        public DMS(IRadian angle)
        {
            double deg = angle.ToRadians().Angle * 180 / Math.PI;
            Sign = Math.Sign(deg);
            deg = Math.Abs(deg);


            Degrees = (int)deg;
            Minutes = (int)((deg - Degrees) * 60.0);
            Seconds = (deg - Degrees - Minutes / 60.0) * 3600;
        }

        #endregion Kontruktori

        #region Implicit operators

        public static implicit operator Radians(DMS kut)
        {
            return kut.ToRadians();
        }

        public static implicit operator Degrees(DMS kut)
        {
            return kut.ToRadians().ToDegrees();
        }

        public static implicit operator Seconds(DMS kut)
        {
            return kut.ToRadians().ToSeconds();
        }

        #endregion Implicit operators

        #region Arithmetic operators

        /// <summary>
        /// Matematicka operacija zbrajanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima (seksagezimalni sustav)</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>DMS</returns>
        public static DMS operator +(DMS a, IRadian b)
        {
            return new DMS(a.ToRadians() + b.ToRadians());
        }

        /// <summary>
        /// Matematicka operacija oduzimanja kuteva ili pravaca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima (seksagezimalni sustav)</param>
        /// <param name="b">Kut ili pravac</param>
        /// <returns>DMS</returns>
        public static DMS operator -(DMS a, IRadian b)
        {
            return new DMS(a.ToRadians() - b.ToRadians());
        }

        /// <summary>
        /// Matematicka operacija promjene smjera (predznaka) kuta ili pravca
        /// </summary>
        /// <remarks>Matematicka operacija prvog stupnja</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima (seksagezimalni sustav)</param>
        /// <returns>DMS</returns>
        public static DMS operator -(DMS a)
        {
            DMS angle = new DMS(a.Degrees, a.Minutes, a.Seconds);

            angle.Sign = (-1) * a.Sign;

            return angle;
        }

        #endregion Arithmetic operators

        #region Comparison operators

        /// <summary>
        /// Usporedivanje mjere kuta (ugla) - usporeduju se apsolutne vrijednosti glavne mjere
        /// </summary>
        /// <remarks>Pripazite na smjera mjerenja jer se usporeduju apsolutne vrijednosti glavne mjere</remarks>
        /// <param name="a">Kut ili pravac u stupnjevima</param>
        /// <param name="b">Kut ili pravac (IRadian)</param>
        /// <returns>bool</returns>
        public static bool operator <(DMS a, IRadian b)
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
        public static bool operator >(DMS a, IRadian b)
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
        public static bool operator >=(DMS a, IRadian b)
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
        public static bool operator <=(DMS a, IRadian b)
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
        public static bool operator ==(DMS a, IRadian b)
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
        public static bool operator !=(DMS a, IRadian b)
        {
            if (Math.Abs(a.ToRadians().GlavnaMjeraKuta()) != Math.Abs(b.ToRadians().GlavnaMjeraKuta())) return true;

            return false;
        }

        #endregion Comparison operators

        #region Parse

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u stupnjeve (seksagezimalni sustav)
        /// </summary>
        /// <remarks>
        ///     <para/>Nije moguce konvertirati brojeve sa eksponentom npr. 10° 10' 2.2E-20" 
        /// </remarks>
        /// <exception cref="FormatException">String nije u odgovarajucem formatu</exception>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <returns>DMS</returns>
        public static DMS Parse(string s)
        {
            string regexPattern = @"^(-?)(\d+)([^\d]{1,2})([\d+]{1,2})([^\d]{1,2})(\d+(?:[.|,]{0,1}?\d)+(?:[eE]-?\d+)?)([^\d]{0,1})$";

            if (!Regex.IsMatch(s, regexPattern))
                throw new FormatException("Unesen string nije u odgovarajucem formatu");

            string[] numbers = Regex.Split(s, regexPattern).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            double d;

            numbers = numbers.Where(x => double.TryParse(x, out d)).ToArray();

            return new DMS(int.Parse(numbers[0]), int.Parse(numbers[1]), double.Parse(numbers[2]));
        }

        /// <summary>
        /// Pretvara string u vrijednost kuta ili pravaca u stupnjeve (seksagezimalni sustav)
        /// </summary>
        /// <param name="s">String za konverziju koji sadrzi broj</param>
        /// <param name="dms">Vraca vrijednost kuta u stupnjeve (seksagezimalni sustav), ako je konverzija neuspjesna vraca vrijednost 0-0-0</param>
        /// <returns>bool</returns>
        public static bool TryParse(string s, out DMS dms)
        {
            try
            {
                dms = DMS.Parse(s);
                return true;
            }
            catch
            {
                dms = new DMS(0, 0, 0);
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
            double angle = Math.Sign(Sign) * (Degrees + Minutes / 60.0 + Seconds / 3600.0) * Math.PI / 180;

            return new Radians(angle);
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
            return this.ToRadians().Angle.GetHashCode();
        }

        /// <summary>
        /// Vraca vrijednost kuta u obliku stringa
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if (Sign < 0)
            {
                builder.Append("-");
            }

            builder.Append(Degrees);
            builder.Append("° ");
            builder.Append(Minutes);
            builder.Append("' ");
            builder.Append(Seconds);
            builder.Append("\"");

            return builder.ToString();
        }

        /// <summary>
        /// Zaokruzivanje dijela mjere kuta u sekundama na zadan broj decimala
        /// </summary>
        /// <param name="decimals"></param>
        /// <returns>DMS</returns>
        public DMS Round(int decimals = 0)
        {
            Math.Round(Seconds, decimals);

            return this;
        }
    }
}
