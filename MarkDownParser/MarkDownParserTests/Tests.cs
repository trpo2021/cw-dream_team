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
        public void SharpsHeadersTest()
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

        [Fact]
        public void UnderlineHeadersTest()
        {
            string result = parse("Some text\n=========");
            Assert.Equal("<html><body><h1>Some text</h1></body></html>", result);
            result = parse("Some text\n---------");
            Assert.Equal("<html><body><h2>Some text</h2></body></html>", result);
        }

        [Fact]
        public void BlockquoteTest()
        {
            string result = parse("> Some quote\n> This is same quote\n\n> This is other");
            Assert.Equal("<html><body><blockquote>Some quote<br>This is same quote</blockquote>" +
                "<br><blockquote>This is other</blockquote></body></html>", result);
        }

        [Fact]
        public void InnerBlockquoteTest()
        {
            string result = parse("> Some quote\n> > This is inner quote\n> This is not");
            Assert.Equal("<html><body><blockquote>Some quote<br><blockquote>This is inner quote</blockquote>" +
                "This is not</blockquote></body></html>", result);
        }

        [Fact]
        public void UnorderedListTest()
        {
            string result = parse("* Some\n* List\n* Here\n\n+ Another\n+ List");
            Assert.Equal("<html><body><ul><li>Some</li><li>List</li><li>Here</li></ul><br>" + 
                "<ul><li>Another</li><li>List</li></ul></body></html>", result);
        }

        [Fact]
        public void OrderedListTest()
        {
            string result = parse("1. Some\n2. Ordered\n3. List\n\n3. List\n2. Too");
            Assert.Equal("<html><body><ol><li>Some</li><li>Ordered</li><li>List</li></ol><br>" +
                "<ol><li>List</li><li>Too</li></ol></body></html>", result);
        }

        [Fact]
        public void OrderedListEscapeTest()
        {
            string result = parse("1984\\. Book of life");
            Assert.Equal("<html><body>1984. Book of life</body></html>", result);
        }
    }
}
