angular.module('underscore', []).factory('_', function () {
    return window._; // assumes underscore has already been loaded on the page
});

var app = angular.module('TmApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'underscore', 'ui.sortable']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/views/signup.html"
    });

    $routeProvider.when("/boards", {
        controller: "boardsController",
        templateUrl: "/views/boards.html"
    });

    $routeProvider.when("/boards/:boardId", {
        controller: "boardDetailController",
        templateUrl: "/views/boardDetail.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/views/associate.html"
    });

    $routeProvider.when("/404", {
        controller: "404Controller",
        templateUrl: "/views/404.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

var serviceBase = 'http://tm-api.loc/'; //'http://localhost:18045/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngTmApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);