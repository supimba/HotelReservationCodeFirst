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
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Controllers
{
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
                Number = r.Number,
                Description = r.Description,
                Type = r.Type,
                Price = r.Price,
                HasTv = r.HasTv,
                HasHairDryer = r.HasHairDryer,
                Hotel = r.Hotel,
                Reservations = r.Reservations.ToList()
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
   
            //var reservations = db.Reservations.Where(r => r.StartDate >= endDate && startDate <= r.EndDate).SelectMany(r=>r.Rooms);
          // var reservedRooms = db.Rooms.Where(rr => rr.Reservations.Any(r => r.StartDate >= endDate && startDate <= r.EndDate));


             var reservedRooms = db.Rooms.Where(
                 rr => rr.Reservations.Any(r => r.StartDate >= endDate && startDate <= r.EndDate)); 


 
/*
             rooms.Select(r => new RoomDetailsDto()
             {
                 Number = r.Number,
                 Description = r.Description,
                 Type = r.Type,
                 Price = r.Price,
                 HasTv = r.HasTv,
                 HasHairDryer = r.HasHairDryer,
                 Hotel = r.Hotel,
                 Reservations = r.Reservations.ToList()

             }); 
             */


            return rooms ;

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