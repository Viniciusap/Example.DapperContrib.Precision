USE [DapperContribPrecision]
GO

/****** Object: Table [dbo].[TestPrecision] Script Date: 26/06/2021 16:09:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestPrecision] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Description]  VARCHAR (50) NULL,
    [Date]         DATETIME     NULL,
    [Bool]         BIT          NULL,
    [Description2] VARCHAR (10) NULL,
    [Decimal]      DECIMAL (18) NULL
);


