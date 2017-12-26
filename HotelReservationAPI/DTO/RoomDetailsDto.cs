using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelReservationAPI.DTO;

namespace HotelReservationAPI.Models
{
    // To avoid the problem with the circular object graph
    public class RoomDetailsDto
    {

        public int IdRoom { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }
        public bool HasTv { get; set; }
        public bool HasHairDryer { get; set; }
        public string Location { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<PictureDto> Pictures { get; set; }
    }
}