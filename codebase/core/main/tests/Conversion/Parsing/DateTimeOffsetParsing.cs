using Axle.Conversion.Parsing;
using Axle.Extensions.DateTimeOffset;
using Axle.Text.Formatting.Extensions;
using NUnit.Framework;
using System;
using System.Globalization;

namespace Axle.Core.Tests.Conversion.Parsing
{
    [TestFixture]
    public class DateTimeOffsetParsing
    {
        [Test]
        public void TestDateTimeOffsetParsing()
        {
            var dateTimeOffset = new DateTimeOffset(
                new DateTime(DateTime.Now.Ticks, DateTimeKind.Unspecified),
                TimeSpan.FromHours(2));
            var isoString = dateTimeOffset.ToISOString();
            var parser = new DateTimeOffsetParser();

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                var parsedOffset = culture.Parse(parser, isoString);
                Assert.AreEqual(isoString, parsedOffset.ToISOString(), $"Failed parsing for culture {culture}");
            }
        }
    }
}
