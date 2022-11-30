BEGIN TRANSACTION;
GO

CREATE TABLE [Peliculas] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(300) NOT NULL,
    [Resumen] int NOT NULL,
    [Trailer] int NOT NULL,
    [EnCines] bit NOT NULL,
    [FechaLanzamiento] datetime2 NOT NULL,
    [Poster] nvarchar(max) NULL,
    CONSTRAINT [PK_Peliculas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PeliculasActores] (
    [PeliculaId] int NOT NULL,
    [ActorId] int NOT NULL,
    CONSTRAINT [PK_PeliculasActores] PRIMARY KEY ([ActorId], [PeliculaId]),
    CONSTRAINT [FK_PeliculasActores_Actores_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actores] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PeliculasActores_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PeliculasCines] (
    [PeliculaId] int NOT NULL,
    [CineId] int NOT NULL,
    [Personaje] nvarchar(100) NULL,
    [Order] int NOT NULL,
    CONSTRAINT [PK_PeliculasCines] PRIMARY KEY ([PeliculaId], [CineId]),
    CONSTRAINT [FK_PeliculasCines_Cines_CineId] FOREIGN KEY ([CineId]) REFERENCES [Cines] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PeliculasCines_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PeliculasGeneros] (
    [PeliculaId] int NOT NULL,
    [GeneroId] int NOT NULL,
    CONSTRAINT [PK_PeliculasGeneros] PRIMARY KEY ([PeliculaId], [GeneroId]),
    CONSTRAINT [FK_PeliculasGeneros_Generos_GeneroId] FOREIGN KEY ([GeneroId]) REFERENCES [Generos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PeliculasGeneros_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_PeliculasActores_PeliculaId] ON [PeliculasActores] ([PeliculaId]);
GO

CREATE INDEX [IX_PeliculasCines_CineId] ON [PeliculasCines] ([CineId]);
GO

CREATE INDEX [IX_PeliculasGeneros_GeneroId] ON [PeliculasGeneros] ([GeneroId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221128010257_Peliculas', N'5.0.17');
GO

COMMIT;
GO

