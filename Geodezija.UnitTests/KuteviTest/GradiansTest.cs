using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geodezija.Kutevi;
using System.Threading;

namespace Geodezija.UnitTests.KuteviTest
{
    [TestClass]
    public class GradiansTest
    {
        #region Parse - string

        [TestMethod]
        public void Gradians_Parse_ToString_ReturnsTrue()
        {
            Gradians gon = new Gradians(55.55);

            Assert.IsTrue(gon == Gradians.Parse(gon.ToString()));
        }

        [TestMethod]
        public void Gradians_Parse_string_ReturnsTrue()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            try
            {
                Gradians gon = Gradians.Parse("12.345i");
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
        public void Gradians_Parse_string2_ReturnsTrue()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Gradians deg = new Gradians(55.55);

            Gradians degTest1 = Gradians.Parse("55.55g");
            Assert.IsTrue(deg == degTest1, "Parse string 55.55g " + degTest1);

            Gradians degTest2 = Gradians.Parse("55.55G");
            Assert.IsTrue(deg == degTest2, "Parse string 55.55G " + degTest2);
        }

        #endregion Parse - string

        //testiranje nejednakosti
        [TestMethod]
        public void Gradians_Veci_Manji_VeciJednako_ManjiJednako_test_ReturnsTrue()
        {
            Gradians a = new Gradians(180);
            Gradians b = new Gradians(90);
            Gradians c = new Gradians(90);


            Assert.IsTrue(a > b);
            Assert.IsTrue(b < a);
            Assert.IsTrue(b >= c);
            Assert.IsTrue(c <= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(c <= a);
        }
    }
}
