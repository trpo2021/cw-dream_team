using System;
using System.IO;
using System.Linq;

namespace _mFile
{

    public class MoveFile
    {
        private static string _configFileName = "config.app";
        private static string _currentDirectory;
        public MoveFile()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
        }
        public bool IsWorkingDirectory()
        {
            return Directory.GetFiles(_currentDirectory).Contains(_configFileName);
        }
        public void CheckFile(FileInfo file)
        {
            string extension = ".html";
            string pagesDirectoryPath = "/pages";
            if (file.Extension == extension)
            {
                if (!Directory.Exists(pagesDirectoryPath))
                    Directory.CreateDirectory(pagesDirectoryPath);
                file.MoveTo(pagesDirectoryPath);
            }
            else
                throw new Exception("Invalid file");
        }
    }
}

