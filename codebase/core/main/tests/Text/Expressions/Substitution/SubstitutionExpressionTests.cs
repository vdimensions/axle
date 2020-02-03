using Axle.Text.Expressions.Substitution;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
