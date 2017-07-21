(function () {

    'use strict'
    angular.module('app').controller('supplierController', supplierController);
    supplierController.$inject = ['dataService', 'configService', '$state'];

    function supplierController(dataService, configService, $state) {

        var apiUrl = configService.getApiUrl();
        var vm = this;

        vm.supplier = {};
        vm.supplierList = [];
        vm.modalTitle = "";
        vm.modalButtonTitle = "";
        vm.readOnly = "";
        vm.isDelete = "";

        vm.edit = edit;
        vm.getSupplier = getSupplier;
        vm.create = create;
        vm.delete = del;
        vm.showError = false;

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + "/supplier/list").then(function (result) {
                vm.supplierList = result.data;
            },
            function (error) {
                vm.supplierList = [];
                console.log(error);
            });
        }

        function getSupplier(id) {
            vm.supplier = null;
            dataService.getData(apiUrl + "/supplier/" + id).then(function (result) {
                console.log(result);
                vm.supplier = result.data;
            }, function (error) {
                console.log(error);
            });
        }

        function edit() {
            vm.showError = false;
            vm.modalTitle = "Edit supplier";
            vm.modalButtonTitle = "Edit";
            vm.readOnly = false;
            vm.isDelete = false;
            vm.modalFunction = updateSupplier;
        }

        function updateSupplier() {
            if (!vm.supplier) return;
            dataService.postData(apiUrl + "/supplier/Put", vm.supplier).then(
                function (result) {
                    vm.supplier = {};
                    list();
                    closeModal();
                }, function (error) {
                    console.log(error);
                });
        }

        function create() {
            vm.showError = false;
            vm.supplier = {};
            vm.modalTitle = "Create Supplier";
            vm.modalButtonTitle = 'Create';
            vm.readOnly = false;
            vm.isDelete = false;
            vm.modalFunction = createSupplier;
        }

        function createSupplier() {
            if (!vm.supplier.companyName) { vm.showError = true; closeModal(); return; }
            else {
                dataService.postData(apiUrl + "/supplier", vm.supplier).then(
                    function (result) {
                        list();
                        closeModal();
                    },
                    function (error) {
                        console.log(error);
                    });
            }
        }

        function del() {
            vm.showError = false;
            vm.modalTitle = "Delete supplier";
            vm.modalButtonTitle = "Delete";
            vm.readOnly = true;
            vm.isDelete = true;
            vm.modalFunction = deleteSupplier;
        }

        function deleteSupplier() {
            if (!vm.supplier) return;
            console.log("IdSupllier: " + vm.supplier.id);
            dataService.postData(apiUrl + "/supplier/Delete/" + vm.supplier.id).then(function (result) {
                vm.supplier = {};
                list();
                closeModal();
            }, function (error) {
                console.log(error);
            });
        }

        function closeModal() {
            angular.element('#modal-container').modal('hide');
        }
    }
})();