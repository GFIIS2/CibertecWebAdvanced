(function () {
    'use strict';
    angular.module('app')
        .controller('csvController', loginController);


    function loginController(configService, $state) {
        var vm = this;
        vm.user = {};
        var fileInput = document.getElementById("csvViewer");        

        init();

        function init() {
            if (!configService.getLogin()) $state.go("login");
            fileInput.addEventListener('change', readFile);
        }
      
        function readFile() {            
            var reader = new FileReader();
            reader.onload = function () {
                document.getElementById('content').innerHTML = reader.result;
            };
            reader.readAsBinaryString(fileInput.files[0]);
        };

        
    }
})();