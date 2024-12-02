-- Crear la base de datos
CREATE DATABASE db_ventas;
GO

-- Seleccionar la base de datos
USE db_ventas;
GO

-- Tabla Clientes: Información básica de los clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FechaNacimiento DATE NOT NULL CHECK (FechaNacimiento <= GETDATE())
);
GO

-- Tabla Pedidos: Registro de los pedidos realizados por los clientes
CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    Comentario NVARCHAR(255),
    FechaCreacion DATE DEFAULT GETDATE(),
    TotalPedido DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clientes(Id)
);
GO

-- Tabla Articulos: Información sobre los productos disponibles
CREATE TABLE Articulos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Stock INT NOT NULL CHECK (Stock >= 0)
);
GO

-- Tabla DetallePedidos: Detalles de los artículos incluidos en cada pedido
CREATE TABLE DetallePedidos (
    IdPedido INT NOT NULL,
    IdLinea INT IDENTITY(1,1) PRIMARY KEY,
    IdArticulo INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    TotalLinea DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(Id) ON DELETE CASCADE,
    FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id)
);
GO


-- ------------------------------------------------------------------------------ -- 

-- Insertar datos en la tabla Clientes
INSERT INTO Clientes (Nombre, Apellido, FechaNacimiento) 
VALUES 
('Juan', 'Pérez', '1990-05-15'),
('María', 'Gómez', '1985-08-22'),
('Carlos', 'López', '2000-01-12');
GO

-- Insertar datos en la tabla Articulos
INSERT INTO Articulos (Nombre, Descripcion, Precio, Stock) 
VALUES 
('Laptop', 'Laptop de última generación', 5000000, 10),  
('Mouse', 'Mouse inalámbrico', 120000, 50),
('Teclado', 'Teclado mecánico retroiluminado', 300000, 30);
GO

-- Insertar datos en la tabla Pedidos
INSERT INTO Pedidos (IdCliente, Comentario, TotalPedido) 
VALUES 
(1, 'Urgente', 5120000),  
(2, NULL, 420000),
(3, 'Entrega a domicilio', 480000);
GO

-- Insertar datos en la tabla DetallePedidos
INSERT INTO DetallePedidos (IdPedido, IdArticulo, Cantidad, TotalLinea) 
VALUES 
(1, 1, 1, 5000000),  
(1, 2, 1, 120000),
(2, 3, 1, 300000),
(3, 2, 4, 480000);
GO



-- ---------------------------------------------------------- -- 

-- Procedimiento para registrar un cliente
CREATE PROCEDURE sp_RegistrarCliente
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Estado BIT,
    @FechaNacimiento DATE,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Insertar un nuevo cliente
        INSERT INTO Clientes (Nombre, Apellido, Estado, FechaNacimiento)
        VALUES (@Nombre, @Apellido, @Estado, @FechaNacimiento);

        SET @Resultado = SCOPE_IDENTITY(); -- Retorna el ID del cliente registrado
        SET @Mensaje = 'Cliente registrado con éxito.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

-- Procedimiento para registrar un artículo
CREATE PROCEDURE sp_RegistrarArticulo
    @Nombre NVARCHAR(50),
    @Descripcion NVARCHAR(255),
    @Precio DECIMAL(10,2),
    @Stock INT,
    @Resultado INT OUTPUT,
    @Mensaje NVARCHAR(500) OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Validar que el nombre del artículo no sea nulo o vacío
        IF (@Nombre IS NULL OR @Nombre = '')
        BEGIN
            SET @Resultado = 0;
            SET @Mensaje = 'El nombre del artículo es obligatorio.';
            RETURN;
        END

        -- Insertar un nuevo artículo
        INSERT INTO Articulos (Nombre, Descripcion, Precio, Stock)
        VALUES (@Nombre, @Descripcion, @Precio, @Stock);

        SET @Resultado = 1; -- Indica éxito
        SET @Mensaje = 'Artículo registrado con éxito.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

-- Procedimiento para obtener totales específicos
CREATE PROCEDURE ObtenerTotales
AS
BEGIN
    -- Total de Clientes Activos (únicamente clientes con estado activo)
    DECLARE @TotalClientes INT;
    SELECT @TotalClientes = COUNT(DISTINCT c.Id)
    FROM Clientes c
    WHERE c.Estado = 1;  -- Estado activo es '1'

    -- Total de Pedidos Activos (considerando las líneas de detalle de pedidos)
    DECLARE @TotalPedidos INT;
    SELECT @TotalPedidos = COUNT(*)
    FROM DetallePedidos dp
    INNER JOIN Pedidos p ON dp.IdPedido = p.Id
    WHERE p.Estado = 1;  -- Estado activo es '1'

    -- Total de Artículos Disponibles (artículos con stock positivo)
    DECLARE @TotalArticulos INT;
    SELECT @TotalArticulos = COUNT(*)
    FROM Articulos
    WHERE Stock > 0;

    -- Retornar los totales calculados
    SELECT 
        @TotalClientes AS TotalClientes,
        @TotalPedidos AS TotalPedidos,
        @TotalArticulos AS TotalArticulos;
