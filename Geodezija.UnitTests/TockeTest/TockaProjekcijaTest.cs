using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Tocke;
using Geodezija.Kutevi;

namespace Geodezija.UnitTests.TockeTest
{
    [TestClass]
    public class TockaProjekcijaTest
    {

        [TestMethod]
        public void TockaProjekcija_PromjenaJediniceDuzine_true()
        {
            TockaProjekcija T = new TockaProjekcija(1, 1);

            T.PromjenaJediniceDuzine(JedinicaDuzine.decimetar);

            Assert.IsTrue(T.Y == 10 && T.X == 10, "metar => decimetar: " + T.Y);

            T.PromjenaJediniceDuzine(JedinicaDuzine.centimetar);

            Assert.IsTrue(T.Y == 100 && T.X == 100, "decimetar => centimetar: " + T.Y);

            T.PromjenaJediniceDuzine(JedinicaDuzine.milimetar);

            Assert.IsTrue(T.Y == 1000 && T.X == 1000, "centimetar => milimetar: " + T.Y);

            T.PromjenaJediniceDuzine(JedinicaDuzine.metar);

            Assert.IsTrue(T.Y == 1 && T.X == 1, "milimetar => metar: " + T.Y);
        }

        [TestMethod]
        public void TockaProjekcija_SmjerniKut_kvadranti_true()
        {

            TockaProjekcija stajaliste = new TockaProjekcija(5, 5);

            TockaProjekcija vizura1kvadrant = new TockaProjekcija(6, 6);
            TockaProjekcija vizura2kvadrant = new TockaProjekcija(6, 4);
            TockaProjekcija vizura3kvadrant = new TockaProjekcija(4, 4);
            TockaProjekcija vizura4kvadrant = new TockaProjekcija(4, 6);

            //1. kvadrant
            Degrees Kv1 = stajaliste.SmjerniKut(vizura1kvadrant).ToDegrees();
            Assert.IsTrue(45 == Kv1.Angle, Kv1.ToString());

            //2. kvadrant
            Degrees Kv2 = stajaliste.SmjerniKut(vizura2kvadrant).ToDegrees();
            Assert.IsTrue(135 == Kv2.Angle, Kv2.ToString());

            //3. kvadrant
            Degrees Kv3 = stajaliste.SmjerniKut(vizura3kvadrant).ToDegrees();
            Assert.IsTrue(225 == Kv3.Angle, Kv3.ToString());

            //4. kvadrant
            Degrees Kv4 = stajaliste.SmjerniKut(vizura4kvadrant).ToDegrees();
            Assert.IsTrue(315 == Kv4.Angle, Kv4.ToString());        
        }

        [TestMethod]
        public void TockaProjekcija_SmjerniKut_koordinatene_osi_true()
        {

            TockaProjekcija stajaliste = new TockaProjekcija(5, 5);

            TockaProjekcija vizuraYosPozitivna = new TockaProjekcija(6, 5);
            TockaProjekcija vizuraYosNegativna = new TockaProjekcija(4, 5);
            TockaProjekcija vizuraXosPozitivna = new TockaProjekcija(5, 6);
            TockaProjekcija vizuraXosNegativna = new TockaProjekcija(5, 4);

            //Y pozitivna
            Degrees Kv1 = stajaliste.SmjerniKut(vizuraYosPozitivna).ToDegrees();
            Assert.IsTrue(90 == Kv1.Angle, Kv1.ToString());

            //Y negativna
            Degrees Kv2 = stajaliste.SmjerniKut(vizuraYosNegativna).ToDegrees();
            Assert.IsTrue(270 == Kv2.Angle, Kv2.ToString());

            //X pozitivna
            Degrees Kv3 = stajaliste.SmjerniKut(vizuraXosPozitivna).ToDegrees();
            Assert.IsTrue(0 == Kv3.Angle, Kv3.ToString());

            //X negativna
            Degrees Kv4 = stajaliste.SmjerniKut(vizuraXosNegativna).ToDegrees();
            Assert.IsTrue(180 == Kv4.Angle, Kv4.ToString());
        }
    }
}
