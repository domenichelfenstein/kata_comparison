namespace CSharp.FizzBuzz;

public class FizzBuzz
{
    private readonly int start;
    private readonly int end;

    public FizzBuzz(
        int start,
        int end)
    {
        this.start = start;
        this.end = end;
    }

    public IEnumerable<string> GetResult()
    {
        for (var i = this.start; i <= this.end; i++)
        {
            if (i % 7 == 0)
            {
                yield return $"{i}";
            }
            else if (i % 5 == 0 && i % 3 == 0)
            {
                yield return "FizzBuzz";
            }
            else if (i % 3 == 0)
            {
                yield return "Fizz";
            }
            else if (i % 5 == 0)
            {
                yield return "Buzz";
            }
            else
            {
                yield return $"{i}";
            }
        }
    }
}