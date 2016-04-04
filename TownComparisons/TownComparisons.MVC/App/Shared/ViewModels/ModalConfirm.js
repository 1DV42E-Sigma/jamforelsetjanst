adminModule.controller("modalConfirm", function ($scope, adminService, $http, $q, $routeParams, $window, $location, viewModelHelper, Upload, $modalInstance) {

    var initialize = function () {
        //empty
    }

    $scope.confirmModal = function () {
        $modalInstance.close();
    }

    $scope.cancelModal = function () {
        $modalInstance.dismiss('cancel');
    }

    initialize();
});
