adminModule.controller("adminGroupCategoryViewModel", function (flash, $scope, adminService, $http, $q, $routeParams, $window, $location, viewModelHelper) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.adminService = adminService;

    $scope.validationErrors = [];
    $scope.knownValidationErrors = [];
    $scope.closeValidationAlert = false;
    $scope.updateNotInsert = ($routeParams.groupCategoryId);
    $scope.groupCategoryId = $routeParams.groupCategoryId;

    var initialize = function () {
        $scope.categoryHasBeenLoaded = false;
        $scope.pageHeading = 'Laddar in data...';

        getGroupCategory();
    }

    var getGroupCategory = function () {

        var url = ($scope.updateNotInsert ? 'api/admin/groupcategory/' + $scope.groupCategoryId :
                                            'api/admin/newgroupcategory');
        viewModelHelper.apiGet(url, null,
            function (result) {
                $scope.groupCategory = result.data;
                if ($scope.updateNotInsert) {
                    $scope.categoryName = angular.copy($scope.groupCategory.Name); //to use without data binding to the category
                    $scope.pageHeading = 'Grupp-kategori: ' + $scope.categoryName;
                }
                else {
                    $scope.categoryName = 'Ny';
                    $scope.pageHeading = 'Ny grupp-kategori';
                }
                $scope.categoryHasBeenLoaded = true;
            }
        );
    }

    $scope.saveGroupCategory = function () {
        adminService.resetErrors();

        var url = ($scope.updateNotInsert ? 'api/admin/groupcategory/' + $scope.groupCategoryId :
                                            'api/admin/insertgroupcategory');

        viewModelHelper.apiPost(url, $scope.groupCategory,
            function (result) {
                //success
                console.log(result.data);
                flash('alert-box success radius', 'Kategorigruppen har sparats.');
                if ($scope.updateNotInsert) {
                    viewModelHelper.navigateTo('admin');
                }
                else {
                    flash('alert-box success radius', 'Kategorin har sparats.');
                    viewModelHelper.navigateTo('admin/groupcategory/' + result.data.Id);
                }
            },
            function (errors) {
                //failure
                adminService.parseErrors(errors);
            });
    }

    $scope.cancelEditCategory = function () {
        viewModelHelper.navigateTo('admin');
    }


    initialize();
});
