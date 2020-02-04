using Axle.Text.Expressions.Substitution;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Axle.Core.Tests.Text.Expressions.Substitution
{
    [TestFixture]
    public class SubstitutionExpressionTests
    {
        [Test]
        public void TestStandardSubstitutionExpression()
        {
            const string sourceText = "Hello, ${target}";
            const string targetText = "Hello, World";

            var replacement = new Dictionary<string, string>(StringComparer.Ordinal) { { "target", "World" } };
            var expr = new StandardSubstitutionExpression();
            var replacedText = expr.Replace(sourceText, new DictionarySubstitutionProvider(replacement));

            Assert.IsNotNull(replacedText);
            Assert.AreEqual(targetText, replacedText);
        }

        [Test]
        public void TestStandardSubstitutionExpressionFallbacks()
        {
            const string sourceText = "Hello, ${target:Universe}";
            const string targetText = "Hello, Universe";
        
            var replacement = new Dictionary<string, string>(StringComparer.Ordinal) { };
            var expr = new StandardSubstitutionExpression();
            var replacedText = expr.Replace(sourceText, new DictionarySubstitutionProvider(replacement));
        
            Assert.IsNotNull(replacedText);
            Assert.AreEqual(targetText, replacedText);
        }

        [Test]
        public void TestStandardSubstitutionExpressionFallbackEscapes()
        {
            const string sourceText = "GNU's homepage is: ${GNU_HOMEPAGE:https\\://www.gnu.org}";
            const string targetText = "GNU's homepage is: https://www.gnu.org";
        
            var replacement = new Dictionary<string, string>(StringComparer.Ordinal) { };
            var expr = new StandardSubstitutionExpression();
            var replacedText = expr.Replace(sourceText, new DictionarySubstitutionProvider(replacement));
        
            Assert.IsNotNull(replacedText);
            Assert.AreEqual(targetText, replacedText);
        }

        [Test]
        public void TestMSBuildSubstitutionExpression()
        {
            const string sourceText = "Hello, $(target)";
            const string targetText = "Hello, World";

            var replacement = new Dictionary<string, string>(StringComparer.Ordinal) { { "target", "World" } };
            var expr = new MSBuildSubstitutionExpression();
            var replacedText = expr.Replace(sourceText, new DictionarySubstitutionProvider(replacement));

            Assert.IsNotNull(replacedText);
            Assert.AreEqual(targetText, replacedText);
        }
    }
}
