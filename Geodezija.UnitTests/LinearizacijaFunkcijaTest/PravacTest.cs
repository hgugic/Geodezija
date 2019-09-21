using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Tocke;
using Geodezija.LinearizacijaFunkcija;

namespace Geodezija.UnitTests.LinearizacijaFunkcijaTest
{
    [TestClass]
    public class PravacTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        [TestMethod]
        public void Pravac_Linearizacija_true()
        {
            TockaProjekcija stajaliste = new TockaProjekcija(1234110, 4321220);
            TockaProjekcija vizura = new TockaProjekcija(1000000, 1000000);

            Pravac az = new Pravac(stajaliste, vizura);

            //Vrijednosti razvoja funkcije mjerenja azimuta
            double linearizacijaPoXstajalista = -0.00435609421357366;
            double linearizacijaPoYstajalista = 0.0617980745120036;
            double linearizacijaPoXvizure = -linearizacijaPoXstajalista;
            double linearizacijaPoYvizure = -linearizacijaPoYstajalista;

            //Razlika vrijednosti razvoja funkcije mjerenja azimuta i izracunatih
            double razlikaPoXstajalista = linearizacijaPoXstajalista - az.xStajaliste.ToSeconds().Angle;
            double razlikaPoYstajalista = linearizacijaPoYstajalista - az.yStajaliste.ToSeconds().Angle;
            double razlikaPoXvizure = linearizacijaPoXvizure - az.xVizura.ToSeconds().Angle;
            double razlikaPoYvizure = linearizacijaPoYvizure - az.yVizura.ToSeconds().Angle;

            Assert.IsTrue(Math.Abs(razlikaPoXstajalista) < tolerance, "Razlika vrijednosti x stajaliste: " + razlikaPoYstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoYstajalista) < tolerance, "Razlika vrijednosti y stajaliste: " + razlikaPoYstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoXvizure) < tolerance, "Razlika vrijednosti x vizure: " + razlikaPoXvizure);
            Assert.IsTrue(Math.Abs(razlikaPoYvizure) < tolerance, "Razlika vrijednosti y vizure: " + razlikaPoYvizure);
        }
    }
}
