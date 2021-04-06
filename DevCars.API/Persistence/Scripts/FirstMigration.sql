IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cars] (
    [Id] int NOT NULL IDENTITY,
    [VinCode] nvarchar(max) NULL,
    [Brand] VARCHAR(100) NULL DEFAULT 'PADRÃO',
    [Model] nvarchar(max) NULL,
    [Year] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Color] nvarchar(max) NULL,
    [ProductionDate] datetime2 NOT NULL DEFAULT (getdate()),
    [Status] int NOT NULL,
    [RegisteredAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NULL,
    [Document] nvarchar(max) NULL,
    [BirthDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CarId] int NOT NULL,
    [CustomerId] int NOT NULL,
    [TotalCost] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [Cars] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ExtraOrderItems] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [OrderId] int NOT NULL,
    CONSTRAINT [PK_ExtraOrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExtraOrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ExtraOrderItems_OrderId] ON [ExtraOrderItems] ([OrderId]);
GO

CREATE UNIQUE INDEX [IX_Orders_CarId] ON [Orders] ([CarId]);
GO

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210406085017_InitialMigration', N'5.0.4');
GO

COMMIT;
GO

