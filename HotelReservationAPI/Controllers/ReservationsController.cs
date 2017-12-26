using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Controllers
{
    [RoutePrefix("api/Reservations")]
    public class ReservationsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        private Reservation _reservation =null; 

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
           

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.IdReservation)
            {
                return BadRequest();
            }

            db.Entry(reservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations

        /*
    [ResponseType(typeof(Reservation))]
    public IHttpActionResult PostReservation(Reservation reservation)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.Reservations.Add(reservation);
        db.SaveChanges();

        return CreatedAtRoute("DefaultApi", new { id = reservation.IdReservation }, reservation);
    }

*/

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        [Route("RoomReservation")]
        public IHttpActionResult PostRoomReservation(Reservation reservation)
        {
            this._reservation = reservation; 

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //check validation of room reservation
            if (CheckRoomReservation(reservation))
                return BadRequest(ModelState);

  
            int roomId = reservation.Rooms.First().IdRoom;
            Room room = db.Rooms.Find(roomId);

            reservation.Rooms.Clear();
            reservation.Rooms.Add(room);
            
            db.Reservations.Add(reservation);
            db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservation.IdReservation }, reservation);
        }

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(Reservation reservation)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            //check validation of room reservation
            if (CheckRoomReservation(reservation))
                   return BadRequest(ModelState);

            List<int> roomIdList = new List<int>();


            foreach (Room r in reservation.Rooms)
                roomIdList.Add(r.IdRoom);

            reservation.Rooms.Clear();
            int nbrePers = 0;
           
            foreach (int id in roomIdList)
            {
                Room room = db.Rooms.Find(id);
                nbrePers += room.Type;
                if (nbrePers > int.Parse(reservation.NbrPerson))
                    return BadRequest(ModelState);

                reservation.Rooms.Add(room);

            }
            

            db.Reservations.Add(reservation);
            db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservation.IdReservation }, reservation);
        }



        private bool CheckRoomReservation(Reservation reservation)
        {
            
            var startDate = reservation.StartDate;
            var endDate = reservation.EndDate; 
            var roomsInReservation = reservation.Rooms;

            //Add desired room IDs in a list
            List<int> roomIds = new List<int>();
            foreach (Room room in roomsInReservation)
            {
                roomIds.Add(room.IdRoom);
            }

            /*  var reservedRooms = db.Reservations.Where((r => (startDate >= r.StartDate && startDate <= r.EndDate) ||
                                                               (endDate > r.StartDate && endDate < r.EndDate) ||
                                                               (startDate < r.StartDate && endDate > r.EndDate)
               )).Select(r => r.Rooms);
               */
            //TODO nettoyer
            /*
                        var reservedRooms = db.Rooms.Where(
                            rr => rr.Reservations.Any(r => ((startDate >= r.StartDate && startDate <= r.EndDate) ||
                                                            (endDate > r.StartDate && endDate < r.EndDate) ||
                                                            (startDate < r.StartDate && endDate > r.EndDate))
                            )
                        );
                        */
            var reservedRooms = GetReservedRooms(startDate, endDate); 

            foreach (var id in roomIds)
                if (reservedRooms.Any(r => r.IdRoom == id))
                    return true; 

            return false;
        }

        private ICollection<Room> GetReservedRooms(DateTime startDate, DateTime endDate)
        {
            var reservedRooms = db.Rooms.Where(
                rr => rr.Reservations.Any(r => ((startDate >= r.StartDate && startDate <= r.EndDate) ||
                                                (endDate > r.StartDate && endDate < r.EndDate) ||
                                                (startDate < r.StartDate && endDate > r.EndDate))
                )
            ).ToList();

            return reservedRooms; 
        }


        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.IdReservation == id) > 0;
        }
    }
}