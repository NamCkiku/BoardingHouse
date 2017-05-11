var app;
(function () {
    app = angular.module("myApp", ['blockUI', 'ui.bootstrap', 'ngAnimate', 'ngSanitize', 'rzModule', 'ui.tinymce', 'thatisuday.dropzone', 'angularFileUpload', 'angularjs-dropdown-multiselect', 'ui.select2', 'ngtimeago']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = $('baseurl').attr('value');
        $rootScope.baseUrl = baseUrl;
    }]);
    app.filter("formatDate", ['$log', '$filter', '$rootScope', function ($log, $filter, $rootScope) {
        return function (sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')('dd/MM/yyyy'));
            } else {
                return "";
            }
            return $filter('jsDate')(sDate, $filter('uppercase')('dd/MM/yyyy'));

        };
    }]);
    app.filter("jsDate", function ($log) {
        return function (x, formatDate) {
            return moment(x).format(formatDate);

        };
    });
})();