CREATE TABLE [dbo].[Archive]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(10, 10), 
    [Title] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    [CategoryId] NVARCHAR(100)  NULL, 
    [IsImported] BIT NULL
)
