CREATE DATABASE servicePremierDB;

USE servicePremierDB;

CREATE TABLE Package (
    PackageID int IDENTITY(1,1) PRIMARY KEY,
    PackageName varchar(50) NOT NULL,
    PackageCost money NOT NULL,
);

CREATE TABLE ServiceC (
    ServiceID int IDENTITY(1,1) PRIMARY KEY,
    ServiceType varchar(255) NOT NULL,
    ServiceSpecification varchar(255) NOT NULL,
    ServiceName varchar(30) NOT NULL,
);

CREATE TABLE ServicePackages (
	FOREIGN KEY (PackageID) REFERENCES Package(PackageID),
	FOREIGN KEY (ServiceID) REFERENCES ServiceC(ServiceID)
);

CREATE TABLE Contact(
ContractID int IDENTITY(1,1) PRIMARY KEY,
ContractDuration datetime NOT NULL,
FOREIGN KEY (ClientID) REFERENCES Client(ClientID),
FOREIGN KEY (PackageID) REFERENCES Package(PackageID),
ContractLevel varchar(1)
);


CREATE TABLE WorkRequest(
WorkRequestID int IDENTITY(1,1) PRIMARY KEY,
ProblemType varchar(50) NOT NULL,
Descriptions varchar(50) NOT NULL,
SessionAmount int NOT NULL,
FOREIGN KEY (CallID) REFERENCES Calls(CallID),
FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
);

CREATE TABLE Technician(
TechnicianID int IDENTITY(1,1) PRIMARY KEY,
TechnicianName varchar(50) NOT NULL,
TechnicianExpertise varchar(50) NOT NULL
);

CREATE TABLE Schedule (
ScheduleID    INT    PRIMARY KEY,
ScheduleStartTime TIME,
FOREIGN KEY (ClientID) REFERENCES Client(ClientID),
Dates DATETIME,
FOREIGN KEY (TechnicianID) REFERENCES Technicnian(TechnicianID),
FOREIGN KEY (WorkRequest) REFERENCES WorkRequest(WorkReqauestID)
);

CREATE TABLE Client(
ClientID int IDENTITY(1,1) PRIMARY KEY,
ClientName varchar(20) NOT NULL,
ClientSurname varchar(20) NOT NULL,
ClientAdress varchar(20) NOT NULL,
PhoneNumber varchar(10) NOT NULL,
BusinessBoolean BIT NOT NULL,
ContractID int IDENTITY(1,1) FOREIGN KEY REFERENCES Contract(ContractID)
);

CREATE TABLE Calls (
CallID int IDENTITY(1,1) PRIMARY KEY,
CallStartTime DATETIME NOT NULL,
CallEndTime DATETIME NOT NULL,
CallDetails varchar(50) NOT NULL,
ClientID int IDENTITY(1,1) FOREIGN KEY REFERENCES Client(ClientID)
);



