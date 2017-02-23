(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope'];

    function roomController($scope, blockUI, $modal, $rootScope) {
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
            $scope.ok = function () {
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
    }
})(angular.module('myApp'));