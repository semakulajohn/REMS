
angular
    .module('homer')
    .controller('TransactionEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var transactionId = $scope.transactionId;
        var action = $scope.action;
        var tenantId = $scope.tenantId;



        if (action == 'create') {
            transactionId = 0;

            var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
            promise.then(
                function (payload) {
                    var c = payload.data;

                    $scope.user = {
                        UserName: c.UserName,
                        Id: c.Id,
                        FirstName: c.FirstName,
                        LastName: c.LastName,
                        UserRoles: c.UserRoles,
                    };
                }

            );
        }
        if (action == 'tenantTransaction') {

            

        }
        if (action == 'edit') {



            var promise = $http.get('/webapi/TransactionApi/GetTransaction?transactionId=' + transactionId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.transaction = {
                        TransactionId: m.TransactionId,
                        HouseId: m.HouseId,
                        Amount: m.Amount,
                        TenantId: m.TenantId,
                        ReceiptNumber: m.ReceiptNumber,
                        FromDate: m.FromDate != null ? moment(m.FromDate).format('YYYY-MM-DD HH:mm:ss') : null,
                        ToDate : m.ToDate != null ? moment(m.ToDate).format('YYYY-MM-DD HH:mm:ss') : null,
                        Timestamp: m.Timestamp,
                        CreatedOn: m.CreatedOn,
                        CreatedBy: m.CreatedBy,
                        UpdatedBy: m.UpdatedBy,
                        TenantName :m.TenantName,

                    };

                });


        }

       

        $scope.Save = function (transaction) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/TransactionApi/Save', {
                   
                    TransactionId: transactionId,
                    Amount: transaction.Amount,
                    ReceiptNumber: transaction.ReceiptNumber,
                    FromDate: transaction.FromDate,
                    ToDate: transaction.ToDate,
                    TenantId: tenantId,
                    CreatedBy: transaction.CreatedBy,
                    CreatedOn: transaction.CreatedOn,


                });

                promise.then(
                    function (payload) {

                        TransactionId = payload.data;

                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('transaction-edit', { 'action': 'edit', 'transactionId': TransactionId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('transactions.list');

        };

        $scope.Delete = function (transactionId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/TransactionApi/Delete?transactionId=' + transactionId, {});
            promise.then(
                function (payload) {
                    $scope.showMessageDeleted = true;
                    $timeout(function () {
                        $scope.showMessageDeleted = false;
                        $scope.Cancel();
                    }, 2500);
                    $scope.showMessageDeleteFailed = false;
                },
                function (errorPayload) {
                    $scope.showMessageDeleteFailed = true;
                    $timeout(function () {
                        $scope.showMessageDeleteFailed = false;
                        $scope.Cancel();
                    }, 1500);
                });
        }


    }
    ]);


angular
    .module('homer').controller('TransactionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/TransactionApi/GetAllTransactions');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedTenantId = $scope.tenantId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                { name: 'Transaction Id', field: 'TransactionId', width: '5%' },
                {
                    name: 'Amount', field:'Amount',width : '10%',
                    
                },
                {name : 'Receipt Number',field:'ReceiptNumber'},

                {
                    name: 'Tenant Name', field: 'TenantName',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                { name: 'From Date', field: 'FromDate' },
                { name: 'To Date ', field: 'ToDate', format: 'yyyy - MM - dd' },
                { name: 'House Number', field: 'HouseNumber' },
                



            ];




        }]);



