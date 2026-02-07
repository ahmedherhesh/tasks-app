namespace TaskApp.Infrastructure.Extensions
{
    public static class IQuaryableExtension
    {
        public static IQueryable<T> When<T>(this IQueryable<T> query, bool condition, Func<IQueryable<T>, IQueryable<T>> then) => condition ? then(query) : query;
    }
}