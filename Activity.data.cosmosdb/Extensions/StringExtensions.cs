using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.data.cosmosdb.Extensions
{
    public static class StringExtensions
    {

        public static string ToCamelCase(this string original)
        {
            if (String.IsNullOrEmpty(original.Trim()))
                return "";

            return Char.ToLowerInvariant(original[0]) + original.Substring(1);
        }
    }
}
