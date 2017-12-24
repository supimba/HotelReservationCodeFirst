using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HotelReservationAPI.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("HotelReservation")
        {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        // source: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application

        // TODO Update database : https://stackoverflow.com/questions/20968520/entity-framework-code-first-migration-fails-with-update-database-forces-unnecc
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Reservations)
                .WithMany(r => r.Rooms)
                .Map(t => t.MapLeftKey("RoomId")
                    .MapRightKey("ReservationId")
                    .ToTable("RoomsReservation"));

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Rooms).WithMany(r => r.Reservations)
                .Map(t => t.MapLeftKey("ReservationId")
                    .MapRightKey("RoomId")
                    .ToTable("RoomsReservation"));


        }

        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

    }
}