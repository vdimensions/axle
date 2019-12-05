using NUnit.Framework;
using System;
using Axle.Conversion.Binding;
using System.Collections.Generic;
using System.Globalization;
using Axle.Globalization;
using Axle.Extensions.DateTime;

namespace Axle.Core.Tests.Conversion.Binding
{
    [TestFixture]
    public class SimpleTypeBindingConverterTest
    {
        private void RunConversion(CultureInfo culture)
        {
            var objects = new Dictionary<Type, object>() {
                { typeof(bool), true },
                { typeof(char), 'c' },
                { typeof(byte), (byte) 1 },
                { typeof(sbyte), (sbyte) 2 },
                { typeof(short), (short) 3 },
                { typeof(ushort), (ushort) 4 },
                { typeof(int), 5 },
                { typeof(uint), (uint) 6 },
                { typeof(long), 7L },
                { typeof(ulong), (ulong) 8 },
                { typeof(float), 9f },
                { typeof(double), 10.0 },
                { typeof(decimal), (decimal) 11.1 },
                { typeof(Guid), Guid.Empty },
                // Date time objects are not guaranteed to be consistenly parseable across cultures, we should skip them for now
                //{ typeof(DateTime), DateTime.Now.Date.ToISOString() },
                { typeof(TimeSpan), TimeSpan.Zero },
                { typeof(Version), new Version(1, 0) },
                { typeof(Uri), new Uri("http://google.com") },
                { typeof(Nullable<bool>), new Nullable<bool>() },
                { typeof(Nullable<char>), new Nullable<char>() },
                { typeof(Nullable<byte>), new Nullable<byte>() },
                { typeof(Nullable<sbyte>), new Nullable<sbyte>() },
                { typeof(Nullable<short>), new Nullable<short>() },
                { typeof(Nullable<ushort>), new Nullable<ushort>() },
                { typeof(Nullable<int>), new Nullable<int>() },
                { typeof(Nullable<uint>), new Nullable<uint>() },
                { typeof(Nullable<long>), new Nullable<long>() },
                { typeof(Nullable<ulong>), new Nullable<ulong>() },
                { typeof(Nullable<float>), new Nullable<float>() },
                { typeof(Nullable<double>), new Nullable<double>() },
                { typeof(Nullable<decimal>), new Nullable<decimal>() },
                { typeof(Nullable<Guid>), new Nullable<Guid>() },
                { typeof(Nullable<DateTime>), new Nullable<DateTime>() },
                { typeof(Nullable<DateTimeOffset>), new Nullable<DateTimeOffset>() },
                { typeof(Nullable<TimeSpan>), new Nullable<TimeSpan>() },
            };

            var converter = new DefaultBindingConverter();

            using (CultureScope.Create(culture))
            {
                foreach (var kvp in objects)
                {
                    var type = kvp.Key;
                    var value = kvp.Value;
                    var stringValue = value?.ToString() ?? string.Empty;
                    if (converter.TryConvertMemberValue(stringValue, type, out var convertedValue))
                    {
                        Assert.AreEqual(value, convertedValue);
                    }
                    else
                    {
                        Assert.Fail($"Unable to convert object of supported type {type}");
                    }
                }
            }
            
        }

        [Test]
        public void TestConversion() => RunConversion(CultureInfo.InvariantCulture);

        [Test]
        public void TestConversionAcrossCultures()
        {
            foreach(var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    RunConversion(culture);
                }
                catch (Exception e)
                {
                    Assert.Fail($"Conversion consistency failed for culture {culture}: {e.Message}");
                }
            }
        }
    }
}
