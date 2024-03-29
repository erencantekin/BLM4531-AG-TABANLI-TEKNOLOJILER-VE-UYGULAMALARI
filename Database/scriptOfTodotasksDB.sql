USE [master]
GO
/****** Object:  Database [tododatabase]    Script Date: 1/10/2024 7:10:18 PM ******/
CREATE DATABASE [tododatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tododatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\tododatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tododatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\tododatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [tododatabase] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tododatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tododatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tododatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tododatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tododatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tododatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [tododatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [tododatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tododatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tododatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tododatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tododatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tododatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tododatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tododatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tododatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [tododatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tododatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tododatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tododatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tododatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tododatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tododatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tododatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [tododatabase] SET  MULTI_USER 
GO
ALTER DATABASE [tododatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tododatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tododatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tododatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tododatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tododatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [tododatabase] SET QUERY_STORE = ON
GO
ALTER DATABASE [tododatabase] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [tododatabase]
GO
/****** Object:  Table [dbo].[Todotasks]    Script Date: 1/10/2024 7:10:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todotasks](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TaskTitle] [nvarchar](255) NOT NULL,
	[TaskDescription] [nvarchar](255) NOT NULL,
	[TaskPriority] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/10/2024 7:10:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Todotasks] ON 

INSERT [dbo].[Todotasks] ([TaskID], [UserID], [TaskTitle], [TaskDescription], [TaskPriority], [CreatedAt]) VALUES (33, 5, N'Drink Water', N'5 liter', 1, CAST(N'2024-01-09T22:00:31.647' AS DateTime))
INSERT [dbo].[Todotasks] ([TaskID], [UserID], [TaskTitle], [TaskDescription], [TaskPriority], [CreatedAt]) VALUES (34, 5, N'Go To GYM', N'Twice a week', 0, CAST(N'2024-01-09T22:01:00.417' AS DateTime))
INSERT [dbo].[Todotasks] ([TaskID], [UserID], [TaskTitle], [TaskDescription], [TaskPriority], [CreatedAt]) VALUES (35, 6, N'Do Homework', N'2 hours', 1, CAST(N'2024-01-09T22:04:16.693' AS DateTime))
INSERT [dbo].[Todotasks] ([TaskID], [UserID], [TaskTitle], [TaskDescription], [TaskPriority], [CreatedAt]) VALUES (36, 6, N'Cook Meal', N'Spaghetti', 2, CAST(N'2024-01-09T22:04:28.323' AS DateTime))
SET IDENTITY_INSERT [dbo].[Todotasks] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Password]) VALUES (5, N'erencan', N'erencan')
INSERT [dbo].[Users] ([UserID], [Username], [Password]) VALUES (6, N'erencanadmin', N'erencanadmin')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Todotasks] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Todotasks]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
USE [master]
GO
ALTER DATABASE [tododatabase] SET  READ_WRITE 
GO
