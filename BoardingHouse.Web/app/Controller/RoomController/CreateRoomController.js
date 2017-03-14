(function (app) {
    app.controller('CreateRoomController', CreateRoomController);

    CreateRoomController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService'];

    function CreateRoomController($scope, blockUI, $modal, $rootScope, BaseService) {
        $scope.isActive = '1';
        $scope.tinymceOptions = {
            height: 100,
            // menubar: false,
            plugins: [
              'advlist autolink lists link image charmap print preview anchor',
              'searchreplace visualblocks code fullscreen',
              'insertdatetime media table contextmenu paste code'
            ],
            toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            inline: false,
            skin: 'lightgray',
            theme: 'modern',
            menubar: false,
        };
        Dropzone.autoDiscover = false;
        $scope.dzOptions = {
            url: '/Home/SaveUploadedFile',
            paramName: 'photo',
            maxFilesize: 2, // MB
            maxFiles: 8,
            addRemoveLinks: true,
            dictDefaultMessage: 'Nhấn vào đây để thêm hoặc thả các bức ảnh',
            dictRemoveFile: 'Xóa Ảnh',
            dictResponseError: 'Không thể tải ảnh này',
            init: function () {
                this.on("maxfilesexceeded", function (file) {
                    this.removeFile(file);
                    //notificationService.displayError('Giới hạn số lượng hình ảnh là 8!');
                });
            },
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            autoProcessQueue: false,
        };
        $scope.dzCallbacks = {
            'addedfile': function (file) {
            },
            'thumbnail': function (file, dataUrl) {
            },
            'success': function (file, xhr) {
                console.log(file, xhr);
            },
        };
        $scope.dzMethods = {
        };


        $scope.nextStep = nextStep;
        function nextStep() {
            BaseService.ValidatorForm("#formStep1");
            var frmAdd = angular.element(document.querySelector('#formStep1'));
            var formValidation = frmAdd.data('formValidation').validate();
            if (formValidation.isValid()) {
                $scope.isActive = '2';
            }
        }

    }
})(angular.module('myApp'));