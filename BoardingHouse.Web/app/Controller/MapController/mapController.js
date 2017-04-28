(function (app) {
    app.controller('mapController', mapController);

    mapController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window'];

    function mapController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window) {
        $scope.map;
        $scope.marker;
        $scope.uluru = { lat: 21.0029317912212212, lng: 105.820226663232323 };
        $scope.data = {
            lstRoom: [],
        }
        $scope.GetAllRoom = GetAllRoom;
        function GetAllRoom(page) {
            page = page || 0;
            var config = {
                page: page,
                pageSize: 12,
            }
            apiService.post('Management/GetAllRoom', true, config, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstRoom = respone.data.lstData.Items;
                    for (var i = 0; i < $scope.data.lstRoom.length; i++) {
                        if ($scope.data.lstRoom[i].Lat !== null && $scope.data.lstRoom[i].Lng != null) {
                            var latlng = new google.maps.LatLng($scope.data.lstRoom[i].Lat, $scope.data.lstRoom[i].Lng);
                            console.log($scope.data.lstRoom[i].Lat, $scope.data.lstRoom[i].Lng);
                            $scope.marker = new google.maps.Marker({
                                position: latlng,
                                map: $scope.map,
                                animation: google.maps.Animation.DROP,
                                icon: '/Content/img/IconMaker.png',
                            });
                            var contentString = '<div class="infoW"><div class="propImg"><div class="featured-label"><div class="featured-label-left"></div><div class="featured-label-content"><span class="fa fa-star"></span></div><div class="featured-label-right"></div><div class="clearfix"></div></div><img src="http://mariusn.com/themes/reales-wp/wp-content/uploads/2015/02/img-prop-400x240.jpg"><div class="propBg"><div class="propPrice">$799 000 </div><div class="propType">For Sale</div></div></div><div class="paWrapper"><div class="propTitle">Sophisticated Residence</div><div class="propAddress">600 40th Ave, San Francisco</div></div><ul class="propFeat"><li><span class="fa fa-moon-o"></span> 2</li><li><span class="icon-drop"></span> 3</li><li><span class="icon-frame"></span> 1270 sq ft</li></ul><div class="clearfix"></div><div class="infoButtons"><a class="btn btn-sm btn-round btn-gray btn-o closeInfo">Close</a><a href="http://mariusn.com/themes/reales-wp/properties/sophisticated-residence/" class="btn btn-sm btn-round btn-green viewInfo">View</a></div></div>'
                            var infowindow = new google.maps.InfoWindow({
                                content: contentString,
                                disableAutoPan: false,
                                maxWidth: 400,
                                pixelOffset: new google.maps.Size(0, 0),
                                zIndex: null,
                                boxStyle: {
                                    'background': '#fff',
                                    'opacity': 1,
                                    'padding': '5px',
                                    'box-shadow': '0 1px 2px 0 rgba(0, 0, 0, 0.13)',
                                    'width': '140px',
                                    'text-align': 'center',
                                    'border-radius': '3px'
                                },
                                closeBoxMargin: "28px 26px 0px 0px",
                                closeBoxURL: "",
                                infoBoxClearance: new google.maps.Size(1, 1),
                                pane: "floatPane",
                                enableEventPropagation: false
                            });
                            google.maps.event.addListener($scope.marker, 'click', (function (marker, i) {
                                return function () {
                                    infowindow.setContent(contentString);
                                    infowindow.open($scope.map, marker);
                                }
                            })($scope.marker, i));
                        }
                        else {
                            $scope.marker = new google.maps.Marker({
                                position: $scope.uluru,
                                map: $scope.map,
                                animation: google.maps.Animation.DROP,
                                icon: '/Content/img/IconMaker.png',
                            });
                        }
                    }
                } else {
                }
            }, function (respone) {
            });
        }
        GetAllRoom();
        //var locations = [
        //{ lat: -31.563910, lng: 147.154312 },
        //{ lat: -33.718234, lng: 150.363181 },
        //{ lat: -33.727111, lng: 150.371124 },
        //{ lat: -33.848588, lng: 151.209834 },
        //{ lat: -33.851702, lng: 151.216968 },
        //{ lat: -34.671264, lng: 150.863657 },
        //{ lat: -35.304724, lng: 148.662905 },
        //{ lat: -36.817685, lng: 175.699196 },
        //{ lat: -36.828611, lng: 175.790222 },
        //{ lat: -37.750000, lng: 145.116667 },
        //{ lat: -37.759859, lng: 145.128708 },
        //{ lat: -37.765015, lng: 145.133858 },
        //{ lat: -37.770104, lng: 145.143299 },
        //{ lat: -37.773700, lng: 145.145187 },
        //{ lat: -37.774785, lng: 145.137978 },
        //{ lat: -37.819616, lng: 144.968119 },
        //{ lat: -38.330766, lng: 144.695692 },
        //{ lat: -39.927193, lng: 175.053218 },
        //{ lat: -41.330162, lng: 174.865694 },
        //{ lat: -42.734358, lng: 147.439506 },
        //{ lat: -42.734358, lng: 147.501315 },
        //{ lat: -42.735258, lng: 147.438000 },
        //{ lat: -43.999792, lng: 170.463352 }
        //]
        $scope.map = new google.maps.Map(document.getElementById('map'), {
            center: $scope.uluru,
            zoom: 14
        });


    }
})(angular.module('myApp'));