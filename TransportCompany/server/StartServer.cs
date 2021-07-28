using network;
using persistence;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    public class StartServer
    {
        public static void Main(string[] args) {
            IUserRepository userRepo = new UserRepository();
            IRouteRepository routeRepo = new RouteRepository();
            IReservationRepository reservationRepo = new ReservationRepository();
            IServices service = new ServicesImpl(userRepo, routeRepo, reservationRepo);

            SerialServer server = new SerialServer("127.0.0.1", 1337, service);
            server.Start();
            Console.WriteLine("Server started ...");
            Console.ReadLine();
        }
    }
    public class SerialServer : ConcurrentServer
    {
        private IServices server;
        private ClientWorker worker;
        public SerialServer(string host, int port, IServices server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }

}
