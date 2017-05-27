(function (app) {
    app.controller('RoomTypeController', RoomTypeController);

    RoomTypeController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'ENUMS', 'apiService', '$filter', '$timeout', '$window'];

    function RoomTypeController($scope, blockUI, $modal, $rootScope, BaseService, ENUMS, apiService, $filter, $timeout, $window) {
        $scope.enums = ENUMS;
        $scope.data = {
            lstRoomType: [],
        }

        function filterData() {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            $scope.mainGridOptions = {
                transport: {
                    read: function (options) {
                        apiService.post('Management/LoadAllRoomType', true, null, function (respone) {
                            if (respone.data.success == true) {
                                options.success(respone.data);
                                console.log(respone.data);
                                myBlockUI.stop();
                            } else {
                            }
                        }, function (respone) {
                            BaseService.displayError("Không tải được loại phòng");
                            options.error(respone.data);
                            myBlockUI.stop();
                        });
                    },
                },
                type: "odata",
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                pageSize: 10,
                height: 550,
                sortable: true,
                sortable: {
                    mode: "single",
                    allowUnsort: false
                },
                schema: {
                    data: "lstData",
                    total: ""
                }

            };
        };
        $scope.gridColumns = [
            { field: "DisplayOrder", title: "Thứ tự", width: 100 },
            { field: "Image", title: "Icon" },
            { field: "RoomTypeName", title: "Tên Loại Phòng" },
            { field: "ParentID", title: "Cha" },
            {
                field: "", title: "Chức năng",
                width: "200px",
                template: "<button class=\"btn btn-xs btn-primary\" ng-click=\"openModal(this.dataItem);\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-eye\" aria-hidden=\"true\"></i>Sửa</button>" +
                          "<button class=\"btn btn-xs btn-danger\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-trash-o\" aria-hidden=\"true\"></i>Xóa</button>"
            },
        ];
        $scope.Search = Search;
        function Search() {
            filterData();
        }
        Search();
    }
})(angular.module('myApp'));