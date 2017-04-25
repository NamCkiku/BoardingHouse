(function (app) {
    app.controller('RoomDetailController', RoomDetailController);

    RoomDetailController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', 'fileUploadService', '$timeout'];

    function RoomDetailController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, fileUploadService, $timeout) {
        $scope.moreImages = [];
        function getID() {
            var path = $window.location.href;
            var result = String(path).split("/");
            if (result != null && result.length > 0) {
                var id = result[result.length - 1];
                return id;
            }
        }
        $scope.RoomInfo = {}
        $timeout(function () {
            $('#product_gallery').lightSlider({
                gallery: true,
                item: 1,
                loop: true,
                currentPagerPosition: 'middle',
                verticalHeight: 390,
                vThumbWidth: 130,
                vThumbHeight: 800,
                thumbMargin: 5,
                slideMargin: 0,
                thumbItem: 5,
                vertical: true,
            });
        },100);
        function GetRoomDetail() {
            var data = {
                ID: getID()
            }
            apiService.post('Management/GetRoomByID', true, data, function (respone) {
                if (respone.data.success == true) {
                    $scope.RoomInfo = respone.data.Objdata;
                    $scope.moreImages = JSON.parse($scope.RoomInfo.MoreImages);
                    console.log($scope.RoomInfo);
                } else {
                }
            }, function (respone) {
            });
        }
        GetRoomDetail();
    }
})(angular.module('myApp'));