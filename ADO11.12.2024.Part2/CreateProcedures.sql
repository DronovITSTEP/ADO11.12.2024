create procedure selectAllInfo
as
begin
	select I.Id, P.Name ProductName,
            S.Name SupplierName, 
            T.Name TypeProdName 
            from Products P 
            join Informations I on P.Id = I.ProductId 
            join Suppliers S on I.SuppliersId = S.Id
            join TypeProducts T on T.Id = I.TypeProductId
end;

create procedure selectTypeProduct
as
begin
    select Name from TypeProducts
end;

create procedure selectSupplier
as
begin
select Name from Suppliers
end;

create procedure minCount
as
begin
select P.Name from Products P
            join Informations I on P.Id = I.ProductId
            where count = (select min(count) from Informations)
end;

create procedure maxCount
as
begin
select Name from Products P 
            join Informations I on P.Id = I.ProductId 
            where count = (select max(count) from Informations)
end;

create procedure minPrice
as
begin
select Name from Products P 
            join Informations I on P.Id = I.ProductId 
            where price = (select min(price) from Informations)
end;

create procedure maxPrice
as
begin
select Name from Products P
            join Informations I on P.Id = I.ProductId
            where price = (select max(price) from Informations)
end;

create procedure typeProduct
    @typeProductName nvarchar(20)
as
begin
    select P.Name from Products P
    join Informations I on P.Id = I.ProductId
    join TypeProducts T on T.Id = I.TypeProductId
    where T.Name = @typeProductName
end;

create procedure supplierProduct
    @supplierName nvarchar(20)
as
begin
    select P.Name from Products P
    join Informations I on P.Id = I.ProductId
    join Suppliers S on S.Id = I.SuppliersId
    where S.Name = @supplierName
end;

create procedure oldProduct
as
begin
    select P.Name from Products P
    join Informations I on P.Id = I.ProductId   
    where I.DateSupply = (select min(DateSupply) from Informations)
end;

create procedure avgCountProduct
as
begin
    select T.Name, avg(I.count) as 'Кол-во продуктов' from Informations I
    join TypeProducts T on I.TypeProductId = T.Id
    group by T.Name
end;