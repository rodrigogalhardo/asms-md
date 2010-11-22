using System;
using System.Collections.Generic;

namespace MRGSP.ASMS.Service
{
    public class NumberToWords
    {
        static public string Do(decimal number)
        {
            string frac;
            var result = string.Empty;
            var rank = new[]
                           { 
                                      new[] { "", "" }, 
                                      new[] { "mie", "mii" },
                                      new[] { "milion", "milioane" },
                                      new[] { "miliard", "miliarde" },
                                      new[] { "bilion", "bilioane" },
                                      new[] { "trilion", "trilioane" },
                                  };
            var ss = number.ToString().Split(new[] { ',', '.' });

            var integral = ss[0];
            if (ss.Length > 1)
            {
                var sf = ss[1];
                frac = sf.Length == 1 ? sf + "0" : (sf.Length == 0 ? "00" : ss[1].Substring(0, 2));
            }
            else
                frac = "00";

            while (integral.Length % 3 != 0)
            {
                integral = "0" + integral;
            }

            if (!string.IsNullOrEmpty(frac))
            {
                result = string.Format("lei {0} bani", frac);
            }

            for (int i = integral.Length - 1, j = 0; i > -1; j++, i -= 3)
            {
                var s3 = integral.Substring(i - 2, 3);
                s3 = Do3(s3, rank[j][0], rank[j][1]);
                result = string.Format("{0} {1}", s3, result);
            }

            while (result.Contains("  "))
                result = result.Replace("  ", " ");

            return result.Trim();
        }

        static private string Do3(string number, string singular, string plural)
        {
            number = Convert.ToInt32(number).ToString();
            if (Convert.ToInt32(number) > 1)
                singular = plural;

            if (number.Length < 3)
            {
                if (Convert.ToInt32(number) < 20)
                    return Word(Convert.ToInt32(number)) + " " + singular;

                var n2 = Word(Convert.ToInt32(number[0].ToString()) * 10);
                var n1 = Word(Convert.ToInt32(number[1].ToString()));
                return n2 + (n1 == string.Empty ? string.Empty : " si " + n1) + " " + singular;
            }
            else
            {
                var n1 = string.Empty;
                var n2 = words[Convert.ToInt32(number.Substring(1, 2))];

                if (string.IsNullOrEmpty(n2))
                {
                    n2 = Word(Convert.ToInt32(number[1].ToString()) * 10);
                    n1 = Word(Convert.ToInt32(number[2].ToString()));
                }

                var n3 = "una";

                var suta = "suta";
                if (Convert.ToInt32(number[0].ToString()) > 1)
                {
                    suta = "sute";
                    n3 = Word(Convert.ToInt32(number[0].ToString()));
                }

                return n3 + " " + suta + " " + n2 + (n1 == string.Empty ? string.Empty : " si " + n1) + " " + singular;
            }
        }

        private static string Word(int o)
        {
            return words.ContainsKey(o) ? words[o] : "";
        }

        private static readonly IDictionary<int, string> words = new Dictionary<int, string>
        {
                { 1, "unu"},
                { 2, "doi"},
                { 3, "trei"},
                { 4, "patru"},
                { 5, "cinci"},
                { 6, "sase"},
                { 7, "sapte"},
                { 8, "opt"},
                { 9, "noua"},
                { 10, "zece"},
                { 11, "unsprezece"},
                { 12, "doisprezece"},
                { 13, "treisprezece"},
                { 14, "patrusprezece"},
                { 15, "cincisprezece"},
                { 16, "sasesprezece"},
                { 17, "saptesprezece"},
                { 18, "optsprezece"},
                { 19, "nouasprezece"},
                { 20, "douazeci"},
                { 30, "treizeci"},
                { 40, "patruzeci"},
                { 50, "cincizeci"},
                { 60, "sasezeci"},
                { 70, "saptezeci"},
                { 80, "optzeci"},
                { 90, "nouazeci"},
    };

    }
}