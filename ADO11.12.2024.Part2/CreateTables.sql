CREATE TABLE [dbo].Products
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	Name nvarchar(20)
);
CREATE TABLE [dbo].TypeProducts
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	Name nvarchar(20)
);
CREATE TABLE [dbo].Suppliers
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	Name nvarchar(20)
);
CREATE TABLE [dbo].Informations
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	ProductId int foreign key references Products(ID),
	TypeProductId int foreign key references TypeProducts(ID),
	[Count] int,
	DateSupply date,
	SuppliersId int foreign key references Suppliers(ID),
	Price money
);




