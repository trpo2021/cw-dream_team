using System;
using System.IO;

namespace jekyll
{
    public class ToDocument
    {
        public string Data;
        public string NameFile; // включая расширение файла (заменить .txt на .html)

        public void ToHTMLFile()
        {
            string pathToFile = NameFile;
            try
            {
                using (FileStream file = new FileStream(pathToFile, FileMode.Append))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(Data);
                    file.Write(array, 0, array.Length);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
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
