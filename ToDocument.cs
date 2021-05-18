using System;
using System.IO;

namespace jekyll
{
    public class ToDocument
    {
        public string Data;
        public string NameFile; // включая расширение файла (заменить .txt на .html)

        public void ToHTMLFile(ToDocument obj)
        {
            string pathToFile = obj.NameFile;
            try
            {
                StreamWriter file = new StreamWriter(pathToFile);
                file.WriteLine(obj.Data);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            // For test
            /* string buf = "";
             try
             {
                 StreamReader file = new StreamReader(pathToFile);
                 buf = file.ReadLine();
             }
             catch (Exception e)
             {
                 Console.WriteLine("Exception: " + e.Message);
             }
             finally
             {
                 Console.WriteLine(buf);
             }*/

        }

        public ToDocument(string data, string path)
        {
            Data = data;
            NameFile = path;
        }

        ~ToDocument()
        {
            Data = null;
            NameFile = null;
        }
    }
}