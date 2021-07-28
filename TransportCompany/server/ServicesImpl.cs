using domain;
using persistence;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class ServicesImpl : IServices
    {
        private IUserRepository userRepository;
        private IRouteRepository routeRepository;
        private IReservationRepository reservationRepository;
        private readonly IDictionary<String, IObserver> loggedClients;

        public ServicesImpl(IUserRepository userRepository, IRouteRepository routeRepository, IReservationRepository reservationRepository)
        {
            this.userRepository = userRepository;
            this.routeRepository = routeRepository;
            this.reservationRepository = reservationRepository;
            loggedClients = new Dictionary<String, IObserver>();
        }

        public IEnumerable<Route> findAllRoutes()
        {
            return routeRepository.findAll();
        }

        public IEnumerable<Reservation> findReservationByRoute(Route route)
        {
            return reservationRepository.findByRoute(route);
        }

        public void login(User user, IObserver client)
        {
            User userR = userRepository.findByUsername(user.username);
            if (userR != null)
            {
                if (loggedClients.ContainsKey(user.username))
                    throw new Exception("User already logged in.");
                loggedClients.Add(user.username, client);
            }
            else
                throw new Exception("Authentication failed.");
        }

        public void logout(User user, IObserver client)
        {
            IObserver localClient = loggedClients[user.username];
            loggedClients.Remove(user.username);
            if (localClient == null)
                throw new Exception("User " + user.username + " is not logged in.");
        }

        public void updateReservation(Reservation reservation)
        {
            Route route = routeRepository.findOne(reservation.route.Id);
            if (reservation.clientName=="-")
            {
                route.availableSeats = route.availableSeats + 1;
            }
            else
            {
                route.availableSeats = route.availableSeats - 1;
            }
            routeRepository.update(route);
            reservationRepository.update(reservation);
            foreach (IObserver obs in loggedClients.Values.ToList())
            {
                obs.notifyNewReservation(reservation);
            }
        }
    }
}
