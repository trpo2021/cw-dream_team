using System;
using System.IO;
using Xunit;
using jekyll;

namespace ToDocumentTests
{
    public class ToDocumentTest
    {
        [Fact]
        public void TestOfDataInDocument()
        {
            string pathToFile = "TestOfDataInDocument.html";
            string data = "<p>Hello World!</p>";

            ToDocument buffer = new ToDocument(data, pathToFile);
            buffer.ToHTMLFile();

            string buf = "";
             try
             {
                StreamReader file = new StreamReader(pathToFile);
                buf = file.ReadLine();
                file.Close();
             }
             catch (Exception e)
             {
                 Console.WriteLine("Exception: " + e.Message);
             }
             finally
             {
                 Console.WriteLine(buf);
             }

            FileInfo fl = new FileInfo(pathToFile);
            fl.Delete();

            Assert.Equal(data, buf);
        }
        
        [Fact]
        public void TestOfDataInDocumentMultipleTimes()
        {
            string pathToFile = "TestOfDataInDocumentMultipleTimess.html";

            string data = "<p>Hello World!</p>";

            ToDocument buffer = new ToDocument(data, pathToFile);
            buffer.ToHTMLFile();

            buffer.ToHTMLFile();

            string buf = "";
            try
            {
                StreamReader file = new StreamReader(pathToFile);
                buf = file.ReadLine();
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine(buf);
            }

            FileInfo fl = new FileInfo(pathToFile);
            fl.Delete();

            Assert.Equal("<p>Hello World!</p><p>Hello World!</p>", buf);
        }
    }
}
