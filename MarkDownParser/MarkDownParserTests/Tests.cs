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
            Assert.Equal("<html><body>Some text in HTML</body></html>", result);
        }
        [Fact]
        public void EmptyHTMLTest()
        {
            String result = parse("");
            Assert.Equal("<html><body></body></html>", result);
        }
        [Fact]
        public void EscapeAmpTest()
        {
            String result = parse("AT&T");
            Assert.Equal("<html><body>AT&amp;T</body></html>", result);
        }
        [Fact]
        public void EscapeLtTest()
        {
            String result = parse("2 < 5");
            Assert.Equal("<html><body>2 &lt; 5</body></html>", result);
        }
        [Fact]
        public void HeadersTest()
        {
            for (int i = 1; i <= 6; i++)
            {
                string sharps = "";
                for (int j = 0; j < i; j++) sharps += "#";
                String result = parse(sharps + " Some text");
                Assert.Equal("<html><body><h" + i + ">Some text</h" + i + "></body></html>", result);
                result = parse(sharps + " Some text " + sharps);
                Assert.Equal("<html><body><h" + i + ">Some text</h" + i + "></body></html>", result);
            }
        }
    }
}