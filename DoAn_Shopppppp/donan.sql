CREATE DATABASE QL_SHOP
use QL_SHOP

CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] nvarchar(25), 
	[TenNhanVien] [nvarchar](50),
	[NgaySinh] [date],
	[GioiTinh] [nvarchar](10),
	[NgayVaoLam] [date],
	[ChucVu] int,
	[DiaChi] [nvarchar](50),
	[SoDT] [varchar](11),
	CONSTRAINT PK_NHANVIEN PRIMARY KEY([MaNhanVien])
	)

CREATE TABLE [dbo].[NhaPhanPhoi](
	[MaNhaPhanPhoi] int IDENTITY(1,1), 
	[TenNhaPhanPhoi] [nvarchar](50),
	[DiaChi] [nvarchar](50),
	[SDT] [varchar](11),
	[Email] [varchar](100),
 CONSTRAINT [PK_NhaPhanPhoi] PRIMARY KEY(MaNhaPhanPhoi) )

CREATE TABLE [dbo].[SanPham](
	[MaSanPham] nvarchar(25), 
	[MaNhaPhanPhoi] int,
	[TenSanPham] [nvarchar](50),
	[LoaiSanPham] int,
	[HangSanXuat] int,
	[GiaBan] [money],
	[TonKho] [int] NOT NULL,
	[Image] image NULL CONSTRAINT [DF_SanPham_image]  DEFAULT (''),
	CONSTRAINT PK_SP PRIMARY KEY(MaSanPham))
	
CREATE TABLE [dbo].[PhieuNhap](
	[MaPhieuNhap] nvarchar(25), 
	[MaNhanVien] nvarchar(25),
	[MaNhaPhanPhoi] int,
	[TongTien] [money],
	[NgayNhap] [date],
	CONSTRAINT PK_PN PRIMARY KEY(MaPhieuNhap))

CREATE TABLE [dbo].[ChucVu](
	[MaChucVu] int IDENTITY(1,1), 
	[TenChucVu] [nvarchar](50),
	CONSTRAINT PK_CV PRIMARY KEY(MaChucVu))

CREATE TABLE [dbo].[LoaiSanPham](
	[MaLoaiSanPham] int IDENTITY(1,1), 
	[TenLoaiSanPham] [nvarchar](50),
	CONSTRAINT PK_LH PRIMARY KEY(MaLoaiSanPham))

CREATE TABLE [dbo].[HangSanXuat](
	[MaHangSanXuat] int IDENTITY(1,1), 
	[TenHangSanXuat] [nvarchar](50),
	CONSTRAINT PK_HSX PRIMARY KEY(MaHangSanXuat))

CREATE TABLE [dbo].[ChiTietPhieuNhap](
	[MaPhieuNhap] nvarchar(25),
	[MaSanPham] nvarchar(25),
	[SoLuong] [int],
	[TienNhap] [money],
	CONSTRAINT PK_CTPN PRIMARY KEY(MaPhieuNhap,MaSanPham))

CREATE TABLE [dbo].[ChiTietDotKhuyenMai]
(
	[MaDot] int,
	[MaSanPham] nvarchar(25),
	[TiLeGiamGia] float,
	CONSTRAINT PK_CTDKM PRIMARY KEY (MaDot,MaSanPham)
)

CREATE TABLE [dbo].[KhachHang](
	[Gmail] [nvarchar](50),
	[Pass] [nvarchar](50),
	[TenKhachHang] [nvarchar](50),
	[Ngaysinh] [date],
	[GioiTinh] [nvarchar](10),
	[DiaChi] [nvarchar](50),
	[SDT] [varchar](11),
	CONSTRAINT PK_KHACHHANG PRIMARY KEY(Gmail))

CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaHoaDon] int IDENTITY(1,1), 
	[MaSanPham] [nvarchar](25),
	[Gmail] nvarchar(50),
	[MucGiam] [money],
	[SoLuong] [int],
	[TongTien] [money],
	[TongTienHoaDon] [money],
	[NgayLapHoaDon] [date],
	CONSTRAINT PK_CTHOADON PRIMARY KEY(MaHoaDon))

CREATE TABLE [dbo].[DotKhuyenMai]
(
	[MaDot] int IDENTITY(1,1), 
	[NgayBD] DATE,
	[NgayKT] DATE,
	CONSTRAINT PK_DOTKM PRIMARY KEY(MaDot)
)
CREATE TABLE [dbo].[Users](
	[TenDangNhap] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[MaNhanVien] nvarchar(25),
	[Quyen] [nvarchar](25),
	CONSTRAINT PK_TaiKhoan PRIMARY KEY (TenDangNhap)
)

