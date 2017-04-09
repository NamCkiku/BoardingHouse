(function (app) {
    app.controller('searchController', searchController);

    searchController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'apiService'];

    function searchController($scope, blockUI, $modal, $rootScope, apiService) {
        $scope.page = 0;
        $scope.pagesCount = 0;
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
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: []
        }
        var QueryString = function () {
            // This function is anonymous, is executed immediately and 
            // the return value is assigned to QueryString!
            var query_string = {};
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                // If first entry with this name
                if (typeof query_string[pair[0]] === "undefined") {
                    query_string[pair[0]] = decodeURIComponent(pair[1]);
                    // If second entry with this name
                } else if (typeof query_string[pair[0]] === "string") {
                    var arr = [query_string[pair[0]], decodeURIComponent(pair[1])];
                    query_string[pair[0]] = arr;
                    // If third or later entry with this name
                } else {
                    query_string[pair[0]].push(decodeURIComponent(pair[1]));
                }
            }
            return query_string;
        }();
        $scope.keywords = {
        };
        function Search() {
            var params = {
                CategoryID: $scope.keywords.CategoryID,
                PriceFrom: $scope.slider.minValue,
                PriceTo: $scope.slider.maxValue,
                Wardid: $scope.keywords.Wardid,
                Districtid: $scope.keywords.Districtid,
                Provinceid: $scope.keywords.Provinceid
            };
            var queryString = [];
            for (var key in params) {
                if (params[key] !== undefined) {
                    queryString.push(key + '=' + params[key]);
                }
            }
            $window.location.href = '/Info/Search?' + queryString.join('&');

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
            apiService.post('Management/GetAllDistrict', true, { id: id }, function (respone) {
                if (respone.data.success == true) {
                    console.log(respone.data);
                    $scope.data.lstDistrict = respone.data.lstData;
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.GetAllWard = GetAllWard;
        function GetAllWard(id) {
            apiService.post('Management/GetAllWard', true, { id: id }, function (respone) {
                if (respone.data.success == true) {
                    console.log(respone.data);
                    $scope.data.lstWard = respone.data.lstData;
                } else {
                }
            }, function (respone) {
            });
        }
        function load() {
            GetAllProvince();
            GetAllRoomType();
        }
        load();
    }
})(angular.module('myApp'));