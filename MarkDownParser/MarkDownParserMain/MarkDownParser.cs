using System;

namespace MarkDownParser
{
    public class MarkDownParser
    {
        public static String parse(String markDownText)
        {
            return "<html><body>" + markDownText + "</body></html>";
        }
    }
}