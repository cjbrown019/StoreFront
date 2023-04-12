USE [master]
GO

/****** Object:  Database [FoodStoreFront]    Script Date: 4/10/2023 6:54:50 PM ******/
CREATE DATABASE [FoodStoreFront]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FoodStoreFront', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FoodStoreFront.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FoodStoreFront_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FoodStoreFront_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FoodStoreFront].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [FoodStoreFront] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [FoodStoreFront] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [FoodStoreFront] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [FoodStoreFront] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [FoodStoreFront] SET ARITHABORT OFF 
GO

ALTER DATABASE [FoodStoreFront] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [FoodStoreFront] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [FoodStoreFront] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [FoodStoreFront] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [FoodStoreFront] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [FoodStoreFront] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [FoodStoreFront] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [FoodStoreFront] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [FoodStoreFront] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [FoodStoreFront] SET  ENABLE_BROKER 
GO

ALTER DATABASE [FoodStoreFront] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [FoodStoreFront] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [FoodStoreFront] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [FoodStoreFront] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [FoodStoreFront] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [FoodStoreFront] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [FoodStoreFront] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [FoodStoreFront] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [FoodStoreFront] SET  MULTI_USER 
GO

ALTER DATABASE [FoodStoreFront] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [FoodStoreFront] SET DB_CHAINING OFF 
GO

ALTER DATABASE [FoodStoreFront] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [FoodStoreFront] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [FoodStoreFront] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [FoodStoreFront] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [FoodStoreFront] SET QUERY_STORE = OFF
GO

ALTER DATABASE [FoodStoreFront] SET  READ_WRITE 
GO
