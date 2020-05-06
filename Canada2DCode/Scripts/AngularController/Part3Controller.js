angular.module('MyApp') // extending from previously created angular module in the First Part
    .controller('Part3Controller', function ($rootScope, RegistrationService, $scope, $window, $http, $q, $location) {
        $scope.IsLogedIn = false;
        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        //$scope.LoginData = {
        //    Username: '',
        //    Password: ''
        //};

        ////Check is Form Valid or Not // Here f1 is our form Name
        //$scope.$watch('f1.$valid', function (newVal) {
        //    $scope.IsFormValid = newVal;
        //});
        //$scope.responseData = {};


        $scope.Customer = {

            CustomerId: 0,
            FirstName: '',
            LastName: '',
            Email: '',
            Password: '',
            ConfirmPassword: '',
            Conditions_BT: true,
            Url: ''


        };

        $scope.UserAccount = {
            "userName": "Taiseer",
            "password": "SuperPass",
            "confirmPassword": "SuperPass"
        };


        $scope.flags = { shownFromList: false };

        $scope.initialize = function () {
            $scope.pageHeading = "Register Section";

        };

        $scope.IsLogedIn = false;
        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;


        $scope.register = function () {

            var deferred = $q.defer();
            var resp = $http({
                url: '/Canada2DCode/api/customer/',
                method: "POST",
                headers: {

                  'Content-Type': 'application/json; charset=utf-8'
                },
                data: $scope.Customer


            }).then(function successCallback(response) {
                alert('register was successful.');
                deferred.resolve('ok');

            }, function errorCallback(response) {
                    alert('register was failed.');
                    deferred.reject('notok');

            });


            return deferred.promise;
        };


        $scope.RegisterData = function () {


            $scope.responseData = {};

            var promiseregister = $scope.register();

            promiseregister.then(function (resp) {
                $scope.responseData = resp.data;
           
                $window.location.href = '/Canada2DCode/home/Customer';
            }, function (err) {
                    $scope.responseData = "Error " + err.status;

                    alert($scope.responseData);
            });

           
        };

        $scope.initialize();

    })
 .factory('RegistrationService', function ($http, $q) { //explained about factory in Part 2
     var fac = {};

     fac.GetUser = function (d) {
         return $http({
             url: '/Canada2DCode/api/customer/',
             method: 'POST',
             dataType: 'json',
             data: d,
             headers: {
                 'Content-Type': 'application/json; charset=UTF-8' }
         });
     };
     return fac;


    });
