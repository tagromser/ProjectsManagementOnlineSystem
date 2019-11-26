/*
Скрипт развертывания для PMOS

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "PMOS"
:setvar DefaultFilePrefix "PMOS"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Выполняется создание $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Выполняется создание [PMOS]...';


GO
CREATE SCHEMA [PMOS]
    AUTHORIZATION [dbo];


GO
PRINT N'Выполняется создание [PMOS].[Role]...';


GO
CREATE TABLE [PMOS].[Role] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[Role].[IX_Role_Name]...';


GO
CREATE NONCLUSTERED INDEX [IX_Role_Name]
    ON [PMOS].[Role]([Name] ASC);


GO
PRINT N'Выполняется создание [PMOS].[UserRole]...';


GO
CREATE TABLE [PMOS].[UserRole] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [ID_User] INT NOT NULL,
    [ID_Role] INT NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[UserRole].[IX_UserRole_IdUser_IdRole]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserRole_IdUser_IdRole]
    ON [PMOS].[UserRole]([ID_User] ASC, [ID_Role] ASC);


GO
PRINT N'Выполняется создание [PMOS].[User]...';


GO
CREATE TABLE [PMOS].[User] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [UserName]      NVARCHAR (256) NOT NULL,
    [PasswordHash]  NVARCHAR (MAX) NULL,
    [SecurityStamp] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[User].[IX_User_UserName]...';


GO
CREATE NONCLUSTERED INDEX [IX_User_UserName]
    ON [PMOS].[User]([UserName] ASC);


GO
PRINT N'Выполняется создание [PMOS].[Status]...';


GO
CREATE TABLE [PMOS].[Status] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (12) NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[Status].[IX_Status_Name]...';


GO
CREATE NONCLUSTERED INDEX [IX_Status_Name]
    ON [PMOS].[Status]([Name] ASC);


GO
PRINT N'Выполняется создание [PMOS].[Task]...';


GO
CREATE TABLE [PMOS].[Task] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (256) NOT NULL,
    [ID_Status] INT            NOT NULL,
    [Comment]   NVARCHAR (MAX) NULL,
    [Priority]  INT            NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[Task].[IX_Task_Name]...';


GO
CREATE NONCLUSTERED INDEX [IX_Task_Name]
    ON [PMOS].[Task]([Name] ASC);


GO
PRINT N'Выполняется создание [PMOS].[Project]...';


GO
CREATE TABLE [PMOS].[Project] (
    [ID]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (256) NOT NULL,
    [CustomerCompanyName]  NVARCHAR (256) NOT NULL,
    [PerformerCompanyName] NVARCHAR (256) NOT NULL,
    [StartDate]            DATE           NOT NULL,
    [EndDate]              DATE           NOT NULL,
    [Priority]             INT            NOT NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[Project].[IX_Project_Name]...';


GO
CREATE NONCLUSTERED INDEX [IX_Project_Name]
    ON [PMOS].[Project]([Name] ASC);


GO
PRINT N'Выполняется создание [PMOS].[ProjectTask]...';


GO
CREATE TABLE [PMOS].[ProjectTask] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [ID_Project] INT NOT NULL,
    [ID_Task]    INT NOT NULL,
    CONSTRAINT [PK_ProjectTask] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[ProjectTask].[IX_ProjectTask_IdProject_IdTask]...';


GO
CREATE NONCLUSTERED INDEX [IX_ProjectTask_IdProject_IdTask]
    ON [PMOS].[ProjectTask]([ID_Project] ASC, [ID_Task] ASC);


GO
PRINT N'Выполняется создание [PMOS].[ProjectWorker]...';


GO
CREATE TABLE [PMOS].[ProjectWorker] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [ID_Project] INT NOT NULL,
    [ID_Worker]  INT NOT NULL,
    CONSTRAINT [PK_ProjectWorker] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[ProjectWorker].[IX_ProjectWorker_IdProject_IdWorker]...';


GO
CREATE NONCLUSTERED INDEX [IX_ProjectWorker_IdProject_IdWorker]
    ON [PMOS].[ProjectWorker]([ID_Project] ASC, [ID_Worker] ASC);


GO
PRINT N'Выполняется создание [PMOS].[Worker]...';


GO
CREATE TABLE [PMOS].[Worker] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [ID_User]    INT            NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [Surname]    NVARCHAR (50)  NOT NULL,
    [Patronymic] NVARCHAR (50)  NOT NULL,
    [Email]      NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_Worker] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[Worker].[IX_Worker_IdUser]...';


GO
CREATE NONCLUSTERED INDEX [IX_Worker_IdUser]
    ON [PMOS].[Worker]([ID_User] ASC);


GO
PRINT N'Выполняется создание [PMOS].[WorkerTask]...';


GO
CREATE TABLE [PMOS].[WorkerTask] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [ID_Worker] INT NOT NULL,
    [ID_Task]   INT NOT NULL,
    [IsAuthor]  BIT NOT NULL,
    CONSTRAINT [PK_WorkerTask] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Выполняется создание [PMOS].[WorkerTask].[IX_WorkerTask_IdWorker_IdTask]...';


GO
CREATE NONCLUSTERED INDEX [IX_WorkerTask_IdWorker_IdTask]
    ON [PMOS].[WorkerTask]([ID_Worker] ASC, [ID_Task] ASC);


GO
PRINT N'Выполняется создание ограничение без названия для [PMOS].[Project]...';


GO
ALTER TABLE [PMOS].[Project]
    ADD DEFAULT GETDATE() FOR [StartDate];


GO
PRINT N'Выполняется создание ограничение без названия для [PMOS].[WorkerTask]...';


GO
ALTER TABLE [PMOS].[WorkerTask]
    ADD DEFAULT 0 FOR [IsAuthor];


GO
PRINT N'Выполняется создание [PMOS].[FK_UserRole_ToUser]...';


GO
ALTER TABLE [PMOS].[UserRole]
    ADD CONSTRAINT [FK_UserRole_ToUser] FOREIGN KEY ([ID_User]) REFERENCES [PMOS].[User] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_UserRole_ToRole]...';


GO
ALTER TABLE [PMOS].[UserRole]
    ADD CONSTRAINT [FK_UserRole_ToRole] FOREIGN KEY ([ID_Role]) REFERENCES [PMOS].[Role] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_Task_ToStatus]...';


GO
ALTER TABLE [PMOS].[Task]
    ADD CONSTRAINT [FK_Task_ToStatus] FOREIGN KEY ([ID_Status]) REFERENCES [PMOS].[Status] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_ProjectTask_ToProject]...';


GO
ALTER TABLE [PMOS].[ProjectTask]
    ADD CONSTRAINT [FK_ProjectTask_ToProject] FOREIGN KEY ([ID_Project]) REFERENCES [PMOS].[Project] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_ProjectTask_ToTask]...';


GO
ALTER TABLE [PMOS].[ProjectTask]
    ADD CONSTRAINT [FK_ProjectTask_ToTask] FOREIGN KEY ([ID_Task]) REFERENCES [PMOS].[Task] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_ProjectWorker_ToProject]...';


GO
ALTER TABLE [PMOS].[ProjectWorker]
    ADD CONSTRAINT [FK_ProjectWorker_ToProject] FOREIGN KEY ([ID_Project]) REFERENCES [PMOS].[Project] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_ProjectWorker_ToWorker]...';


GO
ALTER TABLE [PMOS].[ProjectWorker]
    ADD CONSTRAINT [FK_ProjectWorker_ToWorker] FOREIGN KEY ([ID_Worker]) REFERENCES [PMOS].[Worker] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_Worker_ToUser]...';


GO
ALTER TABLE [PMOS].[Worker]
    ADD CONSTRAINT [FK_Worker_ToUser] FOREIGN KEY ([ID_User]) REFERENCES [PMOS].[User] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_WorkerTask_ToWorker]...';


GO
ALTER TABLE [PMOS].[WorkerTask]
    ADD CONSTRAINT [FK_WorkerTask_ToWorker] FOREIGN KEY ([ID_Worker]) REFERENCES [PMOS].[Worker] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[FK_WorkerTask_ToTask]...';


GO
ALTER TABLE [PMOS].[WorkerTask]
    ADD CONSTRAINT [FK_WorkerTask_ToTask] FOREIGN KEY ([ID_Task]) REFERENCES [PMOS].[Task] ([ID]);


GO
PRINT N'Выполняется создание [PMOS].[Role].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор роли.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Role', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[Role].[Name].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Имя роли.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Role', @level2type = N'COLUMN', @level2name = N'Name';


GO
PRINT N'Выполняется создание [PMOS].[UserRole].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор связи роли и пользователя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[UserRole].[ID_User].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID пользователя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'ID_User';


GO
PRINT N'Выполняется создание [PMOS].[UserRole].[ID_Role].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID роли.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'ID_Role';


GO
PRINT N'Выполняется создание [PMOS].[User].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор пользователя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[User].[UserName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Имя/ник пользователя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserName';


GO
PRINT N'Выполняется создание [PMOS].[User].[PasswordHash].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Хэш пароля.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'PasswordHash';


GO
PRINT N'Выполняется создание [PMOS].[User].[SecurityStamp].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'SecurityStamp';


GO
PRINT N'Выполняется создание [PMOS].[Status].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор статуса.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Status', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[Status].[Name].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Имя статуса.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Status', @level2type = N'COLUMN', @level2name = N'Name';


GO
PRINT N'Выполняется создание [PMOS].[Task].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Task', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[Task].[Name].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Task', @level2type = N'COLUMN', @level2name = N'Name';


GO
PRINT N'Выполняется создание [PMOS].[Task].[ID_Status].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID статуса задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Task', @level2type = N'COLUMN', @level2name = N'ID_Status';


GO
PRINT N'Выполняется создание [PMOS].[Task].[Comment].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Комментарий.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Task', @level2type = N'COLUMN', @level2name = N'Comment';


GO
PRINT N'Выполняется создание [PMOS].[Task].[Priority].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Приоритет задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Task', @level2type = N'COLUMN', @level2name = N'Priority';


GO
PRINT N'Выполняется создание [PMOS].[Project].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор проекта.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[Project].[Name].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название проекта.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'Name';


GO
PRINT N'Выполняется создание [PMOS].[Project].[CustomerCompanyName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название компании-заказчика.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'CustomerCompanyName';


GO
PRINT N'Выполняется создание [PMOS].[Project].[PerformerCompanyName].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название компании-исполнителя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'PerformerCompanyName';


GO
PRINT N'Выполняется создание [PMOS].[Project].[StartDate].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата начала проекта.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
PRINT N'Выполняется создание [PMOS].[Project].[EndDate].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата окончания проекта.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
PRINT N'Выполняется создание [PMOS].[Project].[Priority].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Приоритет проекта.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Project', @level2type = N'COLUMN', @level2name = N'Priority';


GO
PRINT N'Выполняется создание [PMOS].[ProjectTask].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор связи проекта и задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'ProjectTask', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[ProjectTask].[ID_Project].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID проекта.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'ProjectTask', @level2type = N'COLUMN', @level2name = N'ID_Project';


GO
PRINT N'Выполняется создание [PMOS].[ProjectTask].[ID_Task].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'ProjectTask', @level2type = N'COLUMN', @level2name = N'ID_Task';


GO
PRINT N'Выполняется создание [PMOS].[ProjectWorker].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор связи проекта и работника.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'ProjectWorker', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[ProjectWorker].[ID_Project].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID проекта.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'ProjectWorker', @level2type = N'COLUMN', @level2name = N'ID_Project';


GO
PRINT N'Выполняется создание [PMOS].[ProjectWorker].[ID_Worker].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID пользователя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'ProjectWorker', @level2type = N'COLUMN', @level2name = N'ID_Worker';


GO
PRINT N'Выполняется создание [PMOS].[Worker].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор работника.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Worker', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[Worker].[ID_User].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID пользователя.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Worker', @level2type = N'COLUMN', @level2name = N'ID_User';


GO
PRINT N'Выполняется создание [PMOS].[Worker].[Name].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Имя работника.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Worker', @level2type = N'COLUMN', @level2name = N'Name';


GO
PRINT N'Выполняется создание [PMOS].[Worker].[Surname].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Фамилия работника.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Worker', @level2type = N'COLUMN', @level2name = N'Surname';


GO
PRINT N'Выполняется создание [PMOS].[Worker].[Patronymic].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Отчество работника.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Worker', @level2type = N'COLUMN', @level2name = N'Patronymic';


GO
PRINT N'Выполняется создание [PMOS].[Worker].[Email].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Email работника.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'Worker', @level2type = N'COLUMN', @level2name = N'Email';


GO
PRINT N'Выполняется создание [PMOS].[WorkerTask].[ID].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор связи работника и задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'WorkerTask', @level2type = N'COLUMN', @level2name = N'ID';


GO
PRINT N'Выполняется создание [PMOS].[WorkerTask].[ID_Worker].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID работника.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'WorkerTask', @level2type = N'COLUMN', @level2name = N'ID_Worker';


GO
PRINT N'Выполняется создание [PMOS].[WorkerTask].[ID_Task].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID задачи.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'WorkerTask', @level2type = N'COLUMN', @level2name = N'ID_Task';


GO
PRINT N'Выполняется создание [PMOS].[WorkerTask].[IsAuthor].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Является ли автором.', @level0type = N'SCHEMA', @level0name = N'PMOS', @level1type = N'TABLE', @level1name = N'WorkerTask', @level2type = N'COLUMN', @level2name = N'IsAuthor';


GO
-- Выполняется этап рефакторинга для обновления развернутых журналов транзакций на целевом сервере

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ecab911b-ff90-401c-9adc-5ed7402fd68a')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ecab911b-ff90-401c-9adc-5ed7402fd68a')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'e106ee8f-25ad-4724-af0e-c52b610e11c6')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('e106ee8f-25ad-4724-af0e-c52b610e11c6')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '31f4ed97-f0ea-4cf0-bcde-c7ec4d3f233a')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('31f4ed97-f0ea-4cf0-bcde-c7ec4d3f233a')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '84315f1d-b031-42e5-b394-ba348adfaa76')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('84315f1d-b031-42e5-b394-ba348adfaa76')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'da7c2416-4472-4fa1-8969-c8d0d474b925')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('da7c2416-4472-4fa1-8969-c8d0d474b925')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '4923c05c-fa22-4463-b210-c637d66b7ce5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('4923c05c-fa22-4463-b210-c637d66b7ce5')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '7b2ee8cb-3023-4a29-b1ab-b7111cba747a')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('7b2ee8cb-3023-4a29-b1ab-b7111cba747a')

GO

GO
/*
Шаблон скрипта после развертывания							
--------------------------------------------------------------------------------------
 В данном файле содержатся инструкции SQL, которые будут добавлены в скрипт построения.		
 Используйте синтаксис SQLCMD для включения файла в скрипт после развертывания.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте после развертывания.		
 Пример:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


--/*
----------------------------------------------------------------------------------------
--[PMOS].[Role]
----------------------------------------------------------------------------------------
--*/
INSERT INTO [PMOS].[Role] ([Name]) VALUES (N'Руководитель')
INSERT INTO [PMOS].[Role] ([Name]) VALUES (N'Менеджер проекта')
INSERT INTO [PMOS].[Role] ([Name]) VALUES (N'Сотрудник')

--/*
----------------------------------------------------------------------------------------
--[PMOS].[Status]
----------------------------------------------------------------------------------------
--*/
INSERT INTO [PMOS].[Status] ([Name]) VALUES (N'ToDo')
INSERT INTO [PMOS].[Status] ([Name]) VALUES (N'InProgress')
INSERT INTO [PMOS].[Status] ([Name]) VALUES (N'Done')

-- Проверка и добавление пользователя на сервер
IF SUSER_ID('PMOS') IS NULL
    CREATE LOGIN [PMOS] WITH PASSWORD=N'j6D2Wsh5S8VaPg14Rtp2E9Ode', DEFAULT_DATABASE=[PMOS], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
-- Проверка и добавление пользователя для базы
IF USER_ID('PMOS') IS NULL
    CREATE USER PMOS FOR LOGIN PMOS
--Добавление прав пользователю
    ALTER ROLE db_datareader ADD MEMBER PMOS
    ALTER ROLE db_datawriter ADD MEMBER PMOS;
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Обновление завершено.';


GO
