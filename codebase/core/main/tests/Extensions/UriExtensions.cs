using System;
using Axle.Extensions.Uri;
using Axle.Text.Parsing;
using NUnit.Framework;

namespace Axle.Core.Tests.Extensions
{
    using UriParser = Axle.Text.Parsing.UriParser;

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

        [Test]
        public void TestAbsoluteUriNormalization()
        {
            var rawUri = "http://www.yahoo.com/%3a/search?term=123";
            var normalizedUri = "http://www.yahoo.com/%3A/search?term=123";
            Assert.AreEqual(
                _uriParser.Parse(normalizedUri).Normalize().ToString(),
                _uriParser.Parse(rawUri).Normalize().ToString());
        }
        
        [Test]
        public void TestRelativeUriNormalization()
        {
            var rawUri1 = ".././there/was/some/path/../../../is/another/path?query=123";
            var normalizedUri = "../there/is/another/path?query=123";
            Assert.AreEqual(
                _uriParser.Parse(normalizedUri).Normalize().ToString(),
                _uriParser.Parse(rawUri1).Normalize().ToString());
            
            var rawUri2 = "./../there/is/some/path/../../another/path?query=123";
            Assert.AreEqual(
                _uriParser.Parse(normalizedUri).Normalize().ToString(),
                _uriParser.Parse(rawUri2).Normalize().ToString());
        }
    }
}
