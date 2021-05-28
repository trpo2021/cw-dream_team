using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace App
{
    public class app
    {
        private static string _configFileName = "config.yml";
        public static string _currentDirectory;
        static app()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
        }
        private static bool IsWorkingDirectory()
        {
            if (Directory.GetFiles(_currentDirectory).Contains(_configFileName) && Directory.GetDirectories(_currentDirectory).Contains("pages") && Directory.GetDirectories(_currentDirectory).Contains("app"))
            {
                return true;
            }
            return false;
        }
        public static DirectoryInfo SetupWorkingDirectory()
        {
            if (!IsWorkingDirectory())
            {
                File.Create(_configFileName);
                Directory.CreateDirectory("pages");
                Directory.CreateDirectory("app");
                //здесь можно провести дополнительную конфигураци
            }
            return new DirectoryInfo(_currentDirectory);
        }
    }
}