/****** Object:  Table [dbo].[Basic_Salary]    Script Date: 12/14/2018 14:22:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Basic_Salary]') AND type in (N'U'))
DROP TABLE [dbo].[Basic_Salary]
GO

/****** Object:  Table [dbo].[Basic_Salary]    Script Date: 12/14/2018 14:22:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Basic_Salary](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](64) NULL,
	[Key] [nvarchar](64) NULL,
	[FormType] [int] NULL,
	[FormName] [nvarchar](64) NULL,
	[SalaryType] [int] NULL,
	[IsIncluded] [bit] NULL,
	[Description] [nvarchar](255) NULL,
	[DataStatus] [tinyint] NULL,
	[CreateBy] [nvarchar](64) NULL,
	[CreateAt] [datetime] NULL,
	[UpdateBy] [nvarchar](64) NULL,
	[UpdateAt] [datetime] NULL,
	[OrderIndex] [int] NULL,
	[FormContent] [int] NULL,
 CONSTRAINT [PK__Basic_Sa__C41E0288031D3AFB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

