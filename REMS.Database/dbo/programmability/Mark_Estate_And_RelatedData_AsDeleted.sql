CREATE PROCEDURE [dbo].[Mark_Estate_And_RelatedData_AsDeleted]
	@inPutEstateId BIGINT,
	@userId NVARCHAR (128)
		
AS 
DECLARE 
@EstateId BIGINT,
@HouseId BIGINT,
@TenantId BIGINT,
@TransactionId BIGINT

DECLARE @EstateHouses TABLE
(
	HouseId BIGINT
	
)
DECLARE @TenantTransactions TABLE
(
	TransactionId bigint
	)
DECLARE @TenantHouses TABLE(
	TenantId BIGINT
)

BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateEstateRelatedDetails

INSERT INTO @EstateHouses
	SELECT HouseId FROM House WHERE EstateId = @inPutEstateId  AND Deleted = 0 

WHILE(Select Count(*) From @EstateHouses) > 0
BEGIN
	SELECT TOP 1 @HouseId = HouseId From @EstateHouses 

		INSERT INTO @TenantHouses
		SELECT TenantId from Tenant WHERE HouseId = @HouseId AND Deleted = 0

	WHILE(Select Count(*) From @TenantHouses) > 0
	BEGIN
		SELECT TOP 1 @TenantId = TenantId From @TenantHouses 

		
		INSERT INTO @TenantTransactions
		SELECT  TransactionId from [Transaction] WHERE TenantId = @TenantId AND Deleted = 0

			WHILE (SELECT COUNT(*) FROM @TenantTransactions)>0
			BEGIN

			SELECT TOP 1 @TransactionId = TransactionId From @TenantTransactions 
				
		
			Update [Transaction]
			SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
			WHERE TransactionId =@TransactionId AND Deleted = 0
	 
			Delete @TenantTransactions Where TransactionId = @TransactionId

			End



	Update Tenant
	SET Deleted = 1,DeletedBy = @userId, DeletedOn = GETDATE() 
	WHERE TenantId = @TenantId AND Deleted = 0
	
	Delete @TenantHouses Where TenantId = @TenantId

		
	END
		
	
	Update House
	SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE HouseId = @HouseId AND Deleted = 0
	 Delete @EstateHouses Where HouseId = @HouseId
	
	END
		
	Update Estate
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE EstateId = @inPutEstateId AND Deleted = 0
	


 COMMIT TRANSACTION TRA_UpdateEstateRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateEstateRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH
