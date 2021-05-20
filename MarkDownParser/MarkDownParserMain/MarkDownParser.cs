using System;
using System.Text.RegularExpressions;

namespace MarkDownParser
{
    public class MarkDownParser
    {
        public static String parse(String markDownText)
        {
            string result = markDownText;
            result = escapeSpecialSymbols(result);
            result = parseHeaders(result);
            return "<html><body>" + result + "</body></html>";
        }

        public static String escapeSpecialSymbols(String markDownText)
        {
            return markDownText
                 .Replace("&", "&amp;")
                 .Replace("<", "&lt;");
        }

        public static String parseHeaders(String markDownText)
        {
            string result = markDownText;
            for (int i = 1; i <= 6; i++)
            {
                string pattern = "^#{" + i + "}\\s(.+(?<!\\s#{" + i + "}))(?:\\s#{" + i + "})?$";
                result = Regex.Replace(result, pattern, "<h" + i + ">$1</h" + i + ">", RegexOptions.Multiline);
            }
            return result;
        }
    }
}