END;
GO

-- Procedimiento para obtener el historial de ventas
CREATE PROCEDURE ObtenerHistorialVentas
    @FechaInicio DATE = NULL,  -- Fecha inicial del filtro
    @FechaFin DATE = NULL,     -- Fecha final del filtro
    @IdPedido INT = NULL       -- ID de pedido para filtrar resultados específicos
AS
BEGIN
    SET NOCOUNT ON;

    -- Seleccionar el historial de ventas con filtros opcionales
    SELECT 
        p.FechaCreacion AS FechaVenta,          -- Fecha de creación del pedido
        c.Nombre + ' ' + c.Apellido AS Cliente, -- Nombre completo del cliente
        a.Nombre AS Producto,                   -- Nombre del artículo
        dp.TotalLinea / dp.Cantidad AS Precio,  -- Precio unitario
        dp.Cantidad,                            -- Cantidad comprada
        dp.TotalLinea AS Total,                 -- Total de la línea
        p.Id AS IDPedido                        -- ID del pedido
    FROM 
        Pedidos p
        INNER JOIN Clientes c ON p.IdCliente = c.Id
        INNER JOIN DetallePedidos dp ON p.Id = dp.IdPedido
        INNER JOIN Articulos a ON dp.IdArticulo = a.Id
    WHERE
        (@FechaInicio IS NULL OR p.FechaCreacion >= @FechaInicio) AND -- Filtrar por fecha de inicio
        (@FechaFin IS NULL OR p.FechaCreacion <= @FechaFin) AND       -- Filtrar por fecha de fin
        (@IdPedido IS NULL OR p.Id = @IdPedido)                      -- Filtrar por ID de pedido
    ORDER BY 
        p.FechaCreacion; -- Ordenar por fecha de creación
END;
GO

-- Procedimiento para registrar un pedido con artículos
CREATE PROCEDURE RegistrarPedido
    @IdCliente INT,               -- ID del cliente que realiza el pedido
    @Articulos NVARCHAR(MAX),     -- Lista de artículos en formato JSON
    @Comentario NVARCHAR(255) = NULL -- Comentario opcional del pedido
AS
BEGIN
    -- Variables para manejo interno
    DECLARE @NuevoPedidoId INT; 
    DECLARE @TotalPedido DECIMAL(10, 2) = 0;
    DECLARE @ArticuloId INT, @Cantidad INT, @Precio DECIMAL(10, 2), @TotalLinea DECIMAL(10, 2);

    -- Insertar un nuevo pedido
    INSERT INTO Pedidos (IdCliente, Estado, Comentario, FechaCreacion, TotalPedido)
    VALUES (@IdCliente, 1, @Comentario, GETDATE(), @TotalPedido);

    -- Obtener el ID del nuevo pedido creado
    SET @NuevoPedidoId = SCOPE_IDENTITY();

    -- Configurar un cursor para recorrer los artículos proporcionados en JSON
    DECLARE ArticulosCursor CURSOR FOR
        SELECT 
            JSON_VALUE(Articulo.value, '$.IdArticulo') AS IdArticulo,
            JSON_VALUE(Articulo.value, '$.Cantidad') AS Cantidad
        FROM OPENJSON(@Articulos) AS Articulo;

    -- Abrir el cursor y recorrer los datos
    OPEN ArticulosCursor;
    FETCH NEXT FROM ArticulosCursor INTO @ArticuloId, @Cantidad;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Obtener el precio del artículo
        SELECT @Precio = Precio FROM Articulos WHERE Id = @ArticuloId;

        -- Calcular el total de la línea
        SET @TotalLinea = @Precio * @Cantidad;

        -- Insertar el detalle del pedido
        INSERT INTO DetallePedidos (IdPedido, IdArticulo, Cantidad, TotalLinea)
        VALUES (@NuevoPedidoId, @ArticuloId, @Cantidad, @TotalLinea);

        -- Acumular el total del pedido
        SET @TotalPedido = @TotalPedido + @TotalLinea;

        -- Obtener el siguiente artículo del cursor
        FETCH NEXT FROM ArticulosCursor INTO @ArticuloId, @Cantidad;
    END;

    -- Cerrar y liberar el cursor
    CLOSE ArticulosCursor;
    DEALLOCATE ArticulosCursor;

    -- Actualizar el total del pedido en la tabla de pedidos
    UPDATE Pedidos
    SET TotalPedido = @TotalPedido
    WHERE Id = @NuevoPedidoId;
END;
GO





