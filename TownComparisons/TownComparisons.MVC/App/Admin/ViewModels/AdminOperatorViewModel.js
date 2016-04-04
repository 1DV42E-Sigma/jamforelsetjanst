adminModule.controller("adminOperatorViewModel", function ($scope, adminService, $http, $q, $routeParams, $window, $location, viewModelHelper, Upload, $modalInstance) {

    //$scope.viewModelHelper = viewModelHelper;
    //$scope.adminService = adminService;

    $scope.categoryId = $routeParams.categoryId;
    $scope.operatorId = adminService.selectedOperatorId;
    $scope.validationErrors = [];
    $scope.knownValidationErrors = [];
    $scope.closeValidationAlert = false;

    $scope.objectFields = [{ field: 'Name', title: 'Namn', textarea: false },
                            { field: 'ShortDescription', title: 'Kort beskrivning', textarea: true },
                            { field: 'LongDescription', title: 'Längre beskrivning', textarea: true },
                            { field: 'Address', title: 'Adress', textarea: false },
                            { field: 'Latitude', title: 'Latitude', textarea: false },
                            { field: 'Longitude', title: 'Longitude', textarea: false },
                            { field: 'Contact', title: 'Kontakt', textarea: false },
                            { field: 'Telephone', title: 'Telefon', textarea: false },
                            { field: 'Email', title: 'E-post', textarea: false },
                            { field: 'Website', title: 'Webbsida', textarea: false },
                            { field: 'OrganisationalForm', title: 'Organisations-form', textarea: false },
                            { field: 'Other', title: 'Övrigt', textarea: true }];
    

    $scope.getGeo = function() {
        var address = document.getElementById('Address').value;
        var Latitude = document.getElementById('Latitude').value;
        var Longitude = document.getElementById('Longitude').value;
        getLatitudeLongitude(address);
        var autocomplete = new google.maps.places.Autocomplete(document.getElementById("Address"));
    }

    var getLatitudeLongitude = function(address) {
        // If adress is not supplied, use default value 'Kristianstad'
        address = address || 'Kristianstad';
        // Initialize the Geocoder
        geocoder = new google.maps.Geocoder();

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                console.log($scope.operator.Latitude);
                $scope.operator.Latitude = document.getElementById('Latitude').value = results[0].geometry.location.lat();
                $scope.operator.Longitude = document.getElementById('Longitude').value = results[0].geometry.location.lng();
            } else {
                alert("Omöjligt att hämta geografiska koordinater: " + status);
            }
        });
    }


    var initialize = function () {
        var button = document.getElementById('btn');
        refreshOperator();
    }

    var refreshOperator = function () {
        viewModelHelper.apiGet('api/category/' + $scope.categoryId + '/operator/' + $scope.operatorId, null,
            function (result) {
                $scope.operator = result.data;
                $scope.operatorName = angular.copy($scope.operator.Name); //to use without data binding to the category
                $scope.pageHeading = $scope.operatorName;
            });
    }

    $scope.saveOperator = function () {

        viewModelHelper.apiPost('api/admin/category/' + $scope.categoryId + '/operator/' + $scope.operatorId, $scope.operator,
            function (result) {
                //success, close modal
                $modalInstance.close($scope.operator);
            },
            function (errors) {
                //failure
                adminService.parseErrors(errors);
            });
    }

    $scope.saveOperatorImage = function (imageFile) {

        $scope.upload = Upload.upload({
            url: MyApp.rootPath + 'api/admin/category/' + $scope.categoryId + '/operator/' + $scope.operatorId + '/image', // webapi url
            method: "POST",
            file: imageFile
        }).success(function (data, status, headers, config) {
            // file is uploaded successfully
            $scope.operator = data;
        }).error(function (data, status, headers, config) {
            // file failed to upload
            $scope.validationErrors.push({ name: '', message: 'Kunde inte spara bilden.' });
        });
    }

    $scope.cancelEditOperator = function () {
        //close (cancel) modal
        $modalInstance.dismiss('cancel');
    }

    initialize();
});
