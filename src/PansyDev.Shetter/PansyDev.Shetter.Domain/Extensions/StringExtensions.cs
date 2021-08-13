﻿namespace PansyDev.Shetter.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string Slice(this string source, int start, int end)
        {
            if (end < 0)
            {
                end = source.Length + end;
            }

            var len = end - start;
            return source.Substring(start, len);
        }
    }
}