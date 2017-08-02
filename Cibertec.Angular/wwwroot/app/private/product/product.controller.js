(function () {
    'use strict';

    angular.module('app').controller('productController', productController);

    productController.$inject = ['dataService', 'configService', '$state'];

    function productController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        //Propiedades
        vm.product = {};
        vm.productList = [];
        vm.modalButtonTitle = '';
        vm.readOnly = false;
        vm.isDelete = false;
        vm.modalTitle = '';
        vm.showCreate = false;

        vm.totalRecords = 0;
        vm.itemsPerPage = 25;
        vm.currentPage = 1;
        vm.maxSize = 10;

        //Funciones
        vm.getProduct = getProduct;
        vm.create = create;
        vm.edit = edit;
        vm.delete = productDelete;
        vm.list = list;
        vm.pageChanged = pageChanged;

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            configurePagination();
        }

        function configurePagination() {
            var widthScreen = (window.innerWidth > 0) ? window.innerWidth : screen.width;
            if (widthScreen < 420) vm.maxSize = 5;
            getTotalRecords();
        }

        function getTotalRecords() {
            dataService.getData(apiUrl + '/product/count')
                .then(function (result) {
                    vm.totalRecords = result.data;
                    list();
                }, function (error) {
                    vm.totalRecords = 0;
                    console.log(error);
                });
        }

        function list() {
            dataService.getData(apiUrl + '/product/' + vm.currentPage + '/' + vm.itemsPerPage)
                .then(function (result) {
                    vm.productList = result.data;
                }, function (error) {
                    vm.productList = [];
                    console.log(error);
                });
        }

        function getProduct(id) {
            vm.product = null;
            dataService.getData(apiUrl + '/product/' + id)
                .then(function (result) {
                    vm.product = result.data;
                }, function (error) {
                    vm.product = null;
                    console.log(error);
                });
        }

        function updateProduct() {
            if (!vm.product) return;

            dataService.putData(apiUrl + '/product', vm.product)
                .then(function (result) {
                    vm.product = {};
                    list();
                    closeModal();
                }, function (error) {
                    vm.product = {};
                    console.log(error);
                });
        }


        function createProduct() {
            if (!vm.product) return;

            dataService.postData(apiUrl + '/product', vm.product)
                .then(function (result) {
                    //getProduct(result.data.id)
                    vm.currentPage = 1;
                    pageChanged();
                    vm.showCreate = true;
                    closeModal();
                }, function (error) {          
                    console.log(error);
                });
        }

        function deleteProduct() {
            dataService.deleteData(apiUrl + '/product/' + vm.product.id)
                .then(function (result) { 
                    list();
                    closeModal();
                }, function (error) {
                    console.log(error);
                });
        }

        function create() {
            vm.product = {};
            vm.modalTitle = 'New product';
            vm.modalButtonTitle = 'Create';
            vm.readOnly = false;
            vm.modalFunction = createProduct;
            vm.isDelete = false;
        }

        function edit() {
            vm.showCreate = false;
            vm.modalTitle = 'Edit Product';
            vm.modalButtonTitle = 'Update';
            vm.readOnly = false;
            vm.modalFunction = updateProduct;
            vm.isDelete = false;
        }

        function detail() {
            vm.modalTitle = 'Created Product';
            vm.modalButtonTitle = '';
            vm.readOnly = true;
            vm.modalFunction = null;
            vm.isDelete = false;
        }

        function productDelete() {
            vm.showCreate = false;

            vm.modalTitle = 'Delete Product';
            vm.modalButtonTitle = 'Delete';
            vm.readOnly = false;
            vm.modalFunction = deleteProduct;
            vm.isDelete = true;
        }

        function closeModal() {
            angular.element('#modal-container').modal('hide');
        }

        function pageChanged() {       
            list();
        }

    }

})();