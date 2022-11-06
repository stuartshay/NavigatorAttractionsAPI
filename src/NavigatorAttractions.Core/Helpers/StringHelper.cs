using System.Text;

namespace NavigatorAttractions.Core.Helpers
{
    /// <summary>
    /// String Helpers
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Remove Whitespace.
        /// </summary>
        /// <param name="input">Input String</param>
        /// <returns></returns>
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Random String.
        /// </summary>
        /// <param name="size">String Length/Size</param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            var random = new Random((int)DateTime.Now.Ticks);

            var builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// First Character to Uppercase. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// To Camel Case.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string input)
        {
            // If there are 0 or 1 characters, just return the string.
            if (input == null || input.Length < 2)
                return input;

            // Split the string into words.
            string[] words = input.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        /// <summary>
        /// Validate Url.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool CheckUrlValid(this string source)
        {
            return Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == "https" || uriResult.Scheme == "http");
        }
    }
}
