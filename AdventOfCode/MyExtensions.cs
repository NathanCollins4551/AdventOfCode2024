namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static TValue TryGetKey<TKey1, TKey2, TValue>(this IDictionary<(TKey1, TKey2), TValue> @this, TKey1 key1, TKey2 key2, Func<TKey1, TKey2, TValue> f)
            where TKey1 : notnull
            where TKey2 : notnull
        {
            if (@this.ContainsKey((key1, key2)))
                return @this[(key1, key2)];
            var newValue = f(key1, key2);
            @this.Add((key1, key2), newValue);
            return newValue;
        }
    }
}
