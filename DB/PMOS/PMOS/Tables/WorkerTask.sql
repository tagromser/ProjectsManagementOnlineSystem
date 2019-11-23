CREATE TABLE [PMOS].[WorkerTask]
(
	[ID] INT IDENTITY NOT NULL, 
    [ID_Worker] INT NOT NULL, 
    [ID_Task] INT NOT NULL, 
    [IsAuthor] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_WorkerTask] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_WorkerTask_ToWorker] FOREIGN KEY ([ID_Worker]) REFERENCES [PMOS].[Worker]([ID]),
	CONSTRAINT [FK_WorkerTask_ToTask] FOREIGN KEY ([ID_Task]) REFERENCES [PMOS].[Task]([ID])
)
GO

CREATE INDEX [IX_WorkerTask_IdWorker_IdTask] ON [PMOS].[WorkerTask] ([ID_Worker], [ID_Task])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор связи работника и задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'WorkerTask',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID работника.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'WorkerTask',
    @level2type = N'COLUMN',
    @level2name = N'ID_Worker'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID задачи.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'WorkerTask',
    @level2type = N'COLUMN',
    @level2name = N'ID_Task'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Является ли автором.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'WorkerTask',
    @level2type = N'COLUMN',
    @level2name = N'IsAuthor'