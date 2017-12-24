using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Controllers
{
    public class RoomsController_ : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /*
                // GET: api/Rooms -> deprecated
                public IQueryable<Room> GetRooms()
                {
                    return db.Rooms;
                }
        */

        // GET: api/Rooms
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
        /*
        public IList<RoomDetailsDto> AvailableRooms(DateTime checkin, DateTime checkout)
        {
             
        }
        private Room GetAvaibleRooms()
        {
            db.Rooms.Where()

            return 
        }*/


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