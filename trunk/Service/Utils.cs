using System;
using System.Linq;

namespace MRGSP.ASMS.Service
{
    public class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">a decimal number</param>
        /// <param name="cod">840 for euro; 978 for USD; empty string for MDL </param>
        /// <returns>number written in words</returns>
        static public string NumberInWords(decimal number, string cod)
        {
            string s = _NumberInWords(number);

            if (cod == "978")
            {
                return s.Replace("lei", "euro").Replace("bani", "eurocenti");
            }
            else if (cod == "840")
            {
                return s.Replace("lei", "dolari SUA").Replace("bani", "centi");
            }
            else
            {
                return s;
            }

        }

        static public string NumberInWords(decimal number)
        {
            return _NumberInWords(number).Replace("lei", "").Replace("bani", "").Replace("0", "");
        }

        static public string NumberInWords(decimal number, string cod, string lei, string bani)
        {
            string s = _NumberInWords(number);

            if (cod == "978")
            {
                return s.Replace("lei", "euro").Replace("bani", "eurocenti");
            }
            else if (cod == "840")
            {
                return s.Replace("lei", "dolari SUA").Replace("bani", "centi");
            }
            else if (cod == "498")
            {
                return s;
            }
            else
            {
                return s.Replace("lei", lei).Replace("bani", bani);
            }

        }

        static public string DateInWords(DateTime date)
        {
            string month = string.Empty;
            switch (date.Month)
            {
                case 1: month = "ianuarie"; break;
                case 2: month = "februarie"; break;
                case 3: month = "martie"; break;
                case 4: month = "aprilie"; break;
                case 5: month = "mai"; break;
                case 6: month = "iunie"; break;
                case 7: month = "iulie"; break;
                case 8: month = "august"; break;
                case 9: month = "septembrie"; break;
                case 10: month = "octombrie"; break;
                case 11: month = "noiembrie"; break;
                case 12: month = "decembrie"; break;
                default: date.Month.ToString(); break;
            }

            return string.Format("{0} {1} {2}", date.Day, month, date.Year);
        }

        static public string[] SplitSentence(string sentence, int lineLength)
        {
            if (sentence.Length < lineLength)
            {
                return new string[] { sentence };
            }

            int spaceIndex = sentence.Substring(0, lineLength).LastIndexOf(' ');

            if (spaceIndex == -1)
            {
                return new string[] { sentence };
            }

            string newline = sentence.Substring(0, spaceIndex + 1).Trim();
            string rest = sentence.Substring(spaceIndex, sentence.Length - spaceIndex).Trim();

            return new string[] { newline }.Concat(SplitSentence(rest, lineLength)).ToArray();

        }

        static public string[] SplitSentence(string sentence, int[] lineLengths)
        {
            return SplitSentence(sentence, lineLengths, 0);
        }

        #region private methods

        static private string[] SplitSentence(string sentence, int[] lineLengths, int i)
        {
            if (lineLengths.Length < i + 1)
            {
                return new string[] { sentence };
            }
            sentence = sentence.Trim();
            if (sentence.Length <= lineLengths[i])
            {
                return new string[] { sentence };
            }

            int spaceIndex = sentence.Substring(0, lineLengths[i] + 1).LastIndexOf(' ');

            if (spaceIndex == -1)
            {
                //return new string[] { sentence };
                return SplitSentence(sentence, lineLengths, i + 1);
            }

            string newline = sentence.Substring(0, spaceIndex + 1).Trim();
            string rest = sentence.Substring(spaceIndex, sentence.Length - spaceIndex).Trim();

            return new string[] { newline }.Concat(SplitSentence(rest, lineLengths, i + 1)).ToArray();

        }
        static private string _NumberInWords(decimal number)
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
                string s3 = integral.Substring(i - 2, 3);
                s3 = NumberInWords3(s3, rank[j][0], rank[j][1]);
                result = string.Format("{0} {1}", s3, result);
            }

            while (result.Contains("  "))
            {
                result = result.Replace("  ", " ");
            }

            return result.Trim();
        }

        static private string Word(int x)
        {
            var r = string.Empty;
            switch (x)
            {
                case 1: r = "unu"; break;
                case 2: r = "doi"; break;
                case 3: r = "trei"; break;
                case 4: r = "patru"; break;
                case 5: r = "cinci"; break;
                case 6: r = "sase"; break;
                case 7: r = "sapte"; break;
                case 8: r = "opt"; break;
                case 9: r = "noua"; break;
                case 10: r = "zece"; break;
                case 11: r = "unsprezece"; break;
                case 12: r = "doisprezece"; break;
                case 13: r = "treisprezece"; break;
                case 14: r = "patrusprezece"; break;
                case 15: r = "cincisprezece"; break;
                case 16: r = "sasesprezece"; break;
                case 17: r = "saptesprezece"; break;
                case 18: r = "optsprezece"; break;
                case 19: r = "nouasprezece"; break;
                case 20: r = "douazeci"; break;
                case 30: r = "treizeci"; break;
                case 40: r = "patruzeci"; break;
                case 50: r = "cincizeci"; break;
                case 60: r = "sasezeci"; break;
                case 70: r = "saptezeci"; break;
                case 80: r = "optzeci"; break;
                case 90: r = "nouazeci"; break;
                default: break;
            }
            return r;

        }

        static private string NumberInWords3(string number, string singular, string plural)
        {
            number = Convert.ToInt32(number).ToString();
            if (Convert.ToInt32(number) > 1)
            {
                singular = plural;
            }

            if (number.Length < 3)
            {
                if (Convert.ToInt32(number) < 20)
                {
                    return Word(Convert.ToInt32(number)) + " " + singular;
                }
                else
                {
                    string n2 = Word(Convert.ToInt32(number[0].ToString()) * 10);
                    string n1 = Word(Convert.ToInt32(number[1].ToString()));
                    return n2 + (n1 == string.Empty ? string.Empty : " si " + n1) + " " + singular;
                }
            }
            else
            {
                string n1 = string.Empty;
                string n2 = Word(Convert.ToInt32(number.Substring(1, 2)));

                if (string.IsNullOrEmpty(n2))
                {
                    n2 = Word(Convert.ToInt32(number[1].ToString()) * 10);
                    n1 = Word(Convert.ToInt32(number[2].ToString()));
                }

                string n3 = "una";

                string suta = "suta";
                if (Convert.ToInt32(number[0].ToString()) > 1)
                {
                    suta = "sute";
                    n3 = Word(Convert.ToInt32(number[0].ToString()));
                }

                return n3 + " " + suta + " " + n2 + (n1 == string.Empty ? string.Empty : " si " + n1) + " " + singular;
            }
        }
        #endregion

    }
}