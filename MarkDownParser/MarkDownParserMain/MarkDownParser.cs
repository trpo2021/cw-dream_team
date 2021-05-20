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
            result = parseBlockquote(result);
            return "<html><body>" + result + "</body></html>";
        }

        private static String escapeSpecialSymbols(String markDownText)
        {
            return markDownText
                 .Replace("&", "&amp;")
                 .Replace("<", "&lt;");
        }

        private static String parseHeaders(String markDownText)
        {
            string result = markDownText;
            result = parseSharpsHeaders(result);
            result = parseUnderlineHeaders(result);
            result = parseBlockquote(result);
            return result;
        }

        private static String parseSharpsHeaders(String markDownText)
        {
            string result = markDownText;
            for (int i = 1; i <= 6; i++)
            {
                string pattern = "^#{" + i + "}\\s(.+(?<!\\s#{" + i + "}))(?:\\s#{" + i + "})?$";
                result = Regex.Replace(result, pattern, "<h" + i + ">$1</h" + i + ">", RegexOptions.Multiline);
            }
            return result;
        }

        private static String parseUnderlineHeaders(String markDownText)
        {
            string result = markDownText;
            result = Regex.Replace(result, "^(.+)\\n=+$", "<h1>$1</h1>", RegexOptions.Multiline);
            result = Regex.Replace(result, "^(.+)\\n-+$", "<h2>$1</h2>", RegexOptions.Multiline);
            return result;
        }

        private static String parseBlockquote(String markDownText)
        {
            string result = markDownText;
            result = Regex.Replace(result, ">\\s(.+)$", "<blockquote>$1</blockquote>", RegexOptions.Multiline);
            result = result.Replace("</blockquote>\n<blockquote>", "\n");
            return result;
        }
    }
}