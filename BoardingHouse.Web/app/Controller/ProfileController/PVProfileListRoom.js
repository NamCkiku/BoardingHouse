(function (app) {
    app.controller('PVProfileListRoom', PVProfileListRoom);

    PVProfileListRoom.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', '$filter'];

    function PVProfileListRoom($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, $filter) {
        $scope.data = {
            lstRoom:[],
        }
        angular.element(document).ready(function () {
        });
        $scope.changePassword = {};
        $scope.$on('fireLoadListRoomByUserEvent', function (event, userInfo) {
            function GetAllUserInfo() {
                apiService.post('Home/GetUserLogin', true, null, function (respone) {
                    if (respone.data.success == true) {
                        $scope.UserInfomation = respone.data.user;
                    } else {
                    }
                }, function (respone) {
                });
            }
            $scope.load = function () {
                GetAllUserInfo();
                GetListRoomByUse();
            };
            function GetListRoomByUse(page) {
                page = page || 1;
                var config = {
                    page: page,
                    pageSize: 10,
                }
                apiService.post('Management/GetAllRoomByUserID', true, config, function (respone) {
                    if (respone.data.success == true) {
                        $scope.data.lstRoom = respone.data.lstData.Items;
                        console.log($scope.data.lstRoom)
                    } else {
                    }
                }, function (respone) {
                });
            }
            $scope.load();
        });
    }
})(angular.module('myApp'));