USE [master]
GO
/****** Object:  Database [TRAVELS]    Script Date: 20/11/2020 17:24:01 ******/
CREATE DATABASE [TRAVELS]
GO
ALTER DATABASE [TRAVELS] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TRAVELS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TRAVELS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TRAVELS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TRAVELS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TRAVELS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TRAVELS] SET ARITHABORT OFF 
GO
ALTER DATABASE [TRAVELS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TRAVELS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TRAVELS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TRAVELS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TRAVELS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TRAVELS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TRAVELS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TRAVELS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TRAVELS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TRAVELS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TRAVELS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TRAVELS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TRAVELS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TRAVELS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TRAVELS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TRAVELS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TRAVELS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TRAVELS] SET RECOVERY FULL 
GO
ALTER DATABASE [TRAVELS] SET  MULTI_USER 
GO
ALTER DATABASE [TRAVELS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TRAVELS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TRAVELS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TRAVELS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TRAVELS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TRAVELS', N'ON'
GO
USE [TRAVELS]
GO
/****** Object:  Table [dbo].[AvailableTravel]    Script Date: 20/11/2020 17:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AvailableTravel](
	[available_travel_id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](20) NOT NULL,
	[capacity] [smallint] NOT NULL,
	[price] [decimal](18, 0) NOT NULL,
	[destination_id] [int] NOT NULL,
	[origin_id] [int] NOT NULL,
 CONSTRAINT [PK_AvailableTravel] PRIMARY KEY CLUSTERED 
(
	[available_travel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Destination]    Script Date: 20/11/2020 17:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Destination](
	[destination_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Destination] PRIMARY KEY CLUSTERED 
(
	[destination_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Origin]    Script Date: 20/11/2020 17:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Origin](
	[origin_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Origin] PRIMARY KEY CLUSTERED 
(
	[origin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Travel]    Script Date: 20/11/2020 17:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Travel](
	[travel_id] [int] IDENTITY(1,1) NOT NULL,
	[available_travel_id] [int] NOT NULL,
	[traveler_id] [int] NOT NULL,
 CONSTRAINT [PK_Travel] PRIMARY KEY CLUSTERED 
(
	[travel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Traveler]    Script Date: 20/11/2020 17:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Traveler](
	[traveler_id] [int] IDENTITY(1,1) NOT NULL,
	[identification_document] [varchar](20) NOT NULL,
	[name] [varchar](250) NOT NULL,
	[address] [varchar](250) NULL,
	[phone] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Traveler] PRIMARY KEY CLUSTERED 
(
	[traveler_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[AvailableTravel] ON 

INSERT [dbo].[AvailableTravel] ([available_travel_id], [code], [capacity], [price], [destination_id], [origin_id]) VALUES (1, N'000001', 50, CAST(1000 AS Decimal(18, 0)), 1, 1)
INSERT [dbo].[AvailableTravel] ([available_travel_id], [code], [capacity], [price], [destination_id], [origin_id]) VALUES (5, N'000002', 60, CAST(1500 AS Decimal(18, 0)), 2, 2)
SET IDENTITY_INSERT [dbo].[AvailableTravel] OFF
SET IDENTITY_INSERT [dbo].[Destination] ON 

INSERT [dbo].[Destination] ([destination_id], [name]) VALUES (1, N'Londres-Inglaterra')
INSERT [dbo].[Destination] ([destination_id], [name]) VALUES (2, N'Caracas-Venezuela')
SET IDENTITY_INSERT [dbo].[Destination] OFF
SET IDENTITY_INSERT [dbo].[Origin] ON 

INSERT [dbo].[Origin] ([origin_id], [name]) VALUES (1, N'Lima-Perú ')
INSERT [dbo].[Origin] ([origin_id], [name]) VALUES (2, N'Bogota-Colombia')
INSERT [dbo].[Origin] ([origin_id], [name]) VALUES (4, N'Cusco-Perú')
SET IDENTITY_INSERT [dbo].[Origin] OFF
SET IDENTITY_INSERT [dbo].[Travel] ON 

INSERT [dbo].[Travel] ([travel_id], [available_travel_id], [traveler_id]) VALUES (1, 1, 1)
INSERT [dbo].[Travel] ([travel_id], [available_travel_id], [traveler_id]) VALUES (2, 1, 2)
INSERT [dbo].[Travel] ([travel_id], [available_travel_id], [traveler_id]) VALUES (3, 5, 2)
SET IDENTITY_INSERT [dbo].[Travel] OFF
SET IDENTITY_INSERT [dbo].[Traveler] ON 

INSERT [dbo].[Traveler] ([traveler_id], [identification_document], [name], [address], [phone]) VALUES (1, N'44028383', N'José Alonso Palacios Castillo', N'av. los lirios 209', N'997937319')
INSERT [dbo].[Traveler] ([traveler_id], [identification_document], [name], [address], [phone]) VALUES (2, N'44028384', N'Edison Cuadros', N'av. la colmena 502', N'998555525')
SET IDENTITY_INSERT [dbo].[Traveler] OFF
ALTER TABLE [dbo].[AvailableTravel]  WITH CHECK ADD  CONSTRAINT [FK_AvailableTravel_Destination] FOREIGN KEY([destination_id])
REFERENCES [dbo].[Destination] ([destination_id])
GO
ALTER TABLE [dbo].[AvailableTravel] CHECK CONSTRAINT [FK_AvailableTravel_Destination]
GO
ALTER TABLE [dbo].[AvailableTravel]  WITH CHECK ADD  CONSTRAINT [FK_AvailableTravel_Origin] FOREIGN KEY([origin_id])
REFERENCES [dbo].[Origin] ([origin_id])
GO
ALTER TABLE [dbo].[AvailableTravel] CHECK CONSTRAINT [FK_AvailableTravel_Origin]
GO
ALTER TABLE [dbo].[Travel]  WITH CHECK ADD  CONSTRAINT [FK_Travel_AvailableTravel] FOREIGN KEY([available_travel_id])
REFERENCES [dbo].[AvailableTravel] ([available_travel_id])
GO
ALTER TABLE [dbo].[Travel] CHECK CONSTRAINT [FK_Travel_AvailableTravel]
GO
ALTER TABLE [dbo].[Travel]  WITH CHECK ADD  CONSTRAINT [FK_Travel_Traveler] FOREIGN KEY([traveler_id])
REFERENCES [dbo].[Traveler] ([traveler_id])
GO
ALTER TABLE [dbo].[Travel] CHECK CONSTRAINT [FK_Travel_Traveler]
GO
USE [master]
GO
ALTER DATABASE [TRAVELS] SET  READ_WRITE 
GO
