using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.LinearizacijaFunkcija;
using Geodezija.Tocke;

namespace Geodezija.UnitTests.LinearizacijaFunkcijaTest
{
    [TestClass]
    public class AzimutTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        [TestMethod]
        public void Azimut_Linearizacija_true()
        {
            TockaProjekcija stajaliste = new TockaProjekcija(1234110, 4321220);
            TockaProjekcija vizura = new TockaProjekcija(1000000, 1000000);

            Azimut az = new Azimut(stajaliste, vizura);

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

            Assert.IsTrue(Math.Abs(razlikaPoXstajalista) < tolerance, "Razlika vrijednosti x stajaliste: " + az.xStajaliste.ToSeconds().Angle + " " + linearizacijaPoXstajalista + " " + az.xStajaliste.ToSeconds());
            Assert.IsTrue(Math.Abs(razlikaPoYstajalista) < tolerance, "Razlika vrijednosti y stajaliste: " + razlikaPoYstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoXvizure) < tolerance, "Razlika vrijednosti x vizure: " + razlikaPoXvizure);
            Assert.IsTrue(Math.Abs(razlikaPoYvizure) < tolerance, "Razlika vrijednosti y vizure: " + razlikaPoYvizure);
        }

        [TestMethod]
        public void Azimut_Linearizacija_promjenaJediniceDuzine_true()
        {
            TockaProjekcija stajaliste = new TockaProjekcija(12341.10, 43212.20, JedinicaDuzine.metar);
            TockaProjekcija vizura = new TockaProjekcija(10000.00, 10000.00, JedinicaDuzine.metar);

            stajaliste.PromjenaJediniceDuzine(JedinicaDuzine.centimetar);
            vizura.PromjenaJediniceDuzine(JedinicaDuzine.centimetar);

            Azimut az = new Azimut(stajaliste, vizura);

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

            Assert.IsTrue(Math.Abs(razlikaPoXstajalista) < tolerance, "Razlika vrijednosti x stajaliste: " + az.xStajaliste.ToSeconds().Angle + " " + linearizacijaPoXstajalista + " " + az.xStajaliste.ToSeconds());
            Assert.IsTrue(Math.Abs(razlikaPoYstajalista) < tolerance, "Razlika vrijednosti y stajaliste: " + razlikaPoYstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoXvizure) < tolerance, "Razlika vrijednosti x vizure: " + razlikaPoXvizure);
            Assert.IsTrue(Math.Abs(razlikaPoYvizure) < tolerance, "Razlika vrijednosti y vizure: " + razlikaPoYvizure);
        }
    }
}
