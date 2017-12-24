using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelReservationAPI.Models
{
    public class Reservation
    {
        [Key]
        public int IdReservation { get; set; }
        [Required]
        public string NbrPerson { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        
        public string Name { get; set; }
        
        public string Firstname { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}