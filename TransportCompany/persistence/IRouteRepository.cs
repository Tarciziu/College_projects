using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence
{
    public interface IRouteRepository :IRepository<Route>
    {
        IEnumerable<Route> findByDestination(String destination);
    }
}
