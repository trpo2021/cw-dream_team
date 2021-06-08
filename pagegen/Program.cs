using _mFile;
using MarkDownParser;
using httpserver;
using System.IO;

namespace PageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int _cPort = 0;
            int Port = 80;
            string SiteName = "Blog";
            int _cSiteName = 0;
            bool _flagServer = false;
            foreach (string Element in args)
            {
                if (Element == "--sitename")
                {
                    ++_cSiteName;
                }
                if (_cSiteName == 1)
                {
                    SiteName = Element;
                    _cSiteName = 0;
                }
                if (Element == "--port")
                {
                    _flagServer = true;
                    ++_cPort;
                }
                if (_cPort == 1)
                {
                    Port = int.Parse(Element);
                    _cPort = 0;
                }
            }

            var _set = new SetupDir();
            _set.SetupWorkingDirectory();

            string[] _mdFiles = Directory.GetFiles("pages/", "*.txt");
            foreach(string _mdFile in _mdFiles)
            {
                var _rDataMD = new ReadDataFromFile();
                _rDataMD.SetData(_mdFile);
                string _htmlData = _rDataMD.GetData();

                string _fName = Path.GetFileNameWithoutExtension(_mdFile) + ".html";

                var _pTo = new ParseTo(_htmlData, _fName);
                _pTo.ToHTMLFile();

                var _mFileTo = new MoveFile();
                var _htmlFile = new FileInfo(_fName);
                _mFileTo.CheckFile(_htmlFile);
            }


            if (_flagServer)
            {
                var _server = new Server(Port);
                _server.Start();
            }
        }
    }
}
