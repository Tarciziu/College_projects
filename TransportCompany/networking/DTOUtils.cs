using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace network.dto
{
    using User = domain.User;
    using Route = domain.Route;
    using Reservation = domain.Reservation;
    class DTOUtils
    {
        public static User getFromDTO(UserDTO usdto)
        {
            string username = usdto.Id;
            string pw = usdto.password;
            return new User(username, pw);
        }

        public static UserDTO getDTO(User user)
        {
            string id = user.username;
            string pass = user.password;
            return new UserDTO(id, pass);
        }

        public static Route getFromDTO(RouteDTO rdto)
        {
            long id = rdto.Id;
            int avS = rdto.availableSeats;
            string dest = rdto.destination;
            DateTime dT = rdto.departureDateTime;
            return new Route(id, dest, dT, avS);
        }

        public static RouteDTO getDTO(Route route)
        {
            long id = route.Id;
            int avS = route.availableSeats;
            string dest = route.destination;
            DateTime dT = route.departureDateTime;
            return new RouteDTO(id, dest, dT, avS);
        }

        public static Reservation getFromDTO(ReservationDTO resdto)
        {
            long id = resdto.Id;
            string cn = resdto.clientName;
            Route route = getFromDTO(resdto.route);
            int resSeat = resdto.reservedSeat;
            return new Reservation(id, cn, resSeat, route);
        }

        public static ReservationDTO getDTO(Reservation res)
        {
            long id = res.Id;
            string cn = res.clientName;
            RouteDTO route = getDTO(res.route);
            int resSeat = res.reservedSeat;
            return new ReservationDTO(id, cn, resSeat, route);
        }

        public static IEnumerable<Route> getFromDTO(IEnumerable<RouteDTO> rdtos)
        {
            List<Route> routes = new List<Route>();
            foreach(RouteDTO rdto in rdtos)
                routes.Add(getFromDTO(rdto));
            return routes;
        }

        public static IEnumerable<Reservation> getFromDTO(IEnumerable<ReservationDTO> rdtos)
        {
            List<Reservation> reservations = new List<Reservation>();
            foreach (ReservationDTO rdto in rdtos)
                reservations.Add(getFromDTO(rdto));
            return reservations;
        }

        public static IEnumerable<ReservationDTO> getDTO(IEnumerable<Reservation> reservations)
        {
            List<ReservationDTO> reservationDTOs = new List<ReservationDTO>();
            foreach (Reservation res in reservations)
                reservationDTOs.Add(getDTO(res));
            return reservationDTOs;
        }

        public static IEnumerable<RouteDTO> getDTO(IEnumerable<Route> routes)
        {
            List<RouteDTO> routeDTOs = new List<RouteDTO>();
            foreach (Route route in routes)
                routeDTOs.Add(getDTO(route));
            return routeDTOs;
        }
    }
}
