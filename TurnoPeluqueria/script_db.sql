create database [db_turnos_peluqueria]
GO
/****** Object:  Table [dbo].[T_TURNOS]    Script Date: 02/03/2024 15:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_TURNOS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [varchar](10) NULL,
	[hora] [varchar](5) NULL,
	[cliente] [varchar](100) NULL,
 CONSTRAINT [PK_T_TURNOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_SERVICIOS]    Script Date: 02/03/2024 15:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_SERVICIOS](
	[id] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[costo] [int] NOT NULL,
	[enPromocion] [varchar](1) NOT NULL,
 CONSTRAINT [PK_T_SERVICIOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_DETALLES_TURNO]    Script Date: 02/03/2024 15:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_DETALLES_TURNO](
	[id_turno] [int] NOT NULL,
	[id_servicio] [int] NOT NULL,
	[observaciones] [varchar](200) NULL,
 CONSTRAINT [PK_T_DETALLES_TURNO] PRIMARY KEY CLUSTERED 
(
	[id_turno] ASC,
	[id_servicio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_DETALLES]    Script Date: 02/03/2024 15:01:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLES] 
	@id_turno int,
	@id_servicio int, 
	@observaciones varchar(200)
AS
BEGIN
	INSERT INTO T_DETALLES_TURNO(id_turno,id_servicio, observaciones)
    VALUES (@id_turno,@id_servicio, @observaciones);
  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONTAR_TURNOS]    Script Date: 02/03/2024 15:01:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONTAR_TURNOS]
    @fecha VARCHAR(10),
    @hora VARCHAR(8),
    @ctd_turnos INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @ctd_turnos = COUNT(*)
    FROM T_TURNOS
    WHERE fecha = @fecha AND hora = @hora;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_SERVICIOS]    Script Date: 02/03/2024 15:01:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_SERVICIOS]
AS
BEGIN
	
	SELECT * from T_SERVICIOS ORDER BY 2;
END
GO

CREATE PROCEDURE [dbo].[INSERTAR_MAESTRO] 
	@fecha varchar(100),
	@hora varchar (100),
	@cliente varchar(100), 
	@id int output
AS
BEGIN
	INSERT INTO T_TURNOS (fecha, hora, cliente) VALUES(@fecha,@hora,@cliente);
	SET @id = SCOPE_IDENTITY();
END
