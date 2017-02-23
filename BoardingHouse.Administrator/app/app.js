var app;
(function () {
    app = angular.module("myApp", ['kendo.directives', 'frapontillo.bootstrap-switch', 'blockUI', 'ui.bootstrap', 'ngAnimate']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = $('baseurl').attr('value');
        $rootScope.baseUrl = baseUrl;
    }]);
})();