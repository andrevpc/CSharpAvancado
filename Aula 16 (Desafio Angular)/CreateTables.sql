USE master
GO

IF EXISTS(SELECT * FROM SYS.DATABASES WHERE NAME = 'projetoAngular')
	DROP DATABASE projetoAngular

CREATE DATABASE	projetoAngular
go

USE projetoAngular
GO

CREATE TABLE Usuarios(
	Id INT IDENTITY PRIMARY KEY,
	Email VARCHAR(100) NOT NULL,
	Username VARCHAR(100) NOT NULL,
	Password VARCHAR(100) NOT NULL,
	ProfilePic VARCHAR(100),
	Age DATE NOT NULL,
	Salt VARCHAR(12) NOT NULL --Seguranša (Salting (de 6 a 12 caracteres) + SlowHashing)
)

CREATE TABLE Foruns(
	Id INT IDENTITY PRIMARY KEY,
	Title VARCHAR(100) NOT NULL,
	Description VARCHAR(100),
	DateCreate DATE NOT NULL,
	OwnerID INT FOREIGN KEY REFERENCES Usuarios(Id) NOT NULL
)

CREATE TABLE Roles(
	Id INT IDENTITY PRIMARY KEY,
	Cargo VARCHAR(100) NOT NULL,
	ForumID INT FOREIGN KEY REFERENCES Foruns(Id) NOT NULL,
	UserID INT FOREIGN KEY REFERENCES Usuarios(Id) NOT NULL
)

CREATE TABLE Posts(
	Id INT IDENTITY PRIMARY KEY,
	Titulo VARCHAR(100),
	Message VARCHAR(MAX) NOT NULL,
	Anexo VARCHAR(100),
	OwnerID INT FOREIGN KEY REFERENCES Usuarios(Id) NOT NULL,
	ForumID INT FOREIGN KEY REFERENCES Foruns(Id) NOT NULL,
	PostsID INT FOREIGN KEY REFERENCES Posts(Id),
	Created DATETIME DEFAULT CURRENT_TIMESTAMP
)

CREATE TABLE Likes(
	Id INT IDENTITY PRIMARY KEY,
	isLike BIT NOT NULL,
	OwnerID INT FOREIGN KEY REFERENCES Usuarios(Id) NOT NULL,
	PostsID INT FOREIGN KEY REFERENCES Posts(Id)
)