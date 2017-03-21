(function (app) {
    app.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window'];

    function ProfileController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window) {

        angular.element(document).ready(function () {

            $scope.load();
        });
        $scope.data = {
            MainTab: 1
        };
        $scope.changeTab = function (tabIndex) {
            return $scope.data.MainTab = tabIndex;
        }

        $scope.load = function () {
            $scope.fireLoadProfileInformationEvent();
        };

        $scope.fireLoadProfileInformationEvent = function () {
            $scope.changeTab(1);
            $scope.$broadcast('fireLoadProfileInformationEvent',"");
        };
    }
})(angular.module('myApp'));