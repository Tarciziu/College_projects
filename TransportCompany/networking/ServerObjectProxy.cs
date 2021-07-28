using network.dto;
using network.protocol;
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
using domain;

namespace network
{
	public class ServerProxy : IServices
	{
		private string host;
		private int port;

		private IObserver client;

		private NetworkStream stream;

		private IFormatter formatter;
		private TcpClient connection;

		private Queue<Response> responses;
		private volatile bool finished;
		private EventWaitHandle _waitHandle;
		public ServerProxy(string host, int port)
		{
			this.host = host;
			this.port = port;
			responses = new Queue<Response>();
		}

		public virtual void login(User user, IObserver client)
		{
			initializeConnection();
			UserDTO udto = DTOUtils.getDTO(user);
			sendRequest(new LoginRequest(udto));
			Response response = readResponse();
			if (response is OkResponse)
			{
				this.client = client;
				return;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				closeConnection();
				throw new Exception(err.Message);
			}
		}

		public virtual void logout(User user, IObserver client)
		{
			UserDTO udto = DTOUtils.getDTO(user);
			sendRequest(new LogoutRequest(udto));
			Response response = readResponse();
			closeConnection();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new Exception(err.Message);
			}
		}

		private void closeConnection()
		{
			finished = true;
			try
			{
				stream.Close();
				//output.close();
				connection.Close();
				_waitHandle.Close();
				client = null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}

		private void sendRequest(Request request)
		{
			try
			{
				formatter.Serialize(stream, request);
				stream.Flush();
			}
			catch (Exception e)
			{
				throw new Exception("Error sending object " + e);
			}

		}

		private Response readResponse()
		{
			Response response = null;
			try
			{
				_waitHandle.WaitOne();
				lock (responses)
				{
					//Monitor.Wait(responses); 
					response = responses.Dequeue();

				}


			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}
		private void initializeConnection()
		{
			try
			{
				connection = new TcpClient(host, port);
				stream = connection.GetStream();
				formatter = new BinaryFormatter();
				finished = false;
				_waitHandle = new AutoResetEvent(false);
				startReader();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
		private void startReader()
		{
			Thread tw = new Thread(run);
			tw.Start();
		}

		public virtual void run()
		{
			while (!finished)
			{
				try
				{
					object response = formatter.Deserialize(stream);
					Console.WriteLine("response received " + response);
					if (response is UpdateResponse)
					{
						handleUpdate((UpdateResponse)response);
					}
					else
					{

						lock (responses)
						{


							responses.Enqueue((Response)response);

						}
						_waitHandle.Set();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Reading error " + e);
				}

			}
		}

		private void handleUpdate(UpdateResponse update)
		{
			NewReservationResponse resUpd = (NewReservationResponse)update;
			Reservation reservation = DTOUtils.getFromDTO(resUpd.Reservation);
			Console.WriteLine("New reservation " + reservation);
			try
			{
				client.notifyNewReservation(reservation);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
		}

		public IEnumerable<Route> findAllRoutes()
        {
			Console.WriteLine("findAllRoutes PROXY start");
			sendRequest(new GetAllRoutesRequest());
			Console.WriteLine("findAllRoutes PROXY before response");
			Response response = readResponse();
			if(response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new Exception(err.Message);
			}
			Console.WriteLine("findAllRoutes PROXY after response");
			GetAllRoutesResponse resp = (GetAllRoutesResponse)response;
			Console.WriteLine("findAllRoutes PROXY after conversion");
			IEnumerable<RouteDTO> routesDTO = resp.Routes;
			IEnumerable<Route> routes = DTOUtils.getFromDTO(routesDTO);
			return routes;
		}

        public IEnumerable<Reservation> findReservationByRoute(Route route)
        {
			RouteDTO rdto = DTOUtils.getDTO(route);
			sendRequest(new GetAllReservationsRequest(rdto));
			Response response = readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new Exception(err.Message);
			}
			GetAllReservationsResponse resp = (GetAllReservationsResponse)response;
			IEnumerable<ReservationDTO> reservationsDTO = resp.Reservations;
			IEnumerable<Reservation> reservations = DTOUtils.getFromDTO(reservationsDTO);
			return reservations;
		}

        public void updateReservation(Reservation reservation)
        {
			ReservationDTO rdto = DTOUtils.getDTO(reservation);
			sendRequest(new NewReservationRequest(rdto));
			Response response = readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new Exception(err.Message);
			}
		}
    }

}
