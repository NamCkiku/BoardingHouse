(function (app) {
    var BaseService = function ($rootScope, $http, $q, $filter) {
        function postData(controller, action, isValidateToken, data) {
            var result = $q.defer();
            if (isValidateToken) {
                $http({
                    method: 'POST',
                    url: $rootScope.baseUrl + controller + "/" + action,
                    data: data,
                    headers: {
                        'RequestVerificationToken': $(':input:hidden[id*="antiForgeryToken"]').val()
                    }
                })
                .success(function (response) {
                    result.resolve(response);
                })
                .error(function (response) {
                    result.reject(response);
                });


            } else {
                $http({
                    method: 'POST',
                    url: $rootScope.baseUrl + controller + "/" + action,
                    data: data

                })
                .success(function (response) {
                    result.resolve(response);
                })
                .error(function (response) {
                    result.reject(response);
                });
            }
            return result.promise;
        }


        function ValidatorForm(form) {
            $(form).formValidation({
                framework: 'bootstrap',
                icon: {

                },
                excluded: ':disabled',
                fields: {

                },
            })
            .off('success.form.fv')
            .on('success.form.fv', function (e) {
                var $form = $(e.target),
                fv = $(e.target).data('formValidation');
                fv.defaultSubmit();

            })
            .on('err.field.fv', function (e, data) {
                if (data.fv.getSubmitButton()) {
                    data.fv.disableSubmitButtons(false);
                }
            })
            .on('success.field.fv', function (e, data) {
                if (data.fv.getSubmitButton()) {
                    data.fv.disableSubmitButtons(false);
                }
            });
        };

        function formatDate(sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat));
            } else {
                return "";
            }
        }

        function formatMonth(sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeMonthFormat));
            } else {
                return "";
            }
        }


        function formatFullDateTime(sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat) + ' HH:mm');
            } else {
                return "";
            }

        }

        function canculateAgeByDOB(dateString) {
            var today = new Date();
            var birthDate = new Date(dateString);
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }

            return age;
        }


        return {
            postData: postData,
            ValidatorForm: ValidatorForm,
            canculateAgeByDOB: canculateAgeByDOB,
            formatDate: formatDate,
            formatMonth: formatMonth,
            formatFullDateTime: formatFullDateTime
        };
    }
    BaseService.$inject = ['$rootScope', '$http', '$q', '$filter'];
    app.service('BaseService', BaseService);
})(angular.module('myApp'));