(function (app) {
    app.controller('CreateRoomController', CreateRoomController);

    CreateRoomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', 'fileUploadService'];

    function CreateRoomController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, fileUploadService) {
        $scope.isActive = '1';
        $scope.rooms = {
            MoreInfomations: {
            },
            Image: "No.jpg",
            Alias:"No"
        }
        $scope.account = {
            Email: "",
            Password: "",
        }
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: []
        }
        $scope.tinymceOptions = {
            height: 100,
            // menubar: false,
            plugins: [
              'advlist autolink lists link image charmap print preview anchor',
              'searchreplace visualblocks code fullscreen',
              'insertdatetime media table contextmenu paste code'
            ],
            toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            inline: false,
            skin: 'lightgray',
            theme: 'modern',
            menubar: false,
        };
        Dropzone.autoDiscover = false;
        $scope.dzOptions = {
            url: '/Home/SaveUploadedFile',
            paramName: 'photo',
            maxFilesize: 2, // MB
            maxFiles: 8,
            addRemoveLinks: true,
            dictDefaultMessage: 'Nhấn vào đây để thêm hoặc thả các bức ảnh',
            dictRemoveFile: 'Xóa Ảnh',
            dictResponseError: 'Không thể tải ảnh này',
            init: function () {
                this.on("maxfilesexceeded", function (file) {
                    this.removeFile(file);
                    //notificationService.displayError('Giới hạn số lượng hình ảnh là 8!');
                });
            },
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            autoProcessQueue: false,
        };
        $scope.dzCallbacks = {
            'addedfile': function (file) {
            },
            'thumbnail': function (file, dataUrl) {
            },
            'success': function (file, xhr) {
                console.log(file, xhr);
            },
        };
        $scope.dzMethods = {
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
        $scope.GetUserLogin = GetUserLogin;
        function GetUserLogin() {
            apiService.post('Home/GetUserLogin', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.rooms.Email = respone.data.user.Email;
                    $scope.rooms.UserName = respone.data.user.UserName;
                    $scope.rooms.Address = respone.data.user.Address;
                    $scope.rooms.PhoneNumber = respone.data.user.PhoneNumber;

                    console.log(respone.data.user);
                } else {
                    $scope.modalInstance = $modal.open({
                        animation: true,
                        templateUrl: 'ModalLogin.html',
                        backdrop: 'static',
                        windowClass: 'center-modal',
                        scope: $scope,
                        keyboard: false,
                        size: 'md'
                    });
                    $scope.ok = function () {
                        BaseService.ValidatorForm("#formLogin");
                        var frmAdd = angular.element(document.querySelector('#formLogin'));
                        var formValidation = frmAdd.data('formValidation').validate();
                        if (formValidation.isValid()) {
                            apiService.post('Account/Login', true, $scope.account, function (respone) {
                                if (respone.data.success == true) {
                                    $window.location.reload();
                                    $scope.modalInstance.dismiss('cancel');
                                } else {
                                    alert(respone.data.message);
                                }
                            }, function (respone) {
                                console.log('Load product failed.');
                            });
                        }
                    };
                    $scope.close = function () {
                        $scope.modalInstance.dismiss('cancel');
                    };
                    $scope.modalInstance.result.then(function (response) {

                    }, function () {
                    });
                }
            }, function (respone) {
                console.log('Load product failed.');
            });
        }


        var roomImage = null;
        $scope.prepareFiles = prepareFiles;
        function prepareFiles($files) {
            roomImage = $files;
        }
        $scope.nextStep = nextStep;
        function nextStep(item) {
            if (item == 1) {
                BaseService.ValidatorForm("#formStep1");
                var frmAdd = angular.element(document.querySelector('#formStep1'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    $scope.isActive = '2';
                }
            }
            else if (item == 2) {
                BaseService.ValidatorForm("#formStep2");
                var frmAdd = angular.element(document.querySelector('#formStep2'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    apiService.post('Management/CreateRoom', true, $scope.rooms, function (respone) {
                        if (respone.data.success == true) {
                            if (roomImage) {
                                fileUploadService.uploadImage(roomImage, 1003);
                            }
                            $scope.isActive = '3';
                        } else {
                        }
                    }, function (respone) {
                        console.log('Load product failed.');
                    });
                }
            }
            else {
                $scope.isActive = '4';
            }

        }
        $scope.previousStep = previousStep;
        function previousStep(item) {
            if (item == 1) {
                //BaseService.ValidatorForm("#formStep1");
                //var frmAdd = angular.element(document.querySelector('#formStep1'));
                //var formValidation = frmAdd.data('formValidation').validate();
                //if (formValidation.isValid()) {

                //}
                $scope.isActive = '1';
            }
            else if (item == 2) {
                $scope.isActive = '2';
            }
            else {
                $scope.isActive = '3';
            }

        }
        function load() {
            GetAllProvince();
            GetAllRoomType();
        }
        load();
    }
})(angular.module('myApp'));