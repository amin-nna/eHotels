CREATE VIEW HotelCapacity AS 
SELECT Hotel.Hotel_ID AS Hotel, SUM(Rooms.Capacity) AS Capacity
FROM Hotel 
INNER JOIN Room ON Hotel.Hotel_ID = Room.Hotel_ID 
GROUP BY Hotel.Name;