angular
    .module('homer')
    .controller('EstateEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var estateId = $scope.estateId;
        var action = $scope.action;
        
        $http.get('/webapi/EstateApi/GetEstateHouseCount?estateId=' + estateId).success(function (data, status) {
            $scope.houseCount = data;
        });



        if (action == 'create') {
            estateId = 0;

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

        

            var promise = $http.get('/webapi/EstateApi/GetEstate?estateId=' + estateId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.estate = {
                        EstateId: m.EstateId,
                        Name: m.Name,
                        Description: m.Description,
                        Location: m.Location,
                        
                        Timestamp: m.Timestamp,
                        NumberOfHouses: $scope.houseCount,
                        CreatedOn: m.CreatedOn,
                        CreatedBy: m.CreatedBy,
                        UpdatedBy: m.UpdatedBy,
                        

                    };

                });


        }
      
        $scope.Save = function (estate) {
            
                $scope.showMessageSave = false;
                if ($scope.form.$valid) {
                    var promise = $http.post('/webapi/EstateApi/Save', {
                        EstateId: estateId,
                        Name: estate.Name,
                        Description: estate.Description,
                        CreatedBy: estate.CreatedBy,
                        CreatedOn: estate.CreatedOn,
                        Location: estate.Location,
                        NumberOfHouses : $scope.houseCount,


                    });

                    promise.then(
                        function (payload) {

                            EstateId = payload.data;

                            $scope.showMessageSave = true;
                            
                            $timeout(function () {
                                $scope.showMessageSave = false;
                               

                                if (action == "create") {
                                    $state.go('estate-edit', { 'action': 'edit', 'estateId': EstateId });
                                }

                            }, 1500);


                        });
                }

            }
        


        $scope.Cancel = function () {
            $state.go('estates.list');

        };

        $scope.Delete = function (estateId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/EstateApi/Delete?estateId=' + estateId, {});
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
    .module('homer').controller('EstateController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/EstateApi/GetAllEstates');
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

                { name: 'EstateId', field: 'EstateId', width: '5%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/estates/edit/{{row.entity.EstateId}}">{{row.entity.EstateId}}</a></div>' },
                {
                    name: 'Name', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/estates/edit/{{row.entity.EstateId}}">{{row.entity.Name}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },

                 { name: 'Location', field: 'Location' },


                { name: 'Houses', field: 'EstateId',  cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/houses/{{row.entity.EstateId}}">Manage {{row.entity.Name}}  Houses</a></div>' },

                 // { name: 'Number Of Houses', field: 'NumberOfHouses' },



            ];




        }]);

//angular
//    .module('homer').controller('TenantController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
//        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
//            $scope.loadingSpinner = true;
//            var promise = $http.get('/webapi/TenantApi/GetAllTenants');
//            promise.then(
//                function (payload) {
//                    $scope.gridData.data = payload.data;
//                    $scope.loadingSpinner = false;
//                }
//            );

//            $scope.gridData = {
//                enableFiltering: true,
//                columnDefs: $scope.columns,
//                enableRowSelection: true
//            };

//            $scope.gridData.multiSelect = true;

//            $scope.gridData.columnDefs = [

//                { name: 'Tenant Id', field: 'TenantId', width: '5%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/tenants/edit/{{row.entity.TenantId}}">{{row.entity.TenantId}}</a></div>' },
//                {
//                    name: 'First Name', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/tenants/edit/{{row.entity.TenantId}}">{{row.entity.FirstName}}</a> </div>',
//                    sort: {
//                        direction: uiGridConstants.ASC,
//                        priority: 1
//                    }
//                },

//                { name: 'Last Name', field: 'LastName' },

//                { name: 'Email Address', field: 'Email' },

//                 { name: 'Mobile Number ', field: 'MobileNumber' },

//                { name: 'Transactions', field: 'TenantId', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/transactions/{{row.entity.TenantId}}">Tenant Transactions</a></div>' },




//            ];




//        }]);

angular
    .module('homer').controller('EstateHouseController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var estateId = $scope.estateId;
            var promise = $http.get('/webapi/EstateApi/GetAllEstateHouses?estateId=' + estateId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedEstateId = $scope.estateId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                { name: 'House Id', field: 'HouseId', width: '5%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/houses/edit/{{row.entity.HouseId}}">{{row.entity.HouseId}}</a></div>' },
                {
                    name: 'House Number', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/houses/edit/{{row.entity.HouseId}}">{{row.entity.Number}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Rent Fee', field: 'Amount' },

                { name: 'Estate Name ', field: 'EstateName' },






            ];




        }]);
