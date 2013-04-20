
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/20/2013 12:36:39
-- Generated from EDMX file: C:\Users\Dan\Documents\Visual Studio 2010\Projects\OnStage\OnStage.Entities\Entities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OnStageDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ShowMasterCueBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StageManagerCueBooks] DROP CONSTRAINT [FK_ShowMasterCueBook];
GO
IF OBJECT_ID(N'[dbo].[FK_ShowCueBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CueBooks] DROP CONSTRAINT [FK_ShowCueBook];
GO
IF OBJECT_ID(N'[dbo].[FK_CueBookCue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cues] DROP CONSTRAINT [FK_CueBookCue];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCueBookMasterCue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CueGroups] DROP CONSTRAINT [FK_MasterCueBookMasterCue];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCueCue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cues] DROP CONSTRAINT [FK_MasterCueCue];
GO
IF OBJECT_ID(N'[dbo].[FK_ShowScript]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scripts] DROP CONSTRAINT [FK_ShowScript];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Shows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Shows];
GO
IF OBJECT_ID(N'[dbo].[StageManagerCueBooks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StageManagerCueBooks];
GO
IF OBJECT_ID(N'[dbo].[CueBooks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CueBooks];
GO
IF OBJECT_ID(N'[dbo].[CueGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CueGroups];
GO
IF OBJECT_ID(N'[dbo].[Cues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cues];
GO
IF OBJECT_ID(N'[dbo].[Scripts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scripts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Shows'
CREATE TABLE [dbo].[Shows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StageManagerCueBooks'
CREATE TABLE [dbo].[StageManagerCueBooks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Show_Id] int  NOT NULL
);
GO

-- Creating table 'CueBooks'
CREATE TABLE [dbo].[CueBooks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL,
    [Show_Id] int  NOT NULL
);
GO

-- Creating table 'CueGroups'
CREATE TABLE [dbo].[CueGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StageManagerCueBook_Id] int  NOT NULL
);
GO

-- Creating table 'Cues'
CREATE TABLE [dbo].[Cues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ScriptPosition] bigint  NOT NULL,
    [CueBook_Id] int  NOT NULL,
    [MasterCueCue_Cue_Id] int  NOT NULL
);
GO

-- Creating table 'Scripts'
CREATE TABLE [dbo].[Scripts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Data] varbinary(max)  NOT NULL,
    [MimeType] nvarchar(max)  NOT NULL,
    [ShowScript_Script_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Shows'
ALTER TABLE [dbo].[Shows]
ADD CONSTRAINT [PK_Shows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StageManagerCueBooks'
ALTER TABLE [dbo].[StageManagerCueBooks]
ADD CONSTRAINT [PK_StageManagerCueBooks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CueBooks'
ALTER TABLE [dbo].[CueBooks]
ADD CONSTRAINT [PK_CueBooks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CueGroups'
ALTER TABLE [dbo].[CueGroups]
ADD CONSTRAINT [PK_CueGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cues'
ALTER TABLE [dbo].[Cues]
ADD CONSTRAINT [PK_Cues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scripts'
ALTER TABLE [dbo].[Scripts]
ADD CONSTRAINT [PK_Scripts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Show_Id] in table 'StageManagerCueBooks'
ALTER TABLE [dbo].[StageManagerCueBooks]
ADD CONSTRAINT [FK_ShowMasterCueBook]
    FOREIGN KEY ([Show_Id])
    REFERENCES [dbo].[Shows]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShowMasterCueBook'
CREATE INDEX [IX_FK_ShowMasterCueBook]
ON [dbo].[StageManagerCueBooks]
    ([Show_Id]);
GO

-- Creating foreign key on [Show_Id] in table 'CueBooks'
ALTER TABLE [dbo].[CueBooks]
ADD CONSTRAINT [FK_ShowCueBook]
    FOREIGN KEY ([Show_Id])
    REFERENCES [dbo].[Shows]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShowCueBook'
CREATE INDEX [IX_FK_ShowCueBook]
ON [dbo].[CueBooks]
    ([Show_Id]);
GO

-- Creating foreign key on [CueBook_Id] in table 'Cues'
ALTER TABLE [dbo].[Cues]
ADD CONSTRAINT [FK_CueBookCue]
    FOREIGN KEY ([CueBook_Id])
    REFERENCES [dbo].[CueBooks]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CueBookCue'
CREATE INDEX [IX_FK_CueBookCue]
ON [dbo].[Cues]
    ([CueBook_Id]);
GO

-- Creating foreign key on [StageManagerCueBook_Id] in table 'CueGroups'
ALTER TABLE [dbo].[CueGroups]
ADD CONSTRAINT [FK_MasterCueBookMasterCue]
    FOREIGN KEY ([StageManagerCueBook_Id])
    REFERENCES [dbo].[StageManagerCueBooks]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCueBookMasterCue'
CREATE INDEX [IX_FK_MasterCueBookMasterCue]
ON [dbo].[CueGroups]
    ([StageManagerCueBook_Id]);
GO

-- Creating foreign key on [MasterCueCue_Cue_Id] in table 'Cues'
ALTER TABLE [dbo].[Cues]
ADD CONSTRAINT [FK_MasterCueCue]
    FOREIGN KEY ([MasterCueCue_Cue_Id])
    REFERENCES [dbo].[CueGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCueCue'
CREATE INDEX [IX_FK_MasterCueCue]
ON [dbo].[Cues]
    ([MasterCueCue_Cue_Id]);
GO

-- Creating foreign key on [ShowScript_Script_Id] in table 'Scripts'
ALTER TABLE [dbo].[Scripts]
ADD CONSTRAINT [FK_ShowScript]
    FOREIGN KEY ([ShowScript_Script_Id])
    REFERENCES [dbo].[Shows]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShowScript'
CREATE INDEX [IX_FK_ShowScript]
ON [dbo].[Scripts]
    ([ShowScript_Script_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------