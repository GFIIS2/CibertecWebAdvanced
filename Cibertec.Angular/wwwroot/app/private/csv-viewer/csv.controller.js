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