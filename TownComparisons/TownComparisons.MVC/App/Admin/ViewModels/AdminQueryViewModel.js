adminModule.controller("adminQueryViewModel", function ($scope, adminService, $http, $q, $routeParams, $window, $location, viewModelHelper, Upload, $modalInstance) {

    //$scope.viewModelHelper = viewModelHelper;
    //$scope.adminService = adminService;

    $scope.categoryId = $routeParams.categoryId;
    $scope.queryId = adminService.selectedQueryId;
    $scope.validationErrors = [];
    $scope.knownValidationErrors = [];
    $scope.closeValidationAlert = false;

    var initialize = function () {
        refreshQuery();
    }

    var refreshQuery = function () {
        viewModelHelper.apiGet('api/admin/category/' + $scope.categoryId + '/query/' + $scope.queryId, null,
            function (result) {
                $scope.query = result.data;
                $scope.queryName = angular.copy($scope.query.Title); //to use without data binding to the category
                $scope.pageHeading = $scope.queryName;
            });
    }

    $scope.saveQuery = function () {

        viewModelHelper.apiPost('api/admin/category/' + $scope.categoryId + '/query/' + $scope.queryId, $scope.query,
            function (result) {
                //success, close modal
                $modalInstance.close($scope.query);
            },
            function (errors) {
                //failure
                adminService.parseErrors(errors);
            });
    }

    $scope.cancelEditQuery = function () {
        //close (cancel) modal
        $modalInstance.dismiss('cancel');
    }

    initialize();
});
