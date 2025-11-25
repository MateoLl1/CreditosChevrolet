CREATE DATABASE CreditosChevroletDb;
GO

USE CreditosChevroletDb;
GO

CREATE TABLE SolicitudesCredito (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumeroSolicitud NVARCHAR(50) NOT NULL,
    AsesorId INT NOT NULL,
    FechaSolicitud DATETIME2 NOT NULL,
    FinancieraId INT NULL,
    MinutosReintento INT NULL
);

CREATE UNIQUE INDEX IX_SolicitudesCredito_NumeroSolicitud
ON SolicitudesCredito(NumeroSolicitud);

CREATE TABLE RespuestasCredito (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SolicitudCreditoId INT NOT NULL,
    Estado NVARCHAR(50) NOT NULL,
    MontoAprobado DECIMAL(18,2) NULL,
    PlazoMeses INT NULL,
    Tasa DECIMAL(5,2) NULL,
    FechaRespuesta DATETIME2 NOT NULL,
    Observaciones NVARCHAR(500) NULL,
    JsonCompleto NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_RespuestasCredito_SolicitudesCredito
        FOREIGN KEY (SolicitudCreditoId) REFERENCES SolicitudesCredito(Id)
);

CREATE TABLE NotificacionesAsesor (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AsesorId INT NOT NULL,
    SolicitudCreditoId INT NOT NULL,
    Mensaje NVARCHAR(500) NOT NULL,
    Fecha DATETIME2 NOT NULL,
    Leido BIT NOT NULL,
    CONSTRAINT FK_NotificacionesAsesor_SolicitudesCredito
        FOREIGN KEY (SolicitudCreditoId) REFERENCES SolicitudesCredito(Id)
);

INSERT INTO SolicitudesCredito (NumeroSolicitud, AsesorId, FechaSolicitud, FinancieraId, MinutosReintento)
VALUES
('SOL-APROBADO-001', 1001, GETDATE(), NULL, 30),
('SOL-NEGADO-001', 1002, GETDATE(), NULL, 30),
('SOL-CONDICIONADO-001', 1003, GETDATE(), NULL, 30),
('SOL-REQDOC-001', 1004, GETDATE(), NULL, 30),
('SOL-PROCESO-001', 1005, GETDATE(), NULL, 30);

SELECT s.NumeroSolicitud,
       s.AsesorId,
       r.Estado,
       r.MontoAprobado,
       r.Tasa,
       r.FechaRespuesta,
       r.Observaciones
FROM SolicitudesCredito s
LEFT JOIN RespuestasCredito r ON r.SolicitudCreditoId = s.Id
WHERE s.NumeroSolicitud = 'SOL-APROBADO-001';
