(function (app) {
    app.controller('PostController', PostController);

    PostController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'ENUMS', 'apiService', '$filter', '$timeout', '$window'];

    function PostController($scope, blockUI, $modal, $rootScope, BaseService, ENUMS, apiService, $filter, $timeout, $window) {
        $scope.enums = ENUMS;
    }
})(angular.module('myApp'));