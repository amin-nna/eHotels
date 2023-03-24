CREATE VIEW AvailableRoomsPerCity AS 
SELECT h.City, COUNT(*) as AvailableRooms
FROM Room r
INNER JOIN Hotel h ON r.Hotel_ID = h.Hotel_ID
LEFT JOIN Booking b ON r.RoomNumber = b.RoomNumber AND b.Active = 1
WHERE b.RoomNumber IS NULL
GROUP BY h.City;

