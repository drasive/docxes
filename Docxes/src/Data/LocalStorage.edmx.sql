
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/23/2014 23:27:42
-- Generated from EDMX file: C:\Users\dimit_000\SkyDrive\Programming\Windows Desktop\Docxes\Development\Docxes\src\Data\LocalStorage.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [model];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LehrerFach]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fächer] DROP CONSTRAINT [FK_LehrerFach];
GO
IF OBJECT_ID(N'[dbo].[FK_LehrerSchule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lehrer] DROP CONSTRAINT [FK_LehrerSchule];
GO
IF OBJECT_ID(N'[dbo].[FK_FachUnterlage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Unterlagen] DROP CONSTRAINT [FK_FachUnterlage];
GO
IF OBJECT_ID(N'[dbo].[FK_FachNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Noten] DROP CONSTRAINT [FK_FachNote];
GO
IF OBJECT_ID(N'[dbo].[FK_FachEreignis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ereignisse] DROP CONSTRAINT [FK_FachEreignis];
GO
IF OBJECT_ID(N'[dbo].[FK_EreignisNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Noten] DROP CONSTRAINT [FK_EreignisNote];
GO
IF OBJECT_ID(N'[dbo].[FK_EreignisUnterlage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Unterlagen] DROP CONSTRAINT [FK_EreignisUnterlage];
GO
IF OBJECT_ID(N'[dbo].[FK_EreignisNotiz]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notizen] DROP CONSTRAINT [FK_EreignisNotiz];
GO
IF OBJECT_ID(N'[dbo].[FK_FachNotiz]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notizen] DROP CONSTRAINT [FK_FachNotiz];
GO
IF OBJECT_ID(N'[dbo].[FK_AufgabeUnterlage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Unterlagen] DROP CONSTRAINT [FK_AufgabeUnterlage];
GO
IF OBJECT_ID(N'[dbo].[FK_EreignisAufgabe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Aufgaben] DROP CONSTRAINT [FK_EreignisAufgabe];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Fächer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fächer];
GO
IF OBJECT_ID(N'[dbo].[Noten]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Noten];
GO
IF OBJECT_ID(N'[dbo].[Unterlagen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Unterlagen];
GO
IF OBJECT_ID(N'[dbo].[Ereignisse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ereignisse];
GO
IF OBJECT_ID(N'[dbo].[Notizen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notizen];
GO
IF OBJECT_ID(N'[dbo].[Schulen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Schulen];
GO
IF OBJECT_ID(N'[dbo].[Lehrer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lehrer];
GO
IF OBJECT_ID(N'[dbo].[Aufgaben]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Aufgaben];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Fächer'
CREATE TABLE [dbo].[Fächer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Kommentar] nvarchar(max)  NULL,
    [LehrerId] int  NOT NULL
);
GO

-- Creating table 'Noten'
CREATE TABLE [dbo].[Noten] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Wert] int  NOT NULL,
    [Gewicht] int  NOT NULL,
    [Kommentar] nvarchar(max)  NULL,
    [FachId] int  NOT NULL,
    [EreignisId] int  NULL
);
GO

-- Creating table 'Unterlagen'
CREATE TABLE [dbo].[Unterlagen] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FachId] int  NOT NULL,
    [Inhalt] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Ereignisse'
CREATE TABLE [dbo].[Ereignisse] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FachId] int  NOT NULL,
    [Datum] datetime  NOT NULL,
    [Typ] int  NOT NULL
);
GO

-- Creating table 'Notizen'
CREATE TABLE [dbo].[Notizen] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Inhalt] nvarchar(max)  NOT NULL,
    [EreignisId] int  NULL,
    [FachId] int  NOT NULL
);
GO

-- Creating table 'Schulen'
CREATE TABLE [dbo].[Schulen] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Kommentar] nvarchar(max)  NULL
);
GO

-- Creating table 'Lehrer'
CREATE TABLE [dbo].[Lehrer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SchuleId] int  NOT NULL,
    [Vorname] nvarchar(max)  NULL,
    [Nachname] nvarchar(max)  NOT NULL,
    [Männlich] bit  NOT NULL
);
GO

-- Creating table 'Aufgaben'
CREATE TABLE [dbo].[Aufgaben] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Erledigt] datetime  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Beschreibung] nvarchar(max)  NULL,
    [EreignisId] int  NOT NULL
);
GO

-- Creating table 'EreignisUnterlage'
CREATE TABLE [dbo].[EreignisUnterlage] (
    [Ereignis_Id] int  NOT NULL,
    [Unterlage_Id] int  NOT NULL
);
GO

-- Creating table 'AufgabeUnterlage'
CREATE TABLE [dbo].[AufgabeUnterlage] (
    [Aufgabe_Id] int  NOT NULL,
    [Unterlage_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Fächer'
ALTER TABLE [dbo].[Fächer]
ADD CONSTRAINT [PK_Fächer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Noten'
ALTER TABLE [dbo].[Noten]
ADD CONSTRAINT [PK_Noten]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Unterlagen'
ALTER TABLE [dbo].[Unterlagen]
ADD CONSTRAINT [PK_Unterlagen]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ereignisse'
ALTER TABLE [dbo].[Ereignisse]
ADD CONSTRAINT [PK_Ereignisse]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notizen'
ALTER TABLE [dbo].[Notizen]
ADD CONSTRAINT [PK_Notizen]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Schulen'
ALTER TABLE [dbo].[Schulen]
ADD CONSTRAINT [PK_Schulen]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Lehrer'
ALTER TABLE [dbo].[Lehrer]
ADD CONSTRAINT [PK_Lehrer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Aufgaben'
ALTER TABLE [dbo].[Aufgaben]
ADD CONSTRAINT [PK_Aufgaben]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Ereignis_Id], [Unterlage_Id] in table 'EreignisUnterlage'
ALTER TABLE [dbo].[EreignisUnterlage]
ADD CONSTRAINT [PK_EreignisUnterlage]
    PRIMARY KEY NONCLUSTERED ([Ereignis_Id], [Unterlage_Id] ASC);
GO

-- Creating primary key on [Aufgabe_Id], [Unterlage_Id] in table 'AufgabeUnterlage'
ALTER TABLE [dbo].[AufgabeUnterlage]
ADD CONSTRAINT [PK_AufgabeUnterlage]
    PRIMARY KEY NONCLUSTERED ([Aufgabe_Id], [Unterlage_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LehrerId] in table 'Fächer'
ALTER TABLE [dbo].[Fächer]
ADD CONSTRAINT [FK_LehrerFach]
    FOREIGN KEY ([LehrerId])
    REFERENCES [dbo].[Lehrer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LehrerFach'
CREATE INDEX [IX_FK_LehrerFach]
ON [dbo].[Fächer]
    ([LehrerId]);
GO

-- Creating foreign key on [SchuleId] in table 'Lehrer'
ALTER TABLE [dbo].[Lehrer]
ADD CONSTRAINT [FK_LehrerSchule]
    FOREIGN KEY ([SchuleId])
    REFERENCES [dbo].[Schulen]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LehrerSchule'
CREATE INDEX [IX_FK_LehrerSchule]
ON [dbo].[Lehrer]
    ([SchuleId]);
GO

-- Creating foreign key on [FachId] in table 'Unterlagen'
ALTER TABLE [dbo].[Unterlagen]
ADD CONSTRAINT [FK_FachUnterlage]
    FOREIGN KEY ([FachId])
    REFERENCES [dbo].[Fächer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FachUnterlage'
CREATE INDEX [IX_FK_FachUnterlage]
ON [dbo].[Unterlagen]
    ([FachId]);
GO

-- Creating foreign key on [FachId] in table 'Noten'
ALTER TABLE [dbo].[Noten]
ADD CONSTRAINT [FK_FachNote]
    FOREIGN KEY ([FachId])
    REFERENCES [dbo].[Fächer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FachNote'
CREATE INDEX [IX_FK_FachNote]
ON [dbo].[Noten]
    ([FachId]);
GO

-- Creating foreign key on [FachId] in table 'Ereignisse'
ALTER TABLE [dbo].[Ereignisse]
ADD CONSTRAINT [FK_FachEreignis]
    FOREIGN KEY ([FachId])
    REFERENCES [dbo].[Fächer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FachEreignis'
CREATE INDEX [IX_FK_FachEreignis]
ON [dbo].[Ereignisse]
    ([FachId]);
GO

-- Creating foreign key on [EreignisId] in table 'Noten'
ALTER TABLE [dbo].[Noten]
ADD CONSTRAINT [FK_EreignisNote]
    FOREIGN KEY ([EreignisId])
    REFERENCES [dbo].[Ereignisse]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EreignisNote'
CREATE INDEX [IX_FK_EreignisNote]
ON [dbo].[Noten]
    ([EreignisId]);
GO

-- Creating foreign key on [EreignisId] in table 'Notizen'
ALTER TABLE [dbo].[Notizen]
ADD CONSTRAINT [FK_EreignisNotiz]
    FOREIGN KEY ([EreignisId])
    REFERENCES [dbo].[Ereignisse]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EreignisNotiz'
CREATE INDEX [IX_FK_EreignisNotiz]
ON [dbo].[Notizen]
    ([EreignisId]);
GO

-- Creating foreign key on [FachId] in table 'Notizen'
ALTER TABLE [dbo].[Notizen]
ADD CONSTRAINT [FK_FachNotiz]
    FOREIGN KEY ([FachId])
    REFERENCES [dbo].[Fächer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FachNotiz'
CREATE INDEX [IX_FK_FachNotiz]
ON [dbo].[Notizen]
    ([FachId]);
GO

-- Creating foreign key on [EreignisId] in table 'Aufgaben'
ALTER TABLE [dbo].[Aufgaben]
ADD CONSTRAINT [FK_EreignisAufgabe]
    FOREIGN KEY ([EreignisId])
    REFERENCES [dbo].[Ereignisse]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EreignisAufgabe'
CREATE INDEX [IX_FK_EreignisAufgabe]
ON [dbo].[Aufgaben]
    ([EreignisId]);
GO

-- Creating foreign key on [Ereignis_Id] in table 'EreignisUnterlage'
ALTER TABLE [dbo].[EreignisUnterlage]
ADD CONSTRAINT [FK_EreignisUnterlage_Ereignis]
    FOREIGN KEY ([Ereignis_Id])
    REFERENCES [dbo].[Ereignisse]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Unterlage_Id] in table 'EreignisUnterlage'
ALTER TABLE [dbo].[EreignisUnterlage]
ADD CONSTRAINT [FK_EreignisUnterlage_Unterlage]
    FOREIGN KEY ([Unterlage_Id])
    REFERENCES [dbo].[Unterlagen]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EreignisUnterlage_Unterlage'
CREATE INDEX [IX_FK_EreignisUnterlage_Unterlage]
ON [dbo].[EreignisUnterlage]
    ([Unterlage_Id]);
GO

-- Creating foreign key on [Aufgabe_Id] in table 'AufgabeUnterlage'
ALTER TABLE [dbo].[AufgabeUnterlage]
ADD CONSTRAINT [FK_AufgabeUnterlage_Aufgabe]
    FOREIGN KEY ([Aufgabe_Id])
    REFERENCES [dbo].[Aufgaben]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Unterlage_Id] in table 'AufgabeUnterlage'
ALTER TABLE [dbo].[AufgabeUnterlage]
ADD CONSTRAINT [FK_AufgabeUnterlage_Unterlage]
    FOREIGN KEY ([Unterlage_Id])
    REFERENCES [dbo].[Unterlagen]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AufgabeUnterlage_Unterlage'
CREATE INDEX [IX_FK_AufgabeUnterlage_Unterlage]
ON [dbo].[AufgabeUnterlage]
    ([Unterlage_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------