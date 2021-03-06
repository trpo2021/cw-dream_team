using System;
using Xunit;
using static MarkDownParser.MarkDownParser;

namespace MarkDownParserTests
{
    public class Tests
    {
        public string parse(string markdown)
        {
            string result = MarkDownParser.MarkDownParser.parse(markdown, "abc");
            result = result.Replace("<head><title>abc</title></head>", "");
            return result;
        }

        [Fact]
        public void HeaderTest()
        {
            string result = MarkDownParser.MarkDownParser.parse("", "abcd");
            Assert.Equal("<html><head><title>abcd</title></head><body></body></html>", result);
        }

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

        [Fact]
        public void HorizontalRulesTest()
        {
            Assert.Equal("<html><body><hr /></body></html>", parse("* * *"));
            Assert.Equal("<html><body><hr /></body></html>", parse("***"));
            Assert.Equal("<html><body><hr /></body></html>", parse("*****"));
            Assert.Equal("<html><body><hr /></body></html>", parse("- - -"));
            Assert.Equal("<html><body><hr /></body></html>", parse("---------------------------------------"));
        }

        [Fact]
        public void LinksTest()
        {
            string result = parse("This is [an example](http://example.com/ \"Title\") inline link.");
            Assert.Equal("<html><body>This is <a href=\"http://example.com/\" title=\"Title\">" +
                "an example</a> inline link.</body></html>", result);
            result = parse("[This link](http://example.net/) has no title attribute.");
            Assert.Equal("<html><body><a href=\"http://example.net/\">This link</a>" +
                " has no title attribute.</body></html>", result);
            result = parse("<http://example.com/>");
            Assert.Equal("<html><body><a href=\"http://example.com/\">http://example.com/</a></body></html>", result);
        }

        [Fact]
        public void TextModificationsTest()
        {
            Assert.Equal("<html><body><strong>abc</strong></body></html>", parse("**abc**"));
            Assert.Equal("<html><body><em>abc</em></body></html>", parse("_abc_"));
            Assert.Equal("<html><body><strike>abc</strike></body></html>", parse("~~abc~~"));
        }

        [Fact]
        public void ImagesTest()
        {
            string result = parse("![Alt text](/path/to/img.jpg)");
            Assert.Equal("<html><body><img src=\"/path/to/img.jpg\" alt=\"Alt text\" /></body></html>", result);
            result = parse("![Alt text](/path/to/img.jpg \"Optional title\")");
            Assert.Equal("<html><body><img src=\"/path/to/img.jpg\" alt=\"Alt text\" " + 
                "title=\"Optional title\" /></body></html>", result);
        }
    }
}
