using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Tocke;
using Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja;
using Geodezija.Kutevi;

namespace Geodezija.UnitTests.MetodaNajmanjihKvadrataTest.PrikracenaMjerenjaTest
{
    [TestClass]
    public class PravacSlobodanClanTest
    {
        readonly double tolerance = Math.Pow(10, -9);

        [TestMethod]
        public void PravacSlobodaClan_f_manje_360_True()
        {
            TockaProjekcija T1 = new TockaProjekcija(1, 1);
            TockaProjekcija T2 = new TockaProjekcija(2, 2);
            DMS pravac = new DMS(45, 0, 1.1);

            PravacSlobodanClan clan = new PravacSlobodanClan(T1, T2, pravac, new Radians());

            double razlika = clan.f.ToSeconds().Angle+1.1;

            Assert.IsTrue(Math.Abs(razlika) < tolerance, clan.f.ToDMS().ToString());
        }

        [TestMethod]
        public void PravacSlobodaClan_f_vece_360_True()
        {
            TockaProjekcija T1 = new TockaProjekcija(1, 1);
            TockaProjekcija T2 = new TockaProjekcija(2, 2);
            DMS pravac = new DMS(35, 0, 1.1);

            PravacSlobodanClan clan = new PravacSlobodanClan(T1, T2, pravac, new DMS(350, 0, 0));

            double razlika = clan.f.ToSeconds().Angle +1.1;

            Assert.IsTrue(Math.Abs(razlika) < tolerance, clan.f.ToDMS().ToString()+ " razlika: " + razlika.ToString());
        }

        [TestMethod]
        public void PravacSlobodaClan_f_True()
        {
            TockaProjekcija T1 = new TockaProjekcija(1, 1);
            TockaProjekcija T2 = new TockaProjekcija(2, 2);
            DMS pravac = new DMS(44, 59, 58.9);

            PravacSlobodanClan clan = new PravacSlobodanClan(T1, T2, pravac, new Radians());

            double razlika = clan.f.ToSeconds().Angle - 1.1;

            Assert.IsTrue(Math.Abs(razlika) < tolerance, clan.f.ToDMS().ToString()+ " " + razlika.ToString());
        }
    }
}
