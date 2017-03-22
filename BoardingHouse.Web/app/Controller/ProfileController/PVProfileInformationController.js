(function (app) {
    app.controller('PVProfileInformationController', PVProfileInformationController);

    PVProfileInformationController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window'];

    function PVProfileInformationController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window) {

        angular.element(document).ready(function () {
        });
        $scope.$on('fireLoadProfileInformationEvent', function (event, userInfo) {
            console.log(userInfo);
            $scope.UserInfomation = userInfo;
            $scope.SaveUpdateUser = SaveUpdateUser;
            function SaveUpdateUser() {
                BaseService.ValidatorForm("#UpdateUser");
                var frmAdd = angular.element(document.querySelector('#UpdateUser'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    var userUpdate = {
                        FullName: $scope.UserInfomation.FullName,
                        UserName: $scope.UserInfomation.UserName,
                        PhoneNumber: $scope.UserInfomation.PhoneNumber
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
        });
    }
})(angular.module('myApp'));