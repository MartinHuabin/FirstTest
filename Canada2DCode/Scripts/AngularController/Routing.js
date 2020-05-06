


angular.module('MyApp', ['ngRoute']) //extending from previously created angularjs module in part1
    // here ['ngRoute'] is not required, I have just added to make you understand in a single place
    .config(function ($routeProvider, $locationProvider) {

        $routeProvider
            .when('/', { // This is for reditect to another route
                redirectTo: function () {
                    return '/ObjectList';
                }
            })
            .when('/home', {
                templateUrl: '../Template/Home.html',

                controller: 'HomeController'
            })
            .when('/ObjectList', {
                templateUrl: '../Template/ObjectList.html',
                controller: 'AboutController'
            })
            .when('/website', {
                templateUrl: '../Template/CodeWebsite.html',
                controller: 'CodeWebsiteController'
            })
            .when('/codeperson/:id', {
                templateUrl: '../Template/CodePerson.html',
                controller: 'CodePersonController'
            })
            .when('/codedocument', {
                templateUrl: '../Template/CodeDucument.html',
                controller: 'CodeDucumentController'
            })
            .otherwise({
                templateUrl: '../Template/Error.html',
                controller: 'ErrorController'
            });

        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: false
        //});
        //$locationProvider.hashPrefix('');
        $locationProvider.html5Mode(false).hashPrefix('');
    }).run(function ($rootScope, $location, $window, $filter, $q) {

        $rootScope.isLoggedIn = false;

        $rootScope.BaseUrl = 'http://72.137.236.126/Canada2DCode';
        $rootScope.AdminUser = {

            username: '',
            useremail: '',
            usergroup: 0,
            userrole: 0

        };
    })
    .controller('HomeController', function ($rootScope, $scope, $http, $q, $routeParams, $window, $location, $document) {
        $scope.Message = "This is HOME page";

        $scope.username = sessionStorage.getItem('UserName');

        $scope.user = JSON.parse(sessionStorage.getItem('UserObj'));


        $scope.Addresses = {

            AddID_PK: 0,
            Address1: '',
            Address2: '',
            Address3: '',
            City: '',
            State: '',
            Country: '',
            PostalCode: '',
            IsInActive: '',
            Note: '',
            OrganizationID_FK: $scope.user.customerId

        };

        var CustomerModelData = {
            CustomerId: 0,
            FirstName: '',
            LastName: '',
            Email: '',
            Url: '',
            Password: '',
            ConfirmPassword: '',
            Conditions_BT: true,
            AddID_FK: 0,
            Telephone: '',
            AddressesList: []

        };


        $scope.GetAddress = function () {


            if ($scope.customers !== null && $scope.customers.addressesList !== null && $scope.customers.addressesList.length > 0) {


                $scope.Addresses.AddID_PK = $scope.customers.addressesList[0].addID_PK;
                $scope.Addresses.Address1 = $scope.customers.addressesList[0].address1;
                $scope.Addresses.Address2 = $scope.customers.addressesList[0].address2;
                $scope.Addresses.Address3 = $scope.customers.addressesList[0].address3;
                $scope.Addresses.City = $scope.customers.addressesList[0].city;
                $scope.Addresses.State = $scope.customers.addressesList[0].state;
                $scope.Addresses.Country = $scope.customers.addressesList[0].country;
                $scope.Addresses.PostalCode = $scope.customers.addressesList[0].postalCode;
                $scope.Addresses.IsInActive = $scope.customers.addressesList[0].isInActive;
                $scope.Addresses.Note = $scope.customers.addressesList[0].note;
                $scope.Addresses.OrganizationID_FK = $scope.user.customerId;

            }
        };


        $scope.usernameshow = '';

        var resting = $scope.username.split(" ");

        if (resting.length > 1) {

            $scope.usernameshow = resting[1].substring(0, 1).toUpperCase() + '.' + resting[0].substring(0, 1).toUpperCase();

        }
        else {
            $scope.usernameshow = $scope.username;

        }

        $scope.SaveCustomerData = function () {


            CustomerModelData.CustomerId = $scope.customers.customerId;

            CustomerModelData.FirstName = $scope.customers.firstName;
            CustomerModelData.LastName = $scope.customers.lastName;
            CustomerModelData.Email = $scope.customers.email;
            CustomerModelData.Url = $scope.customers.url;
            CustomerModelData.Password = $scope.customers.password;
            CustomerModelData.ConfirmPassword = $scope.customers.confirmPassword;
            CustomerModelData.Conditions_BT = true;
            CustomerModelData.AddID_FK = $scope.customers.addID_FK;
            CustomerModelData.Telephone = $scope.customers.telephone;
            CustomerModelData.AddressesList = [];

            if ($scope.customers.addressesList !== null && $scope.customers.addressesList.length > 0) {
                $scope.Addresses.AddID_PK = $scope.customers.addressesList[0].addID_PK;
                $scope.Addresses.Address3 = $scope.customers.addressesList[0].address3;
                $scope.Addresses.Note = $scope.customers.addressesList[0].note;
                $scope.Addresses.IsInActive = $scope.customers.addressesList[0].isInActive;
            }
            else {

                $scope.Addresses.AddID_PK = 0;
                $scope.Addresses.Address3 = '';
                $scope.Addresses.Note = '';
                $scope.Addresses.IsInActive = false;
            }



            $scope.Addresses.Address1 = angular.element('#street_number').val();
            $scope.Addresses.Address2 = angular.element('#route').val();

            $scope.Addresses.City = angular.element('#locality').val();
            $scope.Addresses.State = angular.element('#administrative_area_level_1').val();
            $scope.Addresses.Country = angular.element('#country').val();
            $scope.Addresses.PostalCode = angular.element('#postal_code').val();


            $scope.Addresses.OrganizationID_FK = $scope.customers.customerId;


            CustomerModelData.AddressesList.push($scope.Addresses);



            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }


            var deferred = $q.defer();
            var resp = $http({
                url: '/Canada2DCode/api/CustomerData/SaveCustomers',
                method: "POST",
                headers: authHeaders,
                //headers: {

                //    'Content-Type': 'application/json; charset=utf-8'
                //},
                data: CustomerModelData


            }).then(function successCallback(response) {
                alert('SaveData was successful.');
                deferred.resolve('ok');

            }, function errorCallback(response) {
                alert('SaveDatd was failed.');
                deferred.reject('notok');

            });


            return deferred.promise;


        };

        $scope.SaveCustomer = function () {


            $scope.responseData = {};

            var promiseSaveCustomerData = $scope.SaveCustomerData();

            promiseSaveCustomerData.then(function (resp) {
                $scope.responseData = resp.data;

                //$window.location.href = '/Canada2DCode/home/Customer';
            }, function (err) {
                $scope.responseData = "Error " + err.status;

                alert($scope.responseData);
            });


        };

        $scope.GetProductionData = function () {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var urlstr = "/Canada2DCode/api/CustomerData/GetCustomersbyId?customerid=" + $scope.user.customerId;
            var response = $http({
                url: urlstr,
                method: "GET",
                headers: authHeaders
            });
            return response;
        };

        $scope.GetCustomerData = function () {



            var promisegetproducts = $scope.GetProductionData();

            promisegetproducts.then(function (resp) {
                $scope.customers = resp.data;
                $scope.GetAddress();

            }, function (err) {
                $scope.responseData = "Error " + err.status;
            });



        };



        $scope.ini = function () {


            $scope.GetCustomerData();

        };

        $scope.logout = function () {

            sessionStorage.clear();

            $window.location.href = '/Canada2DCode/home/Customer';
        };

        $scope.Profile = function () {

            $window.location.href = '/Canada2DCode/home/Product#/home';

            //$window.location.href = '/Canada2DCode/home/Product#/home';
        };


        $scope.ini();


    })
    .controller('AboutController', function ($scope, $http, $q, $routeParams, $window, $location) {

        $scope.username = sessionStorage.getItem('UserName');

        $scope.user = JSON.parse(sessionStorage.getItem('UserObj'));
        $scope.ObjectList = [];

        $scope.customers = {};

        $scope.flags = { shownFromList: false };

        var initialize = function () {
            $scope.pageHeading = "Product Section";
            $scope.GetCustomeData();
        }

        $scope.productList = function () {
            viewModelHelper.navigateTo('product/list');
        }

        $scope.showProduct = function () {
            if (productService.productId != 0) {
                $scope.flags.shownFromList = false;
                viewModelHelper.navigateTo('product/show/' + productService.productId);
            }
        }


        $scope.GetProduction = function () {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var urlstr = "/Canada2DCode/api/customers";
            var response = $http({
                url: urlstr,
                method: "GET",
                headers: authHeaders
            });
            return response;
        };


        $scope.getPersonType = function (v) {

            if (v == null) {

                return '';

            }
            if (v == 'Medical_Professionals') {


                return 'Medical Professionals';

            }
            if (v == 'Volunteers') {


                return 'Volunteers';

            }

            if (v == 'Affected_Person') {


                return 'Affected Person';

            }

            return v;

        }

        $scope.getC2DCodeType = function (v) {

            if (v == null) {

                return '';

            }

            return v;

        }

        $scope.GetRedir = function () {




        }


        $scope.GetObjectList = function () {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var urlstr = "/Canada2DCode/api/customer/" + $scope.user.customerId;
            var response = $http({
                url: urlstr,
                method: "GET",
                headers: authHeaders
            });
            return response;
        };


        $scope.GetCustomeData = function () {



            var promisegetproducts = $scope.GetObjectList();

            promisegetproducts.then(function (resp) {
                $scope.ObjectList = resp.data;


            }, function (err) {
                $scope.responseData = "Error " + err.status;
            });


        };

        $scope.redirectToGoogle = function (UrlVale) {


            $window.location.href = UrlVale;

            //$window.open(UrlVale, '_blank');
        };
        $scope.image = {};

        $scope.GetQRCode = function (itemvalue) {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            viewModelHelper.apiGet('api/ReadProduct', itemvalue,
                function (result) {
                    $scope.image = result.data;

                    return $scope.image;
                });


        };
        $scope.addnewobjectitem = function () {



            $scope.redirectToGoogle("#/codeperson/0");
        }

        initialize();
    })
    .controller('CodeWebsiteController', function ($scope, $routeParams) {

        $scope.Message = $routeParams.id;








    })
    .controller('CodeDucumentController', function ($scope, $routeParams) {

        $scope.Message = $routeParams.id;



    })
    .controller('CodePersonController', function ($scope, $routeParams, $http, $q, $window, $location, $document) {

        $scope.ObjectId = $routeParams.id;
        $scope.PersonID = 0;

        if ($scope.ObjectId == null) {

            $scope.ObjectId = 0;

        }

      
        $scope.username = sessionStorage.getItem('UserName');

        $scope.user = JSON.parse(sessionStorage.getItem('UserObj'));

        $scope.ObjectCategoryList = [

            {
                "Category_id": 1,
                "Category_Name": 'Person'
            },
            {
                "Category_id": 2,
                "Category_Name": 'Product'
            },

            {
                "Category_id": 3,
                "Category_Name": 'Event'
            }

        ];
        $scope.ObjectTypeList = [

            {
                "ObjectType_id": 'Default',
                "ObjectType_Name": '(Please select a Obj Type)'
            },
            {
                "ObjectType_id": 'Medical_Professionals',
                "ObjectType_Name": 'Medical Professionals'
            },
            {
                "ObjectType_id": 'Volunteers',
                "ObjectType_Name": 'Volunteers'
            },

            {
                "ObjectType_id": 'Affected_Person',
                "ObjectType_Name": 'Affected Person'
            }

        ];

        $scope.GenderList = [

            {
                "Gender_id": 'Default',
                "Gender_Name": '(Please select)'
            },
            {
                "Gender_id": 'Male',
                "Gender_Name": 'Male'
            },
            {
                "Gender_id": 'Female',
                "Gender_Name": 'Female'
            },

            {
                "Gender_id": 'Other',
                "Gender_Name": 'Other'
            }

        ];

        $scope.C2DCodeTypeList = [

            {
                "C2DCodeType_id": 'Default',
                "C2DCodeType_Name": '(Please select a Code Type)'
            },
            {
                "C2DCodeType_id": 'QR_Code',
                "C2DCodeType_Name": 'QR Code'
            },
            {
                "C2DCodeType_id": 'Data_Matrix',
                "C2DCodeType_Name": 'Data Matrix'
            }
            //,

            //{
            //    "C2DCodeType_id": 'HanXin',
            //    "C2DCodeType_Name": 'HanXin'
            //}

        ];


        $scope.ObjectPersonData = {

            PersonID_PK: 0,
            ObjectID_FK: 0,
            Person_Name: '',
            PersonType: '',
            Description: '',
            Gender: '',
            Title: '',
            Telephone: '',
            Birthday: Date.now(),
            Position: '',
            Email: '',
            AddID_FK: 0,
            C2DCodeType: '',
            Created_Date: Date.now(),
            Deleted_Date: Date.now()

        };

        $scope.ObjectPersonModel = {


            Object_id: 0,
            Object_name: '',
            Category_id: 0,
            Model_year: 0,
            OrganizationID_FK: 0,
            C2DMACode: '',
            QRCode: '',
            ContentsURL: '',
            PrivateKey: '',
            OpenToRole: '',
            BlockID: 0,
            NextChainID: 0,
            ObjectPersonData: $scope.ObjectPersonData

        };



        $scope.GetPerson = function () {


            $scope.ObjectPersonModel.Object_id = $scope.ObjectPerson.object_id;
            $scope.ObjectPersonModel.Object_name = $scope.ObjectPerson.object_name;

            $scope.ObjectPersonModel.Category_id = 1;
            $scope.ObjectPersonModel.Model_year = $scope.ObjectPerson.model_year;

            if (($scope.ObjectPerson.organizationID_FK) && ($scope.ObjectPerson.organizationID_FK > 0)) {

                $scope.ObjectPersonModel.OrganizationID_FK = $scope.ObjectPerson.organizationID_FK;

            } else {

                $scope.ObjectPersonModel.OrganizationID_FK = $scope.user.customerId;

            }

            $scope.ObjectPersonModel.C2DMACode = $scope.ObjectPerson.c2DMACode;
            $scope.ObjectPersonModel.QRCode = $scope.ObjectPerson.qrCode;
            $scope.ObjectPersonModel.ContentsURL = $scope.ObjectPerson.contentsURL;
            $scope.ObjectPersonModel.PrivateKey = $scope.ObjectPerson.privateKey;
            $scope.ObjectPersonModel.OpenToRole = $scope.ObjectPerson.openToRole;
            $scope.ObjectPersonModel.BlockID = $scope.ObjectPerson.blockID;
            $scope.ObjectPersonModel.NextChainID = $scope.ObjectPerson.nextChainID;

            if ($scope.ObjectPerson.objectPersonData != null) {

                $scope.ObjectPersonModel.ObjectPersonData.PersonID_PK = $scope.ObjectPerson.objectPersonData.personID_PK;
                $scope.ObjectPersonModel.ObjectPersonData.ObjectID_FK = $scope.ObjectPerson.objectPersonData.objectID_FK;
                $scope.ObjectPersonModel.ObjectPersonData.Person_Name = $scope.ObjectPerson.objectPersonData.person_Name;
                $scope.ObjectPersonModel.ObjectPersonData.PersonType = $scope.ObjectPerson.objectPersonData.personType;
                $scope.ObjectPersonModel.ObjectPersonData.Description = $scope.ObjectPerson.objectPersonData.description;
                $scope.ObjectPersonModel.ObjectPersonData.Gender = $scope.ObjectPerson.objectPersonData.gender;
                $scope.ObjectPersonModel.ObjectPersonData.Title = $scope.ObjectPerson.objectPersonData.title;
                $scope.ObjectPersonModel.ObjectPersonData.Telephone = $scope.ObjectPerson.objectPersonData.telephone;
                $scope.ObjectPersonModel.ObjectPersonData.Birthday = $scope.ObjectPerson.objectPersonData.birthday;
                $scope.ObjectPersonModel.ObjectPersonData.Position = $scope.ObjectPerson.objectPersonData.position;
                $scope.ObjectPersonModel.ObjectPersonData.Email = $scope.ObjectPerson.objectPersonData.email;
                $scope.ObjectPersonModel.ObjectPersonData.AddID_FK = $scope.ObjectPerson.objectPersonData.addID_FK;
                $scope.ObjectPersonModel.ObjectPersonData.C2DCodeType = $scope.ObjectPerson.objectPersonData.c2DCodeType;
                $scope.ObjectPersonModel.ObjectPersonData.Created_Date = $scope.ObjectPerson.objectPersonData.created_Date;
                $scope.ObjectPersonModel.ObjectPersonData.Deleted_Date = $scope.ObjectPerson.objectPersonData.deleted_Date;
            }
            else {


                $scope.ObjectPersonModel.ObjectPersonData.PersonType = 'Default';
                $scope.ObjectPersonModel.ObjectPersonData.Gender = 'Default';
                $scope.ObjectPersonModel.ObjectPersonData.C2DCodeType = 'Default';
            }

        };


        $scope.SaveObjectPersonData = function () {



            var ObjectPersonModelData = $scope.ObjectPersonModel;

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }


            var deferred = $q.defer();
            var resp = $http({
                url: '/Canada2DCode/api/ObjectPerson/SaveObjectPerson',
                method: "POST",
                headers: authHeaders,
                data: ObjectPersonModelData


            }).then(function successCallback(response) {
                alert('SaveData was successful.');
                deferred.resolve(response);

            }, function errorCallback(response) {
                alert('SaveDatd was failed.');
                deferred.reject('notok');

            });


            return deferred.promise;


        };

        $scope.SaveObjectPerson = function () {


            $scope.responseData = {};

            var promiseObjectPersonData = $scope.SaveObjectPersonData();

            promiseObjectPersonData.then(function (resp) {

                $scope.responseData = resp.data;
                $scope.ObjectId = $scope.responseData.object_id;
                $scope.PersonID = $scope.responseData.objectPersonData.personID_PK;
                $scope.ini();
            }, function (err) {
                $scope.responseData = "Error " + err.status;

                alert($scope.responseData);
            });


        };


        $scope.GetObjectPersonData = function () {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var urlstr = "/Canada2DCode/api/ObjectPerson/GetObjectPersonById?ObjectId=" + $scope.ObjectId + "&&PersonID=" + $scope.PersonID;
            var response = $http({
                url: urlstr,
                method: "GET",
                headers: authHeaders
            });
            return response;
        };

        $scope.GetObjectPerson = function () {



            var promisegetObjectPerson = $scope.GetObjectPersonData();

            promisegetObjectPerson.then(function (resp) {
                $scope.ObjectPerson = resp.data;
                $scope.GetPerson();

            }, function (err) {
                $scope.responseData = "Error " + err.status;
            });



        };


        $scope.GetC2DCodeTypeData = function (value) {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var urlstr = "/Canada2DCode/api/ReadPerson?Objectid=" + $scope.ObjectId + "&&Objectype=" + value;
            var response = $http({
                url: urlstr,
                method: "GET",
                headers: authHeaders
            });
            return response;
        };


        $scope.getC2DCodeTypeData = function (v) {

            if ($scope.ObjectId == 0) {


                alert("The Project is not created yet!");
                return;

            }

            var promisegetC2DCodeTypeData = $scope.GetC2DCodeTypeData(v);

            promisegetC2DCodeTypeData.then(function (resp) {
                $scope.ObjectPersonModel.QRCode = resp.data;


            }, function (err) {
                $scope.responseData = "Error " + err.status;
            });





        };

        $scope.ini = function () {


            $scope.GetObjectPerson();

        };





        $scope.ini();




    })
    .controller('OrderController', function ($scope, $routeParams) {

        $scope.Message = $routeParams.id;








    })
    .controller('ErrorController', function ($scope) {
        $scope.Message = "404 Not Found!";
    })
    .controller("rootViewModel", function ($scope, $http, $q, $routeParams, $window, $location) {

        //$scope.viewModelHelper = viewModelHelper;
        //$scope.productService = productService;
        $scope.customers = {};

        $scope.flags = { shownFromList: false };

        var initialize = function () {
            $scope.pageHeading = "Product Section";
            $scope.GetCustomeData();
        }

        $scope.productList = function () {
            viewModelHelper.navigateTo('product/list');
        }

        $scope.showProduct = function () {
            if (productService.productId != 0) {
                $scope.flags.shownFromList = false;
                viewModelHelper.navigateTo('product/show/' + productService.productId);
            }
        }


        $scope.GetProduction = function () {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var urlstr = "/Canada2DCode/api/customers";
            var response = $http({
                url: urlstr,
                method: "GET",
                headers: authHeaders
            });
            return response;
        };

        $scope.GetCustomeData = function () {



            var promisegetproducts = $scope.GetProduction();

            promisegetproducts.then(function (resp) {
                $scope.customers = resp.data;


            }, function (err) {
                $scope.responseData = "Error " + err.status;
            });

            //var accesstoken = sessionStorage.getItem('accesstoken');

            //var authHeaders = {};
            //if (accesstoken) {
            //    authHeaders.Authorization = 'Bearer ' + accesstoken;
            //}
            //viewModelHelper.apiGet('api/customers', 
            //    function (result) {
            //        $scope.customers = result.data;
            //    });

        };

        $scope.redirectToGoogle = function (UrlVale) {

            $window.open(UrlVale, '_blank');
        };
        $scope.image = {};

        $scope.GetQRCode = function (itemvalue) {

            var accesstoken = sessionStorage.getItem('accesstoken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            viewModelHelper.apiGet('api/ReadProduct', itemvalue,
                function (result) {
                    $scope.image = result.data;

                    return $scope.image;
                });


        };


        initialize();
    }).directive('datePicker', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, elm, attr, ctrl) {

                // Format date on load
                ctrl.$formatters.unshift(function (value) {
                    if (value && moment(value).isValid()) {
                        return moment(new Date(value)).format('MM/DD/YYYY');
                    }
                    return value;
                })

                //Disable Calendar
                scope.$watch(attr.ngDisabled, function (newVal) {
                    if (newVal === true)
                        $(elm).datepicker("disable");
                    else
                        $(elm).datepicker("enable");
                });

                // Datepicker Settings
                elm.datepicker({
                    autoSize: true,
                    changeYear: true,
                    changeMonth: true,
                    dateFormat: attr["dateformat"] || 'mm/dd/yy',
                    showOn: 'button',
                    buttonText: '<i class="glyphicon glyphicon-calendar"></i>',
                    onSelect: function (valu) {
                        scope.$apply(function () {
                            ctrl.$setViewValue(valu);
                        });
                        elm.focus();
                    },

                    beforeShow: function () {
                        debugger;
                        if (attr["minDate"] != null)
                            $(elm).datepicker('option', 'minDate', attr["minDate"]);

                        if (attr["maxDate"] != null)
                            $(elm).datepicker('option', 'maxDate', attr["maxDate"]);
                    },


                });
            }
        }
    });
