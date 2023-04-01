SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomID] [nvarchar](450) NOT NULL,
	[RoomNumber] [nvarchar](max) NOT NULL,
	[Hotel_ID] [nvarchar](450) NOT NULL,
	[Price] [int] NOT NULL,
	[Currency] [nvarchar](max) NOT NULL,
	[Capacity] [int] NOT NULL,
	[Extendable] [bit] NOT NULL,
	[View] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Room_Hotel_ID] ON [dbo].[Room]
(
	[Hotel_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Hotel_Hotel_ID] FOREIGN KEY([Hotel_ID])
REFERENCES [dbo].[Hotel] ([Hotel_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Hotel_Hotel_ID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[trg_UpdateRoomsCount]
ON [dbo].[Room]
AFTER DELETE
AS
BEGIN
    DECLARE @hotel_id varchar(50)
    SELECT @hotel_id = Hotel_ID FROM deleted

    UPDATE Hotel
    SET RoomsCount = (SELECT COUNT(*) FROM Room WHERE Hotel_ID = @hotel_id)
    WHERE Hotel_ID = @hotel_id
END
GO
ALTER TABLE [dbo].[Room] ENABLE TRIGGER [trg_UpdateRoomsCount]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[trg_UpdateRoomsCountI]
ON [dbo].[Room]
AFTER INSERT
AS
BEGIN
    DECLARE @hotel_id varchar(50)
    SELECT @hotel_id = Hotel_ID FROM inserted

    UPDATE Hotel
    SET RoomsCount = (SELECT COUNT(*) FROM Room WHERE Hotel_ID = @hotel_id)
    WHERE Hotel_ID = @hotel_id
END
GO
ALTER TABLE [dbo].[Room] ENABLE TRIGGER [trg_UpdateRoomsCountI]
GO
