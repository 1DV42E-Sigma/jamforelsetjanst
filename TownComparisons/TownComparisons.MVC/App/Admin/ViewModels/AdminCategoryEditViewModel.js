adminModule.controller("adminCategoryEditViewModel", function (flash, $scope, adminService, $http, $q, $routeParams, $window, $location, viewModelHelper) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.adminService = adminService;

    $scope.editMode = (adminService.editCategoryMode ? adminService.editCategoryMode : 'general');
    $scope.validationErrors = [];
    $scope.knownValidationErrors = [];
    $scope.closeValidationAlert = false;
    $scope.updateNotInsert = ($routeParams.categoryId);

    var initialize = function () {
        $scope.categoryHasBeenLoaded = false;
        $scope.pageHeading = 'Laddar in data...';
        $scope.queriesStateFilter = 'All';
        $scope.operatorsStateFilter = 'All';

        if ($scope.updateNotInsert) {
            adminService.getCategory($routeParams.categoryId, afterCategoryHasBeenLoaded);
        }
        else {
            adminService.getNewCategory($routeParams.groupCategoryId, afterCategoryHasBeenLoaded);
        }

        $window.onscroll = function () {
            $scope.scrollPos = document.body.scrollTop || document.documentElement.scrollTop || 0;
            $scope.$apply();
        };
    }

    $scope.scrollToTop = function () {
        $window.scrollTo(0, 0);
    }

    var afterCategoryHasBeenLoaded = function () {
        if ($scope.updateNotInsert) {
            $scope.categoryName = angular.copy($scope.category.Category.Name); //to use without data binding to the category
            $scope.pageHeading = 'Kategori: ' + $scope.categoryName;
        }
        else {
            $scope.categoryName = '';
            $scope.pageHeading = 'Ny kategori';
        }
        $scope.categoryHasBeenLoaded = true;
    }

    $scope.saveCategory = function () {
        setCategoryOrganisationalUnitsToUse();
        setCategoryQueriesToUse();

        adminService.resetErrors();

        var url = ($scope.updateNotInsert ? 'api/admin/category/' + adminService.categoryId :
                                            'api/admin/groupcategory/' + adminService.groupCategoryId + '/insertcategory');

        viewModelHelper.apiPost(url, $scope.category.Category,
            function (result) {
                //success
                console.log(result.data);
                flash('alert-box success radius', 'Kategorin har sparats.');
                var url = ($scope.updateNotInsert ? 'admin/category/' + adminService.categoryId : 'admin/groupcategory/' + adminService.groupCategoryId)
                viewModelHelper.navigateTo(url);

            },
            function (errors) {
                //failure
                adminService.parseErrors(errors);
            });
    }

    $scope.cancelEditCategory = function () {
        if ($scope.updateNotInsert) {
            viewModelHelper.navigateTo('admin/category/' + adminService.categoryId);
        }
        else {
            viewModelHelper.navigateTo('admin/groupcategory/' + adminService.groupCategoryId);
        }
    }

    var setCategoryOrganisationalUnitsToUse = function () {
        var ousToUse = [];
        for (var i = 0; i < $scope.category.AllOrganisationalUnits.length; i++) {

            //if checked (i.e. Use == true)
            if ($scope.category.AllOrganisationalUnits[i].Use == true) {
                //add it
                ousToUse.push($scope.category.AllOrganisationalUnits[i]);
            }
        }
        $scope.category.Category.OrganisationalUnits = ousToUse;
    }
    var setCategoryQueriesToUse = function () {
        var queriesToUse = [];
        for (var i = 0; i < $scope.category.AllPropertyQueryGroups.length; i++) {
            for (var j = 0; j < $scope.category.AllPropertyQueryGroups[i].Queries.length; j++) {

                //if checked (i.e. Use == true)
                if ($scope.category.AllPropertyQueryGroups[i].Queries[j].Use == true) {

                    //check not the same query already has been added (if exists in multiple query groups)
                    var alreadyUsed = false;
                    for (var u = 0; u < queriesToUse.length; u++) {
                        if (queriesToUse[u].WebServiceName == $scope.category.AllPropertyQueryGroups[i].Queries[j].WebServiceName &&
                            queriesToUse[u].QueryId == $scope.category.AllPropertyQueryGroups[i].Queries[j].QueryId) {
                            alreadyUsed = true;
                        }
                    }
                    if (!alreadyUsed) {
                        //add it
                        queriesToUse.push($scope.category.AllPropertyQueryGroups[i].Queries[j]);
                    }
                }
            }
        }
        $scope.category.Category.Queries = queriesToUse;
    }



    $scope.setOperatorsStateFilter = function (filter) {
        $scope.operatorsStateFilter = filter;
        console.log(filter);
    }
    $scope.setQueriesStateFilter = function (filter) {
        $scope.queriesStateFilter = filter;
        console.log(filter);
    }

    $scope.searchForGroup = function (queryGroup) {

        //console.log('searching query groups');
        if ($scope.searchQueryAll !== undefined && $scope.searchQueryAll.length !== 0) {
            var allMatch = false;
            if ($scope.doSearchGroupTitle(queryGroup, $scope.searchQueryAll) == true) {
                allMatch = true;
            }
            else if ($scope.doSearchGroupQueryTitle(queryGroup, $scope.searchQueryAll) == true) {
                allMatch = true;
            }

            if (!allMatch) {
                return false;
            }
        }

        if ($scope.searchQueryTitle !== undefined && $scope.searchQueryTitle.length !== 0) {
            if ($scope.doSearchGroupQueryTitle(queryGroup, $scope.searchQueryTitle) != true) {
                return false;
            }
        }

        if ($scope.doSearchGroupQueryUse(queryGroup) != true) {
            return false;
        }

        return true;
    };

    $scope.searchForQuery = function (query) {

        //console.log('searching queries');
        if ($scope.searchQueryTitle !== undefined && $scope.searchQueryTitle.length !== 0) {
            if ($scope.doSearchQueryTitle(query, $scope.searchQueryTitle) != true) {
                return false;
            }
        }
        if ($scope.doCheckUseFilter($scope.queriesStateFilter, query) != true) {
            return false;
        }

        return true;
    };



    $scope.searchForOperators = function (operator) {

        //console.log('searching operators');
        if ($scope.searchOperatorsTitle !== undefined && $scope.searchOperatorsTitle.length !== 0) {
            if ($scope.doSearchOperatorName(operator, $scope.searchOperatorsTitle) != true) {
                return false;
            }
        }
        if ($scope.doCheckUseFilter($scope.operatorsStateFilter, operator) != true) {
            return false;
        }

        return true;
    };
    $scope.doSearchOperatorName = function (operator, searchValue) {
        if (operator.Name.toLowerCase().indexOf(searchValue) >= 0) {
            return true;
        }
        return false;
    }

    $scope.doSearchGroupTitle = function (queryGroup, searchValue) {
        if (queryGroup.Title.toLowerCase().indexOf(searchValue) >= 0) {
            return true;
        }
        return false;
    }

    $scope.doSearchQueryTitle = function (query, searchValue) {
        if (query.Title.toLowerCase().indexOf(searchValue) >= 0) {
            return true;
        }
        return false;
    }
    $scope.doSearchGroupQueryTitle = function (queryGroup, searchValue) {
        for (var i = 0; i < queryGroup.Queries.length; i++) {
            if ($scope.doSearchQueryTitle(queryGroup.Queries[i], searchValue)) {
                return true;
            }
        }
        return false;
    }

    $scope.doSearchGroupQueryUse = function (queryGroup) {
        for (var i = 0; i < queryGroup.Queries.length; i++) {
            if ($scope.doCheckUseFilter($scope.queriesStateFilter, queryGroup.Queries[i])) {
                return true;
            }
        }
        return false;
    }
    $scope.doCheckUseFilter = function (filter, model) {
        if (filter == 'All' ||
            (filter == 'Chosen' && model.Use == true) ||
            (filter == 'Unchosen' && model.Use != true)) {

            return true;
        }
        return false;
    }

    initialize();
});
