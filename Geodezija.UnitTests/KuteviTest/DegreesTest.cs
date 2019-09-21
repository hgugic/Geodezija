using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Kutevi;
using System.Globalization;
using System.Threading;

namespace Geodezija.UnitTests.KuteviTest
{
    [TestClass]
    public class DegreesTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        #region Constructors

        [TestMethod]
        public void Degrees_Constructor_Radians_ReturnsTrue()
        {
            Degrees kut = new Degrees(45);
            Degrees kutTest = new Degrees(new Radians(Math.PI / 4));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Degrees_Constructor_Hours_ReturnsTrue()
        {
            Degrees kut = new Degrees(45);
            Degrees kutTest = new Degrees(new Hours(3));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Degrees_Constructor_HMS_ReturnsTrue()
        {
            Degrees kut = new Degrees(45);
            Degrees kutTest = new Degrees(new HMS(3,0,0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Degrees_Constructor_Degrees_ReturnsTrue()
        {
            Degrees kut = new Degrees(45);
            Degrees kutTest = new Degrees(new Degrees(45));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Degrees_Constructor_DMS_ReturnsTrue()
        {
            Degrees kut = new Degrees(45);
            Degrees kutTest = new Degrees(new HMS(45, 0, 0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Degrees_Constructor_Seconds_ReturnsTrue()
        {
            Degrees kut = new Degrees(1);
            Degrees kutTest = new Degrees(new Seconds(1 * 180 * 60 * 60 / Math.PI));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Degrees_Constructor_Gradians_ReturnsTrue()
        {
            Degrees kut = new Degrees(45);
            Degrees kutTest = new Degrees(new Gradians(50));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        #endregion Constructors

        #region Parse - string

        [TestMethod]
        public void Degrees_Parse_ToString_ReturnsTrue()
        {
            Degrees deg = new Degrees(55.55);

            Assert.IsTrue(deg == Degrees.Parse(deg.ToString()));
        }

        [TestMethod]
        public void Degrees_Parse_string_ReturnsTrue()
        {
            try
            {
                Degrees deg = Degrees.Parse("12.345i");
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
        public void Degrees_Parse_string2_ReturnsTrue()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Degrees deg = new Degrees(55.55);

            Degrees degTest1 = Degrees.Parse("55.55°");
            Assert.IsTrue(deg == degTest1, "Parse string 55.55° " + degTest1);

            Degrees degTest2 = Degrees.Parse("55.55d");
            Assert.IsTrue(deg == degTest2, "Parse string 55.55d " + degTest2);

            Degrees degTest3 = Degrees.Parse("55.55D");
            Assert.IsTrue(deg == degTest3, "Parse string 55.55D " + degTest3);
        }

        #endregion Parse - string

        #region Implicit

        [TestMethod]
        public void Degrees_Implicit_Radians_ReturnsTrue()
        {
            Degrees dms = new Degrees(180 / 5);
            Radians rad = dms;

            Assert.IsTrue(rad.Angle == Math.PI / 5);
        }

        [TestMethod]
        public void Degrees_Implicit_DMS_ReturnsTrue()
        {
            DMS dms = new DMS(90, 30, 30);
            Degrees deg = dms;
            
            double razlika = 90.5083333333333333 - deg.Angle;
            Assert.IsTrue(Math.Abs(razlika) < tolerance, "Kut u stupnjevima: " + dms + " " + deg);
        }

        [TestMethod]
        public void DMS_Implicit_Seconds_ReturnsTrue()
        {
            DMS dms = new DMS(0, 0, 31.1);
            Seconds s = dms;

            Assert.IsTrue(Math.Abs(s.Angle - 31.1) < tolerance, "Kut u stupnjevima: " + s.Angle.ToString());
        }

        #endregion Implicit

        //testiranje nejednakosti
        [TestMethod]
        public void Degrees_Veci_Manji_VeciJednako_ManjiJednako_test_ReturnsTrue()
        {
            Degrees a = new Degrees(180);
            Degrees b = new Degrees(90);
            Degrees c = new Degrees(90);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        #region Arithmetic operators

        [TestMethod]
        public void Degrees_Arithmetic_oduzimanje_ReturnsTrue()
        {
            Degrees a = new Degrees(2.5);
            Degrees b = new Degrees(1.6);
            Degrees rjesenje = new Degrees(0.9);

            Degrees razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());
        }

        [TestMethod]
        public void Degrees_Arithmetic_oduzimanje2_ReturnsTrue()
        {
            Degrees a = new Degrees(1.3);
            Degrees b = new Degrees(2.1);
            Degrees rjesenje = new Degrees(-0.8);

            Degrees razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());

        }

        [TestMethod]
        public void Degrees_Arithmetic_zbrajanje_ReturnsTrue()
        {
            Degrees a = new Degrees(2.1);
            Degrees b = new Degrees(1.1);
            Degrees rjesenje = new Degrees(3.2);

            Degrees razlikaZbrajanja = a + b - rjesenje;

            Assert.IsTrue(razlikaZbrajanja.Angle < tolerance, razlikaZbrajanja.ToString());
        }

        [TestMethod]
        public void Degrees_Arithmetic_promjenaPredznaka_ReturnsTrue()
        {
            Degrees a = new Degrees(1);

            Degrees b = -a;

            Assert.IsTrue(b.Angle < 0, "Predznak: " + b);
        }
        #endregion Arithmetic operators
    }
}
