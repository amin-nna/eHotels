SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CentralOffice](
	[Office_ID] [int] IDENTITY(1,1) NOT NULL,
	[HotelChain_Name] [nvarchar](450) NOT NULL,
	[Street] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[Province] [nvarchar](max) NOT NULL,
	[PostalCode] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CentralOffice] ADD  CONSTRAINT [PK_CentralOffice] PRIMARY KEY CLUSTERED 
(
	[Office_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_CentralOffice_HotelChain_Name] ON [dbo].[CentralOffice]
(
	[HotelChain_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CentralOffice]  WITH CHECK ADD  CONSTRAINT [FK_CentralOffice_HotelChain_HotelChain_Name] FOREIGN KEY([HotelChain_Name])
REFERENCES [dbo].[HotelChain] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CentralOffice] CHECK CONSTRAINT [FK_CentralOffice_HotelChain_HotelChain_Name]
GO
