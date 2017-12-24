namespace HotelReservationAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _171221_Update : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Hotels", newName: "Hotel");
            RenameTable(name: "dbo.Rooms", newName: "Room");
            RenameTable(name: "dbo.Reservations", newName: "Reservation");
            RenameTable(name: "dbo.Pictures", newName: "Picture");
            RenameTable(name: "dbo.ReservationRooms", newName: "RoomsReservation");
            RenameColumn(table: "dbo.RoomsReservation", name: "Reservation_IdReservation", newName: "ReservationID");
            RenameColumn(table: "dbo.RoomsReservation", name: "Room_IdRoom", newName: "RoomID");
            RenameIndex(table: "dbo.RoomsReservation", name: "IX_Room_IdRoom", newName: "IX_RoomID");
            RenameIndex(table: "dbo.RoomsReservation", name: "IX_Reservation_IdReservation", newName: "IX_ReservationID");
            DropPrimaryKey("dbo.RoomsReservation");
            AlterColumn("dbo.Reservation", "NbrPerson", c => c.String(nullable: false));
            AddPrimaryKey("dbo.RoomsReservation", new[] { "RoomID", "ReservationID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RoomsReservation");
            AlterColumn("dbo.Reservation", "NbrPerson", c => c.String());
            AddPrimaryKey("dbo.RoomsReservation", new[] { "Reservation_IdReservation", "Room_IdRoom" });
            RenameIndex(table: "dbo.RoomsReservation", name: "IX_ReservationID", newName: "IX_Reservation_IdReservation");
            RenameIndex(table: "dbo.RoomsReservation", name: "IX_RoomID", newName: "IX_Room_IdRoom");
            RenameColumn(table: "dbo.RoomsReservation", name: "RoomID", newName: "Room_IdRoom");
            RenameColumn(table: "dbo.RoomsReservation", name: "ReservationID", newName: "Reservation_IdReservation");
            RenameTable(name: "dbo.RoomsReservation", newName: "ReservationRooms");
            RenameTable(name: "dbo.Picture", newName: "Pictures");
            RenameTable(name: "dbo.Reservation", newName: "Reservations");
            RenameTable(name: "dbo.Room", newName: "Rooms");
            RenameTable(name: "dbo.Hotel", newName: "Hotels");
        }
    }
}
