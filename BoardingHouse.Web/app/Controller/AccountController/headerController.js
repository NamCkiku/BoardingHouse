(function (app) {
    app.controller('headerController', headerController);

    headerController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope'];

    function headerController($scope, blockUI, $modal, $rootScope) {
        $scope.openLoginModal = openLoginModal;
        function openLoginModal() {
            $scope.modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalLogin.html',
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