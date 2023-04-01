SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Renting](
	[RentingID] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [nvarchar](max) NOT NULL,
	[Employee] [nvarchar](max) NOT NULL,
	[Customer] [nvarchar](max) NOT NULL,
	[Start] [datetime2](7) NOT NULL,
	[End] [datetime2](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[RoomsRoomID] [nvarchar](450) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Renting] ADD  CONSTRAINT [PK_Renting] PRIMARY KEY CLUSTERED 
(
	[RentingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Renting_RoomsRoomID] ON [dbo].[Renting]
(
	[RoomsRoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Renting]  WITH CHECK ADD  CONSTRAINT [FK_Renting_Room_RoomsRoomID] FOREIGN KEY([RoomsRoomID])
REFERENCES [dbo].[Room] ([RoomID])
GO
ALTER TABLE [dbo].[Renting] CHECK CONSTRAINT [FK_Renting_Room_RoomsRoomID]
GO