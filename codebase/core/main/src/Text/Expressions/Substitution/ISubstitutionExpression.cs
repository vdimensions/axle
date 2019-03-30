namespace Axle.Text.Expressions.Substitution
{
    public interface ISubstitutionExpression
    {
        string Replace(string input, ISubstitutionProvider sp);
    }
}