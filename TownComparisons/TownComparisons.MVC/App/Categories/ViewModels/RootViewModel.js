categoryModule.controller("rootViewModel", function ($rootScope, $scope, categoryService, $http, $q, $routeParams, $window, $location, viewModelHelper, collectorFactory, $httpParamSerializer) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.categoryService = categoryService;
    
    var initialize = function () {
        $scope.pageHeading = "Hitta och jämför service";
    }

    //List with selected OU's
    $rootScope.listItems = collectorFactory.listOfSubjects;

    //Deletes all item on $scope.listItems
    $rootScope.deleteAllOperators = function () {
        collectorFactory.deleteAllSubjects();
    }

    //Toggle OU on the $scope.listItems
    $rootScope.toggleOperators = function (subject) {
        collectorFactory.toggleSubject(subject);
    }

    $rootScope.deleteOperator = function (subject) {
        collectorFactory.deleteSubject(subject);
    }

    $scope.compare = function () {
        //Create a query string for objects in compare list
        var ouId = [];
        for (var i = 0; i < $rootScope.listItems.length; i++) {
            ouId.push($rootScope.listItems[i].OrganisationalUnitId);
        }
        var params = { "id": ouId }
        var queryString = $httpParamSerializer(params);

        //NavigateTo
        $location.path('category/' + $rootScope.listItems[0].Category + '/compare').search(queryString);
    }

    initialize();
});
