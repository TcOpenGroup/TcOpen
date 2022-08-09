using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoIo
{
    public static class ValidateFrameworkElement
    {
        private static char[] SpecialCharsName = { '`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '=', '+', '{', '[', ']', '}', '\\', '|', ';', ':', '\'', '"', ',', '<', '.', '>', '/', '?', '§', ' ' };
        private static char[] SpecialCharsLink = { '`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '=', '+', '{', '}', '\\', '|', ';', ':', '\'', '"', ',', '<', '.', '>', '/', '?', '§', ' ' };
        private static char[] SpecialCharsType = { '`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '=', '+', '{', '[', ']', '}', '\\', '|', ';', ':', '\'', '"', ',', '<', '>', '/', '?', '§', ' ' };
        private static char[] SpecialCharsArrayType = { '`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '=', '+', '{', '}', '\\', '|', ';', ':', '\'', '"', ',', '<', '>', '/', '?', '§' };

        private static char ReplaceChar = '_';

        private static string ReplacePlusMinus(string inStr)
        {
            string ret = inStr;
            if (!string.IsNullOrEmpty(ret))
            {
                if (ret.EndsWith("+"))
                    ret = ret.Substring(0, ret.Length - 1) + "_plus";
                if (ret.EndsWith("-"))
                    ret = ret.Substring(0, ret.Length - 1) + "_minus";
            }

            return ret;
        }

        private static string ReplaceSpecialChars(string inStr, char[] specialChars)
        {
            string ret = inStr;
            if (!string.IsNullOrEmpty(ret))
            {
                foreach (char specialChar in specialChars)
                {
                    ret = ret.Replace(specialChar, ReplaceChar);
                }
            }

            return ret;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string ReplaceKeywords(string inStr)
        {
            string ret = inStr;
            if (!string.IsNullOrEmpty(ret))
            {
                string[] s = ret.Split('[');
                if (s[0].ToUpper().EndsWith("LIMIT")
                    || s[0].ToUpper().EndsWith("AT")
                    || s[0].ToUpper().EndsWith("BYTE"))
                {
                    s[0] = s[0] + "_x";
                }
                if (s.Length > 1)
                {
                    ret = s[0];
                    for (int i = 1; i < s.Length; i++)
                    {
                        ret = ret + "[" + s[i];
                    }
                }
                else
                {
                    ret = s[0];
                }
            }
            return ret;
        }

        private static string ReplaceDoubleUnderscores(string inStr)
        {
            string ret = inStr;
            if (!string.IsNullOrEmpty(ret))
            {
                while (ret.Contains("__")) ret = ret.Replace("__", ReplaceChar.ToString());
                if (ret.EndsWith("_")) ret = ret.Substring(0, ret.Length - 1);
                if (ret.StartsWith("_")) ret = ret.Substring(1);
            }
            return ret;
        }
        private static string AddUnderscorePrefixIfStartsWithNumber(string inStr)
        {
            string ret = inStr;
            if (!string.IsNullOrEmpty(ret))
            {
                if (ret.Length > 0 && char.IsDigit(ret[0]))
                {
                    ret = ReplaceChar + ret;
                }
            }
            return ret;
        }
        private static string AddUnderscorePrefixIfContainsDotNetKeyword(string inStr)
        {
            string[] keywords = new string[]
                {
                    "abstract","add","alias","and","args ","as","ascending","async","await","base",
                    "bool","break","by","byte","case","catch","char","checked","class","const",
                    "continue","decimal","default","delegate","descending","do","double","dynamic","else","enum",
                    "equals","event","explicit","extern","false","finally","fixed","float","for","foreach",
                    "from","get","global","goto","group","if","implicit","in","init","int",
                    "interface","internal","into","is","join","let","lock","long","managed","nameof",
                    "namespace","new","nint","not","notnull","nuint","null","object","on","operator",
                    "or","orderby","out","override","params","partial","private","protected","public","readonly",
                    "record","ref","remove","return","sbyte","sealed","select","set","short","sizeof",
                    "stackalloc","static","string","struct","switch","this","throw","true","try","typeof",
                    "uint","ulong","unchecked","unmanaged","unsafe","ushort","using","value","var","virtual",
                    "void","volatile","when","where","while","with","yield"
                };

            string ret = inStr;
            if (!string.IsNullOrEmpty(ret))
            {
                foreach (string keyword in keywords)
                {
                    if (ret.Equals(keyword))
                    {
                        ret = ReplaceChar + ret;
                        break;
                    }
                }
            }
            return ret;
        }


        public static string Name(string name)
        {
            string ret = name;
            if (!string.IsNullOrEmpty(ret))
            {
                ret = ReplacePlusMinus(ret);
                ret = ReplaceSpecialChars(ret, SpecialCharsName);
                ret = ReplaceKeywords(ret);
                ret = RemoveDiacritics(ret);
                ret = ReplaceDoubleUnderscores(ret);
                ret = AddUnderscorePrefixIfStartsWithNumber(ret);
                ret = AddUnderscorePrefixIfContainsDotNetKeyword(ret);
            }
            return ret;
        }

    }

}
