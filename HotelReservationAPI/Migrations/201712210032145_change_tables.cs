namespace HotelReservationAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_tables : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RoomsReservation", new[] { "RoomID" });
            DropIndex("dbo.RoomsReservation", new[] { "ReservationID" });
            DropPrimaryKey("dbo.RoomsReservation");
            AddPrimaryKey("dbo.RoomsReservation", new[] { "ReservationId", "RoomId" });
            CreateIndex("dbo.RoomsReservation", "ReservationId");
            CreateIndex("dbo.RoomsReservation", "RoomId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RoomsReservation", new[] { "RoomId" });
            DropIndex("dbo.RoomsReservation", new[] { "ReservationId" });
            DropPrimaryKey("dbo.RoomsReservation");
            AddPrimaryKey("dbo.RoomsReservation", new[] { "RoomID", "ReservationID" });
            CreateIndex("dbo.RoomsReservation", "ReservationID");
            CreateIndex("dbo.RoomsReservation", "RoomID");
        }
    }
}
