(function (app) {
    app.controller('headerController', headerController);

    headerController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window'];

    function headerController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window) {
        $scope.account = {
            Email: "",
            Password: "",
        }
        $scope.accountRegister = {
            Email: "",
            Password: "",
            ConfirmPassword: ""
        }
        $scope.openLoginModal = openLoginModal;
        function openLoginModal() {
            //$scope.modalInstanceRegister.dismiss('cancel');
            $scope.modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalLogin.html',
                backdrop: 'static',
                windowClass: 'center-modal',
                scope: $scope,
                keyboard: false,
                size: 'sm'
            });
            $scope.ok = function () {
                BaseService.ValidatorForm("#formLogin");
                var frmAdd = angular.element(document.querySelector('#formLogin'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    apiService.post('Account/Login', true, $scope.account, function (respone) {
                        if (respone.data.success == true) {
                            $window.location.reload();
                            $scope.modalInstance.close();
                        } else {
                            $scope.messageError = respone.data.message;
                        }
                    }, function (respone) {
                        console.log('Load product failed.');
                    });
                }
            };

            $scope.LoginWithFacebook = function () {
                var data = {
                    provider: 'Google',
                }
                apiService.post('Account/ExternalLogin', false, data, function (respone) {
                    if (respone.data.success == true) {
                        $window.location.reload();
                        $scope.modalInstanceRegister.close();
                    } else {
                        $scope.messageRegisterError = respone.data.message;
                    }
                }, function (respone) {
                    console.log('Load product failed.');
                });
            }
            $scope.showRegisterModal = function () {
                $scope.modalInstance.dismiss('cancel');
                $scope.openRegisterModal();
            }
            $scope.close = function () {
                $scope.modalInstance.dismiss('cancel');
            };
            $scope.modalInstance.result.then(function (response) {

            }, function () {
            });
        }
        $scope.openRegisterModal = openRegisterModal;
        function openRegisterModal() {
            $scope.modalInstanceRegister = $modal.open({
                animation: true,
                templateUrl: 'ModalRegister.html',
                backdrop: 'static',
                windowClass: 'center-modal',
                scope: $scope,
                keyboard: false,
                size: 'sm'
            });
            $scope.ok = function () {
                BaseService.ValidatorForm("#formRegister");
                var frmAdd = angular.element(document.querySelector('#formRegister'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    apiService.post('Account/Register', true, $scope.accountRegister, function (respone) {
                        if (respone.data.success == true) {
                            $window.location.reload();
                            $scope.modalInstanceRegister.close();
                        } else {
                            $scope.messageRegisterError = respone.data.message;
                        }
                    }, function (respone) {
                        console.log('Load product failed.');
                    });
                }
            };
            $scope.showLoginModal = function () {
                $scope.modalInstanceRegister.dismiss('cancel');
                $scope.openLoginModal();
            }
            $scope.close = function () {
                $scope.modalInstanceRegister.dismiss('cancel');
            };
            $scope.modalInstanceRegister.result.then(function (response) {

            }, function () {
            });
        }



        $scope.logOut = logOut;
        function logOut() {
            apiService.post('Account/LogOff', true, null, function (respone) {
                if (respone.data.success == true) {
                    $window.location.reload();
                    $scope.modalInstance.dismiss('cancel');
                } else {
                    alert("Log Out Không thành công");
                }
            }, function (respone) {
                console.log('Load product failed.');
            });
        }
    }
})(angular.module('myApp'));