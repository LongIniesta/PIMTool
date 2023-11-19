CREATE DATABASE [PIMDB] 
USE [PIMDB] 

CREATE TABLE [Employee](
	[Id] DECIMAL(19,0) IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Visa] CHAR(3) NOT NULL,
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[BirthDate] DATE NOT NULL,
	[Version] TimeStamp NOT NULL
)

CREATE TABLE [Group](
	[Id] DECIMAL(19,0) PRIMARY KEY NOT NULL IDENTITY(1,1),
	[GroupLeaderId] DECIMAL(19,0) FOREIGN KEY REFERENCES [Employee]([Id]) NOT NULL,
	[Version] TimeStamp NOT NULL
)

CREATE TABLE Project(
	[Id] DECIMAL(19,0) IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[GroupId] DECIMAL(19,0) NOT NULL FOREIGN KEY REFERENCES [Group]([Id]),
	[ProjectNumber] DECIMAL(4,0) NOT NULL UNIQUE,
	[Name] VARCHAR(50) NOT NULL,
	[Customer] VARCHAR(50) NOT NULL,
	[Status] CHAR(3) NOT NULL,
	[StartDate] DATE NOT NULL,
	[EndDate] DATE,
	[Version] TimeStamp NOT NULL
)

CREATE TABLE [ProjectEmployee](
	[ProjectId] DECIMAL(19,0) NOT NULL FOREIGN KEY REFERENCES [Project]([Id]),
	[EmployeeId] DECIMAL(19,0) NOT NULL FOREIGN KEY REFERENCES [Employee]([Id]),
	PRIMARY KEY([ProjectId], [EmployeeId])
)	

CREATE TRIGGER CheckEndDate
ON Project
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE EndDate IS NOT NULL AND StartDate IS NOT NULL AND EndDate <= StartDate
    )
    BEGIN
        THROW 50000, 'EndDate must be greater than or equal to StartDate.', 1;
        ROLLBACK TRANSACTION;
    END
END;


