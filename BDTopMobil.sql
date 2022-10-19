select * from TopMobil.Veiculo;

create table Veiculo(
	id int not null primary key identity(1,1),
	placa varchar(8) unique,
	modelo varchar(50),
	ano int,
	cor varchar(30),
);

create table CadastroCliente(
	id int not null primary key identity(1,1),
	nome varchar(50),
	telefone varchar(13),
	email varchar(50),
	senha varchar(30),
);

drop table Veiculo;

drop table CadastroCliente

select * from Veiculo

select * from CadastroCliente

insert into Veiculo values ('DGW7222', 'Ferrari', 2019, 'Vermelho')
insert into Veiculo values ('KGF7979', 'Palio', 2002, 'Marrom')
insert into Veiculo values ('MPL0292', 'Jetta', 2017, 'Vermelho')

insert into CadastroCliente values ('João Vitor Cruz','19988501409', 'jaozin@gmail.com', '78945623')
insert into CadastroCliente values ('Matheus Parreao','19988853565', 'brteu@gmail.com', 'brteucomanda')