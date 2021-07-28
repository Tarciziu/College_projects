using network.dto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace network.protocol
{
	using UserDTO = network.dto.UserDTO;
	using ReservationDTO = network.dto.ReservationDTO;

	public interface Response 
	{
	}

	[Serializable]
	public class OkResponse : Response
	{
		
	}

    [Serializable]
	public class ErrorResponse : Response
	{
		private string message;

		public ErrorResponse(string message)
		{
			this.message = message;
		}

		public virtual string Message
		{
			get
			{
				return message;
			}
		}
	}

	public interface UpdateResponse : Response
	{
	}

	[Serializable]
	public class GetAllRoutesResponse : Response
	{
		private IEnumerable<RouteDTO> routes;

		public GetAllRoutesResponse(IEnumerable<RouteDTO> routes)
        {
			this.routes = routes;
        }
		public virtual IEnumerable<RouteDTO> Routes
        {
            get
            {
				return routes;
            }
        }
	}
	[Serializable]
	public class GetAllReservationsResponse : Response
	{
		private IEnumerable<ReservationDTO> reservations;

		public GetAllReservationsResponse(IEnumerable<ReservationDTO> reservations)
		{
			this.reservations = reservations;
		}

		public virtual IEnumerable<ReservationDTO> Reservations
		{
			get
			{
				return reservations;
			}
		}
	}

	[Serializable]
	public class NewReservationResponse : UpdateResponse
	{
		private ReservationDTO reservation;
		public NewReservationResponse(ReservationDTO reservation)
		{
			this.reservation = reservation;
		}

		public virtual ReservationDTO Reservation
		{
			get
			{
				return reservation;
			}
		}
	}
}