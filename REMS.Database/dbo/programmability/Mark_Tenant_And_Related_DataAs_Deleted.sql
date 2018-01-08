CREATE PROCEDURE [dbo].[Mark_Tenant_And_Related_DataAs_Deleted]
	@inPutTenantId BIGINT,
	@userId NVARCHAR (128)
		
AS 
DECLARE 
@TenantId BIGINT,
@TransactionId BIGINT

DECLARE @TenantTransactions TABLE
(
		TransactionId bigint
	)


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateTenantRelatedDetails

INSERT INTO @TenantTransactions
	SELECT TransactionId FROM [Transaction] WHERE TenantId = @inPutTenantId  AND Deleted = 0 

WHILE(Select Count(*) From @TenantTransactions) > 0
BEGIN
	SELECT TOP 1 @TransactionId = TransactionId From @TenantTransactions 
				
		
	 Update [Transaction]
	 SET Deleted = 1,DeletedBy = @userId,DeletedOn = GETDATE()
	 WHERE TransactionId =@TransactionId AND Deleted = 0
	 
	 Delete @TenantTransactions Where TransactionId = @TransactionId

	
	
 END
 
	Update [Tenant]
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE TenantId = @inPutTenantId AND Deleted = 0

 COMMIT TRANSACTION TRA_UpdateTenantRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateTenantRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH

