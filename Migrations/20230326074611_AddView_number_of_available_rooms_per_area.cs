using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHotels.Migrations
{
    /// <inheritdoc />
    public partial class AddView_number_of_available_rooms_per_area : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)  
{  
    var sql = @"  
        CREATE VIEW View_number_of_available_rooms_per_area AS
        SELECT h.City AS Area, COUNT(*) AS Count
        FROM Hotel h
        JOIN Room r ON h.Hotel_ID = r.Hotel_ID
        WHERE NOT EXISTS (
            SELECT *
            FROM Booking b
            WHERE b.RoomNumber = r.RoomNumber AND 
                  b.Start <= GETDATE() AND b.[End] >= GETDATE()
        ) AND NOT EXISTS (
            SELECT *
            FROM Renting rt
            WHERE rt.RoomNumber = r.RoomNumber AND 
                  rt.Start <= GETDATE() AND rt.[End] >= GETDATE()
        )
        GROUP BY h.City";  

    migrationBuilder.Sql(sql);  
}


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)  
{  
    migrationBuilder.Sql(@"DROP VIEW View_number_of_available_rooms_per_area");  
}

    }
}
