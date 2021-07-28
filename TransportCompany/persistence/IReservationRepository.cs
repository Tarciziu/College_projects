using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence
{
    public interface IReservationRepository :IRepository<Reservation>
    {
        IEnumerable<Reservation> findByClientName(String clientName);
        IEnumerable<Reservation> findByRoute(Route route);
    }
}
