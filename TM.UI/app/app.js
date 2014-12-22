angular.module('underscore', []).factory('_', function () {
    return window._; // assumes underscore has already been loaded on the page
});

var app = angular.module('TmApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'underscore', 'ui.sortable']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/boards", {
        controller: "boardsController",
        templateUrl: "/app/views/boards.html"
    });

    $routeProvider.when("/boards/:boardId", {
        controller: "boardDetailController",
        templateUrl: "/app/views/boardDetail.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.when("/404", {
        controller: "404Controller",
        templateUrl: "/app/views/404.html"
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