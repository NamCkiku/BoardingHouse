(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'ENUMS'];

    function roomController($scope, blockUI, $modal, $rootScope, BaseService, ENUMS) {
        $scope.enums = ENUMS;
        $scope.filter = {
            Keywords: "",
            StartDate: "",
            EndDate: "",
            searchByStartDate: true,
            searchByEndDate: true,
            Status: true
        }
        function load() {
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
                    headerText:"NamCkiku",
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


        //$scope.filterData = function () {
        //    var myBlockUI = blockUI.instances.get('BlockUI');
        //    myBlockUI.start();
        //    $scope.mainGridOptions = {
        //        transport: {
        //            read: function (options) {
        //                var listResvStatus = '';

        //                angular.forEach($scope.statusModel, function (value, key) {
        //                    listResvStatus += value.label + ",";
        //                });
        //                var listShift = '';

        //                angular.forEach($scope.statusModel1, function (value, key) {
        //                    listShift += value.id + ",";
        //                });
        //                var listGuestType = '';

        //                angular.forEach($scope.statusModel2, function (value, key) {
        //                    listGuestType += value.id + ",";
        //                });
        //                var startDate;
        //                var endDate;
        //                if ($scope.searchInfo.searchByStartDate == false) {
        //                    startDate = '';
        //                } else {
        //                    startDate = $scope.searchInfo.startDate;
        //                }

        //                if ($scope.searchInfo.searchByEndDate == false) {
        //                    endDate = '';
        //                } else {
        //                    endDate = $scope.searchInfo.endDate;
        //                }
        //                var listRev = '';

        //                angular.forEach($scope.statusModelRev, function (value, key) {
        //                    listRev += value.label + ",";
        //                });
        //                var searchResvEntity = {
        //                    startDate: startDate,
        //                    endDate: endDate,
        //                    lstStatus: listResvStatus,
        //                    keyword: $scope.searchInfo.fullTextSearch,
        //                    shift: listShift,
        //                    revID: listRev,
        //                    siteId: $scope.SiteInfo.SiteID,
        //                    listguestType: listGuestType,
        //                    pageSize: options.data.pageSize,
        //                    pageNumber: options.data.page
        //                };
        //                BaseService.postData("TResConfiguration", "LoadAllTResReservationStatus", true, null).then(function (response) {
        //                    if (response.success == true) {
        //                        $scope.data.AllReservationStatus = response.lstData;
        //                    }
        //                    return BaseService.postData("TResHome", "GetReservationByDateRange", true, searchResvEntity);
        //                }).then(function (response) {
        //                    if (response.success == true) {
        //                        if (response.lstData.length > 0 && response.lstData != null) {
        //                            for (var i = 0; i < response.lstData.length; i++) {
        //                                response.lstData[i].Date = BaseService.formatDate(response.lstData[i].StartTime);
        //                            }
        //                        }
        //                        options.success(response);
        //                        $scope.data.allDataReservationList = response.lstData;
        //                        console.log($scope.data.allDataReservationList)
        //                        $scope.totalRow = response.total;
        //                    }
        //                }).finally(function () {
        //                    myBlockUI.stop();
        //                }, function () { });
        //            }
        //        },
        //        pageSize: $scope.pageSize,
        //        group: [{ field: "Date" }, { field: "RevName" }, { field: "FnbShiftName", dir: "asc" }],
        //        serverPaging: true,
        //        sortable: true,
        //        pageable: {
        //            refresh: true,
        //            pageSizes: true,
        //            buttonCount: 5
        //        },
        //        schema: {
        //            data: "lstData",
        //            total: "total"
        //        }
        //    };
        //};
        //$scope.gridColumns = [
        //       { field: "StartTime", hidden: true, title: $('#UserKey_Date').val(), template: "#=StartTime == null ? '' : kendo.toString(kendo.parseDate(StartTime, 'yyyy-MM-dd'), '" + $rootScope.RootScopeDateFormat + "') #" },
        //       { field: "StartTimeString", title: $('#UserKey_StartTime').val(), template: "#= StartTimeString + ' - ' + EndTimeString #" },
        //       { field: "FnbShiftName", hidden: true, title: $('#UserKey_Shift').val() },
        //       { field: "TableNo", title: $('#UserKey_TableNo').val() + ".", template: "<label ng-bind=GetListTableNos(this.dataItem)></label>" },
        //       { field: "ReceiptNo", title: $('#UserKey_ReceiptNo').val(), template: "<label ng-bind=this.dataItem.ReceiptNo></label>", hidden: !($scope.ShowTableReservation == '1') },
        //       { field: "Pax", title: $('#UserKey_TotalGuest').val() },
        //       { field: "GuestName", title: $('#UserKey_GuestName').val(), template: "#= GuestName #" + "<img ng-if=\"#= DOB #\" src=\"/Content/images/BOD.gif\" width=\"30\" style=\"margin-left: 5px;\" />" + "<img ng-if=\"#=MaritalStatus#\" src=\"/Content/images/animatedgifs3.gif\" width=\"30\" style=\"margin-left: 5px;\" />" },
        //       { field: "GuestType", title: $('#UserKey_GuestType').val() },
        //       { field: "Mobile", title: $('#UserKey_Mobile').val() + "." },
        //       { field: "RoomNo", title: "Room No." },
        //       { field: "RevName", title: "Rev. Center", hidden: true },
        //       { field: "SourceName", title: $('#UserKey_Source').val(), template: "# if (SourceName != null)" + "{# #=SourceName# # }" + " else if (SourceName == null)" + "{ # Unknown # } #" },
        //       { field: "ReservationStatusName", title: $('#UserKey_Status').val(), template: "<label class=\"lable-status\" style=\"#= functionThatReturnsStyle(ReservationStatusID) #\">#= ReservationStatusName #</label>" },
        //       { field: "Remarks", title: $('#UserKey_Remarks').val() },
        //        {
        //            field: "",
        //            width: "200px",
        //            title: $('#UserKey_Operation').val(),
        //            template: "# if (GuestName != '')" + "{#<a href=\"\" ng-click=\"BookingReservationModal(this.dataItem, true)\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Edit\" class=\"text-success margin-left-5\" ng-hide=\"#= ReservationStatusID == Seated() || ReservationStatusID == Cancelled() || ReservationStatusID == CheckOut1() #\"> <i class=\"fa fa-pencil-square-o\"></i></a>"
        //                + "<a href=\"\" ng-click=\"ViewDetailBookingReservation(this.dataItem)\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"View\" class=\"text-info margin-left-5\"><i class=\"fa fa-info-circle\"></i></a>"
        //                + "<a href=\"\" ng-show=\"#=isPreorder#\" ng-click=\"viewPreOrder(this.dataItem)\" class=\"text-default\"><i ng-show=\"#=ReservationStatusID == Reserved() # && getDateTimeKendo(dataItem) > currentDateTime() \" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Preorder\" class=\"fa fa-file-text-o margin-left-5\"></i><i ng-hide=\"(#=ReservationStatusID == Reserved() # && getDateTimeKendo(dataItem) > currentDateTime())|| getItemPreoder(dataItem) == 0\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"View Preorder\" class=\"fa fa-eye margin-left-5\"></i></a>"
        //                + "<a href=\"\" ng-click=\"CancelReservationBlockModal(this.dataItem, 'r')\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Cancel\" class=\"text-danger margin-left-5\" ng-hide=\"#= ReservationStatusID == Cancelled() || ReservationStatusID == CheckOut1() #\"><i class=\"fa fa-ban\"></i></a>"
        //                + "<a href=\"\" ng-if=\"#= ReservationStatusID == Reserved() #\" ng-click=\"ViewDetailBookingReservation(this.dataItem);\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Detail\" class=\"margin-left-5\"><i ng-if=\"!checkDateReservation(dataItem.StartTime)\" class=\"fa fa-sign-in\"></i></a>"
        //                + "<a href=\"\" ng-if=\"#= ReservationStatusID == Seated() #\" ng-click=\"callCheckedOut(this.dataItem);\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Checked Out\" class=\"margin-left-5\"><i class=\"fa fa-sign-out\"></i></a>"
        //                + "<a href=\"\" ng-if=\"#=ReservationStatusID == Seated() #\" ng-click=\"callCancelCheckIn(this.dataItem);\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Cancel CheckIn\" class=\"margin-left-5\"><i class=\"fa fa-times-circle-o\"></i></a>#}#"
        //                + "# if(GuestName =='')"
        //                + "{# <a href=\"\" ng-click=\"updateUserBlock(this.dataItem)\" class=\"text-success margin-left-5\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Edit\"> <i class=\"fa fa-pencil-square-o\"></i></a>"
        //                + "<a href=\"\" ng-click=\"CancelReservationBlockModal(this.dataItem, 'b')\" tooltip-placement=\"top\" tooltip-class=\"hasTooltip\" tooltip=\"Cancel\" class=\"text-danger margin-left-5\" ng-hide=\"#=ReservationStatusName == Cancelled() #\"><i class=\"fa fa-ban\"></i></a>#}#"
        //        },
        //];
    }
})(angular.module('myApp'));