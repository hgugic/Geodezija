using System;
using Geodezija.Kutevi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geodezija.UnitTests.KuteviTest
{
    [TestClass]
    public class RadiansTests
    {   
        readonly double tolerance = Math.Pow(10, -14);

        #region Constructors

        [TestMethod]
        public void Radians_Constructor_Radians_ReturnsTrue()
        {
            Radians kut = new Radians(Math.PI / 4);
            Radians kutTest = new Radians(new Radians(Math.PI / 4));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Radians_Constructor_Hours_ReturnsTrue()
        {
            Radians kut = new Radians(Math.PI / 4);
            Radians kutTest = new Radians(new Hours(3));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Radians_Constructor_HMS_ReturnsTrue()
        {
            Radians kut = new Radians(Math.PI / 4);
            Radians kutTest = new Radians(new HMS(3, 0, 0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Radians_Constructor_Degrees_ReturnsTrue()
        {
            Radians kut = new Radians(Math.PI / 4);
            Radians kutTest = new Radians(new Degrees(45));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Radians_Constructor_DMS_ReturnsTrue()
        {
            Radians kut = new Radians(Math.PI / 4);
            Radians kutTest = new Radians(new HMS(45, 0, 0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Radians_Constructor_Seconds_ReturnsTrue()
        {
            Radians kut = new Radians(Math.PI / 4);
            Radians kutTest = new Radians(new Seconds(45 * 60 * 60));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Radians_Constructor_Gradians_ReturnsTrue()
        {
            Radians kut = new Radians(Math.PI / 4);
            Radians kutTest = new Radians(new Gradians(50));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        #endregion Constructors

        #region Convert To...

        [TestMethod]
        public void ToDegrees_180_ReturnsTrue()
        {
            Radians kutRadijani = new Radians(Math.PI);

            Degrees kutZaProvjeru = new Degrees(180);

            Assert.IsTrue(kutRadijani.ToDegrees().Angle == kutZaProvjeru.Angle);                       
        }

        [TestMethod]
        public void ToDMS_180d0m0s_ReturnsTrue()
        {
            Radians kutRadijani = new Radians(Math.PI);

            DMS kutZaProvjeru = new DMS(180, 0, 0);

            Assert.IsTrue(kutRadijani.ToDMS().Degrees == kutZaProvjeru.Degrees);
            Assert.IsTrue(kutRadijani.ToDMS().Minutes == kutZaProvjeru.Minutes);
            Assert.IsTrue(kutRadijani.ToDMS().Seconds == kutZaProvjeru.Seconds);
        }

        [TestMethod]
        public void ToHours_12_ReturnsTrue()
        {
            Radians kutRadijani = new Radians(Math.PI);

            Degrees kutZaProvjeru = new Degrees(180);

            Assert.IsTrue(kutRadijani.ToDegrees().Angle == kutZaProvjeru.Angle);
        }

        [TestMethod]
        public void ToHMS_12h0m0s_ReturnsTrue()
        {
            Radians kutRadijani = new Radians(Math.PI);

            HMS kutZaProvjeru = new HMS(12, 0, 0);

            Assert.IsTrue(kutRadijani.ToHMS().Hours == kutZaProvjeru.Hours);
            Assert.IsTrue(kutRadijani.ToHMS().Minutes == kutZaProvjeru.Minutes);
            Assert.IsTrue(kutRadijani.ToHMS().Seconds == kutZaProvjeru.Seconds);
        }

        [TestMethod]
        public void ToSeconds_3600_ReturnsTrue()
        {
            Radians kutRadijani = new Radians(Math.PI / 180);

            Seconds kutZaProvjeru = new Seconds(60 * 60);

            Assert.IsTrue(kutRadijani.ToSeconds().Angle == kutZaProvjeru.Angle);
        }

        [TestMethod]
        public void ToGradians_200_ReturnsTrue()
        {
            Radians kutRadijani = new Radians(Math.PI);

            Gradians kutZaProvjeru = new Gradians(200);

            Assert.IsTrue(kutRadijani.ToGradians().Angle == kutZaProvjeru.Angle);
        }

        #endregion Convert To...

        #region Mjere kuta

        [TestMethod]
        public void GlavnaMjeraKuta_PI_ReturnsTrue()
        {
            Radians kutRadijani = new Radians(3 * Math.PI);

            double difference = Math.Abs(kutRadijani.GlavnaMjeraKuta() - Math.PI);

            Assert.IsTrue(difference < tolerance);
        }

        [TestMethod]
        public void GlavnaMjeraKutaPozitivanSmjer_PI_ReturnsTrue()
        {           
            Radians kutRadijani = new Radians(-3 * Math.PI);

            double difference = Math.Abs(kutRadijani.GlavnaMjeraKutaPozitivanSmjer() - Math.PI);

            Assert.IsTrue(difference < tolerance);         
        }

        #endregion Mjere kuta

        #region Comparison operators

        //testiranje nejednakosti
        [TestMethod]
        public void Veci_Manji_VeciJednako_ManjiJednako_test_ReturnsTrue()
        {
            Radians a = new Radians(Math.PI);
            Radians b = new Radians(Math.PI / 2);
            Radians c = new Radians(Math.PI / 2);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void Veci_Manji_VeciJednako_ManjiJednako2_test_ReturnsTrue()
        {
            Radians a = new Radians(-Math.PI);
            Radians b = new Radians(-Math.PI / 2);
            Radians c = new Radians(-Math.PI / 2);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void Veci_Manji_VeciJednako_ManjiJednako_Double_test_ReturnsTrue()
        {
            double a = Math.PI;
            Radians b = new Radians(Math.PI / 2);
            Radians c = new Radians(Math.PI / 2);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void Veci_Manji_VeciJednako_ManjiJednako2_Double_test_ReturnsTrue()
        {
            double a = -Math.PI;
            Radians b = new Radians(-Math.PI / 2);
            Radians c = new Radians(-Math.PI / 2);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void Veci_Manji_VeciJednako_ManjiJednako_Double_test2_ReturnsTrue()
        {
            Radians a = new Radians(Math.PI);
            double b = Math.PI / 2;
            Radians c = new Radians(Math.PI / 2);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void Veci_Manji_VeciJednako_ManjiJednako2_Double_test2_ReturnsTrue()
        {
            Radians a = new Radians(-Math.PI);
            double b = -Math.PI / 2;
            Radians c = new Radians(-Math.PI / 2);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        #endregion Comparison operators

        #region Arithmetic operators

        [TestMethod]
        public void Radians_Arithmetic_oduzimanje_ReturnsTrue()
        {
            Radians a = new Radians(2.5);
            Radians b = new Radians(1.6);
            Radians rjesenje = new Radians(0.9);

            Radians razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());
        }

        [TestMethod]
        public void Radians_Arithmetic_oduzimanje2_ReturnsTrue()
        {
            Radians a = new Radians(1.3);
            Radians b = new Radians(2.1);
            Radians rjesenje = new Radians(-0.8);

            Radians razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());

        }

        [TestMethod]
        public void Radians_Arithmetic_zbrajanje_ReturnsTrue()
        {
            Radians a = new Radians(2.1);
            Radians b = new Radians(1.1);
            Radians rjesenje = new Radians(3.2);

            Radians razlikaZbrajanja = a + b - rjesenje;

            Assert.IsTrue(razlikaZbrajanja.Angle == 0, razlikaZbrajanja.ToString());
        }

        [TestMethod]
        public void Radians_Arithmetic_promjenaPredznaka_ReturnsTrue()
        {
            Radians a = new Radians(1);

            Radians b = -a;

            Assert.IsTrue(b.Angle < 0, "Predznak: " + b);
        }
        #endregion Arithmetic operators


    }
}
