(function (app) {
    app.controller('mapController', mapController);

    mapController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', '$filter'];

    function mapController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, $filter) {
        $scope.map;
        $scope.marker;
        $scope.uluru = { lat: 21.0029317912212212, lng: 105.820226663232323 };
        $scope.data = {
            lstRoom: [],
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: []
        }
        $scope.slider = {
            minValue: 0,
            maxValue: 50,
            options: {
                floor: 0,
                ceil: 50,
                step: 0.5,
                precision: 1,
                hideLimitLabels: true,
                hidePointerLabels: true,
            }
        };
        $scope.select2Options = {
            allowClear: true
        };
        $scope.keywords = {
        };
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
        function GetAllProvince() {
            apiService.post('Management/GetAllProvince', true, null, function (respone) {
                if (respone.data.success == true) {
                    console.log(respone.data);
                    $scope.data.lstProvince = respone.data.lstData;
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.GetAllDistrict = GetAllDistrict;
        function GetAllDistrict(id) {
            apiService.post('Management/GetAllDistrict', true, null, function (respone) {
                if (respone.data.success == true) {
                    console.log(respone.data);
                    $scope.data.lstDistrict = $filter('filter')(respone.data.lstData, { provinceid: id }, true);
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.GetAllWard = GetAllWard;
        function GetAllWard(id) {
            apiService.post('Management/GetAllWard', true, null, function (respone) {
                if (respone.data.success == true) {
                    console.log(respone.data);
                    $scope.data.lstWard = $filter('filter')(respone.data.lstData, { districtid: id }, true);
                } else {
                }
            }, function (respone) {
            });
        }
        function formatPrice(nStr) {
            nStr += '';
            var x = nStr.split('.');
            var x1 = x[0];
            var x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ' ' + '$2');
            }
            return x1 + x2;
        }
        $scope.Search = Search;
        function Search() {
            GetAllRoom();
        }
        $scope.GetAllRoom = GetAllRoom;
        function GetAllRoom(page) {
            page = page || 0;
            var config = {
                RoomTypeID: $scope.keywords.RoomType,
                PriceFrom: $scope.slider.minValue * 1000000,
                PriceTo: $scope.slider.maxValue * 1000000,
                WardID: $scope.keywords.Ward,
                DistrictID: $scope.keywords.District,
                ProvinceID: $scope.keywords.Province,
                Keywords: $scope.keywords.Keywords,
                page: page,
                pageSize: 20,
            }
            apiService.post('Management/GetAllRoomFullSearch', true, config, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstRoom = respone.data.lstData.Items;
                    myLocation($scope.data.lstRoom);
                } else {
                }
            }, function (respone) {
            });
        }
        GetAllProvince();
        GetAllRoomType();
        Search();

        $scope.markers = [];
        function addMarkers(props, map) {
            var marker;
            for (var i = 0; i < $scope.markers.length; i++) {
                $scope.markers[i].setMap(null);
            }
            $.each(props, function (i, prop) {
                var latlng = new google.maps.LatLng(prop.Lat, prop.Lng);
                marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    icon: $rootScope.baseUrl + 'Content/img/IconMaker.png',
                    draggable: false,
                    animation: google.maps.Animation.DROP,
                });
                var infoboxContent = '<a href="/Home/RoomDetail/' + prop.RoomID + '"><div class="infoW">' +
                                        '<div class="propImg">';
                infoboxContent += '<div class="featured-label">' +
                                                '<div class="featured-label-left"></div>' +
                                                '<div class="featured-label-content"><span class="fa fa-star"></span></div>' +
                                                '<div class="featured-label-right"></div>' +
                                                '<div class="clearfix"></div>' +
                                            '</div>';
                infoboxContent += '<img src="http://localhost:6568/Content/images/' + prop.Image + '">' +
                                            '<div class="propBg">' +
                                                '<div class="propPrice">' + prop.RoomTypeName + '</div>';
                if (prop.Price > 0) {
                    infoboxContent += '<div class="propType">' + formatPrice(prop.Price) + 'VNĐ</div>';
                }
                infoboxContent += '</div>' +
                                        '</div>' +
                                        '<div class="paWrapper">' +
                                            '<div class="propTitle">' + prop.RoomName + '</div>' +
                                            '<div class="propAddress">';
                if (prop.Address != '') {
                    infoboxContent += prop.Address + ', ';
                }
                infoboxContent += '</div>' +
                                        '</div>' +
                                        '<ul class="propFeat">';
                if (prop.BedroomNumber != '') {
                    infoboxContent += '<li><span class="fa fa-moon-o"></span> ' + prop.BedroomNumber + '</li>';
                }
                if (prop.ToiletNumber != '') {
                    infoboxContent += '<li><span class="fa fa-moon-o"></span> ' + prop.ToiletNumber + '</li>';
                }
                if (prop.Acreage != '') {
                    infoboxContent += '<li><span class="fa fa-moon-o"></span> ' + formatPrice(prop.Acreage) + '</li>';
                }
                infoboxContent += '</ul></a>';
                var directionsService = new google.maps.DirectionsService;
                var directionsDisplay = new google.maps.DirectionsRenderer;
                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        calculateAndDisplayRoute(directionsService, directionsDisplay, $scope.pointA, latlng);
                        infobox.setContent(infoboxContent);
                        infobox.open(map, marker);
                        directionsDisplay.setMap(map);
                    }
                })(marker, i));

                $(document).on('touchend', '.closeInfo', function (e) {
                    infobox.open(null, null);
                });
                $(document).on('click', '.closeInfo', function (e) {
                    infobox.open(null, null);
                });

                $scope.markers.push(marker);
            });
        }
        var infobox = new google.maps.InfoWindow({
            disableAutoPan: false,
            maxWidth: 202,
            pixelOffset: new google.maps.Size(0, 0),
            zIndex: null,
            boxStyle: {
                background: "url(http://mariusn.com/themes/reales-wp/wp-content/themes/reales/images/infobox-bg.png) no-repeat",
                opacity: 1,
                width: "202px",
                height: "300px"
            },
            closeBoxMargin: "28px 26px 0px 0px",
            closeBoxURL: "",
            infoBoxClearance: new google.maps.Size(1, 1),
            pane: "floatPane",
            enableEventPropagation: false
        });
        $scope.map = new google.maps.Map(document.getElementById('map'), {
            center: $scope.uluru,
            zoom: 14
        });
        function myLocation(arr) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

                    var latlng = new google.maps.LatLng(pos.lat, pos.lng);

                    $scope.pointA = latlng;
                    $scope.map = new google.maps.Map(document.getElementById('map'), {
                        center: latlng,
                        zoom: 14
                    });
                    var populationOptions = {
                        strokeColor: '#67cfd8',
                        strokeOpacity: 0.6,
                        strokeWeight: 1,
                        fillColor: '#67cfd8',
                        fillOpacity: 0.2,
                        center: latlng,
                        map: $scope.map,
                        radius: Math.sqrt(25000000)
                    };
                    var cityCircle = new google.maps.Circle(populationOptions);
                    var listRoomAround = GetListArround(pos.lat, pos.lng, 5000, arr)
                    addMarkers(listRoomAround, $scope.map)
                    marker1 = new google.maps.Marker({
                        position: latlng,
                        map: $scope.map,
                        icon: $rootScope.baseUrl + 'Content/img/user-marker.png',
                        draggable: false,
                        animation: google.maps.Animation.DROP,
                    });
                }, function () {
                });
            } else {
                addMarkers($scope.data.lstRoom, $scope.map)
            }
        }
        function calculateAndDisplayRoute(directionsService, directionsDisplay, pointA, pointB) {
            directionsService.route({
                origin: pointA,
                destination: pointB,
                travelMode: 'WALKING'
            }, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                } else {
                    window.alert('Directions request failed due to ' + status);
                }
            });
        }
        function GetListArround(x1, y1, radius, arrSource) {
            var result = [];
            if (arrSource != null && arrSource.length > 0) {
                $.each(arrSource, function (i, prop) {
                    var room = calculateArourd(x1, y1, prop.Lat, prop.Lng);
                    if (room <= radius) {
                        result.push(prop);
                    }
                });
            }
            return result;
        }

        function calculateArourd(lat1, lon1, lat2, lon2) {
            console.log(lat1, lon1, lat2, lon2)
            var radlat1 = Math.PI * lat1 / 180
            var radlat2 = Math.PI * lat2 / 180
            var theta = lon1 - lon2
            var radtheta = Math.PI * theta / 180
            var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
            dist = Math.acos(dist)
            dist = dist * 180 / Math.PI
            dist = dist * 60 * 1.1515
            dist = dist * 1.609344 *1000;
            console.log(dist);
            return dist
        }
    }
})(angular.module('myApp'));