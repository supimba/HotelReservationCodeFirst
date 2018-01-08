using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HotelReservationAPI.Models;

namespace HotelReservationAPI.DTO
{
    public class PictureDto
    {
        
        public int IdPicture { get; set; }
        public string Url { get; set; }
        public virtual Room Room { get; set; }
    }
}