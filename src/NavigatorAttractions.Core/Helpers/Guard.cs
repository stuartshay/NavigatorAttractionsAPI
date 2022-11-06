namespace NavigatorAttractions.Core.Helpers
{
    /// <summary>
    /// Guard Helpers.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throw If Null Or Whitespace
        /// </summary>
        /// <param name="str"></param>
        /// <param name="name"></param>
        public static void ThrowIfNullOrWhitespace(string str, string name)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throw if Null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throw if Zero or Less
        /// </summary>
        /// <param name="num"></param>
        /// <param name="name"></param>
        public static void ThrowIfZeroOrLess(long num, string name)
        {
            if (num <= 0)
                throw new ArgumentException(name);
        }

        /// <summary>
        /// Throw If
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="predicate"></param>
        /// <param name="name"></param>
        public static void ThrowIf<T>(this T obj, Predicate<T> predicate, string name)
        {
            if (predicate(obj))
                throw new ArgumentException(name);
        }
    }
}
