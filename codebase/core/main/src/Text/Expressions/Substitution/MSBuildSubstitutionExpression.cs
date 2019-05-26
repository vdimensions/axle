namespace Axle.Text.Expressions.Substitution
{
    public class MSBuildSubstitutionExpression : AbstractSubstitutionExpression
    {
        public MSBuildSubstitutionExpression() : base("$(", ")"){}
    }
}