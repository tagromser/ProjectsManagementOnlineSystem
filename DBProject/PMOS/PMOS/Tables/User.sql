CREATE TABLE [PMOS].[User]
(
	[ID] INT IDENTITY NOT NULL, 
    [UserName] NVARCHAR(256) NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NULL, 
    [SecurityStamp] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([ID])
)
GO

CREATE INDEX [IX_User_UserName] ON [PMOS].[User] ([UserName])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор пользователя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Имя/ник пользователя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'UserName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Хэш пароля.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'PasswordHash'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'User',
    @level2type = N'COLUMN',
    @level2name = N'SecurityStamp'
GO