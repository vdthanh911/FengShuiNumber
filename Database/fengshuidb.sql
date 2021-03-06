USE [FengShuiNumberDb]
GO
/****** Object:  Table [dbo].[MobileNumber]    Script Date: 12/9/2021 9:44:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MobileNumber](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MobileNumber] [varchar](10) NULL,
	[NetworkProviderId] [int] NULL,
 CONSTRAINT [PK_MobileNumber] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetworkProvider]    Script Date: 12/9/2021 9:44:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkProvider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_NetworkProvider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetworkProviderPrefix]    Script Date: 12/9/2021 9:44:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkProviderPrefix](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Prefix] [varchar](3) NULL,
	[NetworkProviderId] [int] NULL,
 CONSTRAINT [PK_NetworkProviderPrefix] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MobileNumber]  WITH CHECK ADD  CONSTRAINT [FK_MobileNumber_NetworkProvider] FOREIGN KEY([NetworkProviderId])
REFERENCES [dbo].[NetworkProvider] ([Id])
GO
ALTER TABLE [dbo].[MobileNumber] CHECK CONSTRAINT [FK_MobileNumber_NetworkProvider]
GO
ALTER TABLE [dbo].[NetworkProviderPrefix]  WITH CHECK ADD  CONSTRAINT [FK_NetworkProviderPrefix_NetworkProvider] FOREIGN KEY([NetworkProviderId])
REFERENCES [dbo].[NetworkProvider] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NetworkProviderPrefix] CHECK CONSTRAINT [FK_NetworkProviderPrefix_NetworkProvider]
GO
