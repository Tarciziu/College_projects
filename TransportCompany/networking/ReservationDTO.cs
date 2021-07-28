using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace network.dto
{
    [Serializable]
    public class ReservationDTO
    {
        public String clientName { get; set; }
        public int reservedSeat { get; set; }
        public RouteDTO route { get; set; }
        public long Id { get; set; }

        public ReservationDTO(long id, String clientName, int reservedSeat, RouteDTO route)
        {
            this.Id = id;
            this.clientName = clientName;
            this.reservedSeat = reservedSeat;
            this.route = route;
        }
    }
}