--KHOÁ NGOẠI
ALTER TABLE [dbo].[SanPham]
ADD CONSTRAINT FK_SP_NCC FOREIGN KEY(MaNhaPhanPhoi) REFERENCES [dbo].[NhaPhanPhoi](MaNhaPhanPhoi)
ALTER TABLE [dbo].[SanPham]
ADD CONSTRAINT FK_SP_LH FOREIGN KEY(LoaiSanPham) REFERENCES [dbo].[LoaiSanPham](MaLoaiSanPham)
ALTER TABLE [dbo].[SanPham]
ADD CONSTRAINT FK_SP_HSX FOREIGN KEY(HangSanXuat) REFERENCES [dbo].[HangSanXuat](MaHangSanXuat)
ALTER TABLE [dbo].[PhieuNhap]
ADD CONSTRAINT FK_PN_NCC FOREIGN KEY(MaNhaPhanPhoi) REFERENCES [dbo].[NhaPhanPhoi](MaNhaPhanPhoi)
ALTER TABLE [dbo].[PhieuNhap]
ADD CONSTRAINT FK_PN_NV FOREIGN KEY(MaNhanVien) REFERENCES [dbo].[NhanVien](MaNhanVien)
ALTER TABLE [dbo].[NhanVien]
ADD CONSTRAINT FK_NV_CV FOREIGN KEY(ChucVu) REFERENCES [dbo].[ChucVu](MaChucVu)
ALTER TABLE [dbo].[ChiTietPhieuNhap]
ADD CONSTRAINT FK_CTPN_PN FOREIGN KEY(MaPhieuNhap) REFERENCES [dbo].[PhieuNhap](MaPhieuNhap)
ALTER TABLE [dbo].[ChiTietPhieuNhap]
ADD CONSTRAINT FK_CTPN_SP FOREIGN KEY(MaSanPham) REFERENCES [dbo].[SanPham](MaSanPham)
ALTER TABLE [dbo].[ChiTietDotKhuyenMai]
ADD CONSTRAINT FK_CTDKM_DKM FOREIGN KEY(MaDot) REFERENCES [dbo].[DotKhuyenMai](MaDot)
ALTER TABLE [dbo].[ChiTietDotKhuyenMai]
ADD CONSTRAINT FK_CTDKM_SP FOREIGN KEY(MaSanPham) REFERENCES [dbo].[SanPham](MaSanPham)
ALTER TABLE [dbo].[ChiTietHoaDon]
ADD CONSTRAINT FK_CT_HOADON_SANPHAM FOREIGN KEY(MaSanPham) REFERENCES [dbo].[SanPham](MaSanPham)
ALTER TABLE [dbo].[ChiTietHoaDon]
ADD CONSTRAINT FK_CT_HOADON_User FOREIGN KEY(Gmail) REFERENCES [dbo].[KhachHang](Gmail)
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT FK_User FOREIGN KEY(MaNhanVien) REFERENCES [dbo].[NhanVien](MaNhanVien)
-----------------------RẰNG BUỘC--------------------------------------------
ALTER TABLE [dbo].[ChiTietPhieuNhap]
ADD CONSTRAINT CK_PN CHECK(SoLuong > 0)
ALTER TABLE [dbo].[DotKhuyenMai]
ADD CONSTRAINT CK_NGAY CHECK(NgayBD<NgayKT)
ALTER TABLE [dbo].[SanPham]
ADD CONSTRAINT CK_GIA CHECK(GiaBan > 0)
ALTER TABLE [dbo].[KhachHang]
ADD CONSTRAINT CK_KhachHang CHECK(GioiTinh = N'Nam' or GioiTinh = N'Nữ')
ALTER TABLE [dbo].[NhanVien]
ADD CONSTRAINT CK_NhanVien CHECK(GioiTinh = N'Nam' or GioiTinh = N'Nữ')
ALTER TABLE [dbo].[SanPham]
ADD CONSTRAINT UNI_TENSP UNIQUE(TenSanPham)
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT UNI_TENND UNIQUE(TenDangNhap)
------update tiềnpn------
Go
CREATE TRIGGER TONGTIENPN ON [dbo].[ChiTietPhieuNhap]
FOR INSERT,UPDATE
AS
BEGIN
	UPDATE [dbo].[PhieuNhap]
	SET TongTien = (SELECT SUM(ChiTietPhieuNhap.SoLuong*ChiTietPhieuNhap.TienNhap)
						FROM ChiTietPhieuNhap,inserted
						WHERE ChiTietPhieuNhap.MaPhieuNhap = inserted.MaPhieuNhap
						GROUP BY ChiTietPhieuNhap.MaPhieuNhap)
	FROM PhieuNhap, inserted
	WHERE PhieuNhap.MaPhieuNhap = inserted.MaPhieuNhap
	------------------------
	UPDATE SanPham
	SET TonKho = TonKho + (SELECT SoLuong FROM inserted)
	WHERE MaSanPham=(SELECT MaSanPham FROM inserted)
