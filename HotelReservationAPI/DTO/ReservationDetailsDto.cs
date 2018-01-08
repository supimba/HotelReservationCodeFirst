using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelReservationAPI.Models
{
    public class ReservationDetailsDto
    {
        public int IdReservation { get; set; }
        public string NbrPerson { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}