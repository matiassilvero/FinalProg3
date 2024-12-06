Create database FINALRESTOPROG
use FINALRESTOPROG

Create table USUARIOS(
id int primary key identity (1,1) not null,
nombre varchar(50) not null,
apellido varchar(50) not null,
dni varchar(20) not null,
pass varchar(50) not null,
fechaNacimiento date not null,
urlImagen varchar(100),
tipoUsuario int not null,--1 Gerente 2 Mesero
activo bit not null
);

Alter table USUARIOS
alter column urlImagen varchar(500);

Create table MESAS(
id int primary key identity (1,1) not null,
capacidad int not null,
disponibilidad int not null,--1 LIBRE 2 OCUPADA 3 RESERVADA
activo bit not null
);

Create table PRODUCTOS(
id int primary key identity (1,1) not null,
nombre varchar(50) not null,
stock int not null,
precio float not null,
urlImagen varchar(100),
tipoProducto int not null,--1 PLATO 2 BEBIDA
activo bit
);

Alter table PRODUCTOS
alter column urlImagen varchar(500);

Create table PEDIDOS(
id int primary key identity(1,1) not null,
idMesa int not null foreign key references MESAS(id),
idMesero int not null foreign key references USUARIOS(id),--Solo si es tipoUsuario 1 se va referenciar aca
fecha date not null,
estado int not null
);

ALTER TABLE PEDIDOS
ALTER COLUMN fecha DATETIME not null;

Create table PEDIDOSDEPRODUCTOS(
id int primary key identity(1,1) not null,
idPedido int not null foreign key references PEDIDOS(id),
idProducto int not null foreign key references PRODUCTOS(id),
cantidad int not null
);

Create table FACTURAS(
id int primary key identity(1,1) not null,
numeroFactura int not null,
mesa int not null,
mesero int not null,
fecha datetime not null,
importe float not null
);

Create table INSUMOS(
id int primary key identity (1,1) not null,
nombre varchar(50) not null,
precio float not null,
cantidad int not null
);
