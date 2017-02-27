var app;
(function () {
    app = angular.module("myApp", ['kendo.directives', 'frapontillo.bootstrap-switch', 'blockUI', 'ui.bootstrap', 'ngAnimate', 'ngSanitize']);
    //app.filter("jsDate", function ($log) {
    //    return function (x, formatDate) {
    //        return moment(x).format(formatDate);

    //    };
    //});
    //app.filter("formatDate", ['$log', '$filter', '$rootScope', function ($log, $filter, $rootScope) {
    //    return function (sDate) {
    //        if (sDate != "" && sDate != undefined) {
    //            return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat));
    //        } else {
    //            return "";
    //        }
    //        return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat));

    //    };
    //}]);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = $('baseurl').attr('value');
        $rootScope.baseUrl = baseUrl;
    }]);
    app.constant('ENUMS',
    {
        ModalType: {
            Error: 1,
            Warning: 2,
            Confirmation: 3,
            Information: 4
        },
    });
})();