END
--ngày nhập phải trước ngày hiện tại--
CREATE TRIGGER KT_NGAYNHAP ON PHIEUNHAP
FOR INSERT, UPDATE
AS
	DECLARE @MAPN CHAR(10),@NGAYNHAP DATE
	IF NOT EXISTS (SELECT * FROM deleted)
	BEGIN
		SELECT @MAPN = MaPhieuNhap,@NGAYNHAP = NgayNhap
		FROM inserted
		WHERE MaPhieuNhap = @MAPN
		IF(@NGAYNHAP>GETDATE())
		BEGIN
			RAISERROR (N'Ngày nhập phải trước ngày hiện tại',16,1)
			ROLLBACK
			RETURN
		END
		IF DATEDIFF(DD,@NGAYNHAP,GETDATE()) > 30
		BEGIN
			RAISERROR (N'Ngày hiện tại - ngày nhập <= 30 ngày',16,1)
			ROLLBACK
			RETURN
		END
	END
	ELSE
	BEGIN
		IF UPDATE(NgayNhap)
		BEGIN
			SELECT @MAPN = MaPhieuNhap,@NGAYNHAP = NgayNhap
			FROM inserted
			WHERE MaPhieuNhap = @MAPN
			IF(@NGAYNHAP>GETDATE())
		BEGIN
			RAISERROR (N'Ngày nhập phải trước ngày hiện tại',16,1)
			ROLLBACK
			RETURN
		END
		IF DATEDIFF(DD,@NGAYNHAP,GETDATE()) > 30
		BEGIN
			RAISERROR (N'Ngày hiện tại - ngày nhập <= 30 ngày',16,1)
			ROLLBACK
			RETURN
		END
	END
END
--Ngày BD trước Ngày KT [ChiTietDKM]
CREATE TRIGGER KT_DKM ON DotKhuyenMai
FOR INSERT, UPDATE
AS
	DECLARE @madot CHAR(10),@ngaybd DATE,@ngaykt DATE
	IF NOT EXISTS (SELECT * FROM deleted)
	BEGIN
		SELECT @madot = MaDot,@ngaykt = NgayKT
		FROM inserted
		SELECT @ngaybd = NgayBD
		FROM DotKhuyenMai
		WHERE MaDot = @madot
		IF(@ngaykt<@ngaybd)
		BEGIN
			RAISERROR (N'Ngày kết thúc phải sau ngày bắt đầu',16,1)
			ROLLBACK
			RETURN
		END
		IF DATEDIFF(DD,@ngaybd,@ngaykt) > 30
		BEGIN
			RAISERROR (N'Ngày kết thúc - ngày bắt đầu <= 30 ngày',16,1)
			ROLLBACK
			RETURN
		END
	END
	ELSE
	BEGIN
		IF UPDATE(NgayKT)
		BEGIN
		SELECT @madot = MaDot,@ngaykt = NgayKT
		FROM inserted
		SELECT @ngaybd = NgayBD
		FROM DotKhuyenMai
		WHERE MaDot = @madot
		IF(@ngaykt<@ngaybd)
		BEGIN
			RAISERROR (N'Ngày kết thúc phải sau ngày bắt đầu',16,1)
			ROLLBACK
			RETURN
		END
		IF DATEDIFF(DD,@ngaybd,@ngaykt) > 30
		BEGIN
			RAISERROR (N'Ngày kết thúc - ngày bắt đầu <= 30 ngày',16,1)
			ROLLBACK
			RETURN
		END
	END
