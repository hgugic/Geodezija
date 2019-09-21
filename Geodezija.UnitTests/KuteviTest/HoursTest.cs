using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Kutevi;
using System.Threading;

namespace Geodezija.UnitTests.KuteviTest
{
    [TestClass]
    public class HoursTest
    {
        readonly double tolerance = Math.Pow(10, -14);

        #region Constructors

        [TestMethod]
        public void Hours_Constructor_Radians_ReturnsTrue()
        {
            Hours kut = new Hours(3);
            Hours kutTest = new Hours(new Radians(Math.PI / 4));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Hours_Constructor_Hours_ReturnsTrue()
        {
            Hours kut = new Hours(3);
            Hours kutTest = new Hours(new Hours(3));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        public void Hours_Constructor_HMS_ReturnsTrue()
        {
            Hours kut = new Hours(3);
            Hours kutTest = new Hours(new HMS(3, 0, 0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        public void Hours_Constructor_Degrees_ReturnsTrue()
        {
            Hours kut = new Hours(3);
            Hours kutTest = new Hours(new Degrees(45));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        public void Hours_Constructor_DMS_ReturnsTrue()
        {
            Hours kut = new Hours(3);
            Hours kutTest = new Hours(new DMS(45, 0, 0));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        public void Degrees_Constructor_Seconds_ReturnsTrue()
        {
            Hours kut = new Hours(3);
            Hours kutTest = new Hours(new Seconds(45 * 60 * 60));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        public void Hours_Constructor_Gradians_ReturnsTrue()
        {
            Hours kut = new Hours(3);
            Hours kutTest = new Hours(new Gradians(50));

            Assert.IsTrue((kut - kutTest).Angle < tolerance);
        }

        [TestMethod]
        public void Hours_Constructors_ReturnsTrue()
        {
            Hours kut = new Hours(3);

            Hours h = new Hours(3);
            HMS hms = new HMS(3, 0, 0);

            Degrees d = new Degrees(45);
            DMS dms = new DMS(45, 0, 0);
            Seconds s = new Seconds(45 * 180 * 60 * 60 / Math.PI);

            Gradians g = new Gradians(50);


            Assert.IsTrue((kut - h).Angle < tolerance, "Hours");
            Assert.IsTrue((kut - hms).Angle < tolerance, "HMS");

            Assert.IsTrue((kut - d).Angle < tolerance, "Degrees");
            Assert.IsTrue((kut - dms).Angle < tolerance, "DMS");
            Assert.IsTrue((kut - s).Angle < tolerance, "Seconds");

            Assert.IsTrue((kut - g).Angle < tolerance, "Gradians");
        }

        #endregion Constructors

        #region Parse - string

        [TestMethod]
        public void Parse_string_true()
        {
            Hours h = new Hours(55.55);

            Assert.IsTrue(h == Hours.Parse(h.ToString()));
        }

        [TestMethod]
        public void Hours_Parse_string2_ReturnsTrue()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Hours hrs = new Hours(55.55);

            Hours hrsTest1 = Hours.Parse("55.55h");
            Assert.IsTrue(hrs == hrsTest1, "Parse string 55.55h " + hrsTest1);

            Hours hrsTest2 = Hours.Parse("55.55H");
            Assert.IsTrue(hrs == hrsTest2, "Parse string 55.55H " + hrsTest2);
        }

        #endregion Parse - string

        #region Implicit

        [TestMethod]
        public void Hours_Implicit_Radians_ReturnsTrue()
        {
            Hours dms = new Hours(12 / 3);
            Radians rad = dms;

            Assert.IsTrue(rad.Angle == Math.PI / 3);
        }

        [TestMethod]
        public void Hours_Implicit_HMS_ReturnsTrue()
        {
            HMS hms = new HMS(5, 30, 30);
            Hours deg = hms;

            double razlika = 5.5083333333333333 - deg.Angle;
            Assert.IsTrue(Math.Abs(razlika) < tolerance, "Kut u satima: " + hms + " " + deg);
        }

        #endregion Implicit

        #region Comparison operators

        //testiranje nejednakosti
        [TestMethod]
        public void Hours_Veci_Manji_VeciJednako_ManjiJednako_test_ReturnsTrue()
        {
            Hours a = new Hours(12);
            Hours b = new Hours(6);
            Hours c = new Hours(6);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }

        [TestMethod]
        public void Hours_Veci_Manji_VeciJednako_ManjiJednako2_test_ReturnsTrue()
        {
            Hours a = new Hours(-12);
            Hours b = new Hours(-6);
            Hours c = new Hours(-6);


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
        public void Hours_Arithmetic_oduzimanje_ReturnsTrue()
        {
            Hours a = new Hours(2.5);
            Hours b = new Hours(1.6);
            Hours rjesenje = new Hours(0.9);

            Hours razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());
        }

        [TestMethod]
        public void Hours_Arithmetic_oduzimanje2_ReturnsTrue()
        {
            Hours a = new Hours(1.3);
            Hours b = new Hours(2.1);
            Hours rjesenje = new Hours(-0.8);

            Hours razlikaOduzimanja = a - b - rjesenje;

            Assert.IsTrue(razlikaOduzimanja.Angle < tolerance, razlikaOduzimanja.ToString());

        }

        [TestMethod]
        public void Hours_Arithmetic_zbrajanje_ReturnsTrue()
        {
            Hours a = new Hours(2.1);
            Hours b = new Hours(1.1);
            Hours rjesenje = new Hours(3.2);

            Hours razlikaZbrajanja = a + b - rjesenje;

            Assert.IsTrue(razlikaZbrajanja.Angle < tolerance, razlikaZbrajanja.ToString());
        }

        [TestMethod]
        public void Hours_Arithmetic_promjenaPredznaka_ReturnsTrue()
        {
            Hours a = new Hours(1);

            Hours b = -a;

            Assert.IsTrue(b.Angle < 0, "Predznak: " + b);
        }
        #endregion Arithmetic operators
    }
}
