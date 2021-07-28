using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class Reservation : Entity
    {
        public string clientName { get; set; }
        public long Id { get; set; }
        public int reservedSeat { get; set; }
        public Route route { get; set; }
        
        public Reservation(long id, String clientName, int reservedSeat, Route route)
        {
            this.Id = id;
            this.clientName = clientName;
            this.reservedSeat = reservedSeat;
            this.route = route;
        }

        public Reservation(int reservedSeat, Route route)
        {
            this.clientName = "-";
            this.reservedSeat = reservedSeat;
            this.route = route;
        }
    }
}
