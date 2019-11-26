CREATE TABLE [PMOS].[ProjectWorker]
(
	[ID] INT IDENTITY NOT NULL,
    [ID_Project] INT NOT NULL, 
    [ID_Worker] INT NOT NULL,
	CONSTRAINT [PK_ProjectWorker] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_ProjectWorker_ToProject] FOREIGN KEY ([ID_Project]) REFERENCES [PMOS].[Project]([ID]),
	CONSTRAINT [FK_ProjectWorker_ToWorker] FOREIGN KEY ([ID_Worker]) REFERENCES [PMOS].[Worker]([ID])
)
GO

CREATE INDEX [IX_ProjectWorker_IdProject_IdWorker] ON [PMOS].[ProjectWorker] ([ID_Project], [ID_Worker])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Идентификатор связи проекта и работника.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'ProjectWorker',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID проекта.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'ProjectWorker',
    @level2type = N'COLUMN',
    @level2name = N'ID_Project'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID пользователя.',
    @level0type = N'SCHEMA',
    @level0name = N'PMOS',
    @level1type = N'TABLE',
    @level1name = N'ProjectWorker',
    @level2type = N'COLUMN',
    @level2name = N'ID_Worker'