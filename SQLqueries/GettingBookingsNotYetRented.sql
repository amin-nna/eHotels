SELECT [b].[BookingID], [b].[Active], [b].[Customer], [b].[Employee], [b].[End], [b].[RoomID], [b].[RoomNumber], [b].[Start], [r].[RoomID], [r].[Capacity], [r].[Currency], [r].[Extendable], [r].[Hotel_ID], [r].[Price], [r].[RoomNumber], [r].[View], [h].[Hotel_ID], [h].[City], [h].[Email], [h].[Hotel_chainName_ID], [h].[Name], [h].[PostalCode], [h].[Province], [h].[Rating], [h].[RoomsCount], [h].[Street]
FROM [Booking] AS [b]
INNER JOIN [Room] AS [r] ON [b].[RoomID] = [r].[RoomID]
INNER JOIN [Hotel] AS [h] ON [r].[Hotel_ID] = [h].[Hotel_ID]
WHERE [b].[Active] = CAST(0 AS bit)