USE master;
GO

IF NOT EXISTS (
  SELECT database_id
  FROM [master].[sys].[databases] WHERE [name] = 'BeehiveRegistry'
) CREATE DATABASE BeehiveRegistry;

USE BeehiveRegistry;
GO

-- Clean up existing objects, so they can be rebuilt.
-- TABLES
IF OBJECT_ID('Management.Bindings', 'U')      IS NOT NULL DROP TABLE Management.Bindings;
IF OBJECT_ID('Management.Fields', 'U')        IS NOT NULL DROP TABLE Management.Fields;
IF OBJECT_ID('Management.Exchanges', 'U')     IS NOT NULL DROP TABLE Management.Exchanges;
IF OBJECT_ID('Management.Applications', 'U')  IS NOT NULL DROP TABLE Management.Applications;

--SCHEMAS
IF SCHEMA_ID('Management') IS NOT NULL DROP SCHEMA Management;
GO

CREATE SCHEMA Management;
GO

CREATE TABLE Management.Applications
(
  ApplicationID   INT IDENTITY(1,1) NOT NULL
    CONSTRAINT PK_Applications PRIMARY KEY,
  ApplicationName NVARCHAR(100)     NOT NULL
    CONSTRAINT UQ_ApplicationName UNIQUE,
  ClientGUID      UNIQUEIDENTIFIER  NOT NULL
    CONSTRAINT DF_ClientGUID DEFAULT NEWID(),
  RMQueue         NVARCHAR(100)     NULL
);
GO

CREATE TABLE Management.Exchanges
(
  ExchangeID    INT IDENTITY(1,1) NOT NULL
    CONSTRAINT PK_Exchanges PRIMARY KEY,
  ExchangeName  NVARCHAR(30)      NOT NULL
    CONSTRAINT UQ_ExchangeName UNIQUE
);
GO

CREATE TABLE Management.Fields
(
  FieldID     INT IDENTITY(1,1) NOT NULL
    CONSTRAINT PK_Fields PRIMARY KEY,
  ExchangeID  INT               NOT NULL
    CONSTRAINT FK_Exchanges_ExchangeID FOREIGN KEY
    REFERENCES Management.Exchanges,
  FieldName   NVARCHAR(100)     NOT NULL
)

CREATE TABLE Management.Bindings
(
  ApplicationID INT NOT NULL
    CONSTRAINT FK_Applications_ApplicationID FOREIGN KEY
    REFERENCES Management.Applications,
  FieldID       INT NOT NULL
    CONSTRAINT FK_Fields_FieldID FOREIGN KEY
    REFERENCES Management.Fields,
  CONSTRAINT PK_Bindings
  PRIMARY KEY (ApplicationID, FieldID)
);
GO