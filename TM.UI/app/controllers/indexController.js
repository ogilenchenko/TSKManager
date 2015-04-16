app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    'use strict';
    $scope.logOut = function() {
        authService.logOut();
        $location.path('/home');
    };

    $scope.authentication = authService.authentication;

}]);