CREATE TABLE [dbo].[Patient_User] (
    [UserName]  NCHAR (10) NULL,
    [Password]  NCHAR (10) NULL,
    [FirstName] NCHAR (10) NULL,
    [LastName]  NCHAR (10) NULL,
    [Age]       INT        NULL,
    [Height]    INT        NULL,
    [Weight]    INT        NULL,
    [Diabetic]  BIT        NULL,
    [HBP]       BIT        NULL,
    [Smoke]     BIT        NULL,
    [Alcohol]   BIT        NULL,
    [PatientID] INT        IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientID] ASC)
);

