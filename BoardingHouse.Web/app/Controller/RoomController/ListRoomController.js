(function (app) {
    app.controller('ListRoomController', ListRoomController);

    ListRoomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', 'fileUploadService', '$filter'];

    function ListRoomController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, fileUploadService, $filter) {
        $scope.data = {
            lstRoom: [],
            lstRoomType: [],
        }
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.formatDate = function (date) {
            return BaseService.formatDate(date);
        }
        function GetAllRoomType() {
            apiService.post('Management/GetAllRoomType', true, null, function (respone) {
                if (respone.data.success == true) {
                    console.log(respone.data);
                    $scope.data.lstRoomType = respone.data.lstData;
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.GetAllRoom = GetAllRoom;
        function GetAllRoom(page) {
            page = page || 0;
            var myBlockUI = blockUI.instances.get('LstRoomBlockUI');
            myBlockUI.start();
            var config = {
                page: page,
                pageSize: 12,
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
        GetAllRoom();


        $scope.getCountRoomType = function (roomTypeID) {
            var result = 0;
            if (roomTypeID != null && roomTypeID != "") {
                result = $filter('filter')($scope.data.lstRoom, { RoomTypeID: roomTypeID }, true).length;
            }
            else {
                result = $scope.data.lstRoom.length;
            }
            return result;
        }
    }
})(angular.module('myApp'));