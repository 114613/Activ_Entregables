CREATE DATABASE Practica03

USE [Practica03]
GO

--CREACION TABLAS
CREATE TABLE FormaPago (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Cliente (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(200)
);

CREATE TABLE Articulo (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(18, 2) NOT NULL,
	Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Factura (
    NroFactura INT PRIMARY KEY IDENTITY,
    Fecha DATETIME NOT NULL,
    FormaPagoId INT FOREIGN KEY REFERENCES FormaPago(Id),
    ClienteId INT FOREIGN KEY REFERENCES Cliente(Id)
);

CREATE TABLE DetalleFactura (
    Id INT PRIMARY KEY IDENTITY,
    FacturaId INT FOREIGN KEY REFERENCES Factura(NroFactura),
    ArticuloId INT FOREIGN KEY REFERENCES Articulo(Id),
    Cantidad INT NOT NULL
);


--CREACION STORED PROCEDURE
CREATE PROCEDURE SP_Insertar_Factura
(
    @Fecha DATETIME,
    @FormaPagoId INT,
    @ClienteId INT
)
AS
BEGIN
    INSERT INTO Factura (Fecha, FormaPagoId, ClienteId)
    VALUES (@Fecha, @FormaPagoId, @ClienteId);

    SELECT SCOPE_IDENTITY() AS NroFactura;
END;


CREATE PROCEDURE SP_Insertar_DetalleFactura
(
    @FacturaId INT,
    @ArticuloId INT,
    @Cantidad INT
)
AS
BEGIN
    INSERT INTO DetalleFactura (FacturaId, ArticuloId, Cantidad)
    VALUES (@FacturaId, @ArticuloId, @Cantidad);
END;


CREATE PROCEDURE SP_Actualizar_Factura
    @NroFactura INT,
    @Fecha DATE,
    @FormaPago int,
    @Cliente int
AS
BEGIN
    UPDATE Factura
    SET Fecha = @Fecha,
        FormaPagoId = @FormaPago,
        ClienteId = @Cliente
    WHERE NroFactura = @NroFactura
END


CREATE PROCEDURE SP_Eliminar_Factura
    @NroFactura INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE FacturaId = @NroFactura
    DELETE FROM Factura WHERE NroFactura = @NroFactura
END


--Practica02
CREATE PROCEDURE SP_Agregar_Articulo
    @Nombre NVARCHAR(50),
    @PrecioUnitario DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Articulo (Nombre, PrecioUnitario)
    VALUES (@Nombre, @PrecioUnitario);
END;



CREATE PROCEDURE SP_Consultar_Articulo
    @Id INT
AS
BEGIN
    SELECT * FROM Articulo WHERE Id = @Id;
END;



CREATE PROCEDURE SP_Listar_Articulos
AS
BEGIN
    SELECT * FROM Articulo;
END;


CREATE PROCEDURE SP_Editar_Articulo
    @Id INT,
    @Nombre NVARCHAR(50),
    @PrecioUnitario DECIMAL(18, 2)
AS
BEGIN
UPDATE Articulo
    SET Nombre = @Nombre, PrecioUnitario = @PrecioUnitario
    WHERE Id = @Id;
END;


CREATE PROCEDURE SP_Eliminar_Articulo
    @Id INT
AS
BEGIN
    DELETE FROM Articulo WHERE Id = @Id;
END;


--Practica 03
CREATE PROCEDURE SP_Registrar_Baja_Articulo
    @Id INT
AS
BEGIN
    UPDATE Articulo SET Activo = 0 WHERE Id = @Id;
END


CREATE PROCEDURE SP_Listar_Articulo
AS
BEGIN
    SELECT * FROM Articulo WHERE Activo = 1;
END


--Facturas
CREATE PROCEDURE sp_RegistrarFactura
    @Fecha DATETIME,
    @FormaPago NVARCHAR(100),
    @Cliente NVARCHAR(100)
AS
BEGIN
    INSERT INTO Factura (Fecha, FormaPagoId, ClienteId) VALUES (@Fecha, @FormaPago, @Cliente);
END


CREATE PROCEDURE SP_Consultar_Factura
    @NroFactura INT
AS
BEGIN
    SELECT * FROM Factura WHERE NroFactura = @NroFactura;
END


CREATE PROCEDURE SP_Editar_Factura
    @NroFactura INT,
    @Fecha DATETIME,
    @FormaPago NVARCHAR(100),
    @Cliente NVARCHAR(100)
AS
BEGIN
    UPDATE Factura
    SET Fecha = @Fecha, FormaPagoId = @FormaPago, ClienteId = @Cliente 
    WHERE NroFactura = @NroFactura;
END


CREATE PROCEDURE SP_Registrar_Baja_Factura
    @NroFactura INT
AS
BEGIN
    DELETE FROM Factura WHERE NroFactura = @NroFactura;
END


CREATE PROCEDURE SP_Consultar_Facturas
    @Fecha DATETIME = NULL,
    @FormaPago NVARCHAR(100) = NULL
AS
BEGIN
    SELECT * FROM Factura
    WHERE (@Fecha IS NULL OR Fecha = @Fecha)
    AND (@FormaPago IS NULL OR FormaPagoId = @FormaPago);
END


--Detalles Facturas
CREATE PROCEDURE SP_Agregar_DetallesFactura
    @NroFactura INT,
    @ArticuloId INT,
    @Cantidad INT
AS
BEGIN
    INSERT INTO DetalleFactura (FacturaId, ArticuloId, Cantidad) 
    VALUES (@NroFactura, @ArticuloId, @Cantidad);
END


CREATE PROCEDURE SP_Consultar_DetalleFactura
    @NroFactura INT
AS
BEGIN
    SELECT d.ArticuloId, a.Nombre, a.PrecioUnitario, d.Cantidad
    FROM DetalleFactura d
    JOIN Articulo a ON d.ArticuloId = a.Id
    WHERE d.FacturaId = @NroFactura;
END