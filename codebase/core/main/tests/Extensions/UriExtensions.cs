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
        
        [Test]
        public void TestUriTryGetRelativePathFromAbsoluteUriSources()
        {
            var u1 = _uriParser.Parse("http://someuri.org/a/b/c/d/e/f");
            var u2 = _uriParser.Parse("http://someuri.org/a/b/c/1/2/3");
            var gotRelativePath = u1.TryGetRelativePathFrom(u2, out var result);
            Assert.IsTrue(gotRelativePath);
            Assert.AreEqual("../../../1/2/3",  result.ToString());
        }
        
        [Test]
        public void TestUriTryGetRelativePathFromRelativeUriSources()
        {
            var u1 = _uriParser.Parse("../a/b/c/d/e/f");
            var u2 = _uriParser.Parse("../a/b/c/1/2/3");
            var gotRelativePath = u1.TryGetRelativePathFrom(u2, out var result);
            Assert.IsTrue(gotRelativePath);
            Assert.AreEqual("../../../1/2/3",  result.ToString());
        }
        
        [Test]
        public void TestUriTryGetRelativePathFromInvalidInput()
        {
            Assert.IsFalse(Axle.Extensions.Uri.UriExtensions.TryGetRelativePathFrom(
                _uriParser.Parse("http://someuri.org/a/b/c/d/e/f"),
                _uriParser.Parse("https://someuri.org/a/b/c/1/2/3"),
                out _));
            Assert.IsFalse(Axle.Extensions.Uri.UriExtensions.TryGetRelativePathFrom(
                _uriParser.Parse("http://someuri.org/a/b/c/d/e/f"),
                _uriParser.Parse("http://someuri.com/a/b/c/1/2/3"),
                out _));
            Assert.IsFalse(Axle.Extensions.Uri.UriExtensions.TryGetRelativePathFrom(
                _uriParser.Parse("./a/b/c/d/e/f"),
                _uriParser.Parse("../a/b/c/1/2/3"),
                out _));
        }
    }
}
