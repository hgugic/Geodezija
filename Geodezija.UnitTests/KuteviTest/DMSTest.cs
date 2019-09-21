using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Kutevi;

namespace Geodezija.UnitTests.KuteviTest
{
    [TestClass]
    public class DMSTest
    {
        readonly double tolerance = Math.Pow(10, -10);

        #region Constructors

        [TestMethod]
        public void DMS_Constructor_Radians_ReturnsTrue()
        {
            DMS kut = new DMS(45, 0, 0);
            DMS kutTest = new DMS(new Radians(Math.PI / 4));

            Assert.IsTrue((kut - kutTest).Degrees == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void DMS_Constructor_Hours_ReturnsTrue()
        {
            DMS kut = new DMS(45, 0, 0);
            DMS kutTest = new DMS(new Hours(3));

            Assert.IsTrue((kut - kutTest).Degrees == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void DMS_Constructor_HMS_ReturnsTrue()
        {
            DMS kut = new DMS(45, 0, 0);
            DMS kutTest = new DMS(new HMS(3, 0, 0));

            Assert.IsTrue((kut - kutTest).Degrees == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void DMS_Constructor_Degrees_ReturnsTrue()
        {
            DMS kut = new DMS(45, 0, 0);
            DMS kutTest = new DMS(new Degrees(45));

            Assert.IsTrue((kut - kutTest).Degrees == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void DMS_Constructor_DMS_ReturnsTrue()
        {
            DMS kut = new DMS(45, 0, 0);
            DMS kutTest = new DMS(new DMS(45, 0, 0));

            Assert.IsTrue((kut - kutTest).Degrees == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void DMS_Constructor_Seconds_ReturnsTrue()
        {
            DMS kut = new DMS(45, 0, 0);
            DMS kutTest = new DMS(new Seconds(45 * 60 * 60));

            Assert.IsTrue((kut - kutTest).Degrees == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        [TestMethod]
        public void DMS_Constructor_Gradians_ReturnsTrue()
        {
            DMS kut = new DMS(45, 0, 0);
            DMS kutTest = new DMS(new Gradians(50));

            Assert.IsTrue((kut - kutTest).Degrees == 0);
            Assert.IsTrue((kut - kutTest).Minutes == 0);
            Assert.IsTrue((kut - kutTest).Seconds < tolerance);
        }

        #endregion Constructors

        #region Parse - string

        [TestMethod]
        public void DMS_Parse_ToString_ReturnsTrue()
        {
            DMS dms = new DMS(234, 21, 42.15);

            Assert.IsTrue(dms == DMS.Parse(dms.ToString()));
        }

        [TestMethod]
        public void DMS_Parse_string_ReturnsTrue()
        {
            try
            {
                DMS dms = DMS.Parse("12 66 11");
                Assert.Fail("no exception thrown");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DMS_Parse_string2_ReturnsTrue()
        {
            try
            {
                DMS dms = DMS.Parse("1u2 66 11");
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
        public void DMS_Implicit_Radians_ReturnsTrue()
        {
            DMS dms = new DMS(180, 0, 0);
            Radians rad = dms;

            Assert.IsTrue(rad.Angle == Math.PI);
        }

        [TestMethod]
        public void DMS_Implicit_Degrees_ReturnsTrue()
        {
            DMS dms = new DMS(90, 30, 0);
            Degrees deg = dms;

            Assert.IsTrue(Math.Abs(deg.Angle - 90.5) < tolerance, "Kut u stupnjevima: "+ deg.Angle.ToString());
        }

        [TestMethod]
        public void DMS_Implicit_Seconds_ReturnsTrue()
        {
            DMS dms = new DMS(0, 0, 31.1);
            Seconds s = dms;

            Assert.IsTrue(Math.Abs(s.Angle - 31.1) < tolerance, "Kut u stupnjevima: " + s.Angle.ToString());
        }

        #endregion Implicit

        #region Comparison operators

        //testiranje nejednakosti
        [TestMethod]
        public void DMS_Veci_Manji_VeciJednako_ManjiJednako_test_ReturnsTrue()
        {
            DMS a = new DMS(55, 34, 5.5);
            DMS b = new DMS(44, 33, 3);
            DMS c = new DMS(44, 33, 3);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void DMS_Veci_Manji_VeciJednako_ManjiJednako2_test_ReturnsTrue()
        {
            DMS a = new DMS(-55, 34, 5.5);
            DMS b = new DMS(44, -33, 3);
            DMS c = new DMS(44, 33, -3);


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
        public void DMS_Arithmetic_oduzimanje_ReturnsTrue()
        {
            DMS a = new DMS(55, 34, 5.5);
            DMS b = new DMS(44, 33, 3);
            DMS rjesenje = new DMS(11, 1, 2.5);

            DMS razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Degrees == 0 && razlikaOduzimanja.Minutes == 0, razlikaOduzimanja.ToString());
            Assert.IsTrue(Math.Abs(razlikaOduzimanja.Seconds)<tolerance, "Razlika: "+ razlikaOduzimanja.ToString());
        }

        [TestMethod]
        public void DMS_Arithmetic_oduzimanje2_ReturnsTrue()
        {
            DMS a = new DMS(33, 34, 5.5);
            DMS b = new DMS(44, 33, 3);
            DMS rjesenje = new DMS(-10, 58, 57.5);

            DMS razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Degrees == 0 && razlikaOduzimanja.Minutes == 0, razlikaOduzimanja.ToString());
            Assert.IsTrue(Math.Abs(razlikaOduzimanja.Seconds) < tolerance, "Razlika: " + razlikaOduzimanja.ToString());
        }

        [TestMethod]
        public void DMS_Arithmetic_zbrajanje_ReturnsTrue()
        {
            DMS a = new DMS(55, 34, 5.5);
            DMS b = new DMS(44, 33, 3);
            DMS rjesenje = new DMS(100, 7, 8.5);

            DMS razlikaZbrajanja = a + b - rjesenje;

            Assert.IsTrue(razlikaZbrajanja.Degrees == 0 && razlikaZbrajanja.Minutes == 0, razlikaZbrajanja.ToString());
            Assert.IsTrue(Math.Abs(razlikaZbrajanja.Seconds) < tolerance, "Razlika: " + razlikaZbrajanja.ToString());

        }

        [TestMethod]
        public void DMS_Arithmetic_promjenaPredznaka_ReturnsTrue()
        {
            DMS a = new DMS(55, 34, 5.5);

            DMS b = -a;

            Assert.IsTrue(b.Sign < 0, "Predznak: " + b.Sign);
        }

            #endregion Arithmetic operators


    }
}
