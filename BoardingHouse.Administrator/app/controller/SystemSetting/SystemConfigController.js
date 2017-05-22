(function (app) {
    app.controller('SystemConfigController', SystemConfigController);

    SystemConfigController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'ENUMS', 'apiService', '$filter', '$timeout', '$window'];

    function SystemConfigController($scope, blockUI, $modal, $rootScope, BaseService, ENUMS, apiService, $filter, $timeout, $window) {
        $scope.enums = ENUMS;
        $scope.settings = {
            DateFormat: "dd/MM/yyyy"
        }
        $scope.SaveThemeSetting = function () {
            console.log($scope.settings)
            var myBlockUI = blockUI.instances.get('systemBlockUI');
            myBlockUI.start("Saving...");
            apiService.post('System/SaveThemeSetting', true, $scope.settings, function (respone) {
                if (respone.data.success == true) {
                    $window.location.reload();
                } else {
                }
            }, function (respone) {
            });
        }
    }
})(angular.module('myApp'));