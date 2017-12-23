CREATE TABLE [dbo].[House]
(
	[HouseId]	BIGINT IDENTITY(1,1) NOT NULL,		 
    [Number]		NVARCHAR  NULL, 
    [Amount]	 Float NOT NULL,
	[EstateId]	BIGINT NOT NULL,
    [CreatedOn]		DATETIME NOT NULL, 
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_House] PRIMARY KEY CLUSTERED ([HouseId] ASC),
	CONSTRAINT [FK_dbo_House_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_House_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_db_House_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_House_Estate] FOREIGN KEY ([EstateId]) REFERENCES [dbo].[Estate](EstateId),

	
)
GO
ALTER TABLE [dbo].[House] ADD  CONSTRAINT [DF_dbo_House_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO


