CREATE TABLE [PMOS].[UserRole]
(
	[ID] INT IDENTITY NOT NULL, 
    [ID_User] INT NOT NULL, 
    [ID_Role] INT NOT NULL, 
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([ID]), 
    CONSTRAINT [FK_UserRole_ToUser] FOREIGN KEY ([ID_User]) REFERENCES [PMOS].[User]([ID]), 
    CONSTRAINT [FK_UserRole_ToRole] FOREIGN KEY ([ID_Role]) REFERENCES [PMOS].[Role]([ID]) 
)
GO

CREATE INDEX [IX_UserRole_IdUser_IdRole] ON [PMOS].[UserRole] ([ID_User], [ID_Role])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор связи роли и пользователя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'UserRole',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID пользователя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'UserRole',
    @level2type = N'COLUMN',
    @level2name = N'ID_User'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID роли.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'UserRole',
    @level2type = N'COLUMN',
    @level2name = N'ID_Role'
GO