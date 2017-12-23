CREATE TABLE [dbo].[Transaction]
(
	[TransactionId]	BIGINT IDENTITY(1,1) NOT NULL,		 
    [Amount]		FlOAT NOT NULL, 
    [ReceiptNumber]	NVARCHAR(50) NOT NULL,
	[HouseId]  BIGINT NOT NULL,
	 [FromDate]		DATETIME NOT NULL,
	  [ToDate]		DATETIME NOT NULL,
	[TenantId]		BIGINT NOT NULL,
    [CreatedOn]		DATETIME NOT NULL, 
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([TransactionId] ASC),
	CONSTRAINT [FK_dbo_Transaction_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Transaction_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Transaction_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Transaction_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenant](TenantId),
	CONSTRAINT [FK_dbo_Transaction_House] FOREIGN KEY ([HouseId]) REFERENCES [dbo].[House](HouseId),

)
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_dbo_Transaction_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

