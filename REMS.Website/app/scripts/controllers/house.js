angular
    .module('homer')
    .controller('HouseEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var houseId = $scope.houseId;
        var action = $scope.action;
        var estateId = $scope.estateId;


        if (action == 'create') {
            houseId = 0;

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



            var promise = $http.get('/webapi/HouseApi/GetHouse?houseId=' + houseId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.house = {
                        HouseId: m.HouseId,
                        Number: m.Number,
                        Amount: m.Amount,
                        EstateId: m.EstateId,
                        Timestamp: m.Timestamp,
                        CreatedOn: m.CreatedOn,
                        CreatedBy: m.CreatedBy,
                        UpdatedBy: m.UpdatedBy,
                        EstateName : m.EstateName,


                    };

                });


        }

        $scope.Save = function (house) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/HouseApi/Save', {
                    HouseId: houseId,
                    Number: house.Number,
                    Amount: house.Amount,
                    EstateId : estateId,
                    CreatedBy: house.CreatedBy,
                    CreatedOn: house.CreatedOn,


                });

                promise.then(
                    function (payload) {

                        houseId = payload.data;

                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('house-edit', { 'action': 'edit', 'houseId': houseId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('estates.list');

        };

        $scope.Delete = function (houseId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/HouseApi/Delete?houseId=' + houseId, {});
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
    .module('homer').controller('HouseController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/HouseApi/GetAllHouses');
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

