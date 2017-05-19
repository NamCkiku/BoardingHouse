(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'ENUMS', 'apiService', '$filter', '$timeout'];

    function roomController($scope, blockUI, $modal, $rootScope, BaseService, ENUMS, apiService, $filter, $timeout) {
        $scope.enums = ENUMS;
        $scope.pageSize = 10;
        $scope.filter = {
            Keywords: "",
            StartDate: "",
            EndDate: "",
            searchByStartDate: true,
            searchByEndDate: true,
            Status: true
        }
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: [],
        }
        function GetAllRoomType() {
            apiService.post('Management/LoadAllRoomType', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstRoomType = respone.data.lstData;
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.isDistrict = true;
        $scope.isWard = true;
        function GetAllProvince() {
            apiService.post('Management/LoadAllProvince', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstProvince = respone.data.lstData;
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.GetAllDistrict = GetAllDistrict;
        function GetAllDistrict(id) {
            apiService.post('Management/LoadAllDistrict', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstDistrict = $filter('filter')(respone.data.lstData, { provinceid: id }, true);
                    $scope.isDistrict = false;
                } else {
                }
            }, function (respone) {
            });
        }
        $scope.GetAllWard = GetAllWard;
        function GetAllWard(id) {
            apiService.post('Management/LoadAllWard', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstWard = $filter('filter')(respone.data.lstData, { districtid: id }, true);
                    $scope.isWard = false;
                } else {
                }
            }, function (respone) {
            });
        }
        function load() {
            GetAllProvince();
            GetAllRoomType();
            filterData();
        }
        load();
        $scope.Search = Search;
        function Search() {
            filterData();
        }
        var __appBasePath = $rootScope.baseUrl;
        $scope.editorOptions = {
            tools: [
                    "bold",
                    "italic",
                    "underline",
                    "strikethrough",
                    "justifyLeft",
                    "justifyCenter",
                    "justifyRight",
                    "justifyFull",
                    "insertUnorderedList",
                    "insertOrderedList",
                    "indent",
                    "outdent",
                    "createLink",
                    "unlink",
                    "insertImage",
                    "insertFile",
                    "subscript",
                    "superscript",
                    "createTable",
                    "addRowAbove",
                    "addRowBelow",
                    "addColumnLeft",
                    "addColumnRight",
                    "deleteRow",
                    "deleteColumn",
                    "viewHtml",
                    "formatting",
                    "cleanFormatting",
                    "fontName",
                    "fontSize",
                    "foreColor",
                    "backColor",
                    "print"
            ],

            imageBrowser: {
                messages: {
                    dropFilesHere: "Drop files here"
                },
                transport: {
                    type: "imagebrowser-aspnetmvc",
                    read: __appBasePath + "ImageBrowser/Read",
                    destroy: {
                        url: __appBasePath + "ImageBrowser/Destroy",
                        type: "POST"
                    },
                    create: {
                        url: __appBasePath + "ImageBrowser/Create",
                        type: "POST"
                    },
                    uploadUrl: __appBasePath + "ImageBrowser/upload",
                    imageUrl: __appBasePath + "Content/UserFiles/Images/{0}",
                    thumbnailUrl: __appBasePath + "ImageBrowser/thumbnail"
                }
            },
            fileBrowser: {
                messages: {
                    dropFilesHere: "Drop files here"
                },

                transport: {
                    type: "imagebrowser-aspnetmvc",
                    read: __appBasePath + "FileBrowser/Read",
                    destroy: {
                        url: __appBasePath + "FileBrowser/Destroy",
                        type: "POST"
                    },
                    create: {
                        url: __appBasePath + "FileBrowser/Create",
                        type: "POST"
                    },
                    uploadUrl: __appBasePath + "FileBrowser/upload",
                    fileUrl: __appBasePath + "Content/UserFiles/File/{0}",
                }
            }
        };
        $scope.Add = Add;
        function Add() {
            BaseService.ValidatorForm("#frmAdd");
            var frmAdd = angular.element(document.querySelector('#frmAdd'));
            var formValidation = frmAdd.data('formValidation').validate();
            if (formValidation.isValid()) {
                alert(1);
            }
        }
        $scope.moreImages = [];
        $scope.map;
        $scope.marker;
        $scope.uluru = { lat: 21.0029317912212212, lng: 105.820226663232323 };
        $scope.openModal = openModal;
        function openModal(item) {
            $scope.modalInstance = $modal.open({
                animation: true,
                templateUrl: 'Modal.html',
                backdrop: 'static',
                windowClass: 'center-modal',
                scope: $scope,
                keyboard: false,
                size: 'lg'
            });
            $scope.RoomInfo = item;
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
            }, 500);
            console.log($scope.RoomInfo);
            $scope.moreImages = JSON.parse($scope.RoomInfo.MoreImages);
            $scope.ok = function () {
                //BaseService.displaySuccess("Chuc Mung Nam Moi", 5000);
                //var msgInfor = {
                //    headerText: "NamCkiku",
                //    bodyMsg: "Không có gì hết",
                //    size: "lg",
                //    btnOK: "OK",
                //    btnCancel: "Cancel",
                //    type: $scope.enums.ModalType.Warning,
                //}
                //BaseService.showCommonDialog(msgInfor).then(function (result) {
                //});
                apiService.post('Room/ChangeStatus', true, { id: $scope.RoomInfo.RoomID }, function (respone) {
                    if (respone.data.success == true) {
                        BaseService.displaySuccess(respone.data.message, 5000);
                        filterData();
                    } else {
                    }
                }, function (respone) {
                    BaseService.displayError("Duyệt bài không thành công", 5000);
                });
                $scope.modalInstance.dismiss('cancel');
            };
            $scope.close = function () {
                $scope.modalInstance.dismiss('cancel');
            };
            $scope.modalInstance.result.then(function (response) {

            }, function () {
            });
        }
        function filterData() {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            $scope.mainGridOptions = {
                transport: {
                    read: function (options) {
                        if ($scope.filter.StartDate == null) {
                            $scope.filter.StartDate = '';

                        }

                        if ($scope.filter.StartDate == null) {
                            $scope.filter.StartDate = '';
                        }
                        var startDate;
                        var endDate;
                        if ($scope.filter.searchByStartDate == false) {
                            startDate = '';
                        }
                        if ($scope.filter.searchByStartDate == true) {
                            startDate = $scope.filter.StartDate;
                        }
                        if ($scope.filter.searchByEndDate == false) {
                            endDate = '';
                        }
                        if ($scope.filter.searchByEndDate == true) {
                            endDate = $scope.filter.EndDate;
                        }
                        var filter = {
                            Keywords: $scope.filter.Keywords,
                            StartDate: startDate,
                            EndDate: endDate,
                            Status: $scope.filter.Status,
                            Province: $scope.filter.ProvinceID,
                            District: $scope.filter.DistrictID,
                            Ward: $scope.filter.WardID,
                            RoomType: $scope.filter.RoomTypeID,
                            page: options.data.page - 1,
                            pageSize: options.data.pageSize
                        }
                        apiService.post('Room/LoadAllRoom', true, filter, function (respone) {
                            if (respone.data.lstData.Success == true) {
                                options.success(respone.data.lstData);
                                console.log(respone.data.lstData)
                                myBlockUI.stop();
                            } else {
                            }
                        }, function (respone) {
                            console.log('Load product failed.');
                            options.error(respone.data);
                            myBlockUI.stop();
                        });
                    }
                },
                group: [{ field: "ProvinceName" }, { field: "RoomTypeName" }],
                serverPaging: true,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                sortable: true,
                selectable: "multiple",
                pageable: true,
                groupable: true,
                filterable: true,
                columnMenu: true,
                reorderable: true,
                resizable: true,
                toolbar: ["excel"],
                excel: {
                    fileName: "Kendo UI Grid Export.xlsx",
                    proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
                    filterable: true
                },
                sortable: {
                    mode: "single",
                    allowUnsort: false
                },
                pageSize: 5,
                schema: {
                    data: "Items",
                    total: "TotalCount"
                }
            };
        };
        $scope.gridColumns = [
            {
                field: "Image", title: "Ảnh",
                width: "60px",
                template: "# if (Image != '')" + "{#<div class='customer-photo'" +
                                    "style='background-image: url(http://localhost:6568/Content/images/#:data.Image#);'></div>#}#",
            },
               { field: "RoomName", title: "Tên Phòng" },
               { field: "Address", title: "Địa Chỉ" },
               {
                   field: "Phone", title: "Số điện thoại",
               },
               {
                   field: "Price", title: "Gía tiền(vnđ)",
                   template: "#=Price == null ? '' : kendo.toString(Price, 'n0') #" + " (Vnđ)"
               },
               {
                   field: "Acreage", title: "Diện tích(m2)",
                   template: "#=Acreage == null ? '' : kendo.toString(Acreage, 'n0') #" + "<span style=\"margin-left:5px;\">m<sup>2</sup></span>"
               },
               {
                   field: "UserAvatar", title: "Người đăng",
                   template: "# if (UserAvatar != null)"
                                + "{#<div class='customer-photo'"
                                + "style='background-image: url(#:data.UserAvatar#);'></div>"
                                + "<div class='customer-name'>#: FullName #</div>#}#"
                                + "# if(UserAvatar ==null)"
                                + "{#<div class='customer-photo'"
                                + "style='background-image: url(http://localhost:15144/Content/img/boy-512.png);'></div>"
                                + "<div class='customer-name'>#: FullName #</div>#}#",
               },
               { field: "RoomTypeName", title: "Loại phòng" },
               {
                   field: "CreateDate", title: "Ngày đăng",
                   template: "#=CreateDate == null ? '' : kendo.toString(kendo.parseDate(CreateDate, 'yyyy-MM-dd'), '" + $rootScope.RootScopeDateFormat + "') #"
               },

               { field: "ProvinceName", title: "Tỉnh/Thành phố" },
               {
                   field: "", title: "Chức năng",
                   width: "200px",
                   template: "<button class=\"btn btn-xs btn-primary\" ng-click=\"openModal(this.dataItem);\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-eye\" aria-hidden=\"true\"></i>Xem</button>" +
                             //"<button class=\"btn btn-xs btn-info\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i>Sửa</button>" +
                             "<button class=\"btn btn-xs btn-danger\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-trash-o\" aria-hidden=\"true\"></i>Xóa</button>"

               },
        ];
    }
})(angular.module('myApp'));