using System;
using MarkDownParser;
using _mFile;
using System.IO;

namespace _AppPageGen
{
    public class pagegen
    {
        public static void Main(string[] args)
        {
            string SiteName = "Your Blog";
            int _counter = 0;
            foreach (string Element in args)
            {
                if (Element == "--sitename")
                {
                    ++_counter;
                }
                if (_counter == 1)
                {
                    SiteName = Element;
                }
            }
            //Console.WriteLine(SiteName);
            string[] Files = Directory.GetFiles("pages/");
            foreach (string File in Files)
            {
                string _fileName = Path.GetFileNameWithoutExtension(File);
                var RDFF = new ReadDataFromFile();
                RDFF.SetData(File);
                string mdData = RDFF.GetData();
                string htmlData = mdParser.parse(mdData, SiteName);
                var _htmlParser = new ParseTo(htmlData, _fileName + ".html");
                _htmlParser.ToHTMLFile();
                var _moveFile = new MoveFile();
                var _bufForFile = new FileInfo(_fileName + ".html");
                _moveFile.CheckFile(_bufForFile);
            }
        }
    }
}
