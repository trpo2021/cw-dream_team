using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace httpserver
{
    // Простенький сервер
    public class Server
    {
        // Объект, которые принимает TCP - юзеров
        private TcpListener Listener;

        // Флаг, хранящий в себе информацию о состоянии сервера
        private volatile bool fStarted = false;

        // Переменная для хранения информации о заданном пользователем Порте, по умолчанию этот порт равен 80 (IIS)
        private volatile int fPort = 80;

        // Процедура потока, создает экземпляр класса Client
        private static void ClientThread(object StateInfo)
        {
            new Client((TcpClient)StateInfo);
        }

        // Процедура, которая создает экземпляр класса Listener и запускает его работу
        private void NewListener(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start();
        }

        // Флаг, определяющий состояние сервера (True - Вкл., False - выкл.)
        public bool Started { get => fStarted; set => fStarted = value; }

        public int Port { get => fPort; set => fPort = value; }

        // Процедура, запускающая сервер
        public void Start()
        {
            NewListener(fPort);

            fStarted = true; // Отмечаем, что наш сервер запущен

            Thread thread = null;

            // Короче чтобы постоянно обрабатывать TCP-юзеров, то пустим бесконечный цикл
            while (fStarted)
            {
                // Принимаем юзера
                TcpClient Client = Listener.AcceptTcpClient();
                // Создаем поток
                thread = new Thread(new ParameterizedThreadStart(ClientThread));
                // Запускаем поток, передавая ему юзера
                thread.Start(Client);
            }

            if (thread != null)
            {
                thread.Abort();
            }
        }

        // Процедура, которая останавливает работу сервера
        public void Stop()
        {
            if (Started != false)
            {
                Started = false; // Отмечаем флагом, что наш сервер прекращает работу
            }
        }

        // Конструктор класса - Задаем порт
        public Server(int __port)
        {
            fPort = __port;
        }

        // Деструктор класса - Выключаем сервак
        ~Server()
        {
            Stop();
            // Тип если был создан такой объект, то мы его вырубаем
            if (Listener != null)
            {
                Listener.Stop();
            }
        }
    }
}
