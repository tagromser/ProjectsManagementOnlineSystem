CREATE TABLE [PMOS].[Role]
(
	[ID] INT IDENTITY NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL,
	[SystemName] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Role] PRIMARY KEY ([ID])
)
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор роли.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Role',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Имя роли.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Role',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Системное имя роли.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Role',
    @level2type = N'COLUMN',
    @level2name = N'SystemName'