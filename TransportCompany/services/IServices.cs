using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public interface IServices
    {
        void login(User user, IObserver client);
        void logout(User user, IObserver client);
        IEnumerable<Route> findAllRoutes();
        IEnumerable<Reservation> findReservationByRoute(Route route);
        void updateReservation(Reservation reservation);
    }
}
