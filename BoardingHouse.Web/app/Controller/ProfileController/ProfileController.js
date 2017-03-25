(function (app) {
    app.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window'];

    function ProfileController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window) {
        $scope.userInfo = {

        }
        angular.element(document).ready(function () { 
            $scope.load();
        });
        $scope.data = {
            MainTab: 1
        };
        $scope.changeTab = function (tabIndex) {
            return $scope.data.MainTab = tabIndex;
        }
        
        function GetAllUserInfo() {
            apiService.post('Home/GetUserLogin', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.userInfo = respone.data.user;
                    console.log($scope.userInfo);
                    $scope.fireLoadProfileInformationEvent();
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.load = function () {
            GetAllUserInfo();  
        };

        $scope.fireLoadProfileInformationEvent = function () {
            $scope.changeTab(1);
            $scope.$broadcast('fireLoadProfileInformationEvent', $scope.userInfo);
        };
        $scope.fireLoadListRoomByUserEvent = function () {
            $scope.changeTab(2);
            $scope.$broadcast('fireLoadListRoomByUserEvent', $scope.userInfo);
        };
    }
})(angular.module('myApp'));