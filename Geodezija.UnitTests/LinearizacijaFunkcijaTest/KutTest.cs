using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Tocke;
using Geodezija.LinearizacijaFunkcija;

namespace Geodezija.UnitTests.LinearizacijaFunkcijaTest
{
    [TestClass]
    public class KutTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        [TestMethod]
        public void Kut_Linearizacija_true()
        {
            TockaProjekcija stajaliste = new TockaProjekcija(1234110, 4321220);
            TockaProjekcija vizura = new TockaProjekcija(1000000, 1000000);
            TockaProjekcija vizura2 = new TockaProjekcija(5234110, 3321220);

            Kut az = new Kut(stajaliste, vizura, vizura2);

            //Vrijednosti razvoja funkcije mjerenja azimuta
            double linearizacijaPoXstajalista = 0.0528889898011257;
            double linearizacijaPoYstajalista = -0.0496648506151156;
            double linearizacijaPoXvizure = -0.00435609421357366;
            double linearizacijaPoYvizure = 0.0617980745120036;
            double linearizacijaPoXvizure2 = -0.0485328955875521;
            double linearizacijaPoYvizure2 = -0.012133223896888;

            //Razlika vrijednosti razvoja funkcije mjerenja azimuta i izracunatih
            double razlikaPoXstajalista = linearizacijaPoXstajalista - az.xStajaliste.ToSeconds().Angle;
            double razlikaPoYstajalista = linearizacijaPoYstajalista - az.yStajaliste.ToSeconds().Angle;
            double razlikaPoXvizure = linearizacijaPoXvizure - az.xOd.ToSeconds().Angle;
            double razlikaPoYvizure = linearizacijaPoYvizure - az.yOd.ToSeconds().Angle;
            double razlikaPoXvizure2 = linearizacijaPoXvizure2 - az.xDo.ToSeconds().Angle;
            double razlikaPoYvizure2 = linearizacijaPoYvizure2 - az.yDo.ToSeconds().Angle;

            Assert.IsTrue(Math.Abs(razlikaPoXstajalista) < tolerance, "Razlika vrijednosti x stajaliste: " + razlikaPoYstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoYstajalista) < tolerance, "Razlika vrijednosti y stajaliste: " + razlikaPoYstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoXvizure) < tolerance, "Razlika vrijednosti x vizure: " + razlikaPoXvizure);
            Assert.IsTrue(Math.Abs(razlikaPoYvizure) < tolerance, "Razlika vrijednosti y vizure: " + razlikaPoYvizure);
            Assert.IsTrue(Math.Abs(razlikaPoXvizure2) < tolerance, "Razlika vrijednosti x vizure: " + razlikaPoXvizure2);
            Assert.IsTrue(Math.Abs(razlikaPoYvizure2) < tolerance, "Razlika vrijednosti y vizure: " + razlikaPoYvizure2);
        }
    }
}
