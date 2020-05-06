angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('Part4Controller', function ($scope, EmployeeService) { // explained in Part 2 about controller
    $scope.Employees = null;
    EmployeeService.GetEmployeeList().then(function (d) {
        $scope.Employees = d.data; //Success callback
    }, function (error) {
        alert('Error!'); // Failed Callback
    });
}).controller('rootViewModel', function ($rootScope, $scope, $http, $q, $routeParams, $window, $location) {

   
    $scope.ScanQRCodes = {};
    var initialize = function () {
        $scope.pageHeading = "QR Code Section";

    };



    $scope.username = sessionStorage.getItem('UserName');

    $scope.usernameshow = '';

    var resting = $scope.username.split(" ");

    if (resting.length > 1) {

        $scope.usernameshow = resting[1].substring(0, 1).toUpperCase() + '.' + resting[0].substring(0, 1).toUpperCase();

    }
    else {
        $scope.usernameshow = $scope.username;

    }


    $scope.AddFiles = function (element) {


        $scope.$apply(function ($scope) {


            $scope.files = element.files;

        });

    };


    $scope.ScanQRCode = function (fd) {

       
        var resp = $http({
            url: '/Canada2DCode/api/ScanQRCode/',
            method: "POST",
            processData: false,
            contentType: false,
          
            data: fd,

            headers: {
                'Content-Type': undefined
            }
      
        });

        return resp;
    };

    $scope.ScanQRCodeFn = function () {

        var fd = new FormData();
        angular.forEach($scope.files, function (file) {


            fd.append('file', file);

        });
        fd.append('ScanQRCodeID', '10');
        fd.append('CustomerID', '20');


        //-------------------
        $scope.responseData = {};

        var promiseregister = $scope.ScanQRCode(fd);

        promiseregister.then(function (resp) {
            $scope.ScanQRCodes = resp.data;

            //$window.location.href = '/Canada2DCode/home/Customer';
        }, function (err) {
                $scope.ScanQRCodes = "Error " + err.status;

                alert($scope.ScanQRCodes);
        });




        //--------------------

        //var promiseregister = $scope.ScanQRCode(fd);

        //promiseregister.then(function (resp) {
        //    $scope.responseData = resp.data;
        //    //$scope.Registered = resp.Registered;
        //    //$scope.RegisterText = resp.Text;

        //    $scope.ScanQRCodes = resp;
        //}, function (err) {
        //    $scope.responseData = "Error " + err.status;
        //});


        //var ajaxRequest = $.ajax({

        //    type: 'POST',

        //    url: '/Canada2DCode/api/ScanQRCode/',
        //    processData: false,
        //    contentType: false,
        //    data: fd, success: function (data) {
        //       //$scope.ScanQRCodes = data;

        //    }

        //}).done(function (data) {
        //    $scope.ScanQRCodes = data;
        //});

        
    };

    initialize();
})
.factory('EmployeeService', function ($http) { // I have explained about factory in the Part 2

    var fac = {};
    fac.GetEmployeeList = function () {
        return $http.get('/Data/GetEmployeeList')
    }
    return fac;
});