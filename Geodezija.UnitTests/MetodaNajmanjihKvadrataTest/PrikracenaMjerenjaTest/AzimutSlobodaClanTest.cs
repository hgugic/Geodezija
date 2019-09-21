using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Tocke;
using Geodezija.Kutevi;
using Geodezija.MetodaNajmanjihKvadrata.PrikracenaMjerenja;

namespace Geodezija.UnitTests.MetodaNajmanjihKvadrataTest.PrikracenaMjerenjaTest
{
    [TestClass]
    public class AzimutSlobodaClanTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        [TestMethod]
        public void AzimutSlobodaClan_f_true()
        {
            TockaProjekcija T1 = new TockaProjekcija(1, 1);
            TockaProjekcija T2 = new TockaProjekcija(2, 2);
            DMS azimut = new DMS(45, 0, 1.1);

            AzimutSlobodanClan clan = new AzimutSlobodanClan(T1, T2, azimut);

            double razlika = clan.f.ToSeconds().Angle + 1.1;

            Assert.IsTrue(razlika < tolerance, clan.f.ToSeconds().ToString());
        }

        [TestMethod]
        public void AzimutSlobodaClan_tolerancija_true()
        {
            TockaProjekcija T1 = new TockaProjekcija(1, 1);
            TockaProjekcija T2 = new TockaProjekcija(2, 2);
            DMS azimut = new DMS(45, 0, 1.1);
            Seconds tolerancija = new Seconds(2);

            AzimutSlobodanClan clan = new AzimutSlobodanClan(T1, T2, azimut, tolerancija);

            Assert.IsTrue((bool)clan.TolerancijaZadovoljena);
        }

        [TestMethod]
        public void AzimutSlobodaClan_tolerancija_false()
        {
            TockaProjekcija T1 = new TockaProjekcija(1, 1);
            TockaProjekcija T2 = new TockaProjekcija(2, 2);
            DMS azimut = new DMS(45, 0, 1.1);
            Seconds tolerancija = new Seconds(1);

            AzimutSlobodanClan clan = new AzimutSlobodanClan(T1, T2, azimut, tolerancija);

            Assert.IsFalse((bool)clan.TolerancijaZadovoljena);
        }
    }
}
