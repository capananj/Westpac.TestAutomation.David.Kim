using System;
using System.Linq;

namespace TestAutomation.Framework.Common
{
    public static class DataExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            var isNullOrEmpty = false;

            if (value == null || value == "")
            {
                isNullOrEmpty = true;
            }

            return isNullOrEmpty;
        }

        public static bool MatchesAny(this string value, params string[] values)
        {
            var matchFound = false;

            matchFound = values.Count(x => x == value) > 0;

            return matchFound;
        }

        public static bool CompareSanitized(this string value, string compareTo)
        {
            var isMatch = false;

            if (value.ToLower().Replace(Environment.NewLine, "").Replace(" ", "")
                .Equals(compareTo.ToLower().Replace(Environment.NewLine, "").Replace(" ", "")))
            {
                isMatch = true;
            }

            return isMatch;
        }
    }
}
