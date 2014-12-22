'use strict';
app.factory('boardsService', ['$http', function ($http) {

    var serviceBase = 'http://tm-api.loc/';
    var dashboardServiceFactory = {};

    var _getBoards = function() {
        return $http.get(serviceBase + 'api/dashboard').then(function(results) {
            return results;
        });
    };

    var _updateBoardStatus = function(id) {
        return $http.post(serviceBase + 'api/dashboard/UpdateStatus', id).then(function (result) {
            return result;
        });
    };

    var _addBoard = function (id) {
        return $http.post(serviceBase + 'api/dashboard/add', id).then(function (result) {
            return result;
        });
    };

    dashboardServiceFactory.getBoards = _getBoards;
    dashboardServiceFactory.updateBoardStatus = _updateBoardStatus;
    dashboardServiceFactory.addBoard = _addBoard;

    return dashboardServiceFactory;

}]);