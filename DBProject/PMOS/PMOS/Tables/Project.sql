CREATE TABLE [PMOS].[Project]
(
	[ID] INT IDENTITY NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [CustomerCompanyName] NVARCHAR(256) NOT NULL, 
    [PerformerCompanyName] NVARCHAR(256) NOT NULL, 
    [StartDate] DATE NOT NULL DEFAULT GETDATE(), 
    [EndDate] DATE NOT NULL, 
    [Priority] INT NOT NULL,
	CONSTRAINT [PK_Project] PRIMARY KEY ([ID])
)
GO

CREATE INDEX [IX_Project_Name] ON [PMOS].[Project] ([Name])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор проекта.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Project',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Название проекта.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Project',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Название компании-заказчика.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Project',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCompanyName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Название компании-исполнителя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Project',
    @level2type = N'COLUMN',
    @level2name = N'PerformerCompanyName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Дата начала проекта.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Project',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Дата окончания проекта.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Project',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Приоритет проекта.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Project',
    @level2type = N'COLUMN',
    @level2name = N'Priority'