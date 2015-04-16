app.factory('boardDetailService', ['$http', function ($http) {
    'use strict';
    var serviceBase = 'http://tm-api.loc/';
    var boardDetailServiceFactory = {};

    var _getBoard = function(id) {
        return $http.get(serviceBase + 'api/boarddetail/' + id).then(function (results) {
            return results;
        });
    };

    var _addList = function (data) {
        return $http.post(serviceBase + 'api/boarddetail/addlist', data).then(function (result) {
            return result;
        });
    };

    var _updateCardsPosition = function (listId, ids) {
        var data = {
            listId: listId,
            ids: ids
        };
        return $http.post(serviceBase + 'api/boarddetail/updatecardsposition', JSON.stringify(data)).then(function (result) {
            return result;
        });
    };

    var _updateListsPosition = function (ids) {
        var data = {
            ids: ids
        };
        return $http.post(serviceBase + 'api/boarddetail/updatelistsposition', JSON.stringify(data)).then(function (result) {
            return result;
        });
    };

    boardDetailServiceFactory.getBoard = _getBoard;
    boardDetailServiceFactory.addList = _addList;
    boardDetailServiceFactory.updateCardsPosition = _updateCardsPosition;
    boardDetailServiceFactory.updateListsPosition = _updateListsPosition;

    return boardDetailServiceFactory;

}]);