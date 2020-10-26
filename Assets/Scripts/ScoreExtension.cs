public static class ScoreExtension
{
    public static string ConvertScore(this int score, int num)
    {
        int div = score / num,
            mod = score % num;
        if (div != 0)
            return div + "'" + mod;
        return mod.ToString();
    }

    public static string ConvertScore(this float score, int num)
    {
        return ((int) score).ConvertScore(num);
    }

    public static string ConvertScore(this double score, int num)
    {
        return ((int) score).ConvertScore(num);
    }
}