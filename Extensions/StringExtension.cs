/*
 * Copyright © 2018 Lion Kortlepel
 */

using System;
using System.Collections.Generic;

namespace Lutil.Extensions
{
    /// <summary>
    /// Extends the "string" class in order to add quality-of-life
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Splits the string at the specified index (index itself belongs to left side)
        /// </summary>
        /// <param name="s">String to be split</param>
        /// <param name="index">Index (inclusive to left side)</param>
        public static string[] SplitAt (this string s, int index)
        {
            string[] result = new string[2];
            for (int i = 0; i < index; i++)
            {
                try
                {
                    result[0] += s[i];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException ();
                }
            }
            for (int j = index; j < s.Length; j++)
            {
                result[1] += s[j];
            }
            return result;
        }

        public static SortedDictionary<char, long> CountCharacters ( this string s )
        {
            SortedDictionary<char, long> chars = new SortedDictionary<char, long> ();

            foreach (char c in s)
            {
                if (chars.ContainsKey (c))
                {
                    chars[c]++;
                }
                else
                {
                    chars.Add (c, 1);
                }
            }
            return chars;
        }
    }
}
