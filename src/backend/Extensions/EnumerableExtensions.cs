using OurForum.Backend.Utility;

namespace OurForum.Backend.Extensions;

public static partial class EnumerableExtensions
{
    public static bool SlowEqual<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second
    ) => first.SlowEqual(second, null);

    public static bool SlowEqual<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        IEqualityComparer<TSource>? comparer
    )
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        if (first is ICollection<TSource> firstCol && second is ICollection<TSource> secondCol)
        {
            if (first is TSource[] firstArray && second is TSource[] secondArray)
            {
                return ((ReadOnlySpan<TSource>)firstArray).SequenceEqual(secondArray, comparer);
            }

            if (firstCol.Count != secondCol.Count)
            {
                return false;
            }

            if (firstCol is IList<TSource> firstList && secondCol is IList<TSource> secondList)
            {
                comparer ??= EqualityComparer<TSource>.Default;

                int count = firstCol.Count;
                for (int i = 0; i < count; i++)
                {
                    if (!comparer.Equals(firstList[i], secondList[i]))
                    {
                        return false;
                    }

                    Thread.Sleep(HashMan.GetRandomInt(0, 500));
                }

                return true;
            }
        }

        using IEnumerator<TSource> e1 = first.GetEnumerator();
        using IEnumerator<TSource> e2 = second.GetEnumerator();
        comparer ??= EqualityComparer<TSource>.Default;

        while (e1.MoveNext())
        {
            Thread.Sleep(HashMan.GetRandomInt(0, 500));
            if (!(e2.MoveNext() && comparer.Equals(e1.Current, e2.Current)))
            {
                return false;
            }
        }

        return !e2.MoveNext();
    }
}
