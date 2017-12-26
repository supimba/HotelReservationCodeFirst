using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Description;
using HotelReservationAPI.DTO;
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Controllers
{
    [RoutePrefix("api/Rooms")]
    public class RoomsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        /*
        // GET: api/Rooms 
        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }
        */
        public IList<RoomDetailsDto> GetRooms()
        {
            return db.Rooms.Select(r => new RoomDetailsDto()
            {
                IdRoom = r.IdRoom,
                Number = r.Number,
                Description = r.Description,
                Type = r.Type,
                Price = r.Price,
                HasTv = r.HasTv,
                HasHairDryer = r.HasHairDryer,
                Hotel = r.Hotel,
                Reservations = r.Reservations.ToList(),
                
            }).ToList();

        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

       //TODO Get avaible Rooms by date
         public IList<RoomDetailsDto> GetAvaibleRoomsByDate(DateTime startDate, DateTime endDate)
         {


            // Ref: https://stackoverflow.com/questions/5624614/get-a-list-of-elements-by-their-id-in-entity-framework

            var reservedRooms = db.Rooms.Where(
                rr => rr.Reservations.Any(r =>  (startDate >=  r.StartDate && startDate <= r.EndDate) ||
                                                (endDate > r.StartDate && endDate < r.EndDate) ||
                                                (startDate < r.StartDate && endDate> r.EndDate)
                                                ));

            var allPictures = db.Pictures; 
            var avaibleRoom = db.Rooms.Where(r => !reservedRooms.Contains(r)).Select(r => 
            
            new RoomDetailsDto()
             {
                IdRoom = r.IdRoom,
                 Number = r.Number,
                 Description = r.Description,
                 Type = r.Type,
                 Price = r.Price,
                 HasTv = r.HasTv,
                 HasHairDryer = r.HasHairDryer,
                 Hotel = r.Hotel,
                 Reservations = r.Reservations.ToList(),
                 Pictures = allPictures.Where(p => p.Room.IdRoom == r.IdRoom).Select(i => new PictureDto() { IdPicture = i.IdPicture, Url = i.Url }).ToList()
             }).ToList();             

            return avaibleRoom;

        }

        //TODO Get rooms by Hotel Location
        public IList<RoomDetailsDto> GetAvaibleRoomsByLocation(DateTime startDate, DateTime endDate, String location)
        {
            var reservedRooms = db.Rooms.Where(
                rr => rr.Reservations.Any(r => (startDate >= r.StartDate && startDate <= r.EndDate) ||
                                               (endDate > r.StartDate && endDate < r.EndDate) ||
                                               (startDate < r.StartDate && endDate > r.EndDate) 
                ) && rr.Hotel.Location.Equals(location)
                
                );

            var allPictures = db.Pictures;
            var avaibleRoom = db.Rooms.Where(r => !reservedRooms.Contains(r)).Select(r =>

                new RoomDetailsDto()
                {
                    IdRoom = r.IdRoom,
                    Number = r.Number,
                    Description = r.Description,
                    Type = r.Type,
                    Price = r.Price,
                    HasTv = r.HasTv,
                    HasHairDryer = r.HasHairDryer,
                    Location = r.Hotel.Location,
                    Hotel = r.Hotel,
                    Reservations = r.Reservations.ToList(),
                    Pictures = allPictures.Where(p => p.Room.IdRoom == r.IdRoom).Select(i => new PictureDto() { IdPicture = i.IdPicture, Url = i.Url }).ToList()
                }).ToList();


            return avaibleRoom;

        }

        //TODO Get rooms by characteristics
        [Route("characteristics")]
        public IList<RoomDetailsDto> GetAvaibleRoomsByCharacteristics(DateTime startDate, DateTime endDate, int category, bool hasWifi, bool hasParking, bool hasTv, bool hasHairDryer)
        {

            var charactHotel =
                db.Hotels.Where(h => h.Category == category && h.HasWifi == hasWifi && h.HasParking == hasParking); 

            //!charactHotel.Contains(rr.Hotel)

            var reservedRooms = db.Rooms.Where(
                rr => rr.Reservations.Any(r => ((startDate >= r.StartDate && startDate <= r.EndDate) ||
                                               (endDate > r.StartDate && endDate < r.EndDate) ||
                                               (startDate < r.StartDate && endDate > r.EndDate) )
                      ) 
            );

            // var charactRooms = reservedRooms.Where(rr => !reservedRooms.Contains(rr) && rr.HasTv == hasTv && rr.HasHairDryer == hasHairDryer );

            var allPictures = db.Pictures;

            var avaibleRoom = db.Rooms.Where(r => 
            !reservedRooms.Contains(r) && r.HasTv == hasTv && r.HasHairDryer == hasHairDryer &&
            r.Hotel.HasParking == hasParking && r.Hotel.HasWifi == hasWifi && r.Hotel.Category == category).Select( r=>

                new RoomDetailsDto()
                {
                    IdRoom = r.IdRoom,
                    Number = r.Number,
                    Description = r.Description,
                    Type = r.Type,
                    Price = r.Price,
                    HasTv = r.HasTv,
                    HasHairDryer = r.HasHairDryer,
                    Location = r.Hotel.Location,
                    Hotel = r.Hotel,
                    Reservations = r.Reservations.ToList(),
                    Pictures = allPictures.Where(p => p.Room.IdRoom == r.IdRoom).Select(i => new PictureDto() { IdPicture = i.IdPicture, Url = i.Url }).ToList()
                }).ToList();


            return avaibleRoom;

        }


        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.IdRoom)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.IdRoom }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.IdRoom == id) > 0;
        }
    }
}