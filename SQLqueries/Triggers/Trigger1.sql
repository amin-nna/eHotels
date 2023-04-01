CREATE TRIGGER trg_UpdateRoomsCount
ON Room
AFTER DELETE
AS
BEGIN
    DECLARE @hotel_id varchar(50)
    SELECT @hotel_id = Hotel_ID FROM deleted

    UPDATE Hotel
    SET RoomsCount = (SELECT COUNT(*) FROM Room WHERE Hotel_ID = @hotel_id)
    WHERE Hotel_ID = @hotel_id
END