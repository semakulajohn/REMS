angular
    .module('homer')
    .controller('TenantEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var tenantId = $scope.tenantId;
        var action = $scope.action;
        var houseId = $scope.houseId;

       if (action == 'create') {
            tenantId = 0;

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

        if (action == 'edit') {



            var promise = $http.get('/webapi/TenantApi/GetTenant?tenantId=' + tenantId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.tenant = {
                        TenantId: m.TenantId,
                        FirstName: m.FirstName,
                        LastName : m.LastName,
                        Email: m.Email,
                        HouseId :m.HouseId,
                        Timestamp: m.Timestamp,
                        MobileNumber: m.MobileNumber,
                        CreatedOn: m.CreatedOn,
                        CreatedBy: m.CreatedBy,
                        UpdatedBy: m.UpdatedBy,


                    };

                });


        }

        $scope.Save = function (tenant) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/TenantApi/Save', {
                    TenantId: tenantId,
                    FirstName: tenant.FirstName,
                    LastName: tenant.LastName,
                    Email: tenant.Email,
                    HouseId : houseId,
                    MobileNumber : tenant.MobileNumber,
                    CreatedBy: tenant.CreatedBy,
                    CreatedOn: tenant.CreatedOn,


                });

                promise.then(
                    function (payload) {

                        TenantId = payload.data;

                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('tenant-edit', { 'action': 'edit', 'tenantId': TenantId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('tenants.list');

        };

        $scope.Delete = function (tenantId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/TenantApi/Delete?tenantId=' + tenantId, {});
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
    .module('homer').controller('TenantController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/TenantApi/GetAllTenants');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                { name: 'Tenant Id', field: 'TenantId', width: '5%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/tenants/edit/{{row.entity.TenantId}}">{{row.entity.TenantId}}</a></div>' },
                {
                    name: 'First Name', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/tenants/edit/{{row.entity.TenantId}}">{{row.entity.FirstName}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Last Name', field: 'LastName' },

                { name: 'Email Address', field: 'Email' },

                 { name: 'Mobile Number ', field: 'MobileNumber' },

                { name: 'Transactions', field: 'TenantId', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/transactions/{{row.entity.TenantId}}">Tenant Transactions</a></div>' },
                  



            ];




        }]);

angular
    .module('homer').controller('TenantTransactionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var tenantId = $scope.tenantId;
            var promise = $http.get('/webapi/TenantApi/GetTransactionsForParticularTenant?tenantId=' + tenantId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                { name: 'Transaction Id', field: 'TransactionId', width: '5%' },
                {
                    name: 'Amount', field: 'Amount', width: '10%',

                },
                { name: 'Receipt Number', field: 'ReceiptNumber' },

                {
                    name: 'Tenant Name', field: 'TenantName',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                { name: 'From Date', field: 'FromDate' },
                { name: 'To Date ', field: 'ToDate' },

            ];




        }]);
