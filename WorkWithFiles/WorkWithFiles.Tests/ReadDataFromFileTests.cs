﻿using Xunit;
using System;
using System.IO;
using System.Threading.Tasks;
using _mFile;

namespace _mFileTests
{
    public class ReadDataFromFileTests
    {
        [Fact]
        public void isRightReading()
        {
            Task.Factory.StartNew(() =>
            {
                string _tPath = "test.txt";
                File.Create(_tPath);
                string[] data = { "abc", "abc" };
                string _expData = "abcabc";
                try
                {
                    StreamWriter _tObj = new StreamWriter(_tPath);
                    foreach (string _tEl in data)
                    {
                        _tObj.Write(_tEl);
                    }
                    _tObj.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }

                string _curData = "";
                try
                {
                    var _tObj = new ReadDataFromFile();
                    _tObj.SetData(_tPath);
                    _curData += _tObj.GetData();
                    File.Delete(_tPath);
                    Assert.Equal(_expData, _curData);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }

            });
        }
    }
}
