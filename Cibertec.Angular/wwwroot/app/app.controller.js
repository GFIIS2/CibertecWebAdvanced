(function () {
    'use strict';

    angular.module('app').controller('applicationController', applicationController);

    applicationController.$inject = ['$state', '$scope', 'configService', 'authenticationService', 'localStorageService'];

    function applicationController($state, $scope, configService, authenticationService, localStorageService) {
        var vm = this;
        vm.validate = validate;
        vm.logout = logout;
        vm.product = product;
        vm.order = order;
        vm.orderItem = orderItem;
        vm.customer = customer;
        vm.supplier = supplier;
        vm.csv = csv;

        $scope.init = function (url) {
            configService.setApiUrl(url);
        }

        function validate() {
            return configService.getLogin();
        }

        function logout() {
            authenticationService.logout();
        }

        function product() {
            $state.go("product");
        }

        function order() {
            $state.go("order");
        }

        function orderItem() {
            $state.go("orderitem");
        }

        function customer() {
            $state.go("customer");
        }

        function supplier() {
            $state.go("supplier");
        }

        function csv() {
            $state.go("csv");
        }

    }


})();