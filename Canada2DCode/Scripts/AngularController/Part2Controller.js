//here I am separating each angular controller to separate file for make it manageable  

var app = angular.module('MyApp') //extending from previously created angular module in the previous part
    .controller('Part2Controller', function ($rootScope, $scope, ContactService, $http, $q, $routeParams, $window, $location, angularService) { //inject ContactService
        $scope.Contact = null;

        $scope.flags = { shownFromList: false };
        $scope.title = 'login';

        //$scope.userLogin = {
        //    grant_type: 'password',
        //    username: 'hhhh@yahoo.ca',
        //    password: '1234'
        //};
        $scope.userLogin = {
            grant_type: 'password',
            username: '',
            password: ''
         };


        $scope.initialize = function () {
            $scope.pageHeading = "Customer Section";
            $scope.title = 'login';
            $rootScope.isLoggedIn = false;
        };


        $scope.loginService = function (userlogin) {



            var resp = $http({
                url: "/Canada2DCode/Token",
                method: "POST",
                data: $.param({ grant_type: userlogin.grant_type, username: userlogin.username, password: userlogin.password }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
            return resp;
        };

        $scope.login = function () {



            var promiselogin = $scope.loginService($scope.userLogin);

            promiselogin.then(function (resp) {

                $scope.userName = resp.data.userName;


                $rootScope.isLoggedIn = true;

                $rootScope.User.username = resp.data.userName;
                $rootScope.User.email = $scope.userLogin.username;

                sessionStorage.setItem('accesstoken', resp.data.access_token);
                sessionStorage.setItem('UserName', resp.data.userName);
                sessionStorage.setItem('UserObj', JSON.stringify(resp.data));
                //sessionStorage.setItem('refreshToken', resp.data.refresh_token);
                $scope.accessToken = sessionStorage.getItem('accesstoken');
                //$scope.refreshToken = resp.data.refresh_token;
                $window.location.href = '/Canada2DCode/home/Product';
            }, function (err) {

                $scope.responseData = "Error " + err.status + "; " + "Login failed. ";
                alert($scope.responseData);
            });

        };

        $scope.initialize();


    })
    .factory('ContactService', function ($http) { // here I have created a factory which is a populer way to create and configure services
        var fac = {};
        fac.GetLastContact = function () {
            return $http.get('/Data/GetLastContact');
        }
        return fac;
    });



app.controller("indexViewModel", function ($scope, $http, $q, $routeParams, $window, $location, angularService) {

    var self = this;

    $scope.sessionName = "Canada2DCode";
    $scope.speakerName = "Two-Dimensional Barcode Registration System";



});


app.controller("AngularCtrl", function ($scope, angularService) {
    $scope.divEmpModification = false;

    //To Get All Records  
    $scope.GetAll = function () {
        var Data = angularService.getEmp();
        Data.then(function (emp) {
            $scope.employees = emp.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.edit = function (employee) {

        $scope.ID = employee.ID;
        $scope.FirstName = employee.FirstName;
        $scope.LastName = employee.LastName;
        $scope.UserName = employee.UserName;
        $scope.Password = employee.Password;
        $scope.Operation = "Update";
        $scope.divEmpModification = true;
    }

    $scope.add = function () {

        $scope.ID = "";
        $scope.FirstName = "";
        $scope.LastName = "";
        $scope.UserName = "";
        $scope.Password = "";
        $scope.Operation = "Add";
        $scope.divEmpModification = true;
    }

    $scope.Save = function () {
        var Employee = {
            FirstName: $scope.FirstName,
            LastName: $scope.LastName,
            UserName: $scope.UserName,
            Password: $scope.Password
        };
        var Operation = $scope.Operation;

        if (Operation == "Update") {
            Employee.ID = $scope.ID;
            var getMSG1 = angularService.update(Employee);
            getMSG1.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.divEmpModification = false;
            }, function () {
                alert('Update Error');
            });
        }
        else {
            var getMSG2 = angularService.Add(Employee);
            getMSG2.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.divEmpModification = false;
            }, function () {
                alert('Insert Error');
            });
        }
    };

    $scope.delete = function (employee) {
        var getMSG = angularService.Delete(employee.ID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
        }, function () {
            alert('Delete Error');
        });
    }

    $scope.GetAll();
});

app.controller("AngularCtrlRole", function ($scope, angularServiceRole) {
    GetAllNames();
    GetAllRoles();
    GetAllEmpRole();
    //To Get All Records  
    function GetAllEmpRole() {
        var Data = angularServiceRole.getEmpRole();
        Data.then(function (emp) {
            $scope.views = emp.data;
        }, function () {
            alert('Error');
        });
    }

    //To Get All Records  
    function GetAllNames() {
        var Data = angularServiceRole.getName();
        Data.then(function (emp) {
            $scope.items = emp.data;
        }, function () {
            alert('Error');
        });
    }

    function GetAllRoles() {
        var Data = angularServiceRole.getRole();
        Data.then(function (role) {
            $scope.roleitems = role.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.SavePermission = function () {
        var Permission = {
            ID: $scope.selectedItem.ID,
            RoleID: $scope.selectedItemRole.ID
        };

        var getMSG = angularServiceRole.updateRole(Permission);
        getMSG.then(function (messagefromController) {
            GetAllNames();
            GetAllRoles();
            GetAllEmpRole();
            alert(messagefromController.data);
        }, function () {
            alert('Save Permission Error');
        });


    }

});

app.controller("CustomerModel", function ($scope, $http, $q, $routeParams, $window, $location, angularService) {

    $scope.flags = { shownFromList: false };
    $scope.title = 'login';

    //$scope.userLogin = {
    //    grant_type: 'password',
    //    username: 'Taiseer',
    //    password: 'SuperPass' //,
    //    //ProviderSystemName: $scope.ProviderSystemName
    //};

    $scope.userLogin = {
        grant_type: 'password',
        username: 'Taiseer',
        password: 'SuperPass' 
    };



    var initialize = function () {
        $scope.pageHeading = "Customer Section";
        $scope.title = 'login';
    };

   
    $scope.loginService = function (userlogin) {



        var resp = $http({
            url: "http://localhost/Canada2DCodeSite/Token",
            method: "POST",
            data: $.param({ grant_type: userlogin.grant_type, username: userlogin.username, password: userlogin.password }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return resp;
    };

    $scope.login = function () {



        var promiselogin = $scope.loginService($scope.userLogin);

        promiselogin.then(function (resp) {

            $scope.userName = resp.data.userName;


            sessionStorage.setItem('accesstoken', resp.data.access_token);
            //sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            //$scope.responseData = "User: " + resp.data.userName + " --logined. " + "; " + "Token: " + resp.data.access_token;
            $scope.accessToken = sessionStorage.getItem('accesstoken');
            //$scope.refreshToken = resp.data.refresh_token;
            //$scope.userName = resp.data.userName;
            //$location.path("http://localhost/Canada2DCodeSite/product");
            $window.location.href = '/Canada2DCodeSite/product';
        }, function (err) {

            $scope.responseData = "Error " + err.status + "; " + "Login failed. ";
        });

    };

    initialize();
});

app.controller("RegisterModel", function ($scope, $http, $q, $routeParams, $window, $location) {

    // This is the parent controller/viewmodel for 'RegisterModule' and its $scope is accesible
    // down controllers set by the routing engine. This controller is bound to the Register.cshtml in the
    // Home view-folder.


    $scope.Customer = {

        CustomerId: 0,
        FirstName: '',
        LastName: '',
        Email: '',
        Password: '',
        ConfirmPassword: '',
        Conditions_BT: false,
        Url: ''


    };

    $scope.UserAccount = {
        "userName": "Taiseer",
        "password": "SuperPass",
        "confirmPassword": "SuperPass"
    };

    //$scope.viewModelHelper = viewModelHelper;
    //$scope.RegisterService = RegisterService;

    $scope.flags = { shownFromList: false };

    var initialize = function () {
        $scope.pageHeading = "Register Section";
        $scope.showRegister();
    };

   
    $scope.register = function () {
        var resp = $http({
            url: 'api/customer/',
            method: "POST",
            data: $scope.Customer
        });
        return resp;
    }

    $scope.RegisterData = function () {

        $scope.responseData = {};

        var promiseregister = $scope.register();

        promiseregister.then(function (resp) {
            $scope.responseData = resp.data;
            $scope.Registered = resp.Registered;
            $scope.RegisterText = resp.Text;

        }, function (err) {
            $scope.responseData = "Error " + err.status;
        });

    };



    //$scope.RegisterData = function () {


    //    viewModelHelper.apiPost('api/customer/', $scope.Customer,
    //        function (result) {
    //            $scope.customers = result.data;
    //        });

    //    //viewModelHelper.apiPost('api/Account/register', $scope.Customer,
    //    //    function (result) {
    //    //        $scope.customers = result.data;
    //    //    });

    //};

    initialize();
});

