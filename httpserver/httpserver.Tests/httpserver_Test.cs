using Xunit;
using httpserver;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace httpserver_Tests
{
    public class TestsForServer
    {
        [Fact]
        public void IsServerStartedWhenItsFalse()
        {
            bool isStarted = new Server(80).Started;
            Assert.False(isStarted);
        }

        [Fact]
        public void IsRightPortWhatSettedInClassConstructor()
        {
            int portForTest = 144;
            Assert.Equal(portForTest, new Server(portForTest).Port);
        }

        [Fact]
        public void isServerStarted()
        {
            var serv = new Server(80);
            Task.Factory.StartNew(() =>
            { 
                serv.Start();
                bool isStarted = serv.Started;
                Assert.True(isStarted); 
            });
        }

        [Fact]
        public void CheckPortWhenServerStart()
        {
            int expPort = 80;
            var serv = new Server(expPort);
            Task.Factory.StartNew(() =>
            {
                serv.Start();
                int curPort = serv.Port;
                Assert.Equal(expPort, curPort);
            });
        }

        [Fact]
        public void IsServerStoppedByStopMethod()
        {
            var server = new Server(80);
            Task.Factory.StartNew(() =>
            {
                server.Start(); 
                if (server.Started)
                {
                    server.Stop();
                    Assert.False(server.Started);
                }
            });
        }

        [Fact]
        public void CheckResponseOnGetRequest()
        {
            var server = new Server(80);
            Task.Factory.StartNew (() =>
            {
                server.Start();
                WebRequest __getRequest = WebRequest.Create("127.0.0.1/pages/index.htmlhgjfhkflhl");
                WebResponse __curResponse = __getRequest.GetResponse();
                Stream __stream = __curResponse.GetResponseStream();
                StreamReader __streamReader = new StreamReader(__stream);
                string __currentReponse = __streamReader.ReadToEnd();
                // Получаем пусто, т.к. нет такого файла 
                string __expResponse = "";
                Assert.Equal(__expResponse, __currentReponse);
                server.Stop();
            });
        }
    }
}
