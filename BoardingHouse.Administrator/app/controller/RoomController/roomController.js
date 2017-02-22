(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope'];

    function roomController($scope) {
        $scope.filter = {
            Keywords: "",
            StartDate: "",
            EndDate: "",
            searchByStartDate: true,
            searchByEndDate: true,
            Status: true
        }
    }

})(angular.module('myApp'));