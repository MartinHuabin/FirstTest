var app = angular.module('c2dcode', ['ngRoute', 'ngCookies'])
    .config(function ($routeProvider, $locationProvider) {
       
        $routeProvider
            .when('/featureWrap', {
                templateUrl: './Partials/LandingPage.html'




            })
            .when('/PersonDetail/:Object_id', {
                templateUrl: './Partials/PersonDetail.html'
            })
            .otherwise({ redirectTo: '/featureWrap' });
        $locationProvider.html5Mode(false).hashPrefix('');
    })
    .run(function ($rootScope, $cookies, $location, $window,  $filter, $q) {
        $rootScope.isLoggedIn = false;

        $rootScope.BaseUrl = 'http://72.137.236.126/Canada2DCode';
        $rootScope.AdminUser = {

            username: '',
            useremail: '',
            usergroup: 0,
            userrole: 0

        };
    }).controller('PersonDetailHomeController', function ($scope, $routeParams, $http, $q, ) {





    }).controller('PersonDetailController', function ($scope, $routeParams, $http, $q, ) {


        $scope.Message = "PersonDetailController";

        $scope.ObjectId = $routeParams.Object_id;
        $scope.PersonID = 0;

        if ($scope.ObjectId == null) {

            $scope.ObjectId = 0;

        }


        $scope.ObjectPerson = {};

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

        $scope.GetObjectPersonData = function () {

            var urlstr = "/Canada2DCode/api/order/detail/" + $scope.ObjectId +"/0";
            var response = $http({
                url: urlstr,
                method: "GET"

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

        $scope.ini = function () {

            $scope.GetObjectPerson();

        };
        $scope.ini();

    })


