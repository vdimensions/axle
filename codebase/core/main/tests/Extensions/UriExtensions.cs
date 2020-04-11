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
        private readonly IParser<Uri> _uriParser = new UriParser();

        [Test]
        public void TestResolveUriAppend()
        {
            var u1 = _uriParser.Parse("http://someuri.org");
            var u2 = _uriParser.Parse("./home");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://someuri.org/home", resolved.ToString());
        }
        [Test]
        public void TestResolveUriAppendNoSlash()
        {
            var u1 = _uriParser.Parse("http://someuri.org");
            var u2 = _uriParser.Parse("home");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://someuri.org/home", resolved.ToString());
        }

        [Test]
        public void TestResolveUriReplace()
        {
            var u1 = _uriParser.Parse("http://someuri.org/home");
            var u2 = _uriParser.Parse("../about");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://someuri.org/about", resolved.ToString());
        }


        [Test]
        public void TestResolveAbsoluteUri()
        {
            var u1 = _uriParser.Parse("http://someuri.org/home");
            var u2 = _uriParser.Parse("http://anotheruri.org/home");
            var resolved = u1.Resolve(u2);
            Assert.AreEqual("http://anotheruri.org/home", resolved.ToString());
        }
        
        [Test]
        public void TestTryGetRelativePathFromAbsoluteUriSources()
        {
            var u1 = _uriParser.Parse("http://someuri.org/a/b/c/d/e/f");
            var u2 = _uriParser.Parse("http://someuri.org/a/b/c/1/2/3");
            var gotRelativePath = u1.TryGetRelativePathFrom(u2, out var result);
            Assert.IsTrue(gotRelativePath);
            Assert.AreEqual("../../../1/2/3", result.ToString());
        }
        
        [Test]
        public void TestTryGetRelativePathFromRelativeUriSources()
        {
            var u1 = _uriParser.Parse("../a/b/c/d/e/f");
            var u2 = _uriParser.Parse("../a/b/c/1/2/3");
            var gotRelativePath = u1.TryGetRelativePathFrom(u2, out var result);
            Assert.IsTrue(gotRelativePath);
            Assert.AreEqual("../../../1/2/3", result.ToString());
        }
        
        [Test]
        public void TestTryGetRelativePathFromInvalidInput()
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
