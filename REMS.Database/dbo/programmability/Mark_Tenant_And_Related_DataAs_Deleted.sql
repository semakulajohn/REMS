CREATE PROCEDURE [dbo].[Mark_Tenant_And_Related_DataAs_Deleted]
	@inPutTenantId BIGINT,
	@userId NVARCHAR (128)
		
AS 
DECLARE 
@TenantId BIGINT,
@TransactionId BIGINT

DECLARE @TenantTransactions TABLE
(
	TenantId bigint,
	TransactionId bigint
	)


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateTenantRelatedDetails

INSERT INTO @TenantTransactions
	SELECT TransactionId,TenantId FROM [Transaction] WHERE TenantId = @inPutTenantId  AND Deleted = 0 

WHILE(Select Count(*) From @TenantTransactions) > 0
BEGIN
	SELECT TOP 1 @TenantId = TenantId From @TenantTransactions 
				
		
	 Update [Transaction]
	 SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	 WHERE TransactionId =@TransactionId AND Deleted = 0
	 
	 Delete @TenantTransactions Where TransactionId = @TransactionId

	
	
	
	Update [Transaction]
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE TransactionId = @TransactionId AND Deleted = 0
	
 END

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

