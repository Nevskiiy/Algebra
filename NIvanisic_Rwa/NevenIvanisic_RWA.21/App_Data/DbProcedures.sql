use AdventureWorksOBP
go

-- Dohvati države.
create proc _GetCountry
as
begin
	select * from Drzava
end
go

-- Dohvati grad.
create proc _GetTown
	@gradId int
as
begin
	select * from Grad where IDGrad = @gradId
end
go

-- Dohvati gradove odabrane države.
create proc _GetTowns
	@drzavaId int
as
begin
	select * from Grad where DrzavaID = @drzavaId
end
go

CREATE PROCEDURE _GetUnqTowns
AS
BEGIN
	select * from Grad
END
GO

-- Izbriši odabrani grad.
create proc _DeleteTown
	@gradId int
as
begin
	delete from Grad where IDGrad = @gradId
end
go

-- Dodaj grad odabrane države.
create proc _InsertTown
	@naziv nvarchar(50),
	@drzavaId int
as
begin
	insert into Grad values (@naziv, @drzavaId)
end
go

-- Dohvati n broj kupaca! Poziv sa backhand-a.
create proc _GetCustomers
	 @customers int
as
begin
	select top (@customers) * from Kupac
end
go

create PROCEDURE _Get_Customers
AS
BEGIN
	select * from Kupac
END
GO

-- Dohvati filtrirane kupce po gradu.
create proc _GetCustomerByTown
	@gradId int
as 
begin
	select * from Kupac where GradID = @gradId
end
go

-- Dohvati traženog kupca.
create proc _GetCustomerById
	@IDKupac int
as
begin
	select * from Kupac where IDKupac = @IDKupac
end
go

-- Ažuriraj podatke kupca.
create proc _UpdateCustomer
	@IDKupac int,
	@Ime nvarchar(50),
	@Prezime nvarchar(50),
	@Email nvarchar(50),
	@Telefon nvarchar(50),
	@GradID int
as
begin
	update Kupac set Ime = @Ime, Prezime = @Prezime, Email = @Email, Telefon = @Telefon, GradID = @GradID where IDKupac = @IDKupac
end
go

-- Dohvati račune od kupca.
/*
create proc _GetInvoiceByCustomer
	@idKupac int
as 
begin
	select r.* from Kupac as k
	inner join Racun as r
	on k.IDKupac = r.KupacID
	where k.IDKupac = @idKupac
end
go
*/

create proc _GetProducts
as
begin
	select * from Proizvod
end
go

create proc _GetProduct
	@IDProizvod int
as
begin
	select * from Proizvod where IDProizvod = @IDProizvod
end
go

create proc _InsertProduct
	@Naziv nvarchar(50),
	@BrojProizvoda nvarchar(25),
	@Boja nvarchar(15),
	@MinimalnaKolicinaNaSkladistu smallint,
	@CijenaBezPDV money,
	@PotkategorijaID int
as
begin
	insert into Proizvod values(@Naziv, @BrojProizvoda, @Boja, @MinimalnaKolicinaNaSkladistu, @CijenaBezPDV, @PotkategorijaID)
end
go

create proc _DeleteProduct
	@id int
as
begin
	delete from Proizvod where IDProizvod = @id
end
go

select * from Proizvod

create proc _UpdateProduct
	@IDProizvod int,
	@Naziv nvarchar(50),
	@BrojProizvoda nvarchar(50),
	@Boja nvarchar(50),
	@MinimalnaKolicinaNaSkladistu int,
	@CijenaBezPDV money,
	@PotkategorijaID int
as
begin
	update Proizvod set Naziv = @Naziv, BrojProizvoda = @BrojProizvoda, Boja = @Boja, MinimalnaKolicinaNaSkladistu = @MinimalnaKolicinaNaSkladistu, 
	CijenaBezPDV = @CijenaBezPDV, PotkategorijaID = @PotkategorijaID where IDProizvod = @IDProizvod
end
go

create proc _GetCategories
as
begin
	select * from Kategorija
end
go

create proc _DeleteCategory
	@id int
as
begin
	delete from Kategorija where IDKategorija = @id
end
go

create proc _GetCategory
	@id int
as
begin
	select * from Kategorija where IDKategorija = @id
end
go

create proc _UpdateCategory
	@IDKategorija int,
	@Naziv nvarchar(50)
as
begin
	update Kategorija set Naziv = @Naziv where IDKategorija = @IDKategorija
end
go

create proc _GetSubcategories
as
begin
	select * from Potkategorija
end
go

create proc _DeleteSubcategory
	@id int
as
begin
	delete from Potkategorija where IDPotkategorija = @id
end
go

create proc _GetSubcategory
	@id int
as
begin
	select * from Potkategorija where IDPotkategorija = @id
end
go

create proc _UpdateSubcategory
	@IDPotkategorija int,
	@KategorijaID int,
	@Naziv nvarchar(50)
as
begin
	update Potkategorija set KategorijaID = @KategorijaID, Naziv = @Naziv where IDPotkategorija = @IDPotkategorija
end
go