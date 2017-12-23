angular
    .module('homer').controller('AgentPaymentController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var agentId = $scope.agentId;
            var promise = $http.get('/webapi/PaymentApi/GetAllPaymentsForParticularAgent?agentId=' + agentId, {});
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


               { name: 'PaymentId', field: 'PaymentId', width: '5%', },
            {
                name: 'Amount Paid', field: 'AmountPaid',  },
            {
                name: 'Notes', field: 'Notes',
            },
            { name: 'PaidBy', field: 'adminName', width: '10%' },

            { name: 'Agent', field: 'AgentName' },

            { name: 'PaymentMethod', field: 'PaymentMethod' },
             { name: 'PaidOn', field: 'PaidOn', cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"' },

            ];


        }]);


angular
    .module('homer').controller('PaymentController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
        
            var promise = $http.get('/webapi/PaymentApi/GetAllPayments');

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

            { name: 'PaymentId', field: 'PaymentId', width: '5%', },
            {
                name: 'Amount Paid',field:'AmountPaid', },
            {
                name:'Notes',field:'Notes',
            },
            { name: 'PaidBy', field: 'adminName', width: '10%' },

            { name: 'Agent', field: 'AgentName' },

            { name: 'PaymentMethod', field: 'PaymentMethod' },

               { name: 'PaidOn', field: 'PaidOn', cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"' },

            ];
           

        }]);