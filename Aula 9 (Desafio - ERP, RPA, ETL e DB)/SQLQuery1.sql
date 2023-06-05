use master
go

if exists(select * from sys.databases where name = 'desafioGit')
	drop database desafioGit
go

create database desafioGit
go

use desafioGit
go

create table Repositorios(
	ID int identity primary key,
	Nome varchar(MAX) not null,
	RepoPath varchar(MAX) not null,
	Created datetime,
	LastPull datetime
);
go

select * from Repositorios