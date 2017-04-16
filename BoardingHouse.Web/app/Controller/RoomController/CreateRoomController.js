(function (app) {
    app.controller('CreateRoomController', CreateRoomController);

    CreateRoomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', 'fileUploadService', 'commonService'];

    function CreateRoomController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, fileUploadService, commonService) {
        $scope.isActive = '1';
        $scope.rooms = {
            MoreInfomations: {
            },
            Image: "No.jpg",
            Lat: 21.0029317912212212,
            Lng: 105.820226663232323,
        }
        $scope.LstConvenient = [
            { id: "Chỗ để xe", label: "Chỗ để xe" },
            { id: "Sân phơi", label: "Sân phơi" },
            { id: "Thang máy", label: "Thang máy" },
            { id: "Internet", label: "Internet" },
            { id: "Điều hòa", label: "Điều hòa" },
            { id: "Bình nóng lạnh", label: "Bình nóng lạnh" },
            { id: "Máy giặt", label: "Máy giặt" },
            { id: "Truyền hình cáp", label: "Truyền hình cáp" },
            { id: "Tivi", label: "Tivi" },
        ];
        $scope.ConvenientModel = [];
        $scope.MultiselectSettings = {
            scrollable: true,
            scrollableHeight: '220px',
            displayProp: 'label',
            idDrop: 'label',
            checkAll: 'Chọn tiện ích'
        };
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
        $(document).bind("location_changed", function (event, object) {
            updateLatLng($(this));
        });
        function updateLatLng(object) {
            $scope.rooms.Lat = $(object).find('.gllpLatitude').val();
            $scope.rooms.Lng = $(object).find('.gllpLongitude').val();
        }
        Dropzone.autoDiscover = false;
        $scope.dzOptions = {
            url: 'http://localhost:6568/api/room/images/uploadFile',
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
                    BaseService.displayError('Giới hạn số lượng hình ảnh là 8!');
                });
            },
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            autoProcessQueue: true,
        };
        $scope.dzCallbacks = {
            'addedfile': function (file) {
                $scope.rooms.MoreImages = [];
            },
            'thumbnail': function (file, dataUrl) {
                $scope.rooms.MoreImages.push(file.name);
            },
            'success': function (file, xhr) {
                console.log(file, xhr);
            },
        };
        $scope.dzMethods = {
        };
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.rooms.Alias = commonService.getSeoTitle($scope.rooms.RoomName);
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
        $scope.GetUserLogin = GetUserLogin;
        function GetUserLogin() {
            apiService.post('Home/GetUserLogin', true, null, function (respone) {
                if (respone.data.success == true) {
                    $('#formStep1').data('formValidation').resetForm();
                    $scope.rooms.Email = respone.data.user.Email;
                    $scope.rooms.FullName = respone.data.user.UserName;
                    $scope.rooms.Address = respone.data.user.Address;
                    $scope.rooms.PhoneNumber = respone.data.user.PhoneNumber;
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
                    $scope.isActive = '3';
                }
            }
            else {
                if ($scope.rooms.MoreImages != null) {
                    $scope.rooms.MoreImages = JSON.stringify($scope.rooms.MoreImages)
                }
                $scope.rooms.MoreInfomations.Convenient = '';
                angular.forEach($scope.ConvenientModel, function (value, key) {
                    $scope.rooms.MoreInfomations.Convenient += value.label + ",";
                })
                $scope.rooms.MoreInfomations.Convenient = JSON.stringify($scope.rooms.MoreInfomations.Convenient)
                apiService.post('Management/CreateRoom', true, $scope.rooms, function (respone) {
                    if (respone.data.success == true) {
                        if (roomImage) {
                            if (respone.data.objData.RoomID != null) {
                                fileUploadService.uploadImage(roomImage, respone.data.objData.RoomID);
                            }
                        }
                        $scope.roomInfomation = respone.data.objData;
                        $scope.isActive = '4';
                        BaseService.displaySuccess("Chúc mừng bạn đã đăng tin thành công", 5000);
                    } else {
                        BaseService.displayError("Đăng tin không thành công bạn vui lòng kiểm tra lại thành công", 5000);
                    }
                }, function (respone) {
                    console.log('Load product failed.');
                });

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