using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class Route :Entity
    {
        public long Id { get; set; }
        public string destination { get; set; }
        public DateTime departureDateTime { get; set; }
        public int availableSeats { get; set; }
        public Route(String destination, DateTime departureDateTime)
        {
            this.destination = destination;
            this.departureDateTime = departureDateTime;
            this.availableSeats = 18;
        }
        public Route(String destination, DateTime departureDateTime, int availableSeats)
        {
            this.destination = destination;
            this.departureDateTime = departureDateTime;
            this.availableSeats = availableSeats;
        }

        public Route(long id,String destination, DateTime departureDateTime, int availableSeats)
        {
            this.Id = id;
            this.destination = destination;
            this.departureDateTime = departureDateTime;
            this.availableSeats = availableSeats;
        }

        public override string ToString()
        {
            return "Destination: " + destination + " | departureDateTime: " + departureDateTime + 
                " | availableSeats: " + availableSeats;
        }
    }
}
