CREATE PROCEDURE [dbo].[Mark_House_And_Related_DataAs_Deleted]
	@inPutHouseId BIGINT,
	@userId NVARCHAR (128)
		
AS 
DECLARE 
@HouseId BIGINT,
@TenantId BIGINT,
@TransactionId BIGINT

DECLARE @TenantTransactions TABLE
(
	TransactionId bigint
	)
DECLARE @TenantHouses TABLE(
	TenantId BIGINT
)

BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateHouseRelatedDetails

INSERT INTO @TenantHouses
	SELECT TenantId FROM Tenant WHERE HouseId = @inPutHouseId  AND Deleted = 0 

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
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE HouseId = @inPutHouseId AND Deleted = 0
	
 

 COMMIT TRANSACTION TRA_UpdateHouseRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateHouseRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH
