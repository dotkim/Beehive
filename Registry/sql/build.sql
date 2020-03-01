USE master;
GO

IF NOT EXISTS (
  SELECT database_id
  FROM [master].[sys].[databases] WHERE [name] = 'BeehiveRegistry'
) CREATE DATABASE BeehiveRegistry;

USE BeehiveRegistry;
GO

-- Clean up existing objects, so they can be rebuilt.
-- PROCEDURES
IF OBJECT_ID('Management.GetApplication', 'P') IS NOT NULL DROP PROCEDURE Management.GetApplication;

-- LOGIN AND USER
IF EXISTS (
  SELECT TOP 1 *
  FROM master.sys.server_principals
  WHERE name = 'srv_beehivereg'
) DROP LOGIN srv_beehivereg;

IF DATABASE_PRINCIPAL_ID('srv_beehivereg')    IS NOT NULL DROP USER srv_beehivereg;

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

CREATE LOGIN srv_beehivereg
WITH PASSWORD = 'Password1';
GO

CREATE USER srv_beehivereg FOR LOGIN srv_beehivereg
WITH DEFAULT_SCHEMA = dbo;
GO

ALTER ROLE db_owner ADD MEMBER srv_beehivereg;
GO

CREATE PROCEDURE Management.GetApplication
(
  @ApplicationName  NVARCHAR(100)
)
AS
BEGIN TRY
  SET XACT_ABORT ON;
  SET NOCOUNT ON;
  BEGIN TRANSACTION
    IF (@ApplicationName IS NULL) THROW 60000, 'ApplicationName is null', 1;
    BEGIN
      SELECT  app.ApplicationName,
              app.ClientGUID,
              app.RMQueue,
              exc.ExchangeName,
              fld.FieldName
      FROM    Management.Applications app
      JOIN    Management.Bindings bnd
              ON app.ApplicationID = bnd.ApplicationID
      JOIN    Management.Fields fld
              ON bnd.FieldID = fld.FieldID
      JOIN    Management.Exchanges exc
              ON fld.ExchangeID = exc.ExchangeID
      WHERE   app.ApplicationName = @ApplicationName;
    END
  COMMIT TRANSACTION
END TRY
BEGIN CATCH
  SELECT
    ERROR_NUMBER()    AS ErrorNumber,
    ERROR_SEVERITY()  AS ErrorSeverity,
    ERROR_STATE()     AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE()      AS ErrorLine,
    ERROR_MESSAGE()   AS ErrorMessage;
END CATCH;
GO

--SAMPLE DATA
INSERT INTO Management.Applications(ApplicationName, RMQueue)
VALUES      ('TestApp', 'TestAppQ');
GO

INSERT INTO Management.Exchanges(ExchangeName)
VALUES      ('User');
GO

INSERT INTO Management.Fields(ExchangeID, FieldName)
VALUES      (1, 'FirstName'),
            (1, 'LastName'),
            (1, 'Email');
GO

INSERT INTO Management.Bindings(ApplicationID, FieldID)
VALUES      (1,1),
            (1,2),
            (1,3);
GO