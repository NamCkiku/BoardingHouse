(function (app) {
    app.controller('RoomDetailController', RoomDetailController);

    RoomDetailController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', 'fileUploadService', '$timeout'];

    function RoomDetailController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, fileUploadService, $timeout) {
        $scope.moreImages = [];
        $scope.map;
        $scope.marker;
        $scope.uluru = { lat: 21.0029317912212212, lng: 105.820226663232323 };
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
                    $scope.uluru = {
                        lat: $scope.RoomInfo.Lat,
                        lng: $scope.RoomInfo.Lng
                    };
                    $scope.map = new google.maps.Map(document.getElementById('map'), {
                        center: $scope.uluru,
                        zoom: 12
                    });
                    $scope.marker = new google.maps.Marker({
                        position: $scope.uluru,
                        map: $scope.map,
                        icon: '/Content/img/IconMaker.png',
                        draggable: false,
                        animation: google.maps.Animation.DROP,
                    });
                    var populationOptions = {
                        strokeColor: '#67cfd8',
                        strokeOpacity: 0.6,
                        strokeWeight: 1,
                        fillColor: '#67cfd8',
                        fillOpacity: 0.2,
                        center: $scope.uluru,
                        map: $scope.map,
                        radius: 5000
                    };
                    var cityCircle = new google.maps.Circle(populationOptions);
                } else {
                }
            }, function (respone) {
            });
        }
        GetRoomDetail();
    }
})(angular.module('myApp'));