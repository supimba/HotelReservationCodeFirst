using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Controllers
{
    public class ReservationsController_ : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

/*
        // GET: api/Reservations -> deprecated
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations;
        }
*/

        public IList<ReservationDetailsDto> GetReservations()
        {
            return db.Reservations.Select(r => new ReservationDetailsDto()
            {
                NbrPerson = r.NbrPerson,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Name = r.Name,
                Firstname = r.Firstname,
                Rooms = r.Rooms.ToList()

            }).ToList();
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


        //TODO Complete method to add new Reservations
        
        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(Reservation reservation)
        {

            //  Room roomReservation; 
            // roomReservation = db.Rooms.FirstOrDefault(r => r.IdRoom == roomId); 

            List<Room> roomsList= new List<Room>();
            List<int> roomIdList = new List<int>();


            foreach(Room r in reservation.Rooms)
                roomIdList.Add(r.IdRoom);

            

            foreach(int id in roomIdList)
            {
                Room room = db.Rooms.Find(id); 
                reservation.Rooms.Add(room);
            }
            //roomsList.Add();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            db.Reservations.Add(reservation);
            db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservation.IdReservation }, reservation);
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

        // this method return all avaible rooms between 2 dates and location
        private IList<Room> GetAvaibleRoomsByDate(DateTime startDate, DateTime endDate, string location)
        {
            IList<Room> avaibleRooms = new List<Room>();

            IEnumerable<Reservation> queryReservations  = db.Reservations.Where(r => r.StartDate == startDate && r.EndDate == endDate).ToList();

            foreach (Reservation r in queryReservations)
            foreach (var rRoom in r.Rooms)
                avaibleRooms.Add(rRoom);
           // queryReservations.Select(r => r.Rooms).ToList(); 

            /*var queryReservations  = 
               (from r in db.Reservations
                where r.StartDate == startDate
                where r.EndDate == endDate
                select r.Rooms
                    ).ToList();
                    */

            return avaibleRooms; 
                
            //avaibleRooms = db.Reservations.Where(r => r.StartDate = startDate)


  
        }
    }
}