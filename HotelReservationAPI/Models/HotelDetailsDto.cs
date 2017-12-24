using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelReservationAPI.Models
{
    public class HotelDetailsDto
    {

   
        public string Name { get; set; }
        // ne pas oublier l'annotation
        public string Description { get; set; }

        public string Location { get; set; }
        public int Category { get; set; }

        // pou metre null -> bool? ou <Nullabale>
        public bool HasWifi { get; set; }
        public bool HasParking { get; set; }
      
        public string Phone { get; set; }
       
        public string Email { get; set; }
       
        public string Website { get; set; }

        public ICollection<Room> Rooms { get; set; }


    }
}