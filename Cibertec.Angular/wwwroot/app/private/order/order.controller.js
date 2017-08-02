(function () {
    'use strict';

    angular.module('app').controller('orderController', orderController);

    orderController.$inject = ['dataService', 'configService', '$state'];

    function orderController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        vm.order = {};
        vm.orderList = [];

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/order')
                .then(function (result) {
                    vm.orderList = result.data;
                }, function (error) {
                    vm.orderList = [];
                    console.log(error);
                });
        }

    }

})();