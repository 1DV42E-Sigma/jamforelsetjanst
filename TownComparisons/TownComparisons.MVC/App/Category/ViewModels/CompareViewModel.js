categoryModule.controller("compareViewModel", function ($scope, categoryService, $http, $q, $routeParams, $window, $location, viewModelHelper, $rootScope, collectorFactory) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.categoryService = categoryService;
    $scope.categoryID = $routeParams.categoryId;
    $scope.organisationalUnitIdsToCompare = [];


    var initialize = function () {
        //Save operator ids to compare in an array
        for (var i = 0; i < $routeParams.id.length; i++) {
            $scope.organisationalUnitIdsToCompare.push($routeParams.id[i] + '');
        }

        //Start loading category (and then compare results)
        categoryService.getCategory($scope.categoryID, getCompareResults);

        getCompareOrganisationalUnits();
        $scope.getClientPosition();
    }

    //Runs when a category has been loaded
    var getCompareResults = function () {

        //Load compare result data from API
        var organisationalUnitIdsString = $scope.organisationalUnitIdsToCompare.join(",");
        viewModelHelper.apiGet('api/category/' + $scope.categoryID + '/properties?operators=' + organisationalUnitIdsString, null, 
            function (result) {
                $scope.compareResults = result.data;
                console.log(result.data);
            }
        );
    }

    var getCompareOrganisationalUnits = function () {

        viewModelHelper.apiGet('api/operators/' + $scope.organisationalUnitIdsToCompare.join(","), null,
            function (result) {
                $scope.compareOrganisationalUnits = result.data;
                console.log(result.data);
            }
        );
    }

    //Gets clients position.
    $scope.getClientPosition = function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                $scope.$apply(function () {
                    $scope.posLat = position.coords.latitude;
                    $scope.posLng = position.coords.longitude;
                });
            });
        }
    }

    //Gets distance between client and operators positions.
    $scope.getDistanceBetweenPositions = function (ou) {
        var lat1 = ou.Latitude;
        var lon1 = ou.Longitude;

        var R = 6371; // km 
        var x1 = $scope.posLat - lat1;
        var dLat = $scope.toRad(x1);
        var x2 = $scope.posLng - lon1;
        var dLon = $scope.toRad(x2);
        var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
                        Math.cos($scope.toRad(lat1)) * Math.cos($scope.toRad($scope.posLat)) *
                        Math.sin(dLon / 2) * Math.sin(dLon / 2);
        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var d = R * c;

        return parseFloat(d.toFixed(2));
    }

    $scope.toRad = function (coord) {
        return coord * Math.PI / 180;
    }

    //Rounds a float number to two decimals.
    $scope.roundOneDecimal = function (num) {
        if (num != null) {
            return num.toFixed(1);
        }
        return null;
    }
    
    initialize();
});