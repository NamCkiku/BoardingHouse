(function (app) {
    app.controller('searchController', searchController);

    searchController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope'];

    function searchController($scope, blockUI, $modal, $rootScope) {
        $scope.slider = {
            minValue: 0,
            maxValue: 50,
            options: {
                floor: 0,
                ceil: 50,
                step: 0.5,
                precision: 1,
                hideLimitLabels: true,
                hidePointerLabels: true,
            }
        };
    }
})(angular.module('myApp'));