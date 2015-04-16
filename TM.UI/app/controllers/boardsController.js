app.controller('boardsController', ['$scope', 'boardsService', '$modal', function ($scope, boardsService, $modal) {
    'use strict';
    $scope.starredBoards = [];
    $scope.boards = [];
    $scope.userId = '';

    boardsService.getBoards().then(function (results) {
        $scope.boards = results.data.boards;
        $scope.userId = results.data.userId;
        $scope.newBoard.userId = results.data.userId;
        $scope.updateStarredBoards();
    }, function (error) {
        console.log(error.data.message);
    });

    $scope.updateStarredBoards = function () {
        $scope.starredBoards = _.where($scope.boards, { isStarred: true });
    };
   
    $scope.updateStarred = function (board, e) {
        e.preventDefault();
        boardsService.updateBoardStatus(board.id).then(function () {
            board.isStarred = !board.isStarred;
            $scope.updateStarredBoards();
        }, function(error) {
            console.error(error.data.message);
        });
    };

    $scope.newBoard = {
        name: '',
        userId: $scope.userId
    };

    $scope.resetNewBoard = function() {
        $scope.newBoard = {
            name: ''
        };
    };

    $scope.openNewBoard = function () {
        var modalInstance = $modal.open({
            templateUrl: 'newBoardModal.html',
            scope:$scope,
            controller: function($scope, $modalInstance, newBoard) {
                $scope.newBoard = newBoard;
                $scope.submit = function () {
                    boardsService.addBoard($scope.newBoard).then(function (results) {
                        $scope.$parent.boards = results.data;
                        $scope.$parent.updateStarredBoards();
                    }, function (error) {
                        console.error(error.data.message);
                    });
                    $scope.$parent.resetNewBoard();
                    $modalInstance.dismiss('cancel');
                };
                $scope.cancel = function (e) {
                    e.preventDefault();
                    $scope.$parent.resetNewBoard();
                    $modalInstance.dismiss('cancel');
                };
            },
            resolve: {
                newBoard: function() {
                    return $scope.newBoard;
                }
            }
        });
    };

}]);