categoryModule.controller("categoriesViewModel", function ($scope, categoryService, $http, $q, $routeParams, $route, $window, $location, viewModelHelper, categoriesFactory) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.categoryService = categoryService;
    $scope.sortByName = 'Name';
    $scope.sortAsc = 'sortAsc';
    $scope.sortDesc = 'sortDesc';
    $scope.classActive = 'active';
    $scope.classInvisible = 'invisible';

    var initialize = function () {
        $scope.sortCategoryByName();
        $scope.showGroupCateogires();
        $scope.groupClass = "active";
    }

    $scope.showGroupCateogires = function () {
        $scope.viewAllCategories();
        $scope.group = true;
        $scope.alphabet = false;
        $scope.groupClass = "active";
        $scope.alphabetClass = "";
    }
    
    $scope.showAlphabetCategories = function () {
        $scope.viewCategoriesBasedOnAlphabet();
        $scope.group = false;
        $scope.alphabet = true;
        $scope.alphabetClass = "active";
        $scope.groupClass = "";
    }

    //Sorts the categories by Name (Desc).
    $scope.sortCategoryByName = function () {
        $scope.visibleName = '';
        $scope.fileName = $scope.sortAsc;
        $scope.activeName = $scope.classActive;

        if ($scope.sortBy == $scope.sortByName) {
            $scope.sortBy = '-' + $scope.sortByName;
            $scope.fileName = $scope.sortDesc;
        }
        else {
            $scope.sortBy = $scope.sortByName;
            $scope.fileName = $scope.sortAsc;
        }
    }

    //Get all categories based on category via APICategoriesController
    $scope.viewAllCategories = function () {
        viewModelHelper.apiGet('api/categories', null,
            function (result) {
                $scope.groupCategories = result.data;
            });
    }

    //Get all categories based on alphabet via APICategoriesController
    $scope.viewCategoriesBasedOnAlphabet = function () {
        viewModelHelper.apiGet('api/categories/alphabet', null,
            function (result) {
                $scope.alphabetCategories = result.data;
            });
    }

    //Show all organisational units inside a category
    $scope.showCategory = function (category) {
        categoriesFactory.showCategory(category);
    }

     //Switch between sortings
    $scope.changeView = function (value) {
        categoriesFactory.changeView(value);
    }

    initialize();
});
