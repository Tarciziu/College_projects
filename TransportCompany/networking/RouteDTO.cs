using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace network.dto
{
    [Serializable]
    public class RouteDTO
    {
        public long Id { get; set; }
        public string destination { get; set; }
        public DateTime departureDateTime { get; set; }
        public int availableSeats { get; set; }
        public RouteDTO(String destination, DateTime departureDateTime)
        {
            this.destination = destination;
            this.departureDateTime = departureDateTime;
            this.availableSeats = 18;
        }
        public RouteDTO(String destination, DateTime departureDateTime, int availableSeats)
        {
            this.destination = destination;
            this.departureDateTime = departureDateTime;
            this.availableSeats = availableSeats;
        }

        public RouteDTO(long id, String destination, DateTime departureDateTime, int availableSeats)
        {
            this.Id = id;
            this.destination = destination;
            this.departureDateTime = departureDateTime;
            this.availableSeats = availableSeats;
        }
    }
}
