using System;
using Xunit;
using static MarkDownParser.MarkDownParser;

namespace MarkDownParserTests
{
    public class Tests
    {
        [Fact]
        public void BasicHTMLTest()
        {
            string result = parse("Some text in HTML");
            Assert.Equal("<html>Some text in HTML</html>", result);
        }
        [Fact]
        public void EmptyHTMLTest()
        {
            String result = parse("");
            Assert.Equal("<html></html>", result);
        }
    }
}
