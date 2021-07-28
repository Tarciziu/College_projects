using domain;
using network.dto;
using System;
using System.Collections.Generic;

namespace network.protocol
{
	using UserDTO = network.dto.UserDTO;


	public interface Request 
	{
	}


	[Serializable]
	public class LoginRequest : Request
	{
		private UserDTO user;

		public LoginRequest(UserDTO user)
		{
			this.user = user;
		}

        public virtual UserDTO User
        {
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class LogoutRequest : Request
	{
		private UserDTO user;

		public LogoutRequest(UserDTO user)
		{
			this.user = user;
		}

		public virtual UserDTO User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class GetAllRoutesRequest : Request
	{

		public GetAllRoutesRequest()
		{
		}
	}

	[Serializable]
	public class GetAllReservationsRequest : Request
	{
		private RouteDTO route;

		public GetAllReservationsRequest(RouteDTO route)
		{
			this.route = route;
		}

		public virtual RouteDTO Route
		{
			get
			{
				return route;
			}
		}
	}

	[Serializable]
	public class NewReservationRequest : Request
	{
		private ReservationDTO reservation;
		public NewReservationRequest(ReservationDTO reservation)
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