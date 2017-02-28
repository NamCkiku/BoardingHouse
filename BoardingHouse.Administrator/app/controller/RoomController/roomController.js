(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'ENUMS'];

    function roomController($scope, blockUI, $modal, $rootScope, BaseService, ENUMS) {
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
        function filterData () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            $scope.mainGridOptions = {
                transport: {
                    read: function (options) {
                        //var listResvStatus = '';

                        //angular.forEach($scope.statusModel, function (value, key) {
                        //    listResvStatus += value.label + ",";
                        //});
                        //var listShift = '';

                        //angular.forEach($scope.statusModel1, function (value, key) {
                        //    listShift += value.id + ",";
                        //});
                        //var listGuestType = '';

                        //angular.forEach($scope.statusModel2, function (value, key) {
                        //    listGuestType += value.id + ",";
                        //});
                        //var startDate;
                        //var endDate;
                        //if ($scope.searchInfo.searchByStartDate == false) {
                        //    startDate = '';
                        //} else {
                        //    startDate = $scope.searchInfo.startDate;
                        //}

                        //if ($scope.searchInfo.searchByEndDate == false) {
                        //    endDate = '';
                        //} else {
                        //    endDate = $scope.searchInfo.endDate;
                        //}
                        //var listRev = '';

                        //angular.forEach($scope.statusModelRev, function (value, key) {
                        //    listRev += value.label + ",";
                        //});
                        //var searchResvEntity = {
                        //    startDate: startDate,
                        //    endDate: endDate,
                        //    lstStatus: listResvStatus,
                        //    keyword: $scope.searchInfo.fullTextSearch,
                        //    shift: listShift,
                        //    revID: listRev,
                        //    siteId: $scope.SiteInfo.SiteID,
                        //    listguestType: listGuestType,
                        //    pageSize: options.data.pageSize,
                        //    pageNumber: options.data.page
                        //};
                        BaseService.postData("Room", "LoadAllRoom", true, null).then(function (response) {
                            if (response.success == true) {
                                options.success(response);
                                $scope.data.AllRoom = response.lstData;
                            } else {
                            }
                        }).finally(function () {
                            myBlockUI.stop();
                        }, function () { });
                    }
                },
                pageSize: $scope.pageSize,
                group: false,
                serverPaging: false,
                sortable: false,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                schema: {
                    data: "lstData",
                }
            };
        };
        $scope.gridColumns = [
               { field: "RoomName", title: "Tên Phòng" },
        ];
    }
})(angular.module('myApp'));