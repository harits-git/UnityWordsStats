using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace protopronunciation
{
    public class PronounciationMeasurements : MonoBehaviour
    {

        public static int LevenshteinDistance(string a, string b)
        {
            int distance = 0;
            a = a.ToLower();
            b = b.ToLower();

            // swap to save some memory O(min(a,b)) instead of O(a)
            if (a.Length > b.Length)
            {
                string tmp = a;
                a = b;
                b = tmp;
            }

            int[] row = new int[a.Length+1];
            // init the row
            for (var i = 0; i <= a.Length; i++)
            {
                row[i] = i;
            }

            // fill in the rest
            for (var i = 1; i <= b.Length; i++)
            {
                var prev = i;
                for (var j = 1; j <= a.Length; j++)
                {
                    var val = 0;
                    if (b[i - 1] == a[j - 1])
                    {
                        val = row[j - 1]; // match
                    }
                    else
                    {
                        val = Mathf.Min(row[j - 1] + 1, // substitution
                                       prev + 1,     // insertion
                                       row[j] + 1);  // deletion
                    }
                    row[j - 1] = prev;
                    prev = val;
                }
                distance = row[a.Length] = prev;

            }

            return distance;
        }

        public static int SyllableCount(string word)
        {
            word = word.ToLower().Trim();
            bool lastWasVowel = false;
            var vowels = new[] { 'a', 'e', 'i', 'o', 'u', 'y' };
            int count = 0;

            //a string is an IEnumerable<char>; convenient.
            foreach (var c in word)
            {
                if (vowels.Contains(c))
                {
                    if (!lastWasVowel)
                        count++;
                    lastWasVowel = true;
                }
                else
                    lastWasVowel = false;
            }

            if ((word.EndsWith("e") || (word.EndsWith("es") || word.EndsWith("ed")))
                  && !word.EndsWith("le"))
                count--;

            return count;
        }
    }
}
