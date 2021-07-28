using domain;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class ClientCtrl : IObserver
    {
        public event EventHandler<UserEventArgs> updateEvent;
        private readonly IServices server;
        private User currentUser;

        public ClientCtrl(IServices server)
        {
            this.server = server;
            currentUser = null;
        }
        
        public void login(String username, String password)
        {
            User user = new User(username, password);
            server.login(user, this);
            Console.WriteLine("Login succeeded...");
            currentUser = user;
            Console.WriteLine("Current user {0}", user);
        }

        public void updateReservation(Reservation reservation)
        {
            server.updateReservation(reservation);
        }

        public IEnumerable<Route> findAllRoutes()
        {
            return server.findAllRoutes();
        }

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.logout(currentUser, this);
            currentUser = null;
        }

        public void notifyNewReservation(Reservation reservation)
        {
            Console.WriteLine("New reservation " + reservation.ToString());
            UserEventArgs userArgs = new UserEventArgs(UserEvent.NewReservation, reservation);
            OnUserEvent(userArgs);
        }

        protected virtual void OnUserEvent(UserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }

        public IEnumerable<Reservation> findReservationByRoute(Route route)
        {
            return server.findReservationByRoute(route);
        }
    }
}
