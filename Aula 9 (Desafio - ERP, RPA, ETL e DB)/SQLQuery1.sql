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
	Nome varchar(100) not null,
	LastPull datetime not null
);
go

select * from Repositorios