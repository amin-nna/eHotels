SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[Hotel_ID] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Hotel_chainName_ID] [nvarchar](450) NOT NULL,
	[Street] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[Province] [nvarchar](max) NOT NULL,
	[PostalCode] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[RoomsCount] [int] NOT NULL,
	[Rating] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Hotel] ADD  CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED 
(
	[Hotel_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Hotel_Hotel_chainName_ID] ON [dbo].[Hotel]
(
	[Hotel_chainName_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Hotel]  WITH CHECK ADD  CONSTRAINT [FK_Hotel_HotelChain_Hotel_chainName_ID] FOREIGN KEY([Hotel_chainName_ID])
REFERENCES [dbo].[HotelChain] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Hotel] CHECK CONSTRAINT [FK_Hotel_HotelChain_Hotel_chainName_ID]
GO
