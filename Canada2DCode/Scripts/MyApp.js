(function () {
    //Create a Module 
    var app = angular.module('MyApp', ['ngRoute']);  // Will use ['ng-Route'] when we will implement routing
    app.run(function ($rootScope) {

        $rootScope.isLoggedIn = false;

        $rootScope.User = {

            username: '',
            useremail: '',
            usergroup: 0,
            userrole: 0

        };
    });

    app.controller('HomeController', function ($scope, $rootScope, $routeParams, $window, $http, $q, $location) {  // here $scope is used for share data between view and controller
        $scope.Message = "Two-Dimensional Barcode Registration System";

        $scope.username = "";

        $scope.username = sessionStorage.getItem('UserName');

        $scope.usernameshow = '';


        $scope.objData = {
            toname: '',
            toemail: '',
            subject: 'send from contact.',
            message: ''
        };




        $scope.SendingEmail = function () {

            var aas = '';

            $http({
                url: '/Canada2DCode/api/Email/send-email',
                method: "POST",
                headers: {

                    'Content-Type': 'application/json; charset=utf-8'
                },
                data: $scope.objData 
            }).then(function (response) {
                alert($scope.objData.subject);
                    
                },
                    function (response) { // optional
                       
                    });
            

        };

        if ($scope.username) {

            var resting = $scope.username.split(" ");

            if (resting.length > 1) {

                $scope.usernameshow = resting[1].substring(0, 1).toUpperCase() + '.' + resting[0].substring(0, 1).toUpperCase();

            }
            else {
                $scope.usernameshow = $scope.username;

            }
        }
        $scope.logout = function () {

            sessionStorage.clear();

            $window.location.href = '/Canada2DCode/home/Customer';
        };


        $scope.Profile = function () {



            $window.location.href = '/Canada2DCode/home/Product';
        };

    });

    app.controller('MenuController', function ($scope, $rootScope) {  // here $scope is used for share data between view and controller
        $scope.MenuItem = false;
        
    });


    app.service("angularService", function ($http) {

        this.getEmp = function () {
            return $http.get("/Employee/GetAllEmployees");
        };



        //Save (Update)  
        this.update = function (employee) {
            var response = $http({
                method: "post",
                url: "/Employee/Update",
                data: JSON.stringify(employee),
                dataType: "json"
            });
            return response;
        }

        //Delete 
        this.Delete = function (empID) {
            var response = $http({
                method: "post",
                url: "/Employee/Delete",
                params: {
                    id: empID
                }
            });
            return response;
        }

        //Add 
        this.Add = function (employee) {
            var response = $http({
                method: "post",
                url: "/Employee/Add",
                data: JSON.stringify(employee),
                dataType: "json"

            });
            return response;
        }

    });

    app.service("angularServiceRole", function ($http) {
        this.getEmpRole = function () {
            return $http.get("/Permission/GetAllEmpRole");
        };

        this.getName = function () {
            return $http.get("/Permission/GetAllEmpNames");
        };
        this.getRole = function () {
            return $http.get("/Permission/GetAllRoles");
        };

        //Save Permission  
        this.updateRole = function (permission) {
            var response = $http({
                method: "post",
                url: "/Permission/UpdateRole",
                data: JSON.stringify(permission),
                dataType: "json"
            });
            return response;
        }
    });

})();