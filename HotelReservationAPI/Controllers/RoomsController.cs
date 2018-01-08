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

        /// <summary>
        /// Get all rooms and convert it to RoomDetailsDto to get room dependencies
        /// </summary>
        /// <returns></returns>
        public IList<RoomDetailsDto> GetRooms()
        {
            var allPictures = db.Pictures;
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
                Pictures = allPictures.Where(p => p.Room.IdRoom == r.IdRoom).Select(i => new PictureDto() { IdPicture = i.IdPicture, Url = i.Url }).ToList()

            }).ToList();


        }

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IQueryable<RoomDetailsDto> GetRoom(int id)
        {
            var allPictures = db.Pictures;

            IQueryable<RoomDetailsDto> room = db.Rooms.Where(r=> r.IdRoom ==id).Select(r =>
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
                });
           

          
            return room;
        }

       /// <summary>
       /// Get avaible rooms by date range startdate & enddate
       /// </summary>
       /// <param name="startDate"></param>
       /// <param name="endDate"></param>
       /// <returns></returns>
         public IList<RoomDetailsDto> GetAvaibleRoomsByDate(DateTime startDate, DateTime endDate)
         {

            // Ref: https://stackoverflow.com/questions/5624614/get-a-list-of-elements-by-their-id-in-entity-framework

            var reservedRooms = db.Rooms.Where(
                rr => rr.Reservations.Any(r =>  ((startDate >=  r.StartDate) &&( startDate < r.EndDate)) ||
                                               ((endDate > r.StartDate) &&( endDate < r.EndDate) )||
                                                ((startDate < r.StartDate )&& (endDate> r.EndDate))
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
                 Location = r.Hotel.Location,
                Hotel = r.Hotel,
                 Reservations = r.Reservations.ToList(),
                 Pictures = allPictures.Where(p => p.Room.IdRoom == r.IdRoom).Select(i => new PictureDto() { IdPicture = i.IdPicture, Url = i.Url }).ToList()
             }).ToList();             

            return avaibleRoom;

        }

        /// <summary>
        /// Get avaible rooms by date and location
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public IList<RoomDetailsDto> GetAvaibleRoomsByLocation(DateTime startDate, DateTime endDate, String location)
        {
            // get any rooms reserved between date range startdate & endate
            var reservedRooms = db.Rooms.Where(
                rr => rr.Reservations.Any(r => ((startDate >= r.StartDate) && (startDate < r.EndDate)) ||
                                               ((endDate > r.StartDate) && (endDate < r.EndDate)) ||
                                               ((startDate < r.StartDate) && (endDate > r.EndDate))
                
                ) //&& rr.Hotel.Location.Equals(location)
                
                );

            var allPictures = db.Pictures;

            // get all rooms except the reserved rooms, !reservedRoom.Contains()
            var avaibleRoom = db.Rooms.Where(r => !reservedRooms.Contains(r) && r.Hotel.Location.Equals(location)).Select(r =>

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

 
        /// <summary>
        /// Get avaible rooms with some specific room & hotel characteristics
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="location"></param>
        /// <param name="category"></param>
        /// <param name="price"></param>
        /// <param name="hasWifi"></param>
        /// <param name="hasParking"></param>
        /// <param name="hasTv"></param>
        /// <param name="hasHairDryer"></param>
        /// <param name="roomType"></param>
        /// <returns></returns>
        [Route("characteristics")]
        public IList<RoomDetailsDto> GetAvaibleRoomsByCharacteristics(DateTime startDate, DateTime endDate, string location, int category, decimal price, bool hasWifi, bool hasParking, bool hasTv, bool hasHairDryer, int roomType)
        {
            IQueryable<Hotel> charactHotel;
            IList<RoomDetailsDto> avaibleRoom;

            // check if request contains location
            if (location == null)
                charactHotel = db.Hotels.Where(h =>
                    h.Category <= category && h.HasWifi == hasWifi && h.HasParking == hasParking);
            else
            {
                charactHotel = db.Hotels.Where(h =>
                    h.Category <= category && h.HasWifi == hasWifi && h.HasParking == hasParking && h.Location == location);
            }

            // get any rooms reserved between date range startdate & endate
            var reservedRooms = db.Rooms.Where(
                rr => rr.Reservations.Any(r => ((startDate >= r.StartDate) && (startDate < r.EndDate)) ||
                                               ((endDate > r.StartDate) && (endDate < r.EndDate)) ||
                                               ((startDate < r.StartDate) && (endDate > r.EndDate))
                ));

            // get all pictures to seed our rooms
            var allPictures = db.Pictures;

            // get avaible roooms without difference from type, double or single room
            if (roomType == 0)
            {
                // get all rooms except the reserved rooms, !reservedRoom.Contains()
                avaibleRoom = db.Rooms.Where(r =>
                    !reservedRooms.Contains(r) && r.HasTv == hasTv && r.HasHairDryer == hasHairDryer && r.Price <= price &&
                    charactHotel.Contains(r.Hotel)).Select(r =>

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

            }
            else { 
                // get avaible rooms with type
                avaibleRoom = db.Rooms.Where(r => 
                !reservedRooms.Contains(r) && r.HasTv == hasTv && r.HasHairDryer == hasHairDryer && r.Type == roomType && r.Price <= price&&
                charactHotel.Contains(r.Hotel)).Select( r=>

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
            }

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
        
        /// <summary>
        /// no used in project context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Not used in project context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.IdRoom == id) > 0;
        }
    }
}