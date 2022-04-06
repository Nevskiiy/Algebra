use AdventureWorksOBP
go

create proc _GetCountry
as
begin
	select * from Drzava
end
go

create proc _GetTowns
	@drzavaId int
as
begin
	select * from Grad where DrzavaID = @drzavaId
end
go

create proc _DeleteTown
	@gradId int
as
begin
	delete from Grad where IDGrad = @gradId
end
go

create proc _InsertTown
	@naziv nvarchar(50),
	@drzavaId int
as
begin
	insert into Grad values (@naziv, @drzavaId)
end
go

-- 12.01.2022

create proc _GetCustomers
as
begin
	select * from Kupac
end
go

create proc _GetCustomerByTown
	@gradId int
as 
begin
	select * from Kupac where GradID = @gradId
end
go

create proc _GetCustomerById
	@IDKupac int
as
begin
	select * from Kupac where IDKupac = @IDKupac
end
go

create proc _UpdateCustomer
	@IDKupac int,
	@Ime nvarchar(50),
	@Prezime nvarchar(50),
	@Email nvarchar(50),
	@Telefon nvarchar(50)
as
begin
	update Kupac set Ime = @Ime, Prezime = @Prezime, Email = @Email, Telefon = @Telefon where IDKupac = @IDKupac
end
go

create proc _GetTown
	@gradId int
as
begin
	select * from Grad where IDGrad = @gradId
end
go


select * from Kupac where GradID = 1