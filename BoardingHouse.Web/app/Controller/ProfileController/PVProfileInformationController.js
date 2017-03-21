(function (app) {
    app.controller('PVProfileInformationController', PVProfileInformationController);

    PVProfileInformationController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window'];

    function PVProfileInformationController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window) {

        angular.element(document).ready(function () {
        });
        $scope.$on('fireLoadProfileInformationEvent', function (event, userInfo) {
            console.log(userInfo);
            $scope.UserInfomation = userInfo;
        });
    }
})(angular.module('myApp'));