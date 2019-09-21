using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Tocke;
using Geodezija.LinearizacijaFunkcija;

namespace Geodezija.UnitTests.LinearizacijaFunkcijaTest
{
    [TestClass]
    public class DuzinaTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        [TestMethod]
        public void Duzina_Linearizacija_true()
        {
            TockaProjekcija stajaliste = new TockaProjekcija(1234110, 4321220);
            TockaProjekcija vizura = new TockaProjekcija(1000000, 1000000);

            Duzina az = new Duzina(stajaliste, vizura);

            //Vrijednosti razvoja funkcije mjerenja azimuta
            double linearizacijaPoXstajalista = 0.997524859234353;
            double linearizacijaPoYstajalista = 0.0703146870112047;
            double linearizacijaPoXvizure = -linearizacijaPoXstajalista;
            double linearizacijaPoYvizure = -linearizacijaPoYstajalista;

            //Razlika vrijednosti razvoja funkcije mjerenja duzina i izracunatih
            double razlikaPoXstajalista = linearizacijaPoXstajalista - az.xStajaliste;
            double razlikaPoYstajalista = linearizacijaPoYstajalista - az.yStajaliste;
            double razlikaPoXvizure = linearizacijaPoXvizure - az.xVizura;
            double razlikaPoYvizure = linearizacijaPoYvizure - az.yVizura;

            Assert.IsTrue(Math.Abs(razlikaPoXstajalista) < tolerance, "Razlika vrijednosti x stajaliste: " + razlikaPoXstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoYstajalista) < tolerance, "Razlika vrijednosti x stajaliste: " + razlikaPoYstajalista);
            Assert.IsTrue(Math.Abs(razlikaPoXvizure) < tolerance, "Razlika vrijednosti x stajaliste: " + razlikaPoXvizure);
            Assert.IsTrue(Math.Abs(razlikaPoYvizure) < tolerance, "Razlika vrijednosti x stajaliste: " + razlikaPoYvizure);
        }
    }
}
