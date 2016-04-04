adminModule.controller("adminCategoryShowViewModel", function (flash, $route, $scope, adminService, $http, $q, $routeParams, $window, $location, viewModelHelper, $modal) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.adminService = adminService;
    
    var initialize = function () {
        $scope.categoryHasBeenLoaded = false;
        $scope.pageHeading = 'Laddar kategori...';
        adminService.getCategory($routeParams.categoryId, afterCategoryHasBeenLoaded);
    }

    var afterCategoryHasBeenLoaded = function () {
        $scope.pageHeading = 'Kategori: ' + $scope.category.Category.Name;
        $scope.categoryHasBeenLoaded = true;
    }

    $scope.openOperatorEditor = function (operatorId) {

        adminService.selectedOperatorId = operatorId;

        var modalInstance = $modal.open({
            templateUrl: '/App/Admin/Views/AdminOperatorView.html',
            controller: 'adminOperatorViewModel',
            scope: $scope
        });

        modalInstance.result.then(function (operator) {
            //refresh page
            flash('alert-box success radius', 'Aktören har sparats.');
            $route.reload(); //$window.location.href = $window.location.href;
        }, function () {
            //modal cancelled, do something here?
        });
    };

    $scope.openQueryEditor = function (queryId) {

        adminService.selectedQueryId = queryId;

        var modalInstance = $modal.open({
            templateUrl: '/App/Admin/Views/AdminQueryView.html',
            controller: 'adminQueryViewModel',
            scope: $scope
        });

        modalInstance.result.then(function (query) {
            //refresh page
            console.log('saved query');
            flash('alert-box success radius', 'Egenskapen har sparats.');
            $route.reload(); //$window.location.href = $window.location.href;
        }, function () {
            //modal cancelled, do something here?
        });
    };

    $scope.gotoEdit = function (mode) {
        adminService.editCategoryMode = mode;
        viewModelHelper.navigateTo('admin/category/' + adminService.categoryId + '/edit');
    }

    initialize();
});
