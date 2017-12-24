namespace HotelReservationAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        IdHotel = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Location = c.String(maxLength: 50),
                        Category = c.Int(nullable: false),
                        HasWifi = c.Boolean(nullable: false),
                        HasParking = c.Boolean(nullable: false),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Website = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.IdHotel);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        IdRoom = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HasTv = c.Boolean(nullable: false),
                        HasHairDryer = c.Boolean(nullable: false),
                        Hotel_IdHotel = c.Int(),
                    })
                .PrimaryKey(t => t.IdRoom)
                .ForeignKey("dbo.Hotels", t => t.Hotel_IdHotel)
                .Index(t => t.Hotel_IdHotel);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        IdReservation = c.Int(nullable: false, identity: true),
                        NbrPerson = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Firstname = c.String(),
                    })
                .PrimaryKey(t => t.IdReservation);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        IdPicture = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Room_IdRoom = c.Int(),
                    })
                .PrimaryKey(t => t.IdPicture)
                .ForeignKey("dbo.Rooms", t => t.Room_IdRoom)
                .Index(t => t.Room_IdRoom);
            
            CreateTable(
                "dbo.ReservationRooms",
                c => new
                    {
                        Reservation_IdReservation = c.Int(nullable: false),
                        Room_IdRoom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reservation_IdReservation, t.Room_IdRoom })
                .ForeignKey("dbo.Reservations", t => t.Reservation_IdReservation, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_IdRoom, cascadeDelete: true)
                .Index(t => t.Reservation_IdReservation)
                .Index(t => t.Room_IdRoom);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "Room_IdRoom", "dbo.Rooms");
            DropForeignKey("dbo.ReservationRooms", "Room_IdRoom", "dbo.Rooms");
            DropForeignKey("dbo.ReservationRooms", "Reservation_IdReservation", "dbo.Reservations");
            DropForeignKey("dbo.Rooms", "Hotel_IdHotel", "dbo.Hotels");
            DropIndex("dbo.ReservationRooms", new[] { "Room_IdRoom" });
            DropIndex("dbo.ReservationRooms", new[] { "Reservation_IdReservation" });
            DropIndex("dbo.Pictures", new[] { "Room_IdRoom" });
            DropIndex("dbo.Rooms", new[] { "Hotel_IdHotel" });
            DropTable("dbo.ReservationRooms");
            DropTable("dbo.Pictures");
            DropTable("dbo.Reservations");
            DropTable("dbo.Rooms");
            DropTable("dbo.Hotels");
        }
    }
}
