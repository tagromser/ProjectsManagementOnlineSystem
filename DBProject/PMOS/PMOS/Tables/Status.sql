CREATE TABLE [PMOS].[Status]
(
	[ID] INT IDENTITY NOT NULL, 
    [Name] NVARCHAR(12) NOT NULL,
	[SystemName] NVARCHAR(12) NOT NULL, 
    CONSTRAINT [PK_Status] PRIMARY KEY ([ID])
)
GO

CREATE INDEX [IX_Status_Name] ON [PMOS].[Status] ([Name])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор статуса.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Status',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Имя статуса.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Status',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Системное имя статуса.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Status',
    @level2type = N'COLUMN',
    @level2name = N'SystemName'