CREATE PROCEDURE [dbo].[Mark_Estate_And_RelatedData_AsDeleted]
	@inPutEstateId BIGINT,
	@userId NVARCHAR (128)
		
AS 
DECLARE 
@EstateId BIGINT,
@ManagerId NVARCHAR(128),
@HouseId BIGINT,
@TenantId BIGINT,
@TransactionId BIGINT

DECLARE @EstateHouses TABLE
(
	EstateId BIGINT,
	HouseId BIGINT
	
)
DECLARE @TenantTransactions TABLE
(
	TenantId bigint,
	TransactionId bigint
	)
DECLARE @TenantHouses TABLE(
	TenantId BIGINT,
	HouseId BIGINT
)

BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateEstateRelatedDetails

INSERT INTO @EstateHouses
	SELECT EstateId FROM House WHERE EstateId = @inPutEstateId  AND Deleted = 0 

WHILE(Select Count(*) From @EstateHouses) > 0
BEGIN
	SELECT TOP 1 @HouseId = HouseId From @EstateHouses 

		INSERT INTO @TenantHouses
		SELECT TenantId,HouseId from Tenant WHERE HouseId = @HouseId AND Deleted = 0

		WHILE(SELECT COUNT(*) FROM @TenantHouses)>0
		BEGIN

		SELECT TOP 1 @TenantId = TenantId FROM @TenantHouses

		INSERT INTO @TenantTransactions
		SELECT  TransactionId,TenantId from [Transaction] WHERE TenantId = @TenantId AND Deleted = 0

			WHILE (SELECT COUNT(*) FROM @TenantTransactions)>0
			BEGIN
			SELECT TOP 1 @TransactionId = TransactionId FROm @TenantTransactions
			END 
		
	 Update [Transaction]
	 SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	 WHERE TransactionId =@TransactionId AND Deleted = 0
	 
	 Delete @TenantTransactions Where TransactionId = @TransactionId

	Update Tenant
	SET Deleted = 1,DeletedBy = @userId, DeletedOn = GETDATE() 
	WHERE TenantId = @TenantId AND Deleted = 0
	 Delete @TenantHouses Where TenantId = @TenantId

	Update House
	SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE HouseId = @HouseId AND Deleted = 0
	 Delete @EstateHouses Where HouseId = @HouseId
	
	END
	
	
	
	Update Estate
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE EstateId = @EstateId AND Deleted = 0
	
 END

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
