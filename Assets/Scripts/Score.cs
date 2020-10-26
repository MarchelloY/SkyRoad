using System.Text;

public class Score
{
    private int _value;
    
    private readonly StringBuilder _stringBuilder = new StringBuilder();

    public Score(string str)
    {
        if (int.TryParse(str, out var temp))
            _value = temp;
    }

    private Score()
    {
    }

    public int Value
    {
        get => _value;
        set => _value = value;
    }

    public static Score operator ++(Score a)
    {
        return new Score {Value = a.Value + 1};
    }

    public static Score operator --(Score a)
    {
        return new Score {Value = a.Value - 1};
    }

    public static bool operator !=(Score a, Score b)
    {
        return a?.Value != b?.Value;
    }

    public static bool operator ==(Score a, Score b)
    {
        return a?.Value == b?.Value;
    }

    public static implicit operator Score(int score)
    {
        return new Score {Value = score};
    }

    public static explicit operator int(Score s)
    {
        return s?.Value ?? 0;
    }

    public override string ToString()
    {
        _stringBuilder.Clear().AppendFormat("Asteroids: {0}", Value);
        return _stringBuilder.ToString();
    }
}