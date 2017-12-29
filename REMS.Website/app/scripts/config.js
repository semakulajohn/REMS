function configState($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(false);
    
    // Set default state
    $urlRouterProvider

        .otherwise("/dashboard");  

    $stateProvider
          // Dashboard - Main page
        .state('dashboard', {
            url: "/dashboard",
            templateUrl: "/app/views/dashboard.html",
            data: {
                pageTitle: 'Dashboard',
               
            }
        })
   
         .state('login', {
             url: "/login",
             templateUrl: "/app/views/adminAccount/login/login.html",
             data: {
                 //pageTitle: 'Profile'
             }
         })
              // User Profile page
    .state('profile', {
        url: "/profile",
        templateUrl: "/app/views/_common/profile.html",
        data: {
            pageTitle: 'Profile'
        }
    })
     

    

          // Modules section 


   //estates
     .state('estates', {
         abstract: true,
         url: "/estates",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Estates'
         }
     })

    .state('estates.list', {
        url: "/estates",
        templateUrl: "/app/views/estate/list.html",
        data: {
            pageTitle: 'Estates',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('estate-edit', {
        url: "/estates/:action/:estateId",
        templateUrl: "/app/views/estate/edit.html",
        data: {
            pageTitle: 'Estate edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.estateId = $stateParams.estateId;
            $scope.defaultTab = 'edit';
        }
    })
          .state('estate-house-edit', {
              url: "/houses/:action/:estateId/:houseId",
              templateUrl: "/app/views/house/edit.html",
              data: {
                  pageTitle: 'Estate House edit',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.action = $stateParams.action;
                  $scope.houseId = $stateParams.houseId;
                  $scope.estateId = $stateParams.estateId;
                  $scope.defaultTab = 'edit';
              }
          })

          .state('estate-houses', {
              url: "/houses/:estateId",
              templateUrl: "/app/views/estate/estate-house.html",
              data: {
                  pageTitle: 'Estate Houses',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.estateId = $stateParams.estateId;

              }
          })

         //tenants
     .state('tenants', {
         abstract: true,
         url: "/tenants",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Tenants'
         }
     })

    .state('tenants.list', {
        url: "/tenants",
        templateUrl: "/app/views/tenant/list.html",
        data: {
            pageTitle: 'Tenants',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('tenant-edit', {
        url: "/tenants/:action/:houseId/:tenantId",
        templateUrl: "/app/views/tenant/edit.html",
        data: {
            pageTitle: 'Tenant edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.tenantId = $stateParams.tenantId;
            $scope.houseId = $stateParams.houseId;
            $scope.defaultTab = 'edit';
        }
    })

         .state('house-tenants', {
             url: "/tenants/:houseId",
             templateUrl: "/app/views/house/house-tenants.html",
             data: {
                 pageTitle: 'House Tenants',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.houseId = $stateParams.houseId;

             }
         })

   //houses
     .state('houses', {
         abstract: true,
         url: "/houses",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Houses'
         }
     })

    .state('houses.list', {
        url: "/houses",
        templateUrl: "/app/views/house/list.html",
        data: {
            pageTitle: 'Houses',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('house-edit', {
        url: "/houses/:action/:houseId",
        templateUrl: "/app/views/house/edit.html",
        data: {
            pageTitle: 'House edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.houseId = $stateParams.houseId;
            $scope.defaultTab = 'edit';
        }
    })

            //transactions
     .state('transactions', {
         abstract: true,
         url: "/transactions",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Transactions'
         }
     })

    .state('transactions.list', {
        url: "/transactions",
        templateUrl: "/app/views/transaction/list.html",
        data: {
            pageTitle: 'Transactions',
        },
        controller: function ($scope, $stateParams) {

        }
    })
          .state('tenant-transactions', {
              url: "/transactions/:tenantId",
              templateUrl: "/app/views/transaction/tenant-transactions.html",
              data: {
                  pageTitle: 'Transactions',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.tenantId = $stateParams.tenantId;
                
              }
          })

    .state('transaction-edit', {
        url: "/transactions/:action/:transactionId",
        templateUrl: "/app/views/transaction/edit.html",
        data: {
            pageTitle: 'Transaction edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.transactionId = $stateParams.transactionId;
            $scope.defaultTab = 'edit';
        }
    })

     .state('tenant-transaction-edit', {
         url: "/transactions/:action/:tenantId/:transactionId",
         //url: "/transactions/:action/:transactionId",
         templateUrl: "/app/views/transaction/edit.html",
         data: {
             pageTitle: 'Tenant Transaction edit',
             pageDesc: ''
         },
         controller: function ($scope, $stateParams) {
             $scope.action = $stateParams.action;
             $scope.transactionId = $stateParams.transactionId;
             $scope.tenantId = $stateParams.tenantId;
             $scope.defaultTab = 'edit';
         }
     })
    
  
    
    //Search
    $stateProvider
    .state('search', {
        url: "/search/:q",
        templateUrl: "/app/views/search/index.html",
        data: {
            pageTitle: 'Search'
        },
        controller: function ($scope, $stateParams) {
            $scope.q = $stateParams.q;
        }
    })

}

angular
    .module('homer')
    .config(configState).run(function ($rootScope, $state) {
        $rootScope.$state = $state;
      
        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            if (next.match("/UsersAdmin/")) {
                var parts = next.split('#');
                if (parts.length > 1) {
                    if (!next.match('#/dashboard')) {
                        window.location = '/#' + parts[1];
                    }
                }
            }
        });

    })
  