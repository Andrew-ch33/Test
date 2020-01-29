using System;

namespace TestApp.Extensions
{
    public static class StringExtension
    {
        public static string Reverse(this string s)
        {
            if (String.IsNullOrEmpty(s)) return string.Empty;

            char[] cArray = s.ToCharArray();
            Array.Reverse(cArray);
            return new string(cArray);
        }
    }
}
