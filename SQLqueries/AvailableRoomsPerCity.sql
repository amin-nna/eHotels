CREATE VIEW RoomAvailabilityByArea AS
SELECT h.City AS Area, COUNT(*) AS Count
FROM Hotel h
JOIN Room r ON h.Hotel_ID = r.Hotel_ID
WHERE NOT EXISTS (
    SELECT *
    FROM Booking b
    WHERE b.RoomNumber = r.RoomNumber AND 
          b.Start <= NOW() AND b.End >= NOW()
) AND NOT EXISTS (
    SELECT *
    FROM Renting rt
    WHERE rt.RoomNumber = r.RoomNumber AND 
          rt.Start <= NOW() AND rt.End >= NOW()
)
GROUP BY h.City;

