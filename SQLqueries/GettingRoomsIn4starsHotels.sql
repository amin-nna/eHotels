SELECT [r].[RoomID], [r].[Capacity], [r].[Currency], [r].[Extendable], [r].[Hotel_ID], [r].[Price], [r].[RoomNumber], [r].[View], [h].[Hotel_ID], [h].[City], [h].[Email], [h].[Hotel_chainName_ID], [h].[Name], [h].[PostalCode], [h].[Province], [h].[Rating], [h].[RoomsCount], [h].[Street]
FROM [Room] AS [r]
INNER JOIN [Hotel] AS [h] ON [r].[Hotel_ID] = [h].[Hotel_ID]
WHERE [h].[Rating] = 4