using System;
using System.IO;

namespace _mFile
{
    public class ReadDataFromFile
    {
        private string Data = "";

        public void SetData(string _fileName)
        {
            StreamReader _rFile;
            try
            {
                _rFile = new StreamReader(_fileName);
                Data += _rFile.ReadToEnd();
                _rFile.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }

        public string GetData()
        {
            return Data;
        }
    }
}
