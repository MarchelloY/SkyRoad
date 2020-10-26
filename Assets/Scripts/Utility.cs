using System.Collections.Generic;
using System.Linq;

public static class Utility
{
    public static int SumAllEvenNumbers(IEnumerable<int> list)
    {
        return list.Where(item => item % 2 == 0).Sum();
    }
}