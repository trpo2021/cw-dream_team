using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System;
using System.IO;

namespace httpserver
{
    // Простенький клиент
    class Client
    {
        // Процедура, отображающая страницу с заданным кодом ошибки
        private void SendErrorToServer(TcpClient Client, int ErrorCode)
        {
            string ErrorCodeStr = ErrorCode.ToString() + " " + ((HttpStatusCode)ErrorCode).ToString();

            // Код страницы с ошибкой
            string Html = "<html><body><h1>" + ErrorCodeStr + "</h1></body></html>";

            string Str = "HTTP/1.1 " + ErrorCodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;

            byte[] Buffer = Encoding.ASCII.GetBytes(Str);

            Client.GetStream().Write(Buffer, 0, Buffer.Length);

            Client.Close();
        }

        // Функция, принимающая запрос и возвращающая его в виде строки
        private string GetRequest(TcpClient Client, int CountOfBytes, byte[] Buff)
        {
            string Request = "";

            while ((CountOfBytes = Client.GetStream().Read(Buff, 0, Buff.Length)) > 0)
            {
                // Преобразуем эти данные в строку и добавим ее к Request
                Request += Encoding.ASCII.GetString(Buff, 0, CountOfBytes);

                if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                {
                    break;
                }
            }

            return Request;
        }

        // Функция, отбрасывающая лишнее из запроса и возвращающая объект класса Match
        private Match AdaptationRequest(TcpClient Client, string Request)
        {
            // Отсекаем все переменные Get - запроса
            Match RequestedMatch = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");

            // Если запрос пошел по говну
            if (RequestedMatch == Match.Empty)
            {
                // Передаем ошибку 400 - херовые запрос (неверный)
                SendErrorToServer(Client, 400);
            }

            return RequestedMatch;
        }

        private string GetUri(TcpClient Client, Match rMatch)
        {
            // Получаем строку запроса
            string rUri = rMatch.Groups[1].Value;

            // Преобразуем ее к норм. виду
            rUri = Uri.UnescapeDataString(rUri);

            //Если там есть двоеточие, то выдадим ошибку 400
            if (rUri.IndexOf("..") >= 0)
            {
                SendErrorToServer(Client, 400);
            }

            // если заканчивает на "/", то передаем файл
            if (rUri.EndsWith("/"))
            {
                rUri += "index.html";
            }

            return rUri;
        }

        // Процедура, передающая данные в клиент
        private void SendDataToClient(TcpClient Client, FileStream FS, int CountOfBytes, byte[] Buff)
        {
            // Пока не дошли до конца файла 
            while (FS.Position < FS.Length)
            {
                // Читаем данные из файла 
                CountOfBytes = FS.Read(Buff, 0, Buff.Length);

                // Передаем эти данные клиенту
                Client.GetStream().Write(Buff, 0, CountOfBytes);
            }

        }

        // Функция определяющая и возвращающая ContentType по расширению файла
        private string GetContentType(string Extension)
        {
            switch (Extension)
            {
                case ".htm":
                case ".html":
                    return "text/html";
                case ".css":
                    return "text/stylesheet";
                default:
                    if (Extension.Length > 1)
                    {
                        return "application/" + Extension.Substring(1);
                    }
                    else
                    {
                        return "application/unknown";
                    }
            }
        }

        // Конструктор класса - функция, которая принимает клиента от TcpListener
        public Client(TcpClient Client)
        {
            // Строка в которой будет хранится HTTP запрос
            string Request = "";

            // Буфер для хранения данных принятых от юзера
            byte[] Buff = new byte[1024];

            // Храним здесь кол-во байт принятых от клиента, по умолчанию = 0
            int CountOfBytes = 0;

            // Получаем запрос от сервера
            Request += GetRequest(Client, CountOfBytes, Buff);

            // Преобразуем его, отбрасывая все лишнее
            Match RequestedMatch = AdaptationRequest(Client, Request);

            // Получаем строку запроса
            string RequestUri = GetUri(Client, RequestedMatch);

            //Работа с файлами
            string FilePath = "pages/" + RequestUri;

            // Если в папке pages нет такого файла, то отправляем 404 ошибОЧКУ
            if (!File.Exists(FilePath))
            {
                SendErrorToServer(Client, 404);
                return;
            }

            // Получаем расширение файла из строки запроса 
            string Extension = RequestUri.Substring(RequestUri.LastIndexOf('.'));

            // Строка для определения типа содержимого
            string ContentType = GetContentType(Extension);

            FileStream FS;

            // Открываем файл, ловим ошибки юзая try&catch
            try
            {
                FS = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                SendErrorToServer(Client, 500);

                return;
            }

            // Посылаем заголовочки
            string Headers = "HTTP/1.1 200 OK\nContentType: " + ContentType + "\nContent-Length:" + FS.Length + "\n\n";

            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);

            Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);

            SendDataToClient(Client, FS, CountOfBytes, Buff);

            FS.Close();
            Client.Close();
        }
    }
}