END
--------------------tính tiền
create trigger cntien on [dbo].[ChiTietHoaDon]
for insert
as
begin
	declare @mahd int,@masp nchar(10),@gmail nvarchar(50),@sl int,@ngaydat date, @tiensp money,@tongtien money
	set @mahd=(select MaHoaDon from inserted)
	set @masp=(select MaSanPham from inserted)
	set @gmail=(select Gmail from inserted)
	set @sl=(select SoLuong from inserted)
	set @ngaydat=(select NgayLapHoaDon from inserted)
	set @tiensp=(select GiaBan from SanPham where MaSanPham=@masp)
	update [dbo].[ChiTietHoaDon]
	set TongTien=@tiensp where MaSanPham=@masp
end

create trigger cntt on [dbo].[ChiTietHoaDon]
for insert
as
begin
	declare @mahd int,@masp nchar(10),@gmail nvarchar(50),@sl int,@ngaydat date,@tiensp money,@tlgiam float,@madot int,@tongtt money
	set @mahd=(select MaHoaDon from inserted)
	set @masp=(select MaSanPham from inserted)
	set @gmail=(select Gmail from inserted)
	set @sl=(select SoLuong from inserted)
	set @ngaydat=(select NgayLapHoaDon from inserted)
	set @tiensp=(select GiaBan from SanPham where MaSanPham=@masp)
	set @madot=(select MaDot from DotKhuyenMai where @ngaydat between NgayBD and NgayKT)
	set @tlgiam=(select TiLeGiamGia from ChiTietDotKhuyenMai where MaDot=@madot and MaSanPham=@masp)
	if(@tlgiam is null)
	begin
		set @tlgiam=1
	end
	else
	begin
		set @tlgiam=@tlgiam
	end
	set @tongtt=(@sl*@tiensp*@tlgiam)
	update [dbo].[ChiTietHoaDon]
	set TongTienHoaDon=@tongtt where MaSanPham=@masp
end

create trigger mucgiam on [dbo].[ChiTietHoaDon]
for insert
as
begin
	declare @mahd int,@masp nchar(10),@gmail nvarchar(50),@sl int,@ngaydat date,@tiensp money,@tlgiam float,@madot int,@tongtt money
	set @mahd=(select MaHoaDon from inserted)
	set @masp=(select MaSanPham from inserted)
	set @gmail=(select Gmail from inserted)
	set @sl=(select SoLuong from inserted)
	set @ngaydat=(select NgayLapHoaDon from inserted)
	set @tiensp=(select GiaBan from SanPham where MaSanPham=@masp)
	set @madot=(select MaDot from DotKhuyenMai where @ngaydat between NgayBD and NgayKT)
	set @tlgiam=(select TiLeGiamGia from ChiTietDotKhuyenMai where MaDot=@madot and MaSanPham=@masp)
	if(@tlgiam is null)
	begin
		set @tlgiam=1
	end
	else
	begin
		set @tlgiam=@tlgiam
	end
	set @tongtt=((@sl*@tiensp) - (@sl*@tiensp*@tlgiam))
	update [dbo].[ChiTietHoaDon]
	set MucGiam=@tongtt where MaHoaDon=@mahd
end
--tự động giảm khi tạo hóa đơn
go
CREATE TRIGGER SOLUONG2
ON ChiTietHoaDon for insert
as
begin
		DECLARE @SOLUONGGIAM INT;
		SELECT @SOLUONGGIAM = SanPham.TonKho FROM SanPham, inserted WHERE inserted.MaSanPham = SanPham.MaSanPham
		IF(@SOLUONGGIAM < 0)
		BEGIN
			RAISERROR(N'HIỆN TẠI MẶT HÀNG KHÔNG ĐỦ SỐ LƯỢNG',16,1)
		END
		ELSE
		BEGIN
		UPDATE SanPham
		SET TonKho = TonKho - (SELECT SoLuong FROM inserted) FROM inserted,SanPham WHERE inserted.MaSanPham =SanPham.MaSanPham
		END
end

go
CREATE TRIGGER SOLUONG3
ON ChiTietHoaDon for delete
as
begin
  BEGIN
  UPDATE SanPham
  SET TonKho = TonKho + (SELECT SoLuong FROM deleted) FROM deleted,SanPham WHERE deleted.MaSanPham =SanPham.MaSanPham
  END
end

Go
CREATE TRIGGER SOLuongPN ON [dbo].[ChiTietPhieuNhap]
FOR delete
AS
BEGIN
	UPDATE SanPham
	SET TonKho = TonKho - (SELECT SoLuong FROM deleted)
	WHERE MaSanPham=(SELECT MaSanPham FROM deleted)
