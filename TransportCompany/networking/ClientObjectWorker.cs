using domain;
using network.protocol;
using network.dto;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace network
{
    public class ClientWorker : IObserver
    {
        private IServices server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

		public ClientWorker(IServices server, TcpClient connection)
		{
			this.server = server;
			this.connection = connection;
			try
			{

				stream = connection.GetStream();
				formatter = new BinaryFormatter();
				connected = true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

        public void notifyNewReservation(Reservation reservation)
        {
			ReservationDTO resdto = DTOUtils.getDTO(reservation);
			Console.WriteLine("New reservation " + reservation);
            try
            {
				sendResponse(new NewReservationResponse(resdto));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        public virtual void run()
		{
			while (connected)
			{
				try
				{
					object request = formatter.Deserialize(stream);
					object response = handleRequest((Request)request);
					if (response != null)
					{
						sendResponse((Response)response);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.StackTrace);
				}

				try
				{
					Thread.Sleep(1000);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.StackTrace);
				}
			}
			try
			{
				stream.Close();
				connection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error " + e);
			}
		}

		private Response handleRequest(Request request)
		{
			Response response = null;
			if (request is LoginRequest)
			{
				Console.WriteLine("Login request...");
				LoginRequest logReq = (LoginRequest)request;
				UserDTO udto = logReq.User;
				User user = DTOUtils.getFromDTO(udto);
				try
				{
					lock (server)
					{
						server.login(user, this);
					}
					return new OkResponse();
				}
				catch (Exception e)
				{
					connected = false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is LogoutRequest)
			{
				Console.WriteLine("Logout request...");
				LogoutRequest logReq = (LogoutRequest)request;
				UserDTO udto = logReq.User;
				User user = DTOUtils.getFromDTO(udto);
				try
				{
					lock (server)
					{

						server.logout(user, this);
					}
					connected = false;
					return new OkResponse();

				}
				catch (Exception e)
				{
					return new ErrorResponse(e.Message);
				}
			}

			if (request is GetAllRoutesRequest)
            {
				Console.WriteLine("GetAllRoutes Request...");
				GetAllRoutesRequest getReq = (GetAllRoutesRequest)request;
                try
                {
					IEnumerable<Route> routes;
                    lock (server)
                    {
						routes = server.findAllRoutes();
                    }
					IEnumerable<RouteDTO> routeDTOs = DTOUtils.getDTO(routes);
					return new GetAllRoutesResponse(routeDTOs);
                }
				catch(Exception e)
                {
					return new ErrorResponse(e.Message);
                }
            }

			if (request is GetAllReservationsRequest)
            {
				Console.WriteLine("GetAllReservations Request...");
				GetAllReservationsRequest getReq = (GetAllReservationsRequest)request;
				try
				{
					IEnumerable<Reservation> reservations;
					lock (server)
					{
						Route route = DTOUtils.getFromDTO(getReq.Route);
						reservations = server.findReservationByRoute(route);
					}
					IEnumerable<ReservationDTO> reservationDTOs = DTOUtils.getDTO(reservations);
					return new GetAllReservationsResponse(reservationDTOs);
				}
				catch (Exception e)
				{
					return new ErrorResponse(e.Message);
				}
			}

			if (request is NewReservationRequest)
            {
				Console.WriteLine("GetAllReservations Request...");
				NewReservationRequest resReq = (NewReservationRequest)request;
				try
				{
					Reservation reservation;
					lock (server)
					{
						reservation = DTOUtils.getFromDTO(resReq.Reservation);
						server.updateReservation(reservation);
					}
					return new OkResponse();
				}
				catch (Exception e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			return response;
		}

		private void sendResponse(Response response)
		{
			Console.WriteLine("sending response " + response);
			formatter.Serialize(stream, response);
			stream.Flush();

		}
	}

}
