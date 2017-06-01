//https://www.codeproject.com/Tips/1073972/Google-Map-JQuery-AngularJS-Address-Form-autocompl
(function (app) {
    'use strict';

    app.directive('ngMap', ngMap);
    function ngMap() {
        return {
            restrict: "EA",
            replace: true,
            template: "<div></div>",
            scope: {
                geoCoord: "=",
                center: "=",        //{latitude: 10, longitude: 10 } .
                markers: "=",       //[{ lat: 10, lon: 10, name: "sName" }] .
                zoom: "@",          //(1  zoomed out, 25  zoomed in).
                mapTypeId: "@",     //(roadmap, satellite, hybrid, terrain).
                panControl: "@",    //show a pan control on the map.
                zoomControl: "@",   //show a zoom control on the map.
                scaleControl: "@"   //show scale control on the map.
            },
            controller: function ($scope) {
                $scope.updateMap = function (elem) {
                    var options = getOptions();
                    $scope.map = new google.maps.Map(elem, options);
                    $scope.updateMarkers();
                    google.maps.event.addListener($scope.map, 'center_changed', function () {
                        if ($scope.initCenter) clearTimeout($scope.initCenter);
                        $scope.initCenter = $timeout(function () {
                            if ($scope.center) {
                                if ($scope.map.center.lat() != $scope.center.lat ||
                                    $scope.map.center.lng() != $scope.center.lon) {
                                    $scope.center = {
                                        lat: $scope.map.center.lat(),
                                        lon: $scope.map.center.lng()
                                    };
                                    if (!$scope.$$phase) $scope.$apply("center");
                                }
                            }
                        }, 500);
                    });
                }
                $scope.updateMarkers = function () {
                    if ($scope.map && $scope.markers) {
                        if ($scope.currentMarkers != null) {
                            for (var i = 0; i < $scope.currentMarkers.length; i++) {
                                $scope.currentMarkers[i] = m.setMap(null);
                            }
                        }
                        $scope.currentMarkers = [];
                        var markers = $scope.markers;
                        if (angular.isString(markers)) markers = $scope.$eval($scope.markers);
                        for (var i = 0; i < markers.length; i++) {
                            var m = markers[i];
                            var loc = new google.maps.LatLng(m.lat, m.lon);
                            var mm = new google.maps.Marker({ position: loc, map: $scope.map, title: m.name });
                            $scope.currentMarkers.push(mm);
                        }
                    }
                }
                $scope.getLocation = function (loc) {
                    if (loc == null) return new google.maps.LatLng(40, -73);
                    if (angular.isString(loc)) loc = $scope.$eval(loc);
                    return new google.maps.LatLng(loc.lat, loc.lon);
                }
                function getOptions() {
                    var options =
                                {
                                    center: new google.maps.LatLng(40, -73),
                                    zoom: 6,
                                    mapTypeId: "roadmap"
                                };
                    if ($scope.center) options.center = $scope.getLocation($scope.center);
                    if ($scope.zoom) options.zoom = $scope.zoom / 1;
                    if ($scope.mapTypeId) options.mapTypeId = $scope.mapTypeId;
                    if ($scope.panControl) options.panControl = $scope.panControl;
                    if ($scope.zoomControl) options.zoomControl = $scope.zoomControl;
                    if ($scope.scaleControl) options.scaleControl = $scope.scaleControl;
                    return options;
                }
            },
            link: function (scope, element, attrs) {
                var scopeArray = ["markers", "mapTypeId",
                "panControl", "zoomControl", "scaleControl"], toCenter;
                for (var i = 0, cnt = scopeArray.length; i < scopeArray.length; i++) {
                    scope.$watch(scopeArray[i], function () {
                        cnt--;
                        if (cnt <= 0) {
                            scope.updateMap(element[0]);
                        }
                    });
                }
                scope.$watch("zoom", function () {
                    if (scope.map && scope.zoom)
                        scope.map.setZoom(scope.zoom / 1);
                });
                scope.$watch("center", function () {
                    if (scope.map && scope.center)
                        scope.map.setCenter(scope.getLocation(scope.center));
                });
                scope.$watch("geoCoord", function (val) {
                    if (val) {
                        var coord = new google.maps.LatLng(val.lat, val.lon);
                        scope.map.setCenter(coord);
                        scope.map.setZoom(12);
                    }
                });
            }
        }
    }
 

    app.directive('ngAddressAutocomplete', ngAddressAutocomplete);
    function ngAddressAutocomplete() {
        return {
            restrict: "EA",
            scope: {
                ngModel: "=",
                city: "=",
                zipCode: "=",
                country: "=",
                streetName: "=",
                streetNumber: "="
            },
            link: function (scope, element, attrs) {
                var autocomplete = new google.maps.places.Autocomplete(element[0]);
                google.maps.event.addListener(autocomplete, 'place_changed', function () {
                    var place = autocomplete.getPlace();

                    if (!scope.$$phase) scope.$apply(function () {
                        var componenents = place.address_components || [];
                        for (var i = 0; i < componenents.length; i++) {
                            var item = componenents[i];
                            if (item && item.types) {
                                if (/route/gi.test(item.types[0])) {
                                    scope.streetName = componenents[1].long_name;
                                }
                                if (/country/gi.test(item.types[0])) {
                                    scope.country = item.long_name;
                                }
                                if (/street_number/gi.test(item.types[0])) {
                                    scope.streetNumber = item.long_name;
                                }
                                if (/postal_code/gi.test(item.types[0])) {
                                    scope.zipCode = item.long_name;
                                }
                                if (/dministrative_area_level_2|city/gi.test(item.types[0])) {
                                    scope.city = item.long_name;
                                }
                            }
                        }
                    });
                })
            }
        }
    }
    
    app.directive('ngGeocoder', ngGeocoder);
    function ngGeocoder() {
        return {
            restrict: "EA",
            template: '<input   type="text"  ng-model="ngModel" style="width:500px"/>' + '<button class="btn" type="button" ng-click="geoCode()" ng-disabled="address.length == 0" title="address" >' +

        '&nbsp;<i class="icon-search"></i></button>',
            scope: {
                ngModel: "=",
                geoCoord: "="
            },
            controller: function ($scope) {

                $scope.geoCode = function () {
                    if ($scope.ngModel && $scope.ngModel.length > 0) {
                        if (!$scope.geocoder) $scope.geocoder = new google.maps.Geocoder();
                        $scope.geocoder.geocode
                        ({ 'address': $scope.ngModel }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                var location = results[0].geometry.location;
                                if (!$scope.$$phase) $scope.$apply(function () {
                                    $scope.geoCoord = { lat: location.lat(), lon: location.lng() }
                                    $scope.ngModel = results[0].formatted_address;
                                });


                            } else {
                                alert("Sorry,  address produced no results.");
                            }
                        });
                    }
                };
            },
            link: function (scope, element, attrs) {
            }
    }
    
}

})(angular.module('myApp'));