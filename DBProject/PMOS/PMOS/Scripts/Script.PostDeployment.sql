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
INSERT INTO [PMOS].[Role] ([Name],[SystemName]) VALUES (N'Supervisor', N'Руководитель')
INSERT INTO [PMOS].[Role] ([Name],[SystemName]) VALUES (N'ProjectManager','Руководитель проекта')
INSERT INTO [PMOS].[Role] ([Name],[SystemName]) VALUES (N'Employee', N'Сотрудник')

--/*
----------------------------------------------------------------------------------------
--[PMOS].[Status]
----------------------------------------------------------------------------------------
--*/
INSERT INTO [PMOS].[Status] ([Name],[SystemName]) VALUES (N'ToDo', N'На рассмотрении')
INSERT INTO [PMOS].[Status] ([Name],[SystemName]) VALUES (N'InProgress', N'В процессе')
INSERT INTO [PMOS].[Status] ([Name],[SystemName]) VALUES (N'Done', N'Сделано')

-- Проверка и добавление пользователя на сервер
IF SUSER_ID('PMOS') IS NULL
    CREATE LOGIN [PMOS] WITH PASSWORD=N'j6D2Wsh5S8VaPg14Rtp2E9Ode', DEFAULT_DATABASE=[PMOS], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
-- Проверка и добавление пользователя для базы
IF USER_ID('PMOS') IS NULL
    CREATE USER PMOS FOR LOGIN PMOS
--Добавление прав пользователю
    ALTER ROLE db_datareader ADD MEMBER PMOS
    ALTER ROLE db_datawriter ADD MEMBER PMOS;