(function (app) {
    'use strict';

    app.directive('searchControl', searchControl);
    searchControl.$inject = ['$rootScope'];
    function searchControl($rootScope) {
        var controller = ['$scope', '$rootScope', '$filter', '$q', '$http', '$timeout', '$window', '$modal', 'blockUI', '$log', '$location', 'ENUMS', 'BaseService', function ($scope, $rootScope, $filter, $q, $http, $timeout, $window, $modal, blockUI, $log, $location, ENUMS, BaseService) {
            $scope.filter = {
                Keywords: "",
                StartDate: "",
                EndDate: "",
                searchByStartDate: true,
                searchByEndDate: true,
                Status: true
            }
        }]
        return {
            restrict: 'EA', //Default in 1.3+
            scope: {
            },
            controller: controller,
            link: function (scope, elem, attr) {

            },
            templateUrl: $rootScope.baseUrl + 'SharedPartialView/_PVSearchControl',
        };
    }

})(angular.module('myApp'));