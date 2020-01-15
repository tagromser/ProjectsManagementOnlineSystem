CREATE TABLE [PMOS].[ProjectTask]
(
	[ID] INT IDENTITY NOT NULL, 
    [ID_Project] INT NOT NULL, 
    [ID_Task] INT NOT NULL,
	CONSTRAINT [PK_ProjectTask] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_ProjectTask_ToProject] FOREIGN KEY ([ID_Project]) REFERENCES [PMOS].[Project]([ID]),
	CONSTRAINT [FK_ProjectTask_ToTask] FOREIGN KEY ([ID_Task]) REFERENCES [PMOS].[Task]([ID])
)
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор связи проекта и задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'ProjectTask',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID проекта.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'ProjectTask',
    @level2type = N'COLUMN',
    @level2name = N'ID_Project'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'ProjectTask',
    @level2type = N'COLUMN',
    @level2name = N'ID_Task'
GO
