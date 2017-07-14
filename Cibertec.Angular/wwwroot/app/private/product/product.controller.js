(function () {
    'use strict';
    angular.module('app')
        .controller('productController', productController);

    productController.$inject = ['dataService', 'configService', '$state'];


    function productController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        vm.product = {};
        vm.productList = [];
        
        init();

        function init() {
            if (!configService.getLogin()) return $state.go("login");
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/product')
                .then(
                function (result) {
                    vm.productList = result.data;                        
                }, function (error) {
                    vm.productList = [];
                    console.log(error);
                });            
        }
    }
})();