using System.Text.RegularExpressions;

namespace CSharp.StringCalculator;

public static class Calculator
{
    private static readonly Regex regex = new Regex(@"(?<sign>[-\+]?)(?<number>\d+)", RegexOptions.Compiled);
    
    public static int Calculate(string expression)
    {
        var total = 0;
        
        var matches = regex.Matches(expression.Replace(" ", string.Empty));
        foreach (Match match in matches)
        {
            var factor = match.Groups["sign"].Success && match.Groups["sign"].Value == "-"
                ? -1
                : 1;
            
            total += factor * int.Parse(match.Groups["number"].Value);
        }

        return total;
    }
}