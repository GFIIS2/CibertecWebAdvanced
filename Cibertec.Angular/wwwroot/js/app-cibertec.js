(function () {
    angular
        .module('app')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$state', 'localStorageService', 'configService', '$q'];

    function authenticationService($http, $state, localStorageService, configService,$q) {        
        var service = {};
        service.login = login;
        service.logout = logout;
        return service;

        function login(user) {

            var defer = $q.defer();
            var url = configService.getApiUrl() + '/Token';
            var data = "username=" + user.userName + "&password=" + user.password;

            $http.post(url, data, {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            }).then(function (result) {
                $http.defaults.headers.common.Authorization = 'Bearer ' + result.data.access_token;
                localStorageService.set('userToken', {
                    token: result.data.access_token,
                    userName: user.userName
                });
                configService.setLogin(true);
                defer.resolve(true);
                }, function error(response) {
                    defer.reject(false);
                });

            return defer.promise;

        }

        function logout() {
            $http.defaults.headers.common.Authorization = '';
            localStorageService.remove('userToken');
            configService.setLogin(false);            
        }

    }
})();
(function () {
    angular
        .module('app')
        .factory('dataService', dataService);

    dataService.$inject = ['$http'];

    function dataService($http) {
        var service = {};
        service.getData = getData;
        service.postData = postData;
        service.putData = putData;
        service.deleteData = deleteData;

        return service;

        function getData(url) {
            return $http.get(url);
        }
        function postData(url, data) {
            return $http.post(url, data);
        }
        function putData(url, data) {
            return $http.put(url, data);
        }

        function deleteData(url, data) {
            return $http.delete(url, data);
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('app')
        .factory('configService', configService);

    function configService() {
        var service = {};
        var apiUrl = undefined;
        var isLogged = false;
        service.setLogin = setLogin;
        service.getLogin = getLogin;
        service.setApiUrl = setApiUrl;
        service.getApiUrl = getApiUrl;


        return service;

        function setLogin(state) {
            isLogged = state;
        }

        function getLogin() {
            return isLogged;
        }

        function getApiUrl() {
            return apiUrl;
        }

        function setApiUrl(url) {
            apiUrl = url;
        }
    }
})();

(function () {
    angular.module('app').directive('modalPanel', modalPanel);

    function modalPanel() {
        return {
            templateUrl: 'app/components/modal/modal-directive.html',
            restrict: 'E',
            transclude: true,
            scope: {
                title: '@',
                buttonTitle: '@',
                saveFunction: '=',
                closeFunction: '=',
                readOnly: '=',
                isDelete: '='
            }
        };
    }

})();
(function () {
    'use strict';

    angular.module('app').controller('loginController', loginController);

    loginController.$inject = ['$http', 'authenticationService', 'configService', '$state'];

    function loginController($http, authenticationService, configService, $state) {
        var vm = this;
        vm.user = {};
        vm.title = 'Login';
        vm.login = login;
        vm.showError = false;

        init();

        function init() {
            if (configService.getLogin()) $state.go("product");
            authenticationService.logout();
        }

        function login() {
            authenticationService.login(vm.user).then(function (result) {
                vm.showError = false;
                $state.go("home");
            }, function (error) {
                vm.showError = true;
            });
        }

    }

})();
(function () {
    'use strict';

    angular.module('app').controller('csvController', loginController);

    function loginController(configService, $state) {
        var vm = this;
        vm.csvLines = [];
        vm.processFile = processFile;

        var fileInput = document.getElementById("csvViewer");

        init();

        function init() {
            if (!configService.getLogin()) $state.go('login');

            //fileInput.addEventListener('change', readFile);
        }

        function processFile() {
            vm.csvLines = [];

            readFile(function (result) {
                var list = [];
                var totalLines = result.length;
                var count = 0;
                var csvWorker = new Worker("/js/worker.js");

                csvWorker.addEventListener('message', function (message) {                    
                    list.push(message.data);
                    console.log('Processing...');
                    count++;
                    if (count >= totalLines) csvWorker.terminate();
                });

                for (var i = 0; i < result.length; i++) {
                    csvWorker.postMessage(result[i]);
                }                
                //csvWorker.postMessage();
            });
        }

        function readFile(callback) {
            var reader = new FileReader();
            var list = [];


            reader.onload = function () {
                return callback(reader.result.split("\r\n"));
            };
            /*
            reader.onload = function () {
                var line = reader.result.split("\r\n");
                for (var i = 0; i < lines.length; i++){
                    list.push(formatLine(lines[i]));
                    console.log('Processed Line');
                }                
                vm.csvLines.push(list);
            };
            */

            reader.readAsBinaryString(fileInput.files[0]);
        }

        function formatLine() {

        }
    }

})();
(function () {
    'use strict';

    angular.module('app').controller('customerController', customerController);

    customerController.$inject = ['dataService', 'configService', '$state'];

    function customerController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        vm.customer = {};
        vm.customerList = [];

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/customer/list')
                .then(function (result) {
                    vm.customerList = result.data;
                }, function (error) {
                    vm.customerList = [];
                    console.log(error);
                });
        }

    }

})();
(function () {
    'use strict';

    angular.module('app').directive('customerCard', customerCard);

    function customerCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                id: '@',
                firstName: '@',
                lastName: '@',
                city: '@',
                country: '@',
                phone: '@'
            },
            templateUrl: 'app/private/customer/directives/customer-card/customer-card.html',
            controller: directiveController
        };
    }

    function directiveController() {

    }

})();
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
(function () {
    'use strict';

    angular.module('app').directive('orderCard', orderCard);

    function orderCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                id: '@',
                orderDate: '@',
                orderNumber: '@',
                customerId: '@',
                totalAmount: '@'
            },
            templateUrl: 'app/private/order/directives/order-card/order-card.html',
            controller: directiveController
        };
    }

    function directiveController() {

    }

})();
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
(function () {
    'use strict';

    angular.module('app').directive('orderitemCard', orderitemCard);

    function orderitemCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                id: '@',
                orderId: '@',
                productId: '@',
                unitPrice: '@',
                quantity: '@'
            },
            templateUrl: 'app/private/orderitem/directives/orderitem-card/orderitem-card.html',
            controller: directiveController
        };
    }

    function directiveController() {

    }

})();
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
(function () {
    'use strict';

    angular.module('app').directive('productCard', productCard);

    function productCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                id: '@',
                productName: '@',
                supplierId: '@',
                unitPrice: '@',
                package: '@',
                isDiscontinued: '='
            },
            templateUrl: 'app/private/product/directives/product-card/product-card.html',
            controller: directiveController
        };
    }

    function directiveController() {

    }
})();
(function () {
    'use strict';

    angular.module('app').directive('productForm', productForm);

    function productForm() {
        return {
            restrict: 'E',
            scope: {
                product: '='
            },
            templateUrl: 'app/private/product/directives/product-form/product-form.html'
        };        
    }


})();
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
(function () {
    'use strict';

    angular.module('app').directive('supplierCard', supplierCard);

    function supplierCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                id: '@',
                companyName: '@',
                contactName: '@',
                contactTitle: '@',
                city: '@',
                country: '@',
                phone: '@',
                fax: '@'
            },
            templateUrl: 'app/private/supplier/directives/supplier-card/supplier-card.html',
            controller: directiveController
        };
    }

    function directiveController() {

    }

})();
(function (undefined) {

    'use strict';

    angular.module('app').directive('supplierForm', supplierForm);

    function supplierForm() {
        return {
            restrict: 'E',
            scope: {
                supplier: '='
            },
            templateUrl: 'app/private/supplier/directives/supplier-form/supplier-form.html'
        };
    }

})();