END
--------------------------------------------------------------------------------------------
SET DATEFORMAT DMY

SET IDENTITY_INSERT [NhaPhanPhoi] ON
INSERT [dbo].[NhaPhanPhoi] ([MaNhaPhanPhoi], [TenNhaPhanPhoi], [DiaChi], [SDT], [Email]) VALUES (1, N'Công Ty ABC', N'Hồ Chí Minh', N'098674322', N'lv@gmail.com')
INSERT [dbo].[NhaPhanPhoi] ([MaNhaPhanPhoi], [TenNhaPhanPhoi], [DiaChi], [SDT], [Email]) VALUES (2, N'Công Ty', N'Trung Quốc ', N'34657865434', N'cpo@gmail.com')
INSERT [dbo].[NhaPhanPhoi] ([MaNhaPhanPhoi], [TenNhaPhanPhoi], [DiaChi], [SDT], [Email]) VALUES (3, N'Hoà Bình', N'Việt Nam', N'34657865434', N'dr@gmail.com')
SET IDENTITY_INSERT [NhaPhanPhoi] OFF

SET IDENTITY_INSERT [LoaiSanPham] ON
INSERT [dbo].[LoaiSanPham] ([MaLoaiSanPham], [TenLoaiSanPham]) VALUES (1, N'Áo Thun')
INSERT [dbo].[LoaiSanPham] ([MaLoaiSanPham], [TenLoaiSanPham]) VALUES (2, N'Áo Khoác')
INSERT [dbo].[LoaiSanPham] ([MaLoaiSanPham], [TenLoaiSanPham]) VALUES (3, N'Quần Jean')
SET IDENTITY_INSERT [LoaiSanPham] OFF

SET IDENTITY_INSERT [HangSanXuat] ON
INSERT [dbo].[HangSanXuat] ([MaHangSanXuat], [TenHangSanXuat]) VALUES (1, N'LV')
INSERT [dbo].[HangSanXuat] ([MaHangSanXuat], [TenHangSanXuat]) VALUES (2, N'Champion')
INSERT [dbo].[HangSanXuat] ([MaHangSanXuat], [TenHangSanXuat]) VALUES (3, N'Gucci')
SET IDENTITY_INSERT [HangSanXuat] OFF

SET IDENTITY_INSERT [ChucVu] ON
INSERT [dbo].[ChucVu] ([MaChucVu], [TenChucVu]) VALUES (1, N'Thu Ngân')
INSERT [dbo].[ChucVu] ([MaChucVu], [TenChucVu]) VALUES (2, N'Kế Toán')
INSERT [dbo].[ChucVu] ([MaChucVu], [TenChucVu]) VALUES (3, N'Bảo Vệ')
SET IDENTITY_INSERT [ChucVu] OFF

INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [GioiTinh], [NgayVaoLam], [ChucVu], [DiaChi], [SoDT]) VALUES('N1',N'NGUYỄN NGỌC SƠN','11/1/1999',N'Nam','16/3/2019',1,N'Việt Nam',N'0123131231');
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [GioiTinh], [NgayVaoLam], [ChucVu], [DiaChi], [SoDT]) VALUES('N2',N'TRẦN TUẤN ANH','24/2/2001',N'Nam','16/4/2020',2,N'Thái Lan',N'0121231231');
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [GioiTinh], [NgayVaoLam], [ChucVu], [DiaChi], [SoDT]) VALUES('N3',N'NGUYỄN VĂN HÀO','14/3/1996',N'Nữ','16/1/2015',3,N'Campuchia',N'0176131231');

INSERT [dbo].[Users] ([TenDangNhap], [Password],[MaNhanVien], [Quyen]) VALUES (N'son', N'son','N1', 'ad')
INSERT [dbo].[Users] ([TenDangNhap], [Password],[MaNhanVien], [Quyen]) VALUES (N'huy', N'huy','N2', 'own')

INSERT [dbo].[PhieuNhap] ([MaPhieuNhap], [MaNhanVien], [MaNhaPhanPhoi], [TongTien], [NgayNhap]) VALUES ('P1','N1',1,NULL,'16/3/2021')
INSERT [dbo].[PhieuNhap] ([MaPhieuNhap], [MaNhanVien], [MaNhaPhanPhoi], [TongTien], [NgayNhap]) VALUES ('P2','N2',2,NULL,'16/3/2021')
INSERT [dbo].[PhieuNhap] ([MaPhieuNhap], [MaNhanVien], [MaNhaPhanPhoi], [TongTien], [NgayNhap]) VALUES ('P3','N3',3,NULL,'16/3/2021')

