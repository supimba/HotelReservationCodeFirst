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
    public class ReservationsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

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
        public IHttpActionResult PostReservation(Reservation reservation)
        {
         

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            //check validation of room reservation
            if (checkRoomsGroupReservation(reservation))
                return BadRequest(ModelState);


            //  Room roomReservation; 
            // roomReservation = db.Rooms.FirstOrDefault(r => r.IdRoom == roomId); 


            List<Room> roomsList = new List<Room>();
            List<int> roomIdList = new List<int>();


            foreach (Room r in reservation.Rooms)
                roomIdList.Add(r.IdRoom);

            foreach (int id in roomIdList)
            {
                Room room;
                room = db.Rooms.Find(id);
                reservation.Rooms.Add(room);
            }
            

            db.Reservations.Add(reservation);
            db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservation.IdReservation }, reservation);
        }

        private bool checkRoomsGroupReservation(Reservation checkReservation)
        {
            if (checkReservation != null)
            {
                // get all reservations stored in DB
                foreach (Reservation rr in db.Reservations)
                {
                    // get all room reservations stored in DB
                    foreach (Room r in rr.Rooms)
                    {
                        // get all rooms choosen 
                        foreach (Room room in checkReservation.Rooms)
                        {
                            if (r.IdRoom == room.IdRoom)
                            {
                                if ((rr.StartDate >= checkReservation.EndDate) &&
                                    (checkReservation.StartDate <= rr.EndDate))
                                    return true;
                            }
                        }
                    }
                }


                return false;
            }

            throw new ArgumentNullException(nameof(checkReservation));
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