using System.Diagnostics.CodeAnalysis;

namespace DotNet.GitHubAction.Extensions;

public static class MinMaxExtensions
{
    public static (T min, T max) MinMax<T>(this IEnumerable<T> source)
        where T : IComparable<T>
        => source.TryMinMax(out T? min, out T? max)
        ? (min, max)
        : throw new InvalidOperationException("Sequence contains no elements");
    public static bool TryMinMax<T>(
        this IEnumerable<T> source,
        [NotNullWhen(true)] out T? min,
        [NotNullWhen(true)] out T? max)
        where T : IComparable<T>
    {
        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            min = default;
            max = default;
            return false;
        }
        min = enumerator.Current;
        max = enumerator.Current;
        while (enumerator.MoveNext())
        {
            T current = enumerator.Current;
            if (current.CompareTo(min) < 0)
                min = current;
            if (current.CompareTo(max) > 0)
                max = current;
        }
        return true;
    }
}
