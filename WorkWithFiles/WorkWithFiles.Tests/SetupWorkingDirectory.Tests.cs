using Xunit;
using _mFile;
using System.Threading.Tasks;
using System.IO;

namespace _mFileTests
{
    public class WorkWithFilesTest
    {
        [Fact]
        public void IsRightSettupingDir()
        {
            var _tObj = new SetupDir();
            const string _tPages = "pages";
            const string _tApp = "app";
            const string _tCfg = "config.yml";
            string[] _tDirNames;
            string[] _tFileNames;
            int _tCounter = 0;
            int _expCounter = 3;
            Task.Factory.StartNew(() =>
            {
                _tObj.SetupWorkingDirectory();
                _tFileNames = Directory.GetFiles(Directory.GetCurrentDirectory());
                _tDirNames = Directory.GetDirectories(Directory.GetCurrentDirectory());
                foreach(string _tElNameFile in _tFileNames)
                {
                    if (_tElNameFile == _tCfg)
                    {
                        ++_tCounter;
                    }
                }
                foreach(string _tElNameDir in _tDirNames)
                {
                    if (_tElNameDir == _tPages)
                    {
                        ++_tCounter;
                    }
                    if (_tElNameDir == _tApp)
                    {
                        ++_tCounter;
                    }
                }
                Assert.Equal(_expCounter, _tCounter);
            });
        }
        [Fact]
        public void IsRightStatusFromFunction_IsSettupedDir()
        {
            var _tObj = new SetupDir();
            const string _tPages = "pages";
            const string _tApp = "app";
            const string _tCfg = "config.yml";
            bool _tCurrentStatus;
            Task.Factory.StartNew(() =>
            {
                Directory.CreateDirectory(_tPages);
                Directory.CreateDirectory(_tApp);
                File.Create(_tCfg);
                _tCurrentStatus = _tObj.IsWorkingDirectory();
                Assert.True(_tCurrentStatus);
            });
        }
    }
}
