using System;
using System.Collections.Generic;
using System.Globalization;
using Axle.Globalization;
using Axle.Text.Documents.Binding;
using NUnit.Framework;

namespace Axle.Text.Documents.Tests.Binding
{
    [TestFixture]
    public class SimpleTypeBindingConverterTests
    {
        private void RunConversion(CultureInfo culture)
        {
            var objects = new Dictionary<Type, object[]>() {
                { typeof(bool), new object[] { false, true } },
                { typeof(char), new object[] { 'c', char.MinValue, char.MaxValue } },
                { typeof(byte), new object[] { (byte) 1, byte.MinValue, byte.MaxValue } },
                { typeof(sbyte), new object[] { (sbyte) 2, sbyte.MinValue, sbyte.MaxValue } },
                { typeof(short), new object[] { (short) 3, short.MinValue, short.MaxValue } },
                { typeof(ushort), new object[] { (ushort) 4, ushort.MinValue, ushort.MaxValue } },
                { typeof(int), new object[] { 5, int.MinValue, int.MaxValue } },
                { typeof(uint), new object[] { (uint) 6, uint.MinValue, uint.MaxValue } },
                { typeof(long), new object[] { 7L, long.MinValue, long.MaxValue } },
                { typeof(ulong), new object[] { (ulong) 8, ulong.MinValue, ulong.MaxValue } },
                { typeof(float), new object[] { 9f } },
                { typeof(double), new object[] { 10.0 } },
                { typeof(decimal), new object[] { (decimal) 11.1, decimal.MinValue, decimal.MaxValue } },
                { typeof(string), new object[] { string.Empty, "Hello World" } },
                { typeof(Guid), new object[] { Guid.Empty, Guid.NewGuid() } },
                // Date time objects are not guaranteed to be consistently parseable across cultures, we should skip them for now
                //{ typeof(DateTime), DateTime.Now.Date.ToISOString() },
                { typeof(TimeSpan), new object[] { TimeSpan.Zero, TimeSpan.MinValue, TimeSpan.MaxValue } },
                { typeof(Version), new object[] { new Version(1, 0) } },
                { typeof(Uri), new object[] { new Uri("http://google.com") } },
                { typeof(Nullable<bool>), new object[] { new Nullable<bool>(), new bool?(false) } },
                { typeof(Nullable<char>), new object[] { new Nullable<char>(), new char?('c') } },
                { typeof(Nullable<byte>), new object[] { new Nullable<byte>(), new byte?(0) } },
                { typeof(Nullable<sbyte>), new object[] { new Nullable<sbyte>(), new sbyte?(0) } },
                { typeof(Nullable<short>), new object[] { new Nullable<short>(), new short?(0) } },
                { typeof(Nullable<ushort>), new object[] { new Nullable<ushort>(), new ushort?(0) } },
                { typeof(Nullable<int>), new object[] { new Nullable<int>(), new int?(0) } },
                { typeof(Nullable<uint>), new object[] { new Nullable<uint>(), new uint?(0u) } },
                { typeof(Nullable<long>), new object[] { new Nullable<long>(), new long?(0L) } },
                { typeof(Nullable<ulong>), new object[] { new Nullable<ulong>(), new ulong?(0L) } },
                { typeof(Nullable<float>), new object[] { new Nullable<float>(), new float?(0.1F) } },
                { typeof(Nullable<double>), new object[] { new Nullable<double>(), new double?(0.1) } },
                { typeof(Nullable<decimal>), new object[] { new Nullable<decimal>(), new decimal?(0.1M) } },
                { typeof(Nullable<Guid>), new object[] { new Nullable<Guid>(), new Guid?(Guid.NewGuid()) } },
                { typeof(Nullable<DateTime>), new object[] { new Nullable<DateTime>() } },
                { typeof(Nullable<DateTimeOffset>), new object[] { new Nullable<DateTimeOffset>() } },
                { typeof(Nullable<TimeSpan>), new object[] { new Nullable<TimeSpan>(), new TimeSpan?(TimeSpan.Zero) } },
            };

            var converter = new DefaultBindingConverter();

            using (CultureScope.Create(culture))
            {
                foreach (var kvp in objects)
                {
                    var type = kvp.Key;
                    var values = kvp.Value;
                    foreach (var value in values)
                    {
                        var stringValue = value?.ToString() ?? string.Empty;
                        if (converter.TryConvertMemberValue(stringValue, type, out var convertedValue))
                        {
                            Assert.AreEqual(value, convertedValue);
                        }
                        else
                        {
                            Assert.Fail($"Unable to convert value '{value}' to an instance of type {type}");
                        }
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
