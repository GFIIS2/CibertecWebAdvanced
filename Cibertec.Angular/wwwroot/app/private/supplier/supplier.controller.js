(function () {
    'use strict';

    angular.module('app').controller('supplierController', supplierController);

    supplierController.$inject = ['dataService', 'configService', '$state'];

    function supplierController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        vm.supplier = {};
        vm.supplierList = [];

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/supplier')
                .then(function (result) {
                    vm.supplierList = result.data;
                }, function (error) {
                    vm.supplierList = [];
                    console.log(error);
                });
        }

    }

})();