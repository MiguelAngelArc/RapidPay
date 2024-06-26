-- CREATE DATABASE $(MSSQL_DB);
-- GO
-- USE $(MSSQL_DB);
-- GO
-- CREATE LOGIN $(MSSQL_USER) WITH PASSWORD = '$(MSSQL_SA_PASSWORD)';
-- GO
-- CREATE USER $(MSSQL_USER) FOR LOGIN $(MSSQL_USER);
-- GO
-- ALTER SERVER ROLE sysadmin ADD MEMBER [$(MSSQL_USER)];
-- GO
-- USE [CloudPMDatamart]
-- GO

USE [master]
CREATE TABLE Users (
    [Id] BIGINT PRIMARY KEY IDENTITY(1, 1), 
    [Email] VARCHAR(320) NOT NULL,
    [Name] VARCHAR(50) NOT NULL,
    [Password] VARCHAR(256) NOT NULL
);

CREATE TABLE Cards (
    Id BIGINT PRIMARY KEY IDENTITY(1, 1), 
    UserId BIGINT NOT NULL,
	Number VARCHAR(15) NOT NULL,
    Balance DECIMAL(30,10) NOT NULL, 
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Payments (
    Id BIGINT PRIMARY KEY IDENTITY(1, 1), 
    CardId BIGINT NOT NULL, 
    ItemPrice DECIMAL(30,10) NOT NULL,
	Fee DECIMAL(30,10) NOT NULL,
    FOREIGN KEY (CardId) REFERENCES Cards(Id)
);

INSERT INTO Users (Email, Name, Password) VALUES ("miguel.arcos@encora.com", "Miguel", "P455w0rd");
