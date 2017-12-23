angular
    .module('homer').controller('AgentController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/AgentApi/GetAllAgents');
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

              
                {
                    name: 'First Name', field:'FirstName',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'LastName', field: 'LastName', width: '15%', },

                { name: 'Email', field: 'Email' },
                 { name: 'PhoneNumber', field: 'PhoneNumber', width: '15%', },
                   { name: 'Action', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/agents/{{row.entity.Id}}">Manage agent</a></div>' },

            ];




        }]);


angular
    .module('homer').controller('AgentCommissionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var agentId = $scope.agentId;
            var promise = $http.get('/webapi/AgentApi/GetAllAgentCommissions?agentId=' + agentId, {});
            
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

            { name: 'CommissionId', field: 'CommissionId', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/commissions/edit/{{row.entity.CommissionId}}">{{row.entity.CommissionId}}</a></div>' },
            {
                name: 'Amount', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/commissions/edit/{{row.entity.CommissionId}}">{{row.entity.CommissionAmount}}</a> </div>',
                sort: {
                    direction: uiGridConstants.ASC,
                    priority: 1
                }
            },

            { name: 'IsPaid', field: 'IsPaid', width: '10%' },

            { name: 'Client', field: 'ClientName' },

            { name: 'Service', field: 'ServiceName' },
            ];


        }]);

angular
    .module('homer').controller('AgentPaidCommissionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var agentId = $scope.agentId;
            var promise = $http.get('/webapi/AgentApi/GetAllAgentPaidCommissions?agentId=' + agentId, {});

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

            { name: 'CommissionId', field: 'CommissionId', width: '15%' },
            {
                name: 'Amount', field:'CommissionAmount',
                sort: {
                    direction: uiGridConstants.ASC,
                    priority: 1
                }
            },

            { name: 'IsPaid', field: 'IsPaid', width: '10%' },

            { name: 'Client', field: 'ClientName' },

            { name: 'Service', field: 'ServiceName' },
            ];


        }]);

angular
    .module('homer').controller('AgentUnPaidCommissionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var agentId = $scope.agentId;
            $scope.commissionAmount = 0;
            var promise = $http.get('/webapi/AgentApi/GetAllAgentUnPaidCommissions?agentId=' + agentId, {});

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

            { name: 'CommissionId', field: 'CommissionId', width: '15%' },
            {
                name: 'Amount', field:'CommissionAmount',
                sort: {
                    direction: uiGridConstants.ASC,
                    priority: 1
                }
            },

            { name: 'IsPaid', field: 'IsPaid', width: '10%' },

            { name: 'Client', field: 'ClientName' },

            { name: 'Service', field: 'ServiceName' },
            ];

            $http.get('/webapi/CommissionApi/GetAllPaymentMethods').success(function (data, status) {
                $scope.paymentMethods = data;
            });



            $scope.currentFocused = "";


            $scope.UpdateCommissionAmount = function (value) {
                $scope.commissionAmount += value;
                $scope.commission.PaymentAmount = $scope.commissionAmount;
                //console.log($scope.commission.PaymentAmount);

            };
            $scope.DecreaseCommissionAmount = function (value) {
                $scope.commissionAmount -= value;
                $scope.commission.PaymentAmount = $scope.commissionAmount;
                //console.log($scope.commission.PaymentAmount);

            };
            $scope.commission = {

                PaymentAmount: "",


            };

            $scope.gridData.onRegisterApi = function (gridApi) {
                $scope.gridApi = gridApi;
               
                gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    if (row.isSelected) {
                        $scope.UpdateCommissionAmount(row.entity.CommissionAmount);
                    }
                    else if(!row.isSelected) {
                        $scope.DecreaseCommissionAmount(row.entity.CommissionAmount);
                    }
                    var msg = 'row selected ' + row.isSelected;
                    //$log.log(msg);
                });


            };




            $scope.PayCommission = function () {
                $scope.selectedCommissions = $scope.gridApi.selection.getSelectedRows($scope.gridData);
                if ($scope.selectedCommissions != null) {

                    var payment = {

                        AmountPaid: $scope.commission.PaymentAmount,
                        AgentId: agentId,
                        Notes: $scope.commission.Notes,
                        PaymentMethodId: $scope.commission.PaymentMethodId,
                        IsPaid: true,
                        Deleted: false,
                    };

                    var promise = $http.post('/webapi/CommissionApi/PayMultipleCommissions', { Commissions: $scope.selectedCommissions, Payment: payment });
                    promise.then(
                        function (payload) {
                            var paymentId = payload.data;

                        });
                }
            }


        }]);

angular
    .module('homer').controller('AgentClientsController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var agentId = $scope.agentId;
            var promise = $http.get('/webapi/AgentApi/GetAllAgentClients?agentId=' + agentId, {});

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

            { name: 'ClientId', field: 'ClientId', width: '5%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/clients/edit/{{row.entity.ClientId}}">{{row.entity.ClientId}}</a></div>' },
                {
                    name: 'Name',
                    field: 'Name',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },
                {
                    name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/agent/{{row.entity.AgentId}}/{{row.entity.ClientId}}">Commission</a> </div>',
                }
            ];


        }]);

angular
    .module('homer').controller('AgentPaidCommissionPerClientController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var agentId = $scope.agentId;
            var clientId = $scope.clientId;
            var promise = $http.get('/webapi/AgentApi/GetAllAgentPaidCommissionPerClient?agentId=' + agentId + '&clientId=' + clientId, {});

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

            { name: 'CommissionId', field: 'CommissionId', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/commissions/edit/{{row.entity.CommissionId}}">{{row.entity.CommissionId}}</a></div>' },
            {
                name: 'Amount', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/commissions/edit/{{row.entity.CommissionId}}">{{row.entity.CommissionAmount}}</a> </div>',
                sort: {
                    direction: uiGridConstants.ASC,
                    priority: 1
                }
            },

            { name: 'Client', field: 'ClientName' },

            { name: 'Service', field: 'ServiceName' },
            ];


        }]);

angular
    .module('homer').controller('AgentUnPaidCommissionPerClientController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var agentId = $scope.agentId;
            var clientId = $scope.clientId;
            var promise = $http.get('/webapi/AgentApi/GetAllAgentUnPaidCommissionPerClient?agentId=' + agentId + '&clientId=' + clientId, {});

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

            { name: 'CommissionId', field: 'CommissionId', width: '15%'},
            {
                name: 'Amount', field:'CommissionAmount' ,
                sort: {
                    direction: uiGridConstants.ASC,
                    priority: 1
                }
            },


            { name: 'Client', field: 'ClientName' },

            { name: 'Service', field: 'ServiceName' },
            ];


        }]);