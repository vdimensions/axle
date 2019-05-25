#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Text.Expressions.Substitution
{
    public class MSBuildSubstitutionExpression : AbstractSubstitutionExpression
    {
        public MSBuildSubstitutionExpression() : base("$(", ")"){}
    }
}
#endif