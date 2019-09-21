using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Kutevi;

namespace Geodezija.UnitTests.KuteviTest
{
    [TestClass]
    public class HMSTest
    {
        readonly double tolerance = Math.Pow(10, -10);

        #region Constructors

        [TestMethod]
        public void HMS_Constructor_Radians_ReturnsTrue()
        {
            HMS kut = new HMS(3, 0, 0);
            HMS kutTest = new HMS(new Radians(Math.PI / 4));

            Assert.IsTrue((kut - kutTest).Hours == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void HMS_Constructor_Hours_ReturnsTrue()
        {
            HMS kut = new HMS(3, 0, 0);
            HMS kutTest = new HMS(new Hours(3));

            Assert.IsTrue((kut - kutTest).Hours == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void HMS_Constructor_HMS_ReturnsTrue()
        {
            HMS kut = new HMS(3, 0, 0);
            HMS kutTest = new HMS(new HMS(3, 0, 0));

            Assert.IsTrue((kut - kutTest).Hours == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void HMS_Constructor_Degrees_ReturnsTrue()
        {
            HMS kut = new HMS(3, 0, 0);
            HMS kutTest = new HMS(new Degrees(45));

            Assert.IsTrue((kut - kutTest).Hours == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void HMS_Constructor_DMS_ReturnsTrue()
        {
            HMS kut = new HMS(3, 0, 0);
            HMS kutTest = new HMS(new DMS(45, 0, 0));

            Assert.IsTrue((kut - kutTest).Hours == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void HMS_Constructor_Seconds_ReturnsTrue()
        {
            HMS kut = new HMS(3, 0, 0);
            HMS kutTest = new HMS(new Seconds(45 * 60 * 60));

            Assert.IsTrue((kut - kutTest).Hours == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void HMS_Constructor_Gradians_ReturnsTrue()
        {
            HMS kut = new HMS(3, 0, 0);
            HMS kutTest = new HMS(new Gradians(50));

            Assert.IsTrue((kut - kutTest).Hours == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        #endregion Constructors

        #region Parse - string

        [TestMethod]
        public void HMS_Parse_ToString_true()
        {
            HMS h = new HMS(2, 56, 45.4);

            Assert.IsTrue(h == HMS.Parse(h.ToString()));
        }

        [TestMethod]
        public void DMS_Parse_string_true()
        {
            try
            {
                DMS dms = DMS.Parse("1x2 58 0");
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

        #endregion Parse - string

        #region Implicit

        [TestMethod]
        public void HMS_Implicit_Radians_ReturnsTrue()
        {
            HMS hms = new HMS(12, 0, 0);
            Radians rad = hms;

            Assert.IsTrue(rad.Angle == Math.PI, rad.Angle.ToString());
        }

        [TestMethod]
        public void HMS_Implicit_Hours_ReturnsTrue()
        {
            HMS hms = new HMS(6, 30, 0);
            Hours hrs = hms;

            Assert.IsTrue(Math.Abs(hrs.Angle - 6.5) < tolerance, "Kut u satima: " + hrs.Angle.ToString());
        }


        #endregion Implicit

        #region Comparison operators

        //testiranje nejednakosti
        [TestMethod]
        public void HMS_Veci_Manji_VeciJednako_ManjiJednako_test_ReturnsTrue()
        {
            HMS a = new HMS(1, 34, 5.5);
            HMS b = new HMS(1, 33, 3);
            HMS c = new HMS(1, 33, 3);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void HMS_Veci_Manji_VeciJednako_ManjiJednako2_test_ReturnsTrue()
        {
            HMS a = new HMS(-1, 34, 5.5);
            HMS b = new HMS(1, -33, 3);
            HMS c = new HMS(1, 33, -3);


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
        public void HMS_Arithmetic_oduzimanje_ReturnsTrue()
        {
            DMS a = new DMS(5, 34, 5.5);
            DMS b = new DMS(4, 33, 3);
            DMS rjesenje = new DMS(1, 1, 2.5);

            DMS razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Degrees == 0 && razlikaOduzimanja.Minutes == 0, razlikaOduzimanja.ToString());
            Assert.IsTrue(Math.Abs(razlikaOduzimanja.Seconds) < tolerance, "Razlika: " + razlikaOduzimanja.ToString());

        }

        [TestMethod]
        public void HMS_Arithmetic_oduzimanje2_ReturnsTrue()
        {
            DMS a = new DMS(1, 34, 5.5);
            DMS b = new DMS(7, 33, 3);
            DMS rjesenje = new DMS(-5, 58, 57.5);

            DMS razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Degrees == 0 && razlikaOduzimanja.Minutes == 0, razlikaOduzimanja.ToString());
            Assert.IsTrue(Math.Abs(razlikaOduzimanja.Seconds) < tolerance, "Razlika: " + razlikaOduzimanja.ToString());

        }

        [TestMethod]
        public void HMS_Arithmetic_zbrajanje_ReturnsTrue()
        {
            DMS a = new DMS(1, 34, 5.5);
            DMS b = new DMS(7, 33, 3);
            DMS rjesenje = new DMS(9, 7, 8.5);

            DMS razlikaZbrajanja = a + b - rjesenje;

            Assert.IsTrue(razlikaZbrajanja.Degrees == 0 && razlikaZbrajanja.Minutes == 0, razlikaZbrajanja.ToString());
            Assert.IsTrue(Math.Abs(razlikaZbrajanja.Seconds) < tolerance, "Razlika: " + razlikaZbrajanja.ToString());
        }

        [TestMethod]
        public void HMS_Arithmetic_promjenaPredznaka_ReturnsTrue()
        {
            HMS a = new HMS(1, 34, 5.5);

            HMS b = -a;

            Assert.IsTrue(b.Sign < 0, "Predznak: " + b.Sign);
        }
        #endregion Arithmetic operators
    }
}
