categoryModule.controller("operatorViewModel", function ($rootScope, $scope, categoryService, $http, $q, $routeParams, $window, $location, viewModelHelper, collectorFactory) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.categoryService = categoryService;
    
    $scope.organisationalUnitId = $routeParams.operatorId;

    var initialize = function () {
        categoryService.getCategory($routeParams.categoryId, setOperator);
        $scope.mapOperators();
    }
    
    //Runs when a category has been loaded
    var setOperator = function () {
        for (var i = 0; i < $scope.category.OrganisationalUnits.length; i++) {
            if ($scope.category.OrganisationalUnits[i].OrganisationalUnitId == $scope.organisationalUnitId) {
                $scope.organisationalUnit = $scope.category.OrganisationalUnits[i];
                return;
            }
        }
    }

    //Get Organisational Unit Info via OperatorController
    $scope.getOrganisationalUnitInfoByOUId = function (ouId) {
        viewModelHelper.apiGet('api/operators/' + ouId, null,
            function (result) {
                $scope.organisationalUnit = result.data;
            });
    }

    // Shows OU:s inside a Map box.
    $scope.$root.windowClicked = function (ouId) {
        viewModelHelper.navigateTo('category/' + $routeParams.categoryId + '/operator/' + ouId);
    }

    $scope.checkIfOuIsInCompareList = function (ou) {
        for (var i = 0; i < collectorFactory.listOfSubjects.length; i++) {
            if (collectorFactory.listOfSubjects[i].OrganisationalUnitId === ou.OrganisationalUnitId) {
                return true;
            }
        }
        return false;
    }

    $scope.setCompareSettingsForOu = function (ou) {
        if (ou != null) {
            //check if is in compare list
            var ouIsInCompare = collectorFactory.checkIfInList(ou);
            ou.class = (ouIsInCompare ? "after-compare" : "before-compare");
            ou.icon = (ouIsInCompare ? "fi-check" : "fi-plus");
            ou.text = (ouIsInCompare ? "" : "Jämför");
        }
    }

    //List with selected OU's
    $rootScope.listItems = collectorFactory.listOfSubjects;

    //Toggle OU on the $scope.listItems
    $rootScope.toggleOperators = function (subject) {
        collectorFactory.toggleSubject(subject);
    }

    $rootScope.deleteOperator = function (subject) {
        collectorFactory.deleteSubject(subject);
    }

    //Deletes all item on $scope.listItems
    $rootScope.deleteAllOperators = function () {
        collectorFactory.deleteAllSubjects();
    }

    // Gives the maps its start position.
    $scope.mapOperators = function () {
        $scope.mapBox = {
            center: {
                latitude: 56.029394,
                longitude: 14.156678
            },
            zoom: 10,
            bounds: {}
        };
    }

    //Show OU:s inside a category
    $scope.showOperatorMap = function (ouId) {
        viewModelHelper.navigateTo('category/' + $routeParams.categoryId + '/operator/' + ouId + '/map');
    }

    initialize();
});