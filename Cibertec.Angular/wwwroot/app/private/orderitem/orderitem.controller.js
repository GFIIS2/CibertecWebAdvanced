(function () {
    'use strict';

    angular.module('app').controller('orderitemController', orderitemController);

    orderitemController.$inject = ['dataService', 'configService', '$state'];

    function orderitemController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        vm.orderitem = {};
        vm.orderitemList = [];

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/orderitem')
                .then(function (result) {
                    vm.orderitemList = result.data;
                }, function (error) {
                    vm.orderitemList = [];
                    console.log(error);
                });
        }

    }

})();