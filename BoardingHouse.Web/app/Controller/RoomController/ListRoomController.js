(function (app) {
    app.controller('ListRoomController', ListRoomController);

    ListRoomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', 'fileUploadService'];

    function ListRoomController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, fileUploadService) {
        $scope.data = {
            lstRoom:[],
        }
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.GetAllRoomType = GetAllRoomType;
        function GetAllRoomType(page) {
            page = page || 0;
            var myBlockUI = blockUI.instances.get('LstRoomBlockUI');
            myBlockUI.start();
            var config = {
                page: page,
                pageSize: 5,
            }
            apiService.post('Management/GetAllRoom', true, config, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstRoom = respone.data.lstData.Items;
                    $scope.page = respone.data.lstData.Page;
                    $scope.pagesCount = respone.data.lstData.TotalPages;
                    $scope.totalCount = respone.data.lstData.TotalCount;
                    myBlockUI.stop();
                } else {
                }
            }, function (respone) {
            });
        }
        GetAllRoomType();
    }
})(angular.module('myApp'));