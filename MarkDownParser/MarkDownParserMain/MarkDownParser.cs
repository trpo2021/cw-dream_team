using System; 
using System.Text.RegularExpressions;

namespace MarkDownParser
{
    public class mdParser
    {
        public static String parse(String markDownText, string siteName)
        {
            string result = markDownText;
            result = escapeSpecialSymbols(result);
            result = parseHeaders(result);
            result = parseBlockquote(result);
            result = parseHorizontalRules(result);
            result = parseUnorderedList(result);
            result = parseOrderedList(result);
            result = parseOrderedListEscape(result);
            result = parseImages(result);
            result = parseLinks(result);
            result = parseTextModificators(result);
            result = escapeBreakline(result);
            return "<html><head><title>" + siteName + "</title></head><body>" + result + "</body></html>";
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
            string newResult = "";
            while (newResult != result)
            {
                if (newResult != "") result = newResult;
                newResult = Regex.Replace(result, "> (.+)$", "<blockquote>$1</blockquote>", RegexOptions.Multiline);
                newResult = newResult.Replace("</blockquote>\n<blockquote>", "\n");
            }
            newResult = newResult.Replace("</blockquote>\n", "</blockquote>");
            result = newResult;
            return result;
        }

        private static String escapeBreakline(String markDownText)
        {
            return markDownText.Replace("\n", "<br>");
        }

        private static String parseUnorderedList(String markDownText)
        {
            string result = Regex.Replace(markDownText, "(?:\\+|\\*|-)\\s(.+)$", "<li>$1</li>", RegexOptions.Multiline);
            result = Regex.Replace(result, "((?:^<li>.+</li>\n?)+)", "<ul>$1</ul>", RegexOptions.Multiline);
            result = result.Replace("</li>\n", "</li>");
            return result;
        }

        private static String parseOrderedList(String markDownText)
        {
            string result = Regex.Replace(markDownText, "\\d+\\.\\s(.+)$", "<li>$1</li>", RegexOptions.Multiline);
            result = Regex.Replace(result, "((?:^<li>.+</li>\n?)+)", "<ol>$1</ol>", RegexOptions.Multiline);
            result = result.Replace("</li>\n", "</li>");
            return result;
        }

        private static String parseOrderedListEscape(String markDownText)
        {
            return Regex.Replace(markDownText, "(\\d+)\\\\\\.", "$1.");
        }

        private static String parseHorizontalRules(String markDownText)
        {
            return Regex.Replace(markDownText, "(?:(?:\\*|-) *){3,}", "<hr />");
        }

        private static String parseLinks(String markDownText)
        {
            string result = markDownText;
            result = Regex.Replace(result, "\\[(.+)\\]\\((.+)(?: (\\\".+\\\"))\\)", "<a href=\"$2\" title=$3>$1</a>");
            result = Regex.Replace(result, "\\[(.+)\\]\\((.+)\\)", "<a href=\"$2\">$1</a>");
            result = Regex.Replace(result, "&lt;((?:https?:/)?/[\\w./]*/)>", "<a href=\"$1\">$1</a>");
            return result;
        }

        private static String parseTextModificators(String markDownText)
        {
            string result = markDownText;
            result = Regex.Replace(result, "\\*\\*(.+)\\*\\*", "<strong>$1</strong>");
            result = Regex.Replace(result, "_(.+)_", "<em>$1</em>");
            result = Regex.Replace(result, "~~(.+)~~", "<strike>$1</strike>");
            return result;
        }

        private static String parseImages(String markDownText)
        {
            string result = markDownText;
            result = Regex.Replace(result, "!\\[(.+)\\]\\((.+) \\\"(.+)\\\"\\)", 
                "<img src=\"$2\" alt=\"$1\" title=\"$3\" />");
            result = Regex.Replace(result, "!\\[(.+)\\]\\((.+)\\)",
                "<img src=\"$2\" alt=\"$1\" />");
            return result;
        }
    }
}