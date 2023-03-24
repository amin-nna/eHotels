CREATE VIEW HotelRoomCapacities AS
SELECT Hotel.Hotel_ID, Hotel.Name, SUM(Room.Capacity) AS TotalCapacity
FROM Hotel
INNER JOIN Room ON Hotel.Hotel_ID = Room.Hotel_ID
GROUP BY Hotel.Hotel_ID, Hotel.Name;