using System;

using Axle.Conversion.Parsing;
using Axle.Extensions.Uri;
using NUnit.Framework;

namespace Axle.Core.Tests.Extensions
{
    using UriParser = Axle.Conversion.Parsing.UriParser;

    [TestFixture]
    public class UriExtensions
    {
        private IParser<Uri> _uriParser = new UriParser();

        [Test]
        public void TestUriResolveAppend()
        {
            var u1 = _uriParser.Parse("http://someuri.org");
            var u2 = _uriParser.Parse("./home");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://someuri.org/home", resolved.ToString());
        }
        [Test]
        public void TestUriResolveAppendNoSlash()
        {
            var u1 = _uriParser.Parse("http://someuri.org");
            var u2 = _uriParser.Parse("home");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://someuri.org/home", resolved.ToString());
        }

        [Test]
        public void TestUriResolveReplace()
        {
            var u1 = _uriParser.Parse("http://someuri.org/home");
            var u2 = _uriParser.Parse("../about");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://someuri.org/about", resolved.ToString());
        }


        [Test]
        public void TestUriResolveAbsolute()
        {
            var u1 = _uriParser.Parse("http://someuri.org/home");
            var u2 = _uriParser.Parse("http://anotheruri.org/home");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://anotheruri.org/home", resolved.ToString());
        }
    }
}
