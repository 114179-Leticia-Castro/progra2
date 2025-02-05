USE [db_envios]
GO
/****** Object:  Table [dbo].[T_Envio]    Script Date: 03/04/2024 02:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Envio](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[fecha_envio] [date] NOT NULL,
	[direccion] [varchar](50) NOT NULL,
	[estado] [varchar](50) NOT NULL,
	[dni_cliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Envios] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Empresas]    Script Date: 03/04/2024 02:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Empresas](
	[id] [int] NOT NULL,
	[razonSocial] [varchar](50) NOT NULL,
	[rubro] [varchar](50) NOT NULL,
	[fecha_baja] [datetime] NOT NULL,
	[cod_postal] [int] NOT NULL,
 CONSTRAINT [PK_T_Empresas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRAR_ENVIO]    Script Date: 03/04/2024 02:32:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTRAR_ENVIO] 
	@fecha_envio date,
	@direccion varchar(50),
	@dni_cliente varchar(50)
AS
BEGIN
	INSERT INTO T_Envio VALUES(@fecha_envio, @direccion, 'Para enviar', @dni_cliente)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRAR_CANCELACION_ENVIO]    Script Date: 03/04/2024 02:32:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTRAR_CANCELACION_ENVIO] 
	@codigo int
AS
BEGIN
	UPDATE T_Envio SET estado = 'Cancelado'
	WHERE estado not in ('Entregado')
	AND codigo = @codigo
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_ENVIOS]    Script Date: 03/04/2024 02:32:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_ENVIOS] 
	@dni_cliente int,
	@fecha_desde datetime,
	@fecha_hasta datetime
AS
BEGIN
	SELECT t.* 
	FROM T_Envio t
	WHERE ((@dni_cliente = '') OR (t.dni_cliente = @dni_cliente))
	 AND((@fecha_desde is null and @fecha_hasta is null) OR (fecha_envio between @fecha_desde and @fecha_hasta));
END
GO
