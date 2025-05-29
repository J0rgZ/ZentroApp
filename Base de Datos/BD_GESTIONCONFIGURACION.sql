-- =============================================================================
-- SCRIPT DE CREACIÓN DE BASE DE DATOS - GESTIÓN DE PROYECTOS MEJORADO
-- Fecha: 28/05/2025
-- Descripción: Sistema de gestión de proyectos con metodologías de desarrollo
-- Versión: 2.0 - Mejorada con username, cronograma y GitHub
-- =============================================================================

-- Configuración inicial
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================================================
-- CREACIÓN DE TABLAS
-- =============================================================================

-- Tabla: Tipo_Usuario
CREATE TABLE [dbo].[Tipo_Usuario](
	[Id_tipousuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NULL,
 CONSTRAINT [PK_Tipo_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id_tipousuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Usuario (MEJORADA con username)
CREATE TABLE [dbo].[Usuario](
	[Id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL UNIQUE,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[Correo] [varchar](150) NOT NULL UNIQUE,
	[Password] [varchar](255) NOT NULL,
	[Estado] [char](1) NOT NULL DEFAULT 'A',
	[Fecha_creacion] [datetime] NOT NULL DEFAULT GETDATE(),
	[Id_tipousuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Metodologia
CREATE TABLE [dbo].[Metodologia](
	[Id_metodologia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[Estado] [char](1) NOT NULL DEFAULT 'A',
 CONSTRAINT [PK_Metodologia] PRIMARY KEY CLUSTERED 
(
	[Id_metodologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Fase
CREATE TABLE [dbo].[Fase](
	[Id_fase] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[Orden] [int] NOT NULL DEFAULT 1,
	[Id_metodologia] [int] NOT NULL,
 CONSTRAINT [PK_Fase] PRIMARY KEY CLUSTERED 
(
	[Id_fase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Solicitud
CREATE TABLE [dbo].[Solicitud](
	[Id_solicitud] [int] IDENTITY(1,1) NOT NULL,
	[Requerimiento] [varchar](255) NOT NULL,
	[Descripcion] [varchar](1000) NULL,
	[Estado] [varchar](20) NOT NULL DEFAULT 'ACTIVA',
	[Fecha_creacion] [datetime] NOT NULL DEFAULT GETDATE(),
	[Id_usuario_solicitante] [int] NOT NULL,
 CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED 
(
	[Id_solicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Proyecto (MEJORADA con GitHub URL)
CREATE TABLE [dbo].[Proyecto](
	[Id_proyecto] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NOT NULL UNIQUE,
	[Nombre] [varchar](200) NOT NULL,
	[Descripcion] [varchar](1000) NULL,
	[Github_url] [varchar](500) NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaTermino] [date] NULL,
	[Estado] [varchar](20) NOT NULL DEFAULT 'ACTIVO',
	[Progreso] [decimal](5,2) DEFAULT 0.00,
	[Id_metodologia] [int] NOT NULL,
	[Id_solicitud] [int] NOT NULL,
 CONSTRAINT [PK_Proyecto] PRIMARY KEY CLUSTERED 
(
	[Id_proyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Cronograma (NUEVA)
CREATE TABLE [dbo].[Cronograma](
	[Id_cronograma] [int] IDENTITY(1,1) NOT NULL,
	[Id_proyecto] [int] NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Descripcion] [varchar](1000) NULL,
	[FechaInicio] [datetime] NOT NULL,
	[Fechafin] [datetime] NULL,
	[Estado] [varchar](20) NOT NULL DEFAULT 'PLANIFICADO',
	[Prioridad] [varchar](10) NOT NULL DEFAULT 'MEDIA',
	[Progreso] [decimal](5,2) DEFAULT 0.00,
	[Fecha_creacion] [datetime] NOT NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_Cronograma] PRIMARY KEY CLUSTERED 
(
	[Id_cronograma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Solicitud_Cambios (MEJORADA)
CREATE TABLE [dbo].[Solicitud_Cambios](
	[Id_solicitud_cambios] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL DEFAULT GETDATE(),
	[Objetivo] [varchar](500) NOT NULL,
	[Descripcion] [varchar](1000) NULL,
	[Justificacion] [varchar](1000) NULL,
	[Impacto] [varchar](500) NULL,
	[Respuesta] [varchar](500) NULL,
	[Estado] [varchar](20) NOT NULL DEFAULT 'PENDIENTE',
	[Prioridad] [varchar](10) NOT NULL DEFAULT 'MEDIA',
	[Id_solicitud] [int] NOT NULL,
	[Id_proyecto] [int] NOT NULL,
	[Id_elemento_configuracion] [int] NULL,
	[Id_usuario_solicitante] [int] NOT NULL,
	[Id_usuario_aprobador] [int] NULL,
	[Fecha_aprobacion] [datetime] NULL,
 CONSTRAINT [PK_Solicitud_Cambios] PRIMARY KEY CLUSTERED 
(
	[Id_solicitud_cambios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Rol
CREATE TABLE [dbo].[Rol](
	[Id_rol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[Permisos] [varchar](1000) NULL,
	[Estado] [char](1) NOT NULL DEFAULT 'A',
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[Id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Miembro_Proyecto
CREATE TABLE [dbo].[Miembro_Proyecto](
	[Id_miembro] [int] IDENTITY(1,1) NOT NULL,
	[Id_usuario] [int] NOT NULL,
	[Id_rol] [int] NOT NULL,
	[Id_proyecto] [int] NOT NULL,
	[Fecha_asignacion] [datetime] NOT NULL DEFAULT GETDATE(),
	[Fecha_desasignacion] [datetime] NULL,
	[Estado] [char](1) NOT NULL DEFAULT 'A',
	[Responsabilidades] [varchar](1000) NULL,
 CONSTRAINT [PK_Miembro_Proyecto] PRIMARY KEY CLUSTERED 
(
	[Id_miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Elemento_Configuracion
CREATE TABLE [dbo].[Elemento_Configuracion](
	[Id_elementoconfiguracion] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Nomenclatura] [varchar](50) NULL,
	[Descripcion] [varchar](1000) NULL,
	[Tipo] [varchar](50) NULL,
	[Version] [varchar](20) DEFAULT '1.0',
	[Estado] [varchar](20) NOT NULL DEFAULT 'BORRADOR',
	[Id_fase] [int] NOT NULL,
	[Fecha_creacion] [datetime] NOT NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_Elemento_Configuracion] PRIMARY KEY CLUSTERED 
(
	[Id_elementoconfiguracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tabla: Miembro_Elemento
CREATE TABLE [dbo].[Miembro_Elemento](
	[Id_miembroelemento] [int] IDENTITY(1,1) NOT NULL,
	[Id_miembro] [int] NOT NULL,
	[Id_elementoconfiguracion] [int] NOT NULL,
	[Url] [varchar](500) NULL,
	[FechaInicio] [date] NOT NULL,
	[Fechafin] [date] NULL,
	[Estado] [varchar](20) NOT NULL DEFAULT 'EN_PROGRESO',
	[Progreso] [decimal](5,2) DEFAULT 0.00,
	[Observaciones] [varchar](1000) NULL,
	[Horas_estimadas] [decimal](6,2) NULL,
	[Horas_trabajadas] [decimal](6,2) DEFAULT 0.00,
 CONSTRAINT [PK_Miembro_Elemento] PRIMARY KEY CLUSTERED 
(
	[Id_miembroelemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- =============================================================================
-- CREACIÓN DE FOREIGN KEYS
-- =============================================================================

-- FK: Usuario -> Tipo_Usuario
ALTER TABLE [dbo].[Usuario] WITH CHECK ADD CONSTRAINT [FK_Usuario_Tipo_Usuario] 
FOREIGN KEY([Id_tipousuario]) REFERENCES [dbo].[Tipo_Usuario] ([Id_tipousuario])
GO

-- FK: Fase -> Metodologia
ALTER TABLE [dbo].[Fase] WITH CHECK ADD CONSTRAINT [FK_Fase_Metodologia] 
FOREIGN KEY([Id_metodologia]) REFERENCES [dbo].[Metodologia] ([Id_metodologia])
GO

-- FK: Solicitud -> Usuario (solicitante)
ALTER TABLE [dbo].[Solicitud] WITH CHECK ADD CONSTRAINT [FK_Solicitud_Usuario] 
FOREIGN KEY([Id_usuario_solicitante]) REFERENCES [dbo].[Usuario] ([Id_usuario])
GO

-- FK: Proyecto -> Metodologia
ALTER TABLE [dbo].[Proyecto] WITH CHECK ADD CONSTRAINT [FK_Proyecto_Metodologia] 
FOREIGN KEY([Id_metodologia]) REFERENCES [dbo].[Metodologia] ([Id_metodologia])
GO

-- FK: Proyecto -> Solicitud
ALTER TABLE [dbo].[Proyecto] WITH CHECK ADD CONSTRAINT [FK_Proyecto_Solicitud] 
FOREIGN KEY([Id_solicitud]) REFERENCES [dbo].[Solicitud] ([Id_solicitud])
GO

-- FK: Cronograma -> Proyecto
ALTER TABLE [dbo].[Cronograma] WITH CHECK ADD CONSTRAINT [FK_Cronograma_Proyecto] 
FOREIGN KEY([Id_proyecto]) REFERENCES [dbo].[Proyecto] ([Id_proyecto])
GO

-- FK: Solicitud_Cambios -> Solicitud
ALTER TABLE [dbo].[Solicitud_Cambios] WITH CHECK ADD CONSTRAINT [FK_Solicitud_Cambios_Solicitud] 
FOREIGN KEY([Id_solicitud]) REFERENCES [dbo].[Solicitud] ([Id_solicitud])
GO

-- FK: Solicitud_Cambios -> Proyecto
ALTER TABLE [dbo].[Solicitud_Cambios] WITH CHECK ADD CONSTRAINT [FK_Solicitud_Cambios_Proyecto] 
FOREIGN KEY([Id_proyecto]) REFERENCES [dbo].[Proyecto] ([Id_proyecto])
GO

-- FK: Solicitud_Cambios -> Elemento_Configuracion (opcional)
ALTER TABLE [dbo].[Solicitud_Cambios] WITH CHECK ADD CONSTRAINT [FK_Solicitud_Cambios_Elemento] 
FOREIGN KEY([Id_elemento_configuracion]) REFERENCES [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion])
GO

-- FK: Solicitud_Cambios -> Usuario (solicitante)
ALTER TABLE [dbo].[Solicitud_Cambios] WITH CHECK ADD CONSTRAINT [FK_Solicitud_Cambios_Usuario_Solicitante] 
FOREIGN KEY([Id_usuario_solicitante]) REFERENCES [dbo].[Usuario] ([Id_usuario])
GO

-- FK: Solicitud_Cambios -> Usuario (aprobador)
ALTER TABLE [dbo].[Solicitud_Cambios] WITH CHECK ADD CONSTRAINT [FK_Solicitud_Cambios_Usuario_Aprobador] 
FOREIGN KEY([Id_usuario_aprobador]) REFERENCES [dbo].[Usuario] ([Id_usuario])
GO

-- FK: Miembro_Proyecto -> Usuario
ALTER TABLE [dbo].[Miembro_Proyecto] WITH CHECK ADD CONSTRAINT [FK_Miembro_Proyecto_Usuario] 
FOREIGN KEY([Id_usuario]) REFERENCES [dbo].[Usuario] ([Id_usuario])
GO

-- FK: Miembro_Proyecto -> Rol
ALTER TABLE [dbo].[Miembro_Proyecto] WITH CHECK ADD CONSTRAINT [FK_Miembro_Proyecto_Rol] 
FOREIGN KEY([Id_rol]) REFERENCES [dbo].[Rol] ([Id_rol])
GO

-- FK: Miembro_Proyecto -> Proyecto
ALTER TABLE [dbo].[Miembro_Proyecto] WITH CHECK ADD CONSTRAINT [FK_Miembro_Proyecto_Proyecto] 
FOREIGN KEY([Id_proyecto]) REFERENCES [dbo].[Proyecto] ([Id_proyecto])
GO

-- FK: Elemento_Configuracion -> Fase
ALTER TABLE [dbo].[Elemento_Configuracion] WITH CHECK ADD CONSTRAINT [FK_Elemento_Configuracion_Fase] 
FOREIGN KEY([Id_fase]) REFERENCES [dbo].[Fase] ([Id_fase])
GO

-- FK: Miembro_Elemento -> Miembro_Proyecto
ALTER TABLE [dbo].[Miembro_Elemento] WITH CHECK ADD CONSTRAINT [FK_Miembro_Elemento_Miembro_Proyecto] 
FOREIGN KEY([Id_miembro]) REFERENCES [dbo].[Miembro_Proyecto] ([Id_miembro])
GO

-- FK: Miembro_Elemento -> Elemento_Configuracion
ALTER TABLE [dbo].[Miembro_Elemento] WITH CHECK ADD CONSTRAINT [FK_Miembro_Elemento_Elemento_Configuracion] 
FOREIGN KEY([Id_elementoconfiguracion]) REFERENCES [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion])
GO

-- =============================================================================
-- ÍNDICES PARA MEJORAR RENDIMIENTO
-- =============================================================================

CREATE INDEX [IX_Usuario_Username] ON [dbo].[Usuario] ([Username])
GO

CREATE INDEX [IX_Proyecto_Codigo] ON [dbo].[Proyecto] ([Codigo])
GO

CREATE INDEX [IX_Cronograma_Proyecto] ON [dbo].[Cronograma] ([Id_proyecto])
GO

CREATE INDEX [IX_Solicitud_Cambios_Proyecto] ON [dbo].[Solicitud_Cambios] ([Id_proyecto])
GO

-- =============================================================================
-- RESTRICCIONES Y VALIDACIONES
-- =============================================================================

-- Validación de estados para Usuario
ALTER TABLE [dbo].[Usuario] ADD CONSTRAINT [CK_Usuario_Estado] 
CHECK ([Estado] IN ('A', 'I'))
GO

-- Validación de estados para Metodologia
ALTER TABLE [dbo].[Metodologia] ADD CONSTRAINT [CK_Metodologia_Estado] 
CHECK ([Estado] IN ('A', 'I'))
GO

-- Validación de estados para Solicitud
ALTER TABLE [dbo].[Solicitud] ADD CONSTRAINT [CK_Solicitud_Estado] 
CHECK ([Estado] IN ('ACTIVA', 'COMPLETADA', 'CANCELADA'))
GO

-- Validación de estados para Proyecto
ALTER TABLE [dbo].[Proyecto] ADD CONSTRAINT [CK_Proyecto_Estado] 
CHECK ([Estado] IN ('ACTIVO', 'PAUSADO', 'COMPLETADO', 'CANCELADO'))
GO

-- Validación de progreso para Proyecto
ALTER TABLE [dbo].[Proyecto] ADD CONSTRAINT [CK_Proyecto_Progreso] 
CHECK ([Progreso] >= 0 AND [Progreso] <= 100)
GO

-- Validación de estados para Cronograma
ALTER TABLE [dbo].[Cronograma] ADD CONSTRAINT [CK_Cronograma_Estado] 
CHECK ([Estado] IN ('PLANIFICADO', 'EN_PROGRESO', 'COMPLETADO', 'CANCELADO', 'RETRASADO'))
GO

-- Validación de prioridad para Cronograma
ALTER TABLE [dbo].[Cronograma] ADD CONSTRAINT [CK_Cronograma_Prioridad] 
CHECK ([Prioridad] IN ('BAJA', 'MEDIA', 'ALTA', 'CRITICA'))
GO

-- Validación de progreso para Cronograma
ALTER TABLE [dbo].[Cronograma] ADD CONSTRAINT [CK_Cronograma_Progreso] 
CHECK ([Progreso] >= 0 AND [Progreso] <= 100)
GO

-- Validación de estados para Solicitud_Cambios
ALTER TABLE [dbo].[Solicitud_Cambios] ADD CONSTRAINT [CK_Solicitud_Cambios_Estado] 
CHECK ([Estado] IN ('PENDIENTE', 'EN_REVISION', 'APROBADA', 'RECHAZADA', 'IMPLEMENTADA'))
GO

-- Validación de prioridad para Solicitud_Cambios
ALTER TABLE [dbo].[Solicitud_Cambios] ADD CONSTRAINT [CK_Solicitud_Cambios_Prioridad] 
CHECK ([Prioridad] IN ('BAJA', 'MEDIA', 'ALTA', 'CRITICA'))
GO

-- Validación de estados para Rol
ALTER TABLE [dbo].[Rol] ADD CONSTRAINT [CK_Rol_Estado] 
CHECK ([Estado] IN ('A', 'I'))
GO

-- Validación de estados para Miembro_Proyecto
ALTER TABLE [dbo].[Miembro_Proyecto] ADD CONSTRAINT [CK_Miembro_Proyecto_Estado] 
CHECK ([Estado] IN ('A', 'I'))
GO

-- Validación de estados para Elemento_Configuracion
ALTER TABLE [dbo].[Elemento_Configuracion] ADD CONSTRAINT [CK_Elemento_Configuracion_Estado] 
CHECK ([Estado] IN ('BORRADOR', 'EN_REVISION', 'APROBADO', 'OBSOLETO'))
GO

-- Validación de estados para Miembro_Elemento
ALTER TABLE [dbo].[Miembro_Elemento] ADD CONSTRAINT [CK_Miembro_Elemento_Estado] 
CHECK ([Estado] IN ('PENDIENTE', 'EN_PROGRESO', 'COMPLETADO', 'CANCELADO', 'EN_REVISION'))
GO

-- Validación de progreso para Miembro_Elemento
ALTER TABLE [dbo].[Miembro_Elemento] ADD CONSTRAINT [CK_Miembro_Elemento_Progreso] 
CHECK ([Progreso] >= 0 AND [Progreso] <= 100)
GO

-- Validación de fechas para Proyecto
ALTER TABLE [dbo].[Proyecto] ADD CONSTRAINT [CK_Proyecto_Fechas] 
CHECK ([FechaTermino] IS NULL OR [FechaTermino] >= [FechaInicio])
GO

-- Validación de fechas para Cronograma
ALTER TABLE [dbo].[Cronograma] ADD CONSTRAINT [CK_Cronograma_Fechas] 
CHECK ([Fechafin] IS NULL OR [Fechafin] >= [FechaInicio])
GO

-- Validación de fechas para Miembro_Elemento
ALTER TABLE [dbo].[Miembro_Elemento] ADD CONSTRAINT [CK_Miembro_Elemento_Fechas] 
CHECK ([Fechafin] IS NULL OR [Fechafin] >= [FechaInicio])
GO

-- Validación de horas para Miembro_Elemento
ALTER TABLE [dbo].[Miembro_Elemento] ADD CONSTRAINT [CK_Miembro_Elemento_Horas] 
CHECK ([Horas_trabajadas] >= 0 AND ([Horas_estimadas] IS NULL OR [Horas_estimadas] >= 0))
GO

-- =============================================================================
-- INSERCIÓN DE DATOS CORREGIDA
-- =============================================================================

-- Insertar Tipos de Usuario
SET IDENTITY_INSERT [dbo].[Tipo_Usuario] ON 
INSERT [dbo].[Tipo_Usuario] ([Id_tipousuario], [Nombre], [Descripcion]) VALUES 
(1, N'Administrador', N'Administrador del sistema con todos los permisos'),
(2, N'Jefe de Proyecto', N'Responsable de la gestión de proyectos'),
(3, N'Desarrollador', N'Desarrollador de software'),
(4, N'Analista', N'Analista de sistemas y procesos'),
(5, N'Tester', N'Encargado de pruebas y calidad')
SET IDENTITY_INSERT [dbo].[Tipo_Usuario] OFF
GO

-- Insertar Usuarios (CON USERNAME)
SET IDENTITY_INSERT [dbo].[Usuario] ON 
INSERT [dbo].[Usuario] ([Id_usuario], [Username], [Nombre], [Apellido], [Correo], [Password], [Estado], [Id_tipousuario]) VALUES 
(1, N'jbriceno', N'Jorge', N'Briceño', N'jorge@gmail.com', N'123456', N'A', 1),
(2, N'mcuadros', N'Mirian', N'Cuadros', N'mirian@gmail.com', N'123456', N'A', 3),
(3, N'blopez', N'Brayar', N'Lopez', N'brayar@gmail.com', N'123456', N'A', 3),
(4, N'lhurtado', N'Leandro', N'Hurtado', N'leandro@gmail.com', N'123456', N'A', 4),
(5, N'mgarcia', N'María', N'García', N'maria@gmail.com', N'123456', N'A', 2),
(6, N'crodriguez', N'Carlos', N'Rodríguez', N'carlos@gmail.com', N'123456', N'A', 3),
(7, N'aperez', N'Ana', N'Pérez', N'ana@gmail.com', N'123456', N'A', 5)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO

-- Insertar Metodologías
SET IDENTITY_INSERT [dbo].[Metodologia] ON 
INSERT [dbo].[Metodologia] ([Id_metodologia], [Nombre], [Descripcion], [Estado]) VALUES 
(1, N'RUP', N'Rational Unified Process - Metodología iterativa con 4 fases principales', N'A'),
(2, N'AGIL', N'Metodología Ágil - Desarrollo iterativo e incremental con entregas continuas', N'A'),
(3, N'XP', N'Extreme Programming - Metodología ágil centrada en prácticas técnicas', N'A'),
(4, N'AUP', N'Agile Unified Process - Versión simplificada de RUP con enfoque ágil', N'A'),
(5, N'CASCADA', N'Modelo en Cascada - Metodología secuencial tradicional', N'A'),
(6, N'SCRUM', N'Framework ágil para gestión de proyectos con sprints', N'A'),
(7, N'SCRUM/AGIL', N'Combinación de Scrum con principios ágiles extendidos', N'A')
SET IDENTITY_INSERT [dbo].[Metodologia] OFF
GO

-- Insertar Fases
SET IDENTITY_INSERT [dbo].[Fase] ON 
INSERT [dbo].[Fase] ([Id_fase], [Nombre], [Descripcion], [Orden], [Id_metodologia]) VALUES 
-- Fases RUP
(1, N'INICIO', N'Fase inicial del proyecto RUP', 1, 1),
(2, N'ELABORACION', N'Fase de elaboración RUP', 2, 1),
(3, N'CONSTRUCCION', N'Fase de construcción RUP', 3, 1),
(4, N'TRANSICION', N'Fase de transición RUP', 4, 1),
-- Fases AGIL
(21, N'REVISION', N'Revisión en metodología ágil', 1, 2),
(22, N'DESPLEGAR', N'Despliegue en metodología ágil', 2, 2),
(23, N'TESTEO', N'Pruebas en metodología ágil', 3, 2),
(24, N'DESARROLLO', N'Desarrollo en metodología ágil', 4, 2),
(25, N'DISEÑO', N'Diseño en metodología ágil', 5, 2),
-- Fases XP
(31, N'PLANIFICACION', N'Planificación XP', 1, 3),
(32, N'DISEÑO', N'Diseño XP', 2, 3),
(33, N'CODIFICACION', N'Codificación XP', 3, 3),
(34, N'PRUEBAS', N'Pruebas XP', 4, 3),
-- Fases AUP
(5, N'INICIO', N'Fase inicial AUP', 1, 4),
(6, N'ELABORACION', N'Fase de elaboración AUP', 2, 4),
(7, N'CONSTRUCCION', N'Fase de construcción AUP', 3, 4),
(8, N'TRANSICION', N'Fase de transición AUP', 4, 4),
(9, N'ANALISIS', N'Fase de análisis AUP', 5, 4),
(10, N'DISEÑO', N'Fase de diseño AUP', 6, 4),
-- Fases CASCADA
(11, N'REQUISITOS', N'Análisis de requisitos', 1, 5),
(12, N'DISEÑO', N'Diseño del sistema', 2, 5),
(13, N'IMPLEMENTACION', N'Implementación del sistema', 3, 5),
(14, N'VERIFICACION', N'Verificación y pruebas', 4, 5),
(15, N'MANTENIMIENTO', N'Mantenimiento del sistema', 5, 5),
-- Fases SCRUM
(16, N'PLANIFICACION', N'Planificación del sprint', 1, 6),
(17, N'DESARROLLO', N'Desarrollo del sprint', 2, 6),
(18, N'REVISION', N'Revisión del sprint', 3, 6),
(19, N'RETROSPECTIVA', N'Retrospectiva del sprint', 4, 6),
(20, N'REFINAMIENTO', N'Refinamiento del backlog', 5, 6),
-- Fases SCRUM/AGIL
(26, N'PLANIFICACION', N'Planificación ágil', 1, 7),
(27, N'DESARROLLO', N'Desarrollo ágil', 2, 7),
(28, N'TESTEO', N'Pruebas ágiles', 3, 7),
(29, N'REVISION', N'Revisión ágil', 4, 7),
(30, N'ENTREGA', N'Entrega continua', 5, 7)
SET IDENTITY_INSERT [dbo].[Fase] OFF
GO

-- Insertar Roles
SET IDENTITY_INSERT [dbo].[Rol] ON 
INSERT [dbo].[Rol] ([Id_rol], [Nombre], [Descripcion], [Estado]) VALUES 
(1, N'Jefe de Proyecto', N'Responsable de la gestión general del proyecto', N'A'),
(2, N'Analista', N'Encargado del análisis y documentación', N'A'),
(3, N'Desarrollador', N'Responsable del desarrollo de software', N'A'),
(4, N'Tester', N'Encargado de pruebas y control de calidad', N'A'),
(5, N'Diseñador', N'Responsable del diseño UI/UX', N'A')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO

-- Insertar Solicitudes
SET IDENTITY_INSERT [dbo].[Solicitud] ON 
INSERT [dbo].[Solicitud] ([Id_solicitud], [Requerimiento], [Descripcion], [Estado], [Id_usuario_solicitante]) VALUES 
(1, N'Sistema Veterinaria', N'Requerimientos para sistema de gestión veterinaria', N'ACTIVA', 1),
(2, N'Sistema Farmacia', N'Requerimientos para sistema de gestión de farmacia', N'ACTIVA', 2),
(3, N'Sistema Agro', N'Requerimientos para sistema de gestión agrícola', N'ACTIVA', 3)
SET IDENTITY_INSERT [dbo].[Solicitud] OFF
GO

-- Insertar Proyectos (CORREGIDO: Eliminada referencia a Id_solicitud_cambios)
SET IDENTITY_INSERT [dbo].[Proyecto] ON 
INSERT [dbo].[Proyecto] ([Id_proyecto], [Codigo], [Nombre], [Descripcion], [Github_url], [FechaInicio], [FechaTermino], [Estado], [Progreso], [Id_metodologia], [Id_solicitud]) VALUES 
(1, N'PRY001', N'Sistema Veterinaria', N'Sistema integral de gestión veterinaria', N'https://github.com/empresa/sistema-veterinaria', '2021-12-02', '2022-02-02', N'COMPLETADO', 100.00, 1, 1),
(4, N'PRY002', N'Sistema Farmacia', N'Sistema de gestión para farmacias', N'https://github.com/empresa/sistema-farmacia', '2022-12-02', '2023-02-02', N'ACTIVO', 75.50, 2, 2),
(5, N'PRY005', N'Sistema Asistencia', N'Control de asistencia de personal', N'https://github.com/empresa/sistema-asistencia', '2021-12-03', '2022-01-03', N'COMPLETADO', 100.00, 1, 1),
(6, N'PRY055', N'Sistema Reservas Medicas', N'Gestión de citas médicas', N'https://github.com/empresa/reservas-medicas', '2021-03-10', '2021-12-10', N'COMPLETADO', 100.00, 7, 1),
(7, N'PRY007', N'Sistema Inventario', N'Control de inventarios', N'https://github.com/empresa/inventario', '2023-01-15', '2023-06-15', N'ACTIVO', 60.25, 6, 3),
(8, N'PRY008', N'Portal Web Corporativo', N'Sitio web institucional', N'https://github.com/empresa/portal-web', '2023-02-01', '2023-05-01', N'ACTIVO', 40.75, 2, 2)
SET IDENTITY_INSERT [dbo].[Proyecto] OFF
GO

-- Insertar Cronogramas (NUEVA TABLA)
SET IDENTITY_INSERT [dbo].[Cronograma] ON 
INSERT [dbo].[Cronograma] ([Id_cronograma], [Id_proyecto], [Nombre], [Descripcion], [FechaInicio], [Fechafin], [Estado], [Prioridad], [Progreso]) VALUES 
(1, 1, N'Fase Inicial Veterinaria', N'Cronograma para fase inicial del sistema veterinaria', '2021-12-02 08:00:00', '2021-12-15 18:00:00', N'COMPLETADO', N'ALTA', 100.00),
(2, 4, N'Sprint 1 - Farmacia', N'Primer sprint del sistema de farmacia', '2022-12-02 09:00:00', '2022-12-16 17:00:00', N'COMPLETADO', N'MEDIA', 100.00),
(3, 4, N'Sprint 2 - Farmacia', N'Segundo sprint del sistema de farmacia', '2022-12-17 09:00:00', '2023-01-02 17:00:00', N'EN_PROGRESO', N'ALTA', 65.50),
(4, 7, N'Setup Inicial Inventario', N'Configuración inicial del proyecto inventario', '2023-01-15 08:30:00', '2023-01-22 17:30:00', N'COMPLETADO', N'CRITICA', 100.00),
(5, 8, N'Diseño Portal Web', N'Fase de diseño del portal corporativo', '2023-02-01 09:00:00', '2023-02-14 18:00:00', N'EN_PROGRESO', N'MEDIA', 75.25)
SET IDENTITY_INSERT [dbo].[Cronograma] OFF
GO

-- Insertar Solicitudes de Cambios (CORREGIDAS)
SET IDENTITY_INSERT [dbo].[Solicitud_Cambios] ON
INSERT INTO [dbo].[Solicitud_Cambios] ([Id_solicitud_cambios], [Fecha], [Objetivo], [Descripcion], [Justificacion], [Impacto], [Respuesta], [Estado], [Prioridad], [Id_solicitud], [Id_proyecto], [Id_usuario_solicitante], [Id_usuario_aprobador], [Fecha_aprobacion]) VALUES
(1, '2021-12-02', N'Cambiar requerimientos del sistema veterinaria', N'Modificación de funcionalidades veterinaria', N'Solicitud del cliente para mejorar usabilidad', N'Medio - Requiere 2 semanas adicionales', N'Aprobado por el cliente', N'APROBADA', N'ALTA', 1, 1, 1, 5, '2021-12-03'),
(6, '2021-12-02', N'Cambiar requerimientos del sistema Farmacia', N'Modificación de funcionalidades farmacia', N'Nuevos requisitos normativos', N'Alto - Impacta cronograma general', N'Aprobado por el cliente', N'APROBADA', N'CRITICA', 2, 4, 2, 5, '2021-12-04'),
(8, '2022-03-10', N'Actualizar requisitos veterinaria', N'Segunda iteración de cambios', N'Feedback de usuarios beta', N'Bajo - Cambios menores', N'Aprobado', N'APROBADA', N'MEDIA', 1, 1, 1, 5, '2022-03-11'),
(9, '2022-03-10', N'Mejoras adicionales veterinaria', N'Tercera iteración de cambios', N'Optimización de rendimiento', N'Medio - Mejoras técnicas', N'Aprobado', N'APROBADA', N'MEDIA', 1, 1, 4, 5, '2022-03-12'),
(10, '2022-03-10', N'Optimización sistema farmacia', N'Mejoras de rendimiento', N'Problemas de velocidad reportados', N'Alto - Afecta experiencia usuario', N'Aprobado', N'APROBADA', N'ALTA', 2, 4, 2, 5, '2022-03-12'),
(13, '2021-03-10', N'Implementar sistema reservas', N'Nueva funcionalidad de reservas médicas', N'Expansión del alcance del proyecto', N'Alto - Nuevo módulo completo', N'Aprobado', N'APROBADA', N'ALTA', 1, 6, 1, 5, '2021-03-11')
SET IDENTITY_INSERT [dbo].[Solicitud_Cambios] OFF
GO

-- Insertar Miembros de Proyecto
SET IDENTITY_INSERT [dbo].[Miembro_Proyecto] ON 
INSERT [dbo].[Miembro_Proyecto] ([Id_miembro], [Id_usuario], [Id_rol], [Id_proyecto], [Estado], [Responsabilidades]) VALUES 
(1, 4, 3, 1, 'A', N'Desarrollo de módulos principales del sistema veterinaria'),
(2, 2, 2, 1, 'A', N'Análisis de requisitos y documentación técnica'),
(3, 3, 2, 1, 'A', N'Análisis de procesos y modelado de datos'),
(4, 6, 1, 6, 'A', N'Gestión y coordinación del proyecto de reservas médicas'),
(5, 5, 2, 6, 'A', N'Análisis de requerimientos para sistema de reservas'),
(6, 7, 3, 6, 'A', N'Desarrollo y testing del sistema de reservas'),
(7, 1, 1, 7, 'A', N'Dirección y supervisión del proyecto inventario'),
(14, 1, 1, 4, 'A', N'Gestión general del proyecto farmacia'),
(16, 6, 1, 1, 'A', N'Co-gestión del proyecto veterinaria'),
(17, 7, 3, 1, 'A', N'Testing y control de calidad'),
(18, 1, 1, 5, 'A', N'Dirección del proyecto de asistencia'),
(19, 4, 2, 5, 'A', N'Análisis de procesos de asistencia'),
(20, 2, 3, 5, 'A', N'Desarrollo del sistema de asistencia'),
(21, 1, 1, 8, 'A', N'Gestión del proyecto portal web'),
(22, 2, 2, 8, 'A', N'Análisis de contenidos y estructura'),
(23, 3, 3, 8, 'A', N'Desarrollo frontend y backend del portal')
SET IDENTITY_INSERT [dbo].[Miembro_Proyecto] OFF
GO

-- Insertar Elementos de Configuración
SET IDENTITY_INSERT [dbo].[Elemento_Configuracion] ON 
INSERT [dbo].[Elemento_Configuracion] ([Id_elementoconfiguracion], [Codigo], [Nombre], [Nomenclatura], [Descripcion], [Tipo], [Version], [Estado], [Id_fase]) VALUES 
(1, N'SAD001', N'Sistema de Análisis y Diseño', N'SAD', N'Documento de análisis y diseño del sistema', N'Documento', N'1.0', N'APROBADO', 1),
(4, N'SRSS001', N'Especificación de Requisitos', N'SRSS', N'Documento de especificación de requisitos de software', N'Documento', N'1.2', N'APROBADO', 2),
(5, N'EDT001', N'Estructura de Desglose del Trabajo', N'EDT', N'Documento EDT del proyecto', N'Documento', N'1.0', N'APROBADO', 3),
(9, N'SAD002', N'Análisis Detallado', N'SAD', N'Análisis detallado de componentes', N'Documento', N'2.0', N'EN_REVISION', 26),
(13, N'SAD003', N'Diseño de Arquitectura', N'SAD', N'Documento de arquitectura del sistema', N'Documento', N'1.5', N'APROBADO', 21),
(14, N'CASCADE001', N'Documento Cascada', N'CASCADA', N'Documentación metodología cascada', N'Documento', N'1.0', N'BORRADOR', 11),
(15, N'EDT002', N'EDT Detallado', N'EDT', N'Estructura detallada de trabajo', N'Documento', N'1.1', N'APROBADO', 11)
SET IDENTITY_INSERT [dbo].[Elemento_Configuracion] OFF
GO

-- Insertar Asignaciones Miembro-Elemento
SET IDENTITY_INSERT [dbo].[Miembro_Elemento] ON 
INSERT [dbo].[Miembro_Elemento] ([Id_miembroelemento], [Id_miembro], [Id_elementoconfiguracion], [Url], [FechaInicio], [Fechafin], [Estado], [Progreso], [Horas_estimadas], [Horas_trabajadas], [Observaciones]) VALUES 
(1, 1, 1, N'https://acortar.link/lMmXjU', '2023-05-15', '2023-08-10', N'COMPLETADO', 100.00, 120.00, 115.50, N'Trabajo completado satisfactoriamente con ligera optimización de tiempo'),
(4, 2, 4, N'https://acortar.link/lMmXjU', '2023-05-15', '2023-08-10', N'COMPLETADO', 100.00, 80.00, 85.75, N'Documentación completada con revisiones adicionales solicitadas'),
(5, 3, 5, N'https://docs.google.com/document/d/ejemplo', '2023-06-01', '2023-07-15', N'COMPLETADO', 100.00, 60.00, 58.25, N'EDT desarrollado eficientemente'),
(6, 14, 9, N'https://confluence.empresa.com/sad002', '2023-03-01', NULL, N'EN_PROGRESO', 75.50, 100.00, 72.00, N'Análisis avanzando según cronograma'),
(7, 16, 13, N'https://github.com/empresa/arquitectura-docs', '2023-02-15', '2023-04-30', N'COMPLETADO', 100.00, 90.00, 88.50, N'Arquitectura documentada y aprobada')
SET IDENTITY_INSERT [dbo].[Miembro_Elemento] OFF
GO

