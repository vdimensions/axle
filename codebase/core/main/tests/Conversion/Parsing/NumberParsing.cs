using System;
using System.Globalization;
using Axle.Conversion.Parsing;
using Axle.Text.Formatting.Extensions;
using NUnit.Framework;

namespace Axle.Core.Tests.Conversion.Parsing
{
    [TestFixture]
    public class NumberParsing
    {
        private static double[] Numbers = new[]
        {
            Math.PI,
            Math.E,
            10000.0,
            -0.0001
        };
        private static string[] Formats = new[]
        {
            "{0}",
            "{0:0.##}",
            "{0:#.####}",
            // standard formats
            //"{0:D}",
            //"{0:D5}",
            "{0:C}",
            "{0:E}",
            "{0:C0}",
            "{0:E2}",
            "{0:e2}",
            "{0:F}",
            "{0:F1}",
            "{0:G}",
            "{0:g2}",
            //"{0:N}",
            //"{0:N1}",
            //"{0:P}",
            //"{0:P1}",
            //"{0:X}",
            //"{0:x8}",
        };

        [Test]
        public void TestSingleParsing()
        {
            var parser = new SingleParser(NumberStyles.Any);
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            foreach (float number in Numbers)
            foreach (var format in Formats)
            {
                var numberStr = culture.Format(format, number);
                var parsedStr = culture.Format(format, culture.Parse(parser, numberStr));
                Assert.AreEqual(numberStr, parsedStr);
            }
        }

        [Test]
        public void TestDoubleParsing()
        {
            var parser = new DoubleParser(NumberStyles.Any);
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            foreach (double number in Numbers)
            foreach (var format in Formats)
            {
                var numberStr = culture.Format(format, number);
                var parsedStr = culture.Format(format, culture.Parse(parser, numberStr));
                Assert.AreEqual(numberStr, parsedStr);
            }
        }

        [Test]
        public void TestDecimalParsing()
        {
            var parser = new DecimalParser(NumberStyles.Any);
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            foreach (decimal number in Numbers)
            foreach (var format in Formats)
            {
                var numberStr = culture.Format(format, number);
                var parsedStr = culture.Format(format, culture.Parse(parser, numberStr));
                Assert.AreEqual(numberStr, parsedStr);
            }
        }
    }
}
