using System.IO;
using System.Linq;

namespace _mFile
{
    public class SetupDir
    {
        private static string _configFileName = "config.yml";
        private static string _pagesDirName = "pages";
        private static string _appDirName = "app";
        public static string _currentDirectory;

        public SetupDir()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
        }

        public bool IsWorkingDirectory()
        {
            if (Directory.GetFiles(_currentDirectory).Contains(_configFileName) && Directory.GetDirectories(_currentDirectory).Contains("pages") && Directory.GetDirectories(_currentDirectory).Contains("app"))
            {
                return true;
            }
            return false;
        }

        public DirectoryInfo SetupWorkingDirectory()
        {
            if (!IsWorkingDirectory())
            {
                File.Create(_configFileName);
                Directory.CreateDirectory(_pagesDirName);
                Directory.CreateDirectory(_appDirName);
            }
            return new DirectoryInfo(_currentDirectory);
        }
    }
}
