# EvidenceZamestnancu


Projekt je napojen na MariaDB. 
Pro localdb byl pouzit sql

CREATE TABLE [dbo].[Zamestnanci] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50)  NULL,
    [Surname]  NVARCHAR (50)  NULL,
    [Position] NVARCHAR (MAX) NULL,
    [Salary]   INT            NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


Nenechavejte prosim databazi prazdnou pro budouci navstevniky. Dekuji :]
