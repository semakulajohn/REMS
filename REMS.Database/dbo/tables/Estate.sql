CREATE TABLE [dbo].[Estate]
(
	[EstateId]	BIGINT IDENTITY(1,1) NOT NULL,		 
    [Name]			VARCHAR(50) NOT NULL, 
    [Description]	NVARCHAR(MAX) NOT NULL,
	[NumberOfHouses]  INT NOT NULL,
	[Location]		NVARCHAR(MAX) NOT NULL,
    [CreatedOn]		DATETIME NOT NULL, 
    [Timestamp]		DATETIME NOT NULL,
	[CreatedBy]		nvarchar (128) not null,
	[UpdatedBy]		nvarchar (128) null,
	[Deleted]		BIT NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	

	 CONSTRAINT [PK_Estate] PRIMARY KEY CLUSTERED ([EstateId] ASC),
	CONSTRAINT [FK_dbo_Estate_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Estate_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	CONSTRAINT [FK_dbo_Estate_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
	
)
GO
ALTER TABLE [dbo].[Estate] ADD  CONSTRAINT [DF_dbo_Estate_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
