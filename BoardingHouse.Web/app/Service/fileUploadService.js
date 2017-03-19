(function (app) {
    'use strict';

    app.factory('fileUploadService', fileUploadService);

    fileUploadService.$inject = ['$rootScope', '$http', '$timeout', '$upload'];

    function fileUploadService($rootScope, $http, $timeout, $upload) {

        $rootScope.upload = [];

        var service = {
            uploadImage: uploadImage
        }

        function uploadImage($files, id) {
            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $rootScope.upload[index] = $upload.upload({
                        url: "http://localhost:6568/api/room/images/upload?id=" + id, // webapi url
                        method: "POST",
                        file: $file
                    }).then(function success(response) {
                        // file is uploaded successfully
                    }).catch(function onError(response) {
                    });
                })(i);
            }
        }
        return service;
    }

})(angular.module('myApp'));