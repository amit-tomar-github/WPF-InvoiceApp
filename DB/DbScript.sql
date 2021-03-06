create database [InvoiceData]
USE [InvoiceData]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 10-Jun-2016 9:18:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[TinNo] [varchar](50) NULL,
	[PanNo] [varchar](50) NULL,
	[BillNo] [varchar](50) NULL,
	[InvoiceDate] [datetime] NULL,
	[InvoiceDateTime] [datetime] NULL,
	[Address] [varchar](250) NULL,
	[PoNo] [varchar](50) NULL,
	[ItemDesc] [varchar](250) NULL,
	[Qty] [int] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Vat] [decimal](18, 2) NULL,
	[GrandTotalWords] [varchar](250) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Item]    Script Date: 10-Jun-2016 9:18:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Item](
	[ItemDesc] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mAddress]    Script Date: 10-Jun-2016 9:18:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mAddress](
	[Id] [varchar](150) NOT NULL,
	[Address1] [varchar](150) NULL,
	[Address2] [varchar](150) NULL,
	[Address3] [varchar](150) NULL,
	[Address4] [varchar](150) NULL,
 CONSTRAINT [PK_mAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mHeader]    Script Date: 10-Jun-2016 9:18:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mHeader](
	[Id] [varchar](150) NOT NULL,
	[Header1] [varchar](150) NULL,
	[Header2] [varchar](150) NULL,
	[Header3] [varchar](150) NULL,
	[Header4] [varchar](150) NULL,
 CONSTRAINT [PK_mHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mPanNo]    Script Date: 10-Jun-2016 9:18:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mPanNo](
	[TinNo] [varchar](50) NULL,
	[PanNo] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [InvoiceData] SET  READ_WRITE 
GO
