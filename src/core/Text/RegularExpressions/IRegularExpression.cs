using System.Text.RegularExpressions;


namespace Axle.Text.RegularExpressions
{
	public interface IRegularExpression
	{
	    bool IsMatch(string value);
	    bool IsMatch(string value, int startIndex);

	    Match[] Match(string value);
	    Match[] Match(string value, int startIndex);

	    string[] Split(string value);
	    string[] Split(string value, int count);
        string[] Split(string value, int count, int startIndex);
	}
}
