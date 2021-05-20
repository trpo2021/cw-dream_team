using System;

namespace MarkDownParser
{
    public class MarkDownParser
    {
        public static String parse(String markDownText)
        {
            string result = escapeSpecialSymbols(markDownText);
            return "<html><body>" + result + "</body></html>";
        }

        public static String escapeSpecialSymbols(String markDownText)
        {
            return markDownText
                 .Replace("&", "&amp;")
                 .Replace("<", "&lt;");
        }
    }
}