CREATE TABLE [PMOS].[Worker]
(
	[ID] INT IDENTITY NOT NULL, 
    [ID_User] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(50) NOT NULL, 
    [Patronymic] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(256) NOT NULL,
	CONSTRAINT [PK_Worker] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Worker_ToUser] FOREIGN KEY ([ID_User]) REFERENCES [PMOS].[User]([ID]), 
)
GO

CREATE INDEX [IX_Worker_IdUser] ON [PMOS].[Worker] ([ID_User])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор работника.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Worker',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID пользователя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Worker',
    @level2type = N'COLUMN',
    @level2name = N'ID_User'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Имя работника.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Worker',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Фамилия работника.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Worker',
    @level2type = N'COLUMN',
    @level2name = N'Surname'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Отчество работника.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Worker',
    @level2type = N'COLUMN',
    @level2name = N'Patronymic'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Email работника.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Worker',
    @level2type = N'COLUMN',
    @level2name = N'Email'