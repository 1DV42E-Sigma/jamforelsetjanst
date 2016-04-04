adminModule.controller("adminHomeViewModel", function (flash, $route, $scope, adminService, $http, $q, $routeParams, $window, $location, viewModelHelper, $modal) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.adminService = adminService;

    $scope.errorMessage = '';

    var initialize = function () {
        $scope.refreshCategories();

    }

    $scope.removeCategory = function (categoryId, name) {
        doRemoveCategoryOrGroupCategory(false, categoryId, name);
    }
    $scope.removeGroupCategory = function (groupCategoryId, name) {
        doRemoveCategoryOrGroupCategory(true, groupCategoryId, name);
    }

    var doRemoveCategoryOrGroupCategory = function (isGroupCategory, id, name) {

        var typeTitle = (isGroupCategory ? 'grupp-' : '') + 'kategorin';

        $scope.modalConfirm = {
            title: 'Ta bort ' + typeTitle + ' ' + name + '?',
            description: 'Är du säker på att du vill ta bort ' + (isGroupCategory ? 'hela ' : '') + typeTitle + (isGroupCategory ? ' inkl dess kategorier' : '') +  '? All information inkl sparad information kring aktörerna och egenskaperna kommer att tas bort.',
            confirmButtonClass: 'alert',
            confirmText: 'Ja, ta bort'
        };

        var modalInstance = $modal.open({
            templateUrl: '/App/Shared/Views/ModalConfirm.html',
            controller: 'modalConfirm',
            scope: $scope
        });

        modalInstance.result.then(function () {

            //call the api to delete the category
            viewModelHelper.apiPost('api/admin/' +  (isGroupCategory ? 'groupcategory' : 'category') + '/' + id + '/delete', null,
                function (result) {
                    //success
                    //refresh page
                    //$window.location.href = $window.location.href;
                    $route.reload();
                    if(isGroupCategory){
                        flash('alert-box success radius', 'Kategorigruppen har raderats.');
                    }
                    else {
                        flash('alert-box success radius', 'Kategorin har raderats.');
                    }
                },
                function (errors) {
                    //failure
                    $scope.errorMessage = 'Det gick inte ta bort ' + typeTitle; //errors.data.message;
                });

        }, function () {
            //modal cancelled, do something here?
        });
    }

    $scope.gotoGroupCategory = function (groupCategoryId) {
        viewModelHelper.navigateTo('admin/groupcategory/' + groupCategoryId);
    }


    initialize();
});
