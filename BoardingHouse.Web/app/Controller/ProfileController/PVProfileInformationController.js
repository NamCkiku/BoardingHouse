(function (app) {
    app.controller('PVProfileInformationController', PVProfileInformationController);

    PVProfileInformationController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', '$filter'];

    function PVProfileInformationController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, $filter) {

        angular.element(document).ready(function () {
        });
        $scope.changePassword = {};
        $scope.$on('fireLoadProfileInformationEvent', function (event, userInfo) {
            console.log(userInfo);
            var profileImage = null;
            $scope.prepareFiles = prepareFiles;
            function prepareFiles($files) {
                profileImage = $files;
                $scope.UserInfomation.Avatar = $files[0].name;
                console.log($scope.UserInfomation.Avatar);
            }
            $scope.UserInfomation = userInfo;
            $scope.SaveUpdateUser = SaveUpdateUser;
            function SaveUpdateUser() {
                BaseService.ValidatorForm("#UpdateUser");
                var frmAdd = angular.element(document.querySelector('#UpdateUser'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    var userUpdate = {
                        Email: $scope.UserInfomation.Email,
                        FullName: $scope.UserInfomation.FullName,
                        UserName: $scope.UserInfomation.UserName,
                        PhoneNumber: $scope.UserInfomation.PhoneNumber,
                        Avatar: $scope.UserInfomation.Avatar,
                        Address: $scope.UserInfomation.Address,
                        BirthDay: $scope.UserInfomation.BirthDay
                    }
                    apiService.post('Manage/ChangeUserInfo', true, userUpdate, function (respone) {
                        if (respone.data.success == true) {
                            $window.location.reload();
                        } else {
                            alert("Update không thành công");
                        }
                    }, function (respone) {
                        console.log('Load product failed.');
                    });
                }
            }

            $scope.ChangePassword = ChangePassword;
            function ChangePassword() {
                BaseService.ValidatorForm("#ChangePassword");
                var frmAdd = angular.element(document.querySelector('#ChangePassword'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    var model = {
                        OldPassword: $scope.changePassword.OldPassword,
                        NewPassword: $scope.changePassword.NewPassword,
                        ConfirmPassword: $scope.changePassword.ConfirmPassword,
                    }
                    apiService.post('Manage/ChangePassword', true, model, function (respone) {
                        if (respone.data.success == true) {
                            $window.location.reload();
                        } else {
                            alert("Đổi mật khẩu không thành công");
                        }
                    }, function (respone) {
                        console.log('Load product failed.');
                    });
                }
            }
        });
    }
})(angular.module('myApp'));