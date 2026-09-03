CREATE DATABASE escoladb;
GO

USE escoladb;
GO

CREATE TABLE curso (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(150) NOT NULL,
    CargaHoraria INT NOT NULL CHECK (CargaHoraria > 0),
    Valor DECIMAL(10,2) NOT NULL CHECK (Valor >= 0),
    DataInicio DATETIME NOT NULL,
    Online BIT NOT NULL DEFAULT 0,
    Ativo BIT NOT NULL DEFAULT 1
);
GO

INSERT INTO curso (Nome, CargaHoraria, Valor, DataInicio, Online, Ativo) VALUES
('C# Fundamentals', 40, 250.00, '2026-10-10', 1, 1),
('Angular Avancado', 60, 350.50, '2026-11-15', 1, 1),
('SQL Server Tuning', 20, 500.00, '2026-12-01', 0, 1),
('Introducao ao .NET', 30, 0.00, '2024-05-10', 1, 0),
('Design Patterns', 50, 400.00, '2027-01-20', 1, 1),
('Logica de Programacao', 40, 100.00, '2026-12-01', 0, 1),
('Arquitetura de Software', 80, 800.00, '2027-02-10', 1, 1),
('Scrum e Agil', 16, 150.00, '2024-01-15', 0, 0),
('Entity Framework Core', 40, 300.00, '2027-03-10', 1, 1),
('TypeScript Completo', 30, 200.00, '2026-11-20', 1, 1);
GO