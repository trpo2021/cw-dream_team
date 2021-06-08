using Xunit;
using _mFile;
using System.IO;
using System.Threading.Tasks;

namespace _mFileTests
{
    public class Tests
    {
        [Fact]
        public void IsRightDirStatusFromCheckFileFunction()
        {
            var obj = new MoveFile();
            bool _cur = obj.IsWorkingDirectory();
            Assert.False(_cur);
        }
        [Fact]
        public void IsRightWorkingPart1()
        {
            string _tDirPath = "pages";
            Directory.CreateDirectory(_tDirPath);
            string _tFileName = "Part1.html";
            var _tFile = new FileInfo(_tFileName);
            var _tObj = new MoveFile();
            Task.Factory.StartNew (() =>
            {
                _tObj.CheckFile(_tFile);
                string[] _tList = Directory.GetFiles(_tDirPath);
                bool _tFlag = false;
                if (_tList.Length > 0)
                {
                    _tFlag = true;
                }
                Assert.True(_tFlag);
            });
        }
        [Fact]
        public void IsRightWorkingPart2()
        {
            string _tDirPath = "pages";
            Directory.CreateDirectory(_tDirPath);
            string _tFileName = "Part1.html";
            var _tObj = new MoveFile();
            Task.Factory.StartNew(() =>
            {
                _tObj.CheckFile(new FileInfo(_tFileName));
                string[] _tList = Directory.GetFiles(_tDirPath);
                bool _tFlag = false;
                if (_tList.Length > 0)
                {
                    _tFlag = true;
                }
                Assert.True(_tFlag);
            });
        }
    }
}
