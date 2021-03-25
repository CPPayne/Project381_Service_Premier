CREATE DATABASE servicePremierDB;

USE servicePremierDB;

CREATE TABLE Package (
    PackageID int IDENTITY(1,1) PRIMARY KEY,
    PackageName varchar(50) NOT NULL,
    PackageLevel int NOT NULL,
    PackageCost money NOT NULL,
    PackageStart DATE NOT NULL,
    PackageDuration int NOT NULL,
    FOREIGN KEY (ContractID) REFERENCES Contract(ContractID)

);

CREATE TABLE DiffServices (
    ServiceID int IDENTITY(1,1) PRIMARY KEY,
    ServiceType varchar(255) NOT NULL,
    ServiceSpecification varchar(255) NOT NULL,
    ServiceName varchar(30) NOT NULL,
    FOREIGN KEY (PackageID) REFERENCES Package(PackageID)

);

CREATE TABLE WorkRequest(
WorkRequestID int IDENTITY(1,1) PRIMARY KEY,
RequestName varchar(50) NOT NULL,
StartTime DATETIME NOT NULL,
EndTime DATETIME NOT NULL,
FOREIGN KEY (CallID) REFERENCES Calls(CallID),
FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
);

CREATE TABLE Contact(
ContractID int IDENTITY(1,1) PRIMARY KEY,
FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
);

CREATE TABLE Calls (
CallID int IDENTITY(1,1) PRIMARY KEY,
CallStartTime DATETIME NOT NULL,
CallEndTime DATETIME NOT NULL,
CallDetails varchar(50) NOT NULL,
ClientID int IDENTITY(1,1) FOREIGN KEY REFERENCES Client(ClientID)
);

CREATE TABLE Client(
ClientID int IDENTITY(1,1) PRIMARY KEY,
ClientName varchar(20) NOT NULL,
ClientAdress varchar(20) NOT NULL,
PhoneNumber varchar(10) NOT NULL,
BusinessBoolean BIT NOT NULL,
ContractID int IDENTITY(1,1) FOREIGN KEY REFERENCES Contract(ContractID)
);
CREATE TABLE Schedule
(ScheduleID    INT    PRIMARY KEY,
ScheduleStartTime TIME,
ScheduleDuration TIME,
Servicess VARCHAR(20),
ClientID INT,
Datess DATETIME,
FOREIGN KEY (TechnicianID) REFERENCES Technicnian(TechnicianID),
FOREIGN KEY (WorkRequest) REFERENCES WorkRequest(WorkReqauestID));

CREATE TABLE Technician(
TechnicianID int IDENTITY(1,1) PRIMARY KEY,
TechnicianName varchar(50) NOT NULL,
TechnicianExpertise varchar(50) NOT NULL,
TechnicianTimeAvailable TIME NOT NULL,
);
