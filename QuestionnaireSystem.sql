USE [master]
GO
/****** Object:  Database [QuestionnaireSystem]    Script Date: 2022/5/8 上午 01:28:16 ******/
CREATE DATABASE [QuestionnaireSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuestionnaireSystem', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuestionnaireSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuestionnaireSystem_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuestionnaireSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuestionnaireSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuestionnaireSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuestionnaireSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuestionnaireSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuestionnaireSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuestionnaireSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuestionnaireSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [QuestionnaireSystem] SET  MULTI_USER 
GO
ALTER DATABASE [QuestionnaireSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuestionnaireSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuestionnaireSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuestionnaireSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuestionnaireSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuestionnaireSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuestionnaireSystem', N'ON'
GO
ALTER DATABASE [QuestionnaireSystem] SET QUERY_STORE = OFF
GO
USE [QuestionnaireSystem]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 2022/5/8 上午 01:28:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[BasicAnswerID] [int] NOT NULL,
	[QuestID] [int] NOT NULL,
	[Answer] [nvarchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicAnswer]    Script Date: 2022/5/8 上午 01:28:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicAnswer](
	[BasicAnswerID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionnaireID] [int] NOT NULL,
	[AnswerDate] [datetime2](7) NOT NULL,
	[Nickname] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Age] [int] NOT NULL,
 CONSTRAINT [PK_BasicAnswer] PRIMARY KEY CLUSTERED 
(
	[BasicAnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommonQuest]    Script Date: 2022/5/8 上午 01:28:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommonQuest](
	[CommonQuestID] [int] IDENTITY(1,1) NOT NULL,
	[QuestContent] [nvarchar](500) NOT NULL,
	[AnswerForm] [int] NOT NULL,
	[SelectItem] [nvarchar](500) NULL,
	[Required] [bit] NOT NULL,
 CONSTRAINT [PK_CommonQuest] PRIMARY KEY CLUSTERED 
(
	[CommonQuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 2022/5/8 上午 01:28:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionnaireID] [int] NOT NULL,
	[QuestOrder] [int] NOT NULL,
	[QuestContent] [nvarchar](500) NULL,
	[AnswerForm] [int] NOT NULL,
	[SelectItem] [nvarchar](500) NULL,
	[Required] [bit] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaire]    Script Date: 2022/5/8 上午 01:28:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaire](
	[QuestionnaireID] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](50) NOT NULL,
	[QuestionnaireContent] [nvarchar](500) NULL,
	[BuildDate] [datetime2](7) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[VoteStatus] [bit] NOT NULL,
 CONSTRAINT [PK_Questionnaire] PRIMARY KEY CLUSTERED 
(
	[QuestionnaireID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BasicAnswer] ADD  CONSTRAINT [DF_BasicAnswer_AnswerDate]  DEFAULT (getdate()) FOR [AnswerDate]
GO
ALTER TABLE [dbo].[Questionnaire] ADD  CONSTRAINT [DF_Questionnaire_BuildDate]  DEFAULT (getdate()) FOR [BuildDate]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_BasicAnswer] FOREIGN KEY([BasicAnswerID])
REFERENCES [dbo].[BasicAnswer] ([BasicAnswerID])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_BasicAnswer]
GO
ALTER TABLE [dbo].[BasicAnswer]  WITH CHECK ADD  CONSTRAINT [FK_BasicAnswer_Questionnaire] FOREIGN KEY([QuestionnaireID])
REFERENCES [dbo].[Questionnaire] ([QuestionnaireID])
GO
ALTER TABLE [dbo].[BasicAnswer] CHECK CONSTRAINT [FK_BasicAnswer_Questionnaire]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Questionnaire] FOREIGN KEY([QuestionnaireID])
REFERENCES [dbo].[Questionnaire] ([QuestionnaireID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Questionnaire]
GO
USE [master]
GO
ALTER DATABASE [QuestionnaireSystem] SET  READ_WRITE 
GO
