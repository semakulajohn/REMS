CREATE TABLE [dbo].[Tenant]
(
	[TenantId]	BIGINT IDENTITY(1,1) NOT NULL,		 
    [FirstName]		nvarchar(50) NOT NULL, 
	[LastName]   nvarchar(50) NOT NULL,
	[Email]		nvarchar(50) NULL,
	[MobileNumber] nvarchar(max) NOT NULL,
	[HouseId]		BIGINT NOT NULL,
    [CreatedOn]		DATETIME NOT NULL, 
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED ([TenantId] ASC),
	CONSTRAINT [FK_dbo_Tenant_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Tenant_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Tenant_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Tenant_House] FOREIGN KEY ([HouseId]) REFERENCES [dbo].[House](HouseId),

)
GO
ALTER TABLE [dbo].[Tenant] ADD  CONSTRAINT [DF_dbo_Tenant_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO


