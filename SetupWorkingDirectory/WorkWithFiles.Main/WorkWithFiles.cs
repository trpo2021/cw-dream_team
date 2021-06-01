using System;
using System.IO;
using System.Linq;

namespace Files
{
    public class WorkWithFiles
    {
        private static string _configFileName = "config.yml";
        private static string _pagesDirName = "pages";
        private static string _appDirName = "app";
        public static string _currentDirectory;

        public WorkWithFiles()
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
                //здесь можно провести дополнительную конфигураци
            }
            return new DirectoryInfo(_currentDirectory);
        }
    }
}
