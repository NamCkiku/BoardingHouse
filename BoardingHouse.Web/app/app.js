var app;
(function () {
    app = angular.module("myApp", ['blockUI', 'ui.bootstrap', 'ngAnimate', 'ngSanitize', 'rzModule', 'ui.tinymce', 'thatisuday.dropzone']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = $('baseurl').attr('value');
        $rootScope.baseUrl = baseUrl;
    }]);
})();