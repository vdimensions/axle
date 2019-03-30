namespace Axle.Text.Expressions.Substitution
{
    public interface ISubstitutionProvider
    {
        bool TrySubstitute(string token, out string value);
    }
}