CREATE TABLE [PMOS].[Task]
(
	[ID] INT IDENTITY NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [ID_Status] INT NOT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
    [Priority] INT NOT NULL,
	CONSTRAINT [PK_Task] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Task_ToStatus] FOREIGN KEY ([ID_Status]) REFERENCES [PMOS].[Status]([ID])
)
GO

CREATE INDEX [IX_Task_Name] ON [PMOS].[Task] ([Name])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Task',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Название задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Task',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID статуса задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Task',
    @level2type = N'COLUMN',
    @level2name = N'ID_Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Комментарий.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Task',
    @level2type = N'COLUMN',
    @level2name = N'Comment'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Приоритет задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'Task',
    @level2type = N'COLUMN',
    @level2name = N'Priority'