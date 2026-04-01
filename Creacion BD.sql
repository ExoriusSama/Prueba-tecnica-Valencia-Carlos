CREATE DATABASE examen_tecnico_valencia;
GO

USE examen_tecnico_valencia;
GO

CREATE TABLE pelicula (
    id_pelicula INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(150) NOT NULL,
    duracion INT NOT NULL, -- duración en minutos
    estado BIT NOT NULL DEFAULT 1, -- eliminación lógica
);


CREATE TABLE sala_cine (
    id_sala INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    estado BIT NOT NULL DEFAULT 1, -- activa / inactiva
);


CREATE TABLE pelicula_sala_cine (
    id_pelicula_sala INT IDENTITY(1,1) PRIMARY KEY,
    id_pelicula INT NOT NULL,
    id_sala INT NOT NULL,
    fecha_publicacion DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_PeliculaSala_Pelicula
        FOREIGN KEY (id_pelicula) REFERENCES pelicula(id_pelicula),

    CONSTRAINT FK_PeliculaSala_Sala
        FOREIGN KEY (id_sala) REFERENCES sala_cine(id_sala)
);


INSERT INTO pelicula (nombre, duracion)
VALUES
('Avengers: Endgame', 181),
('Inception', 148),
('El Padrino', 175),
('Interstellar', 169),
('Jurassic Park', 127);


INSERT INTO sala_cine (nombre)
VALUES
('Sala 1 - IMAX'),
('Sala 2 - 3D'),
('Sala 3 - VIP'),
('Sala 4 - Estándar'),
('Sala 5 - Infantil');


INSERT INTO pelicula_sala_cine (id_pelicula, id_sala, fecha_publicacion, fecha_fin)
VALUES
(1, 1, '2026-04-01', '2026-04-15'),
(2, 2, '2026-04-03', '2026-04-18'),
(3, 3, '2026-04-05', '2026-04-20'),
(4, 4, '2026-04-02', '2026-04-12'),
(5, 5, '2026-04-04', '2026-04-14');


GO
CREATE PROCEDURE ObtenerPeliculasEnCartelera
AS
BEGIN
    SELECT 
        p.id_pelicula,
        p.nombre AS pelicula,
        p.duracion,
        s.id_sala,
        s.nombre AS sala,
        psc.fecha_publicacion,
        psc.fecha_fin
    FROM pelicula p
    INNER JOIN pelicula_sala_cine psc 
        ON p.id_pelicula = psc.id_pelicula
    INNER JOIN sala_cine s 
        ON s.id_sala = psc.id_sala
    WHERE 
        p.estado = 1
        AND s.estado = 1
        AND psc.estado = 1
        AND GETDATE() BETWEEN psc.fecha_publicacion AND psc.fecha_fin
END;
GO

EXEC ObtenerPeliculasEnCartelera;