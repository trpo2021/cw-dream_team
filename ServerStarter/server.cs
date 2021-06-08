using httpserver;

namespace _AppServer
{
    public class server
    {
        public static void Main(string[] args)
        {
            int Port = 80;
            int Counter = 0;
            foreach(string Element in args)
            {
                if (Counter == 1)
                {
                    Port = int.Parse(Element);
                }
                if (Element == "--port")
                {
                    ++Counter;
                }
            }
            var __server = new Server(Port);
            __server.Start();
        }
    }
}
