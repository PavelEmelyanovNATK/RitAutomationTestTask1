USE master
GO

CREATE DATABASE MachinesMap
GO

USE MachinesMap
GO

CREATE TABLE Machines (
	Id uniqueidentifier DEFAULT newid() NOT NULL,
	Name nvarchar(50) NOT NULL,

	Constraint Machines_Id_PK PRIMARY KEY CLUSTERED (Id)
);
GO

CREATE TABLE MachinePositions (
	Id uniqueidentifier NOT NULL,
	Latitude float NOT NULL,
	Longitude float NOT NULL,

	Constraint MachinePositions_Id_PK PRIMARY KEY CLUSTERED (Id),
	Constraint MachinePositions_Id_FK FOREIGN KEY (Id)
	REFERENCES Machines (Id)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);
GO

INSERT INTO Machines (Id, Name) VALUES 
	('79ccee3b-7004-41cc-8255-a578ef491d15', 'Machine 1'),
	('93ca3bd6-55dd-4cc1-bed2-43ffb48b525e', 'Machine 2'),
	('7201d979-ba35-44e5-8ae0-816b02c2b12a', 'Machine 3'),
	('bbd7309c-4c57-4072-b224-0b78f73105e6', 'Machine 4'),
	('f2932927-d7e8-460f-9eb9-0bac67c7f427', 'Machine 5'),
	('d8183314-3594-4e44-8b82-e3529b145c2f', 'Machine 6');
GO

INSERT INTO MachinePositions (Id, Latitude, Longitude) VALUES 
	('79ccee3b-7004-41cc-8255-a578ef491d15', '55.029209', '82.921166'),
	('93ca3bd6-55dd-4cc1-bed2-43ffb48b525e', '55.032142', '82.913055'),
	('7201d979-ba35-44e5-8ae0-816b02c2b12a', '55.023871', '82.929277'),
	('bbd7309c-4c57-4072-b224-0b78f73105e6', '55.025523', '82.900138'),
	('f2932927-d7e8-460f-9eb9-0bac67c7f427', '55.042876', '82.925243'),
	('d8183314-3594-4e44-8b82-e3529b145c2f', '55.043073', '82.906704');
GO