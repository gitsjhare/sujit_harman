USE [master]
GO

/****** Object:  Database [sujit_harman]    Script Date: 20-Jan-19 10:03:37 AM ******/
CREATE DATABASE [sujit_harman]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sujit_harman', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\sujit_harman.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'sujit_harman_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\sujit_harman_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [sujit_harman] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sujit_harman].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [sujit_harman] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [sujit_harman] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [sujit_harman] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [sujit_harman] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [sujit_harman] SET ARITHABORT OFF 
GO

ALTER DATABASE [sujit_harman] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [sujit_harman] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [sujit_harman] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [sujit_harman] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [sujit_harman] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [sujit_harman] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [sujit_harman] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [sujit_harman] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [sujit_harman] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [sujit_harman] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [sujit_harman] SET  DISABLE_BROKER 
GO

ALTER DATABASE [sujit_harman] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [sujit_harman] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [sujit_harman] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [sujit_harman] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [sujit_harman] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [sujit_harman] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [sujit_harman] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [sujit_harman] SET RECOVERY FULL 
GO

ALTER DATABASE [sujit_harman] SET  MULTI_USER 
GO

ALTER DATABASE [sujit_harman] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [sujit_harman] SET DB_CHAINING OFF 
GO

ALTER DATABASE [sujit_harman] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [sujit_harman] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [sujit_harman] SET  READ_WRITE 
GO
-----------------------------------------Create Tables----------------------------------------------------
USE [sujit_harman]
GO

/****** Object:  Table [dbo].[patient_forname]    Script Date: 20-Jan-19 10:53:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[patient_forname](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fornames] [varchar](50) NULL,
 CONSTRAINT [PK_patient_forname] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[patient_detail]    Script Date: 20-Jan-19 10:50:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[patient_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[forname] [int] NOT NULL,
	[firstname] [varchar](150) NOT NULL,
	[surname] [varchar](150) NOT NULL,
	[dob] [datetime] NULL,
	[gender] [varchar](50) NOT NULL,
 CONSTRAINT [PK_patient_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[patient_detail]  WITH CHECK ADD  CONSTRAINT [FK_patient_detail_patient_forname] FOREIGN KEY([forname])
REFERENCES [dbo].[patient_forname] ([id])
GO

ALTER TABLE [dbo].[patient_detail] CHECK CONSTRAINT [FK_patient_detail_patient_forname]
GO


/****** Object:  Table [dbo].[patient_telephone]    Script Date: 20-Jan-19 10:54:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[patient_telephone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[phone_id] [int] NOT NULL,
	[home] [varchar](50) NULL,
	[work] [varchar](50) NULL,
	[mobile] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[patient_telephone]  WITH CHECK ADD  CONSTRAINT [FK_patient_telephone_patient_detail] FOREIGN KEY([phone_id])
REFERENCES [dbo].[patient_detail] ([id])
GO

ALTER TABLE [dbo].[patient_telephone] CHECK CONSTRAINT [FK_patient_telephone_patient_detail]
GO

---------------------------------------------Master Table Entries----------------------------------------------------------------------------
INSERT INTO [dbo].[patient_forname]([fornames])
VALUES ('Mr.')
INSERT INTO [dbo].[patient_forname]([fornames])
VALUES ('Miss.')
INSERT INTO [dbo].[patient_forname]([fornames])
VALUES ('Mrs.')
INSERT INTO [dbo].[patient_forname]([fornames])
VALUES ('Dr.')
INSERT INTO [dbo].[patient_forname]([fornames])
VALUES ('Prof.')
GO
-------------------------------------------Create Procedures---------------------------------------------------------------------------------
CREATE PROCEDURE FetchAllPatients
AS
BEGIN
	select pd.id as patient_id, pf.fornames,pd.firstname,pd.surname,pd.dob,pd.gender,pt.home,pt.work,pt.mobile from patient_detail pd 
	inner join patient_forname pf on pf.id=pd.forname
	left join patient_telephone pt on pt.phone_id=pd.id
	order by pd.id desc;
END;

GO


CREATE PROCEDURE FetchPatient @PATIENT_ID INT
AS
BEGIN
	select pd.id as patient_id,pf.fornames,pd.firstname,pd.surname,pd.dob,pd.gender,pt.home,pt.work,pt.mobile 
	from patient_detail pd 
	inner join patient_forname pf on pf.id=pd.forname
	left join patient_telephone pt on pt.phone_id=pd.id
	where pd.id=@PATIENT_ID order by pd.id desc;
END;
GO


CREATE PROCEDURE SavePatient 
    @PATIENT_ID INT,
	@FORNAME VARCHAR(50), 
	@FIRSTNAME VARCHAR(150), 
	@LASTNAME VARCHAR(150), 
	@DOB DATETIME, 
	@GENDER VARCHAR(50),
	@HOME VARCHAR(50),
	@WORK VARCHAR(50),
	@MOBILE VARCHAR(50)
AS
  
  BEGIN
    SET NOCOUNT ON;
	DECLARE @PHONE_ID_LATEST INT;
	DECLARE @FORNAME_ID INT;
	SELECT @FORNAME_ID=id FROM patient_forname WHERE fornames=@FORNAME;
--MERGE THE PATIENT DETAIL 	
    IF @PATIENT_ID IS NOT NULL AND EXISTS (SELECT ID FROM PATIENT_DETAIL WHERE ID=@PATIENT_ID) 
		UPDATE PATIENT_DETAIL SET FORNAME=@FORNAME_ID,FIRSTNAME=@FIRSTNAME,SURNAME=@LASTNAME,DOB=@DOB,GENDER=@GENDER WHERE ID=@PATIENT_ID;
	ELSE
		INSERT INTO PATIENT_DETAIL(FORNAME,FIRSTNAME,SURNAME,DOB,GENDER) VALUES (@FORNAME_ID,@FIRSTNAME,@LASTNAME,@DOB,@GENDER);
		SELECT @PHONE_ID_LATEST= MAX(ID) FROM PATIENT_DETAIL;		
	
	
  	
--MERGE THE TELEPHONE NUMBERS IF NOT AVAILABLE 	
	IF @PATIENT_ID IS NOT NULL AND @PATIENT_ID>0
		UPDATE patient_telephone SET HOME=@HOME,WORK=@WORK,MOBILE=@MOBILE WHERE phone_id=@PATIENT_ID;
     ELSE
		INSERT INTO patient_telephone(PHONE_ID,HOME,WORK,MOBILE) VALUES (@PHONE_ID_LATEST,@HOME,@WORK,@MOBILE);
   END;
GO   
----------------------------------------------------PROCEDURE FOR FETCH FORNAME-------------------------------------------------------------------
CREATE PROCEDURE FetchForname
AS
BEGIN
	SET NOCOUNT ON;
	SELECT A.id AS FORNAME_id, A.fornames AS FORNAME FROM patient_forname A;
   
END
GO