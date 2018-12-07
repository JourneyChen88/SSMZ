USE [HeYiAMIS]
GO

/****** Object:  Table [dbo].[Adims_OTypesetting_test]    Script Date: 04/06/2016 22:48:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Adims_OTypesetting_test](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatID] [nvarchar](100) NOT NULL,
	[PatZhuYuanID] [nvarchar](50) NULL,
	[applyID] [nvarchar](150) NULL,
	[Cardno] [nvarchar](50) NULL,
	[Patname] [nvarchar](50) NULL,
	[Patsex] [nvarchar](50) NULL,
	[Patage] [int] NULL,
	[ageDW] [nvarchar](50) NULL,
	[PatHeight] [nvarchar](50) NULL,
	[PatWeight] [nvarchar](50) NULL,
	[PatBloodType] [nvarchar](50) NULL,
	[Patbedno] [nvarchar](50) NULL,
	[Patdpm] [nvarchar](50) NULL,
	[PatBingqu] [nvarchar](50) NULL,
	[Pattmd] [nvarchar](150) NULL,
	[Patbq] [varchar](50) NULL,
	[Oname] [nvarchar](150) NULL,
	[Oroom] [varchar](50) NULL,
	[xueya] [int] NULL,
	[maibo] [int] NULL,
	[huxi] [int] NULL,
	[tiwen] [float] NULL,
	[Second] [nvarchar](50) NULL,
	[Olevel] [varchar](10) NULL,
	[Amethod] [varchar](50) NULL,
	[GL] [varchar](20) NULL,
	[JZ] [varchar](10) NULL,
	[AP1] [nvarchar](50) NULL,
	[AP2] [varchar](20) NULL,
	[AP3] [varchar](20) NULL,
	[AA1] [varchar](20) NULL,
	[AA2] [varchar](20) NULL,
	[AA3] [varchar](20) NULL,
	[OS] [nvarchar](50) NULL,
	[OA1] [varchar](20) NULL,
	[OA2] [varchar](20) NULL,
	[OA3] [varchar](20) NULL,
	[OA4] [varchar](20) NULL,
	[TP] [varchar](20) NULL,
	[ON1] [nvarchar](50) NULL,
	[ON2] [nvarchar](50) NULL,
	[SN1] [nvarchar](50) NULL,
	[SN2] [nvarchar](50) NULL,
	[SN3] [nvarchar](50) NULL,
	[Remarks] [nvarchar](450) NULL,
	[asa] [varchar](50) NULL,
	[asae] [int] NULL,
	[Ostate] [varchar](20) NULL,
	[Odate] [datetime] NULL,
	[ReplyDate] [datetime] NULL,
	[SSStartTime] [datetime] NULL,
	[SSEndTime] [datetime] NULL,
	[operaddress] [nvarchar](50) NULL,
	[state] [nvarchar](50) NULL,
 CONSTRAINT [PK_OTypesetting_test] PRIMARY KEY CLUSTERED 
(
	[PatID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


