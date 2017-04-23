(function (app) {
    app.controller('searchController', searchController);

    searchController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'apiService', '$window'];

    function searchController($scope, blockUI, $modal, $rootScope, apiService, $window) {
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
        $scope.select2Options = {
            allowClear: true
        };
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
        $scope.Search = Search;
        function Search() {
            var params = {
                RoomTypeID: $scope.keywords.RoomType,
                PriceFrom: $scope.slider.minValue,
                PriceTo: $scope.slider.maxValue,
                WardID: $scope.keywords.Ward,
                DistrictID: $scope.keywords.District,
                ProvinceID: $scope.keywords.Province,
                Keywords: $scope.keywords.Keywords
            };
            var queryString = [];
            for (var key in params) {
                if (params[key] !== undefined) {
                    queryString.push(key + '=' + params[key]);
                }
            }
            $window.location.href = '/Home/RoomDetail?' + queryString.join('&');
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
        $scope.ResultSearch = ResultSearch;
        function ResultSearch(page) {
            page = page || 0;
            var filter = {
                RoomTypeID: QueryString.RoomTypeID,
                PriceFrom: QueryString.PriceFrom * 1000000,
                PriceTo: QueryString.PriceTo * 1000000,
                WardID: QueryString.WardID,
                DistrictID: QueryString.DistrictID,
                ProvinceID: QueryString.ProvinceID,
                Keywords: QueryString.Keywords,
                page: page,
                pageSize: 10,
                //sort: $scope.sortKey
            }
            apiService.post('Management/GetAllRoomFullSearch', true, filter, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstRoom = respone.data.lstData;
                    console.log($scope.data.lstRoom);
                } else {
                }
            }, function () {
            });
        }
        function load() {
            GetAllProvince();
            GetAllRoomType();
            ResultSearch();
        }
        load();
    }
})(angular.module('myApp'));