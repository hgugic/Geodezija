using System;
using Geodezija.Tocke;

namespace Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja
{
    /// <summary>
    /// <para/>Klasa <c>BazniVektorSlobodanClan</c> racuna slobodan clan (prikraceno mjerenja) GNSS mjerenja baznog vektora izmedu dvije tocke
    /// <para/>1D, 2D i 3D 
    /// </summary>
    public class BazniVektorSlobodanClan
    {
        /// <summary>
        /// Slobodan clan (Prikraceno mjerenja) - moze se koristiti i za Nivelman
        /// </summary>
        public double fx
        {
            get
            {
                return Fx;
            }

            set
            {
                Fx = value;
            }
        }

        /// <summary>
        /// Slobodan clan (Prikraceno mjerenja) 
        /// </summary>
        public double fy
        {
            get
            {
                return Fy;
            }

            set
            {
                Fy = value;
            }
        }

        /// <summary>
        /// Slobodan clan (Prikraceno mjerenja) 
        /// </summary>
        public double fz
        {
            get
            {
                return Fz;
            }

            set
            {
                Fz = value;
            }
        }

        double Fx, Fy, Fz;

        /// <summary>
        ///     <para/>Inicijalizira novu instancu klase Geodezija.MetodaNajmanjihKvadrata.SlobodanClanBazniVektor
        ///     <para/>Racunanje slobodnog clana (1D) za matricu slobodnih clanova, preko koordinata jedne koordinate osi i izmjerenog baznog vektora 
        /// </summary>
        /// <param name="stajaliste">Koordinata stajalista</param>
        /// <param name="vizura">Koordinata vizure</param>
        /// <param name="dx">Izmjereni vektor (koordinatna razlika)</param>
        public BazniVektorSlobodanClan(double stajaliste, double vizura, double dx)
        {
            Fx = vizura - stajaliste - dx;
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana (2D) za matricu slobodnih clanova, preko koordinata dvije tocke i izmjerenih baznih vektora 
        /// </summary>
        /// <param name="stajaliste">Tocka ishodista vektora</param>
        /// <param name="vizura">Tocka zavrsekta vektora</param>
        /// <param name="dx">Komponenta izmjerenog vektor po x-osi</param>
        /// <param name="dy">Komponenta izmjerenog vektor po y-osi</param>
        public BazniVektorSlobodanClan(ITockaProjekcija stajaliste, ITockaProjekcija vizura, double dx, double dy)
        {
            Fx = vizura.X - stajaliste.X - dx;
            Fy = vizura.Y - stajaliste.Y - dy;
        }

        /// <summary>
        ///     <para/>Racunanje slobodnog clana (3D) za matricu slobodnih clanova, preko koordinata dvije tocke i izmjerenog vektora (komponenti vektora)
        /// </summary>
        /// <param name="stajaliste"></param>
        /// <param name="vizura"></param>
        /// <param name="dx">Komponenta izmjerenog vektor po x-osi</param>
        /// <param name="dy">Komponenta izmjerenog vektor po y-osi</param>
        /// <param name="dz">Komponenta izmjerenog vektor po z-osi</param>
        public BazniVektorSlobodanClan(Tocka3D stajaliste, Tocka3D vizura, double dx, double dy, double dz)
        {
            Fx = vizura.X - stajaliste.X - dx;
            Fy = vizura.Y - stajaliste.Y - dy;
            Fz = vizura.Z - stajaliste.Z - dz;
        }


    }
}
