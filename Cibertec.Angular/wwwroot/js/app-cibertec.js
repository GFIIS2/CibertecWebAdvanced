(function () {
    angular
        .module('app')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$state', 'localStorageService', 'configService','$q'];

    function authenticationService($http, $state, localStorageService, configService, $q) {
        var service = {};
        service.login = login;
        service.logout = logout;
        return service;

        function login(user) {
            var defer = $q.defer();
            var url = configService.getApiUrl() + '/Token';
            var data = "username=" + user.userName + "&password=" + user.password;
            $http.post(url,
                data,
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                })
                .then(function (result) {
                    $http.defaults.headers.common.Authorization = 'Bearer ' + result.data.access_token;
                    localStorageService.set('userToken',
                        {
                            token: result.data.access_token,
                            userName: user.userName
                        });
                    configService.setLogin(true);
                    defer.resolve(true);
                    //$state.go("home");
                },
                function error(response) {
                    defer.reject(true);
                    //$state.go("login");
                }
                );

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
        function deleteData(url) {
            return $http.delete(url);
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
    angular.module('app')
        .directive('modalPanel', modalPanel);

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
    angular.module('app')
        .controller('loginController', loginController);

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
            authenticationService.login(vm.user).then(
                function (result) {
                    vm.showError = false;
                    $state.go("home");
                }, function (error) {
                    vm.showError = true;
                }
            );
        }
    }
}
)();
(function () {
    'use strict';
    angular.module('app')
        .controller('productController', productController);

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

        //Funciones
        vm.getProduct = getProduct;
        vm.create = create;
        vm.edit = edit;
        vm.delete = productDelete;
        //vm.closeModal = closeModal;

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/product')
                .then(function (result) {
                    vm.productList = result.data;
                },
                function (error) {
                    vm.productList = [];
                    console.log(error);
                });
        }

        function getProduct(id) {
            vm.product = null;
            dataService.getData(apiUrl + '/product/' + id)
                .then(function (result) {
                    vm.product = result.data;                    
                },
                function (error) {
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
                },
                function (error) {
                    vm.product = {};
                    console.log(error);
                });
        }

        function createProduct() {
            if (!vm.product) return;
            dataService.postData(apiUrl + '/product', vm.product)
                .then(function (result) {
                    getProduct(result.data.id);
                    list();
                    vm.showCreate = true;
                },
                function (error) {                    
                    console.log(error);
                });
        }

        function deleteProduct() {
            dataService.deleteData(apiUrl + '/product/'+ vm.product.id)
                .then(function (result) {
                    list();
                    closeModal();
                },
                function (error) {                    
                    console.log(error);
                });
        }
        
        function create() {
            vm.product = {};
            vm.modalTitle = 'New Product';
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
    }
})();
(function () {
    'use strict';
    angular.module('app')
        .directive('productCard', productCard);

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
    angular.module('app')
    .directive('productForm', productForm);

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