#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Text.Expressions.Substitution
{
    public class DefaultSubstitutionExpression : AbstractSubstitutionExpression
    {
        public DefaultSubstitutionExpression() : base("${", "}"){}
    }
}
#endif