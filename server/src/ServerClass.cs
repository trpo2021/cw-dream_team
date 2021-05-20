using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HTTP_Server
{
    // Простенький сервер
    class Server
    {
        // Процедура потока, создает экземпляр класса Client
        static void ClientThread(object StateInfo)
        {
            new Client((TcpClient)StateInfo);
        }

        TcpListener Listener; // Объект, которые принимает TCP - юзеров

        // Конструктор класса - Запускаем наш сервер
        public Server(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port);

            Listener.Start(); // Непосредственно запуск этого сервера

            // Короче чтобы постоянно обрабатывать TCP-юзеров, то пустим бесконечный цикл
            while (true)
            {
                // Принимаем юзера
                TcpClient Client = Listener.AcceptTcpClient();

                // Создаем поток
                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));

                // Запускаем поток, передавая ему юзера
                Thread.Start(Client);
            }
        }

        // Деструктор класса - Выключаем сервак
        ~Server()
        {
            // Тип если был создан такой объект, то мы его вырубаем
            if (Listener != null)
            {
                Listener.Stop();
            }
        }
    }
}
