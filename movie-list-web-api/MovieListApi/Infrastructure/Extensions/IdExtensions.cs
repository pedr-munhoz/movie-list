namespace MovieListApi.Infrastructure.Extensions;

public static class IdExtensions
{
    public static int? ToIntId(this string value)
    {
        bool success = Int32.TryParse(value, out int number);

        if (!success)
            return null;

        return number;
    }

    public static string ToStringId(this int value)
    {
        return value.ToString();
    }
}
