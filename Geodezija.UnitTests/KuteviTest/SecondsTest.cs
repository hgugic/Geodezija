using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Kutevi;
using System.Threading;

namespace Geodezija.UnitTests.KuteviTest
{
    [TestClass]
    public class SecondsTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        #region Constructors

        [TestMethod]
        public void Seconds_Constructor_Radians_ReturnsTrue()
        {
            Seconds kut = new Seconds(45 * 60 * 60);
            Seconds kutTest = new Seconds(new Radians(Math.PI / 4));

            Assert.IsTrue((kut - kutTest).Angle < tolerance, (kut - kutTest).ToString());
        }

        [TestMethod]
        public void Seconds_Constructor_Hours_ReturnsTrue()
        {
            Seconds kut = new Seconds(45 * 60 * 60);
            Seconds kutTest = new Seconds(new Hours(3));

            Assert.IsTrue((kut - kutTest).Angle < tolerance, (kut - kutTest).ToString());
        }

        [TestMethod]
        public void Radians_Constructor_HMS_ReturnsTrue()
        {
            Seconds kut = new Seconds(45 * 60 * 60);
            Seconds kutTest = new Seconds(new HMS(3, 0, 0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Seconds_Constructor_Degrees_ReturnsTrue()
        {
            Seconds kut = new Seconds(45 * 60 * 60);
            Seconds kutTest = new Seconds(new Degrees(45));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Seconds_Constructor_DMS_ReturnsTrue()
        {
            Seconds kut = new Seconds(45 * 60 * 60);
            Seconds kutTest = new Seconds(new HMS(45, 0, 0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Seconds_Constructor_Seconds_ReturnsTrue()
        {
            Seconds kut = new Seconds(45 * 60 * 60);
            Seconds kutTest = new Seconds(new Seconds(45 * 60 * 60));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Seconds_Constructor_Gradians_ReturnsTrue()
        {
            Seconds kut = new Seconds(45 * 60 * 60);
            Seconds kutTest = new Seconds(new Gradians(50));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        #endregion Constructors

        #region Parse - string

        [TestMethod]
        public void Seconds_Parse_ToString_ReturnsTrue()
        {
            Seconds sec = new Seconds(55.55);

            Assert.IsTrue(sec == Seconds.Parse(sec.ToString()));
        }

        [TestMethod]
        public void Seconds_Parse_string_ReturnsTrue()
        {
            try
            {
                Seconds sec = Seconds.Parse("12.345i");
                Assert.Fail("no exception thrown");
            }
            catch (FormatException ex)
            {
                Assert.IsTrue(ex is FormatException);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Seconds_Parse_string2_ReturnsTrue()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Seconds deg = new Seconds(55.55);

            Seconds secTest1 = Seconds.Parse("55.55\"");
            Assert.IsTrue(deg == secTest1, "Parse string 55.55\" " + secTest1);

            Seconds secTest2 = Seconds.Parse("55.55s");
            Assert.IsTrue(deg == secTest2, "Parse string 55.55s " + secTest2);

            Seconds secTest3 = Seconds.Parse("55.55S");
            Assert.IsTrue(deg == secTest3, "Parse string 55.55S " + secTest3);
        }

        #endregion Parse - string

        //testiranje nejednakosti
        [TestMethod]
        public void Seconds_Veci_Manji_VeciJednako_ManjiJednako_test_ReturnsTrue()
        {
            Seconds a = new Seconds(5.5);
            Seconds b = new Seconds(3);
            Seconds c = new Seconds(3);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        #region Arithmetic operators

        [TestMethod]
        public void Seconds_Arithmetic_oduzimanje_ReturnsTrue()
        {
            Seconds a = new Seconds(2.5);
            Seconds b = new Seconds(1.6);
            Seconds rjesenje = new Seconds(0.9);

            Seconds razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());
        }

        [TestMethod]
        public void Seconds_Arithmetic_oduzimanje2_ReturnsTrue()
        {
            Seconds a = new Seconds(1.3);
            Seconds b = new Seconds(2.1);
            Seconds rjesenje = new Seconds(-0.8);

            Seconds razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());

        }

        [TestMethod]
        public void Seconds_Arithmetic_zbrajanje_ReturnsTrue()
        {
            Seconds a = new Seconds(2.1);
            Seconds b = new Seconds(1.1);
            Seconds rjesenje = new Seconds(3.2);

            Seconds razlikaZbrajanja = a + b - rjesenje;

            Assert.IsTrue(razlikaZbrajanja.Angle < tolerance, razlikaZbrajanja.ToString());
        }

        [TestMethod]
        public void Seconds_Arithmetic_promjenaPredznaka_ReturnsTrue()
        {
            Seconds a = new Seconds(1);

            Seconds b = -a;

            Assert.IsTrue(b.Angle < 0, "Predznak: " + b);
        }
        #endregion Arithmetic operators
    }
}
