
CREATE DATABASE KT2
GO
USE KT2
GO
--drop database KT2
-- TAO BANG

CREATE TABLE KHACHHANG(MAKH CHAR(10) NOT NULL,TENKHANG NVARCHAR(30),NGAYSINH DATE, SODT CHAR(12), DIACHI NVARCHAR(30))
GO
CREATE TABLE HOADON(MAHD CHAR(10) NOT NULL, NGAYLAP DATE, SOLUONG INT, DONGIA FLOAT, THANHTIEN float, MAKH CHAR(10))

GO
ALTER TABLE KHACHHANG 
ADD CONSTRAINT PK_KH PRIMARY KEY (MAKH)
GO
ALTER TABLE HOADON 
ADD CONSTRAINT PK_HD PRIMARY KEY (MAHD)
GO
ALTER TABLE HOADON
ADD CONSTRAINT FK_HD_KH FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
go 
insert into KHACHHANG
values('KH001', N'Hang', '1/1/2000', '123456',N'123 vo van ngan')
go
insert into KHACHHANG
values('KH002', N'Lan', '1/1/2001', '123456',N'123 dang van bi')
go
insert into KHACHHANG
values('KH003', N'Diep', '2/2/1999', '1234987',N'33 Le Loi')

insert into HOADON
values ('HD001', '10/10/2023', 20, 20, 400, 'KH001')
GO
insert into HOADON
values ('HD002', '6/4/2022', 10, 32, 500, 'KH002')
go

--select * from HOADON
--select * from KHACHHANG

--Stored procedure
create proc usp_LayAllHoaDon
as
	begin
		select * from HOADON
	end
go
--exec usp_LayAllHoaDon

create proc usp_ThemHoaDon(@MAHD CHAR(10), @NGAYLAP DATE, @SOLUONG INT, @DONGIA FLOAT, @THANHTIEN float, @MAKH CHAR(10))
as
	begin
		insert into HOADON values (@MAHD, @NGAYLAP, @SOLUONG, @DONGIA, @THANHTIEN, @MAKH)
	end
--exec usp_ThemHoaDon'HD003', '5/7/2023', '12', '10', '300', 'KH003'
go

create proc usp_XoaHoaDon(@MAHD CHAR(10))
as
	begin
		delete HOADON where MAHD = @MAHD
	end
go
--exec usp_XoaHoaDon'HD003'

create proc usp_SuaHoaDon(@MAHD CHAR(10), @NGAYLAP DATE, @SOLUONG INT, @DONGIA FLOAT, @THANHTIEN float, @MAKH CHAR(10))
as
	begin
		update HOADON set NGAYLAP = @NGAYLAP, SOLUONG = @SOLUONG, DONGIA = @DONGIA, THANHTIEN = @THANHTIEN, MAKH = @MAKH
		where MAHD = @MAHD
	end
go
--exec usp_SuaHoaDon'HD002', '5/7/2023', '22', '22', '222', 'KH003'

create proc usp_LayAllMaKH
as
	begin
		select MaKH from KHACHHANG
	end
go
--exec usp_LayAllMaKH


create proc usp_TimBangMaHD(@MAHD char(10))
as
	begin
		select * from HOADON where MAHD = @MAHD
	end
go
--exec usp_TimBangMaHD'HD002'


create proc usp_TimBangMaKH(@MAKH char(10))
as
	begin
		select * from KHACHHANG where MAKH = @MAKH
	end
go
--exec usp_TimBangMaKH'KH002'