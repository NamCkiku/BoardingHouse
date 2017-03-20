(function (app) {
    app.controller('ListRoomController', ListRoomController);

    ListRoomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', 'fileUploadService'];

    function ListRoomController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, fileUploadService) {
        alert(1)
        function GetAllRoomType() {
            apiService.post('Management/GetAllRoom', true, null, function (respone) {
                if (respone.data.success == true) {
                } else {
                }
            }, function (respone) {
            });
        }
        GetAllRoomType();
    }
})(angular.module('myApp'));