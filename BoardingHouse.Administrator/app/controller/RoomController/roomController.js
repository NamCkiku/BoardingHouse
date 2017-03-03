(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'ENUMS', 'apiService'];

    function roomController($scope, blockUI, $modal, $rootScope, BaseService, ENUMS, apiService) {
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
        function load() {
            filterData();
            //var myBlockUI = blockUI.instances.get('BlockUIRoom');
            //myBlockUI.start();
            //myBlockUI.stop();
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

        $scope.openModal = openModal;
        function openModal() {
            $scope.modalInstance = $modal.open({
                animation: true,
                templateUrl: 'Modal.html',
                backdrop: 'static',
                windowClass: 'center-modal',
                scope: $scope,
                keyboard: false,
                size: 'lg'
            });
            //BaseService.postData("Report", "GetReportSnapshot", false, null).then(function (response) {
            //    alert(1);
            //})
            $scope.ok = function () {
                BaseService.displaySuccess("Chuc Mung Nam Moi", 5000);
                var msgInfor = {
                    headerText: "NamCkiku",
                    bodyMsg: "Không có gì hết",
                    size: "lg",
                    btnOK: "OK",
                    btnCancel: "Cancel",
                    type: $scope.enums.ModalType.Warning,
                }
                BaseService.showCommonDialog(msgInfor).then(function (result) {
                });
                $scope.modalInstance.dismiss('cancel');
            };
            $scope.close = function () {
                $scope.modalInstance.dismiss('cancel');
            };
            $scope.modalInstance.rendered.then(function (response) {
            })
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
                            page: options.data.page,
                            pageSize: options.data.pageSize
                        }
                        apiService.post('Room/LoadAllRoom', true, filter, function (respone) {
                            if (respone.data.lstData.Success == true) {
                                options.success(respone.data.lstData);
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
                serverPaging: true,
                sortable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                pageSize: 10,
                schema: {
                    data: "Items",
                    total: "TotalCount"
                }
            };
        };
        $scope.gridColumns = [
               { field: "RoomName", title: "Tên Phòng" },
                { field: "Address", title: "Địa Chỉ" },
        ];
    }
})(angular.module('myApp'));