INSERT [dbo].[ChiTietPhieuNhap] ([MaPhieuNhap], [MaSanPham], [SoLuong], [TienNhap]) VALUES ('P1', 'S01', 3, 7390000.0000)
INSERT [dbo].[ChiTietPhieuNhap] ([MaPhieuNhap], [MaSanPham], [SoLuong], [TienNhap]) VALUES ('P2', 'S02', 43, 1800000.0000)
INSERT [dbo].[ChiTietPhieuNhap] ([MaPhieuNhap], [MaSanPham], [SoLuong], [TienNhap]) VALUES ('P3', 'S03', 1, 1300000.0000)

INSERT [dbo].[KhachHang] ([Gmail], [Pass], [TenKhachHang], [Ngaysinh], [GioiTinh], [DiaChi], [SDT]) VALUES ('sonlaso1119@gmail.com','123',N'ĐỖ GIA HUY','25/1/2001',N'Nam',N'BÌNH TÂN TPHCM',0903714326)
INSERT [dbo].[KhachHang] ([Gmail], [Pass], [TenKhachHang], [Ngaysinh], [GioiTinh], [DiaChi], [SDT]) VALUES ('sonlaso11119@gmail.com','123',N'NGUYỄN NGỌC SƠN','16/3/2001',N'Nam',N'LONG AN ',0906533337)
INSERT [dbo].[KhachHang] ([Gmail], [Pass], [TenKhachHang], [Ngaysinh], [GioiTinh], [DiaChi], [SDT]) VALUES ('sonlaso111119@gmail.com','123',N'NGUYỄN MINH TRUNG HIẾU','12/6/2001',N'Nam',N'BÌNH DƯƠNG ',0902114326)

SET IDENTITY_INSERT [DotKhuyenMai] ON
INSERT [dbo].[DotKhuyenMai] ([MaDot], [NgayBD], [NgayKT]) VALUES (1, '11/3/2021', '16/3/2021')
INSERT [dbo].[DotKhuyenMai] ([MaDot], [NgayBD], [NgayKT]) VALUES (2,'17/3/2021', '20/3/2021')
INSERT [dbo].[DotKhuyenMai] ([MaDot], [NgayBD], [NgayKT]) VALUES (3, '22/3/2021', '27/3/2021')
SET IDENTITY_INSERT [DotKhuyenMai] OFF

INSERT [dbo].[ChiTietDotKhuyenMai] ([MaDot], [MaSanPham], [TiLeGiamGia]) VALUES (1, 'S01',0.5)
INSERT [dbo].[ChiTietDotKhuyenMai] ([MaDot], [MaSanPham], [TiLeGiamGia]) VALUES (2, 'S01',0.6)
INSERT [dbo].[ChiTietDotKhuyenMai] ([MaDot], [MaSanPham], [TiLeGiamGia]) VALUES (2, 'S02',0.6)

SET IDENTITY_INSERT [ChiTietHoaDon] ON
INSERT [dbo].[ChiTietHoaDon] ([MaHoaDon], [MaSanPham],[Gmail], [MucGiam], [SoLuong], [TongTien], [TongTienHoaDon], [NgayLapHoaDon]) VALUES (1, 'S01','sonlaso1119@gmail.com', NULL, 5, 7890000.0000, NULL, '12/3/2021')
INSERT [dbo].[ChiTietHoaDon] ([MaHoaDon], [MaSanPham],[Gmail], [MucGiam], [SoLuong], [TongTien], [TongTienHoaDon], [NgayLapHoaDon]) VALUES (2, 'S02','sonlaso1119@gmail.com',NULL, 6,  1890000.0000, NULL, '15/3/2021')
INSERT [dbo].[ChiTietHoaDon] ([MaHoaDon], [MaSanPham],[Gmail], [MucGiam], [SoLuong], [TongTien], [TongTienHoaDon], [NgayLapHoaDon]) VALUES (3, 'S02','sonlaso1119@gmail.com', NULL, 8,  1890000.0000, NULL, '23/3/2021')
SET IDENTITY_INSERT [ChiTietHoaDon] OFF