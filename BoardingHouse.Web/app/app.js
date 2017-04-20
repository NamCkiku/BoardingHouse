﻿var app;
(function () {
    app = angular.module("myApp", ['blockUI', 'ui.bootstrap', 'ngAnimate', 'ngSanitize', 'rzModule', 'ui.tinymce', 'thatisuday.dropzone', 'angularFileUpload', 'angularjs-dropdown-multiselect', 'ui.select2']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = $('baseurl').attr('value');
        $rootScope.baseUrl = baseUrl;
    }]);
})();