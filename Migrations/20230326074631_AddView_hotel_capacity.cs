using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHotels.Migrations
{
    /// <inheritdoc />
    public partial class AddView_hotel_capacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"  
            CREATE VIEW View_hotel_capacity AS  
            SELECT Hotel.Hotel_ID AS Hotel, SUM(Room.Capacity) AS Capacity
            FROM Hotel 
            INNER JOIN Room ON Hotel.Hotel_ID = Room.Hotel_ID 
            GROUP BY Hotel.Hotel_ID";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW View_hotel_capacity");
        }
    